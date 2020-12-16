using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Toystore.Models
{
  public class EFProductRepository : IProductRepository
  {
    private StoreDbContext _context;
    private HttpClient _httpClient;

    public EFProductRepository(StoreDbContext context, HttpClient httpClient)
    {
      _context = context;
      _httpClient = httpClient;
    }
    public async Task<List<Product>> GetProductsAsync()
    {
      var imagesUrl = Settings.ApiUrl;
      string imagesJson = await _httpClient.GetStringAsync(imagesUrl);
      IEnumerable<string> ImagesList = JsonConvert.DeserializeObject<IEnumerable<string>>(imagesJson);
      List<string> imageList = ImagesList.ToList<string>();
      //List<string> imageList = ImagesList.OrderByDescending(c => c).ToList<string>();

      List<Product> products = await _context.Products.ToListAsync<Product>();
      for (int i = 0; i < (products.Count <= imageList.Count ? products.Count : imageList.Count); i++)
      {
        products.ElementAt(i).Photo = imageList.ElementAt(i);
      }
      _context.SaveChanges();
      return products;
    }

    public Product GetProductById(int id)
    {
      Product product = _context.Products
        .Include(p => p.MyUser).DefaultIfEmpty()
        .Include(p => p.Lines)
          .ThenInclude(l => l.Orders)
            .ThenInclude(o => o.MyUser).DefaultIfEmpty()
        .FirstOrDefault(p => p.ProductId == id);
      return product;
    }
    public void UpdateProduct(Product product)
    {
      Product dbproduct = _context.Products.Where(p => p.ProductId == product.ProductId).FirstOrDefault();
      if(dbproduct != null)
      {
        dbproduct.Name = product.Name;
        dbproduct.Category = product.Category;
        dbproduct.Description = product.Description;
        dbproduct.Price = product.Price;
        _context.SaveChanges();
      }
    }
    public async Task<Product> AddProductAsync(Product2 p2)
    {
      Product p1 = new Product
      {
        Name = p2.Name,
        Category = p2.Category,
        Description = p2.Description,
        UserId = p2.UserId
      };
      _context.Products.Add(p1);
      _context.SaveChanges();
      //image upload to blob storage
      var imagesUrl = Settings.ApiUrl;
      using (var image = new StreamContent(p2.Upload.OpenReadStream()))
      {
        image.Headers.ContentType = new MediaTypeHeaderValue(p2.Upload.ContentType);
        var response = await _httpClient.PostAsync(imagesUrl, image);
      }

      return p1;
    }
  }
}
