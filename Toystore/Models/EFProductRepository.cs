using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Toystore.Models
{
  public class EFProductRepository : IProductRepository
  {
    private StoreDbContext _context;
    private HttpClient _httpClient;
    private Options _options;
    public EFProductRepository(StoreDbContext context, HttpClient httpClient, Options options)
    {
      _context = context;
      _httpClient = httpClient;
      _options = options;
    }
    public async Task<List<Product>> GetProductsAsync()
    {
      List<Product> products = await _context.Products.ToListAsync<Product>();
      var imagesUrl = _options.ApiUrl;
      string imagesJson = await _httpClient.GetStringAsync(imagesUrl);
      IEnumerable<string> ImagesList = JsonConvert.DeserializeObject<IEnumerable<string>>(imagesJson);
      List<string> imageList = ImagesList.ToList<string>();
      //List<string> imageList = ImagesList.OrderByDescending(c => c).ToList<string>();
      for (int i = 0; i < (products.Count <= imageList.Count ? products.Count : imageList.Count); i++)
      {
        products.ElementAt(i).Photo = imageList.ElementAt(i);
      }
      _context.SaveChanges();
      return products;
    }

    public Product GetProductById(int id)
    {
      Product product = _context.Products.Where(p => p.id == id).FirstOrDefault();
      return product;
    }
    public Product UpdateProduct(Product product)
    {
      Product dbproduct = _context.Products.Where(p => p.id == product.id).FirstOrDefault();
      if(dbproduct != null)
      {
        dbproduct.Name = product.Name;
        dbproduct.Category = product.Category;
        dbproduct.Description = product.Description;
        _context.SaveChanges();
      }
      return dbproduct;
    }
    public Product AddProduct(Product product)
    {
      _context.Products.Add(product);
      _context.SaveChanges();
      return product;
    }
  }
}
