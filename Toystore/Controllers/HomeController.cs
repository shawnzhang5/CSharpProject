using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Toystore.Models;
using Toystore.Models.ViewModels;

namespace Toystore.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private IProductRepository _productrepo;
    private IOrderRepository _orderrepo;
    private Cart _cart;

    public HomeController(ILogger<HomeController> logger, IProductRepository productrepo, IOrderRepository orderrepo, Cart cartservice)
    {
      _logger = logger;
      _productrepo = productrepo;
      _orderrepo = orderrepo;
      _cart = cartservice;
    }

    public IActionResult Index()
    {
      return View();
    }

    public async Task<IActionResult> Product(string category, int productPage = 1)
    {
      List<Product> products = await _productrepo.GetProductsAsync();
      int PageSize = 3;
      return View(new ProductsListViewModel
      {
        Products = products
                   .Where(p => category == null || p.Category == category)
                   .OrderBy(p => p.ProductId)
                   .Skip((productPage - 1) * PageSize)
                   .Take(PageSize),
        PagingInfo = new PagingInfo
        {
          CurrentPage = productPage,
          ItemsPerPage = PageSize,
          TotalItems = category == null ? products.Count() : products.Where(p => p.Category == category).Count()
        },
        CurrentCategory = category
      });
    }

    public IActionResult Details(int productId = 1)
    {
      return View(_productrepo.GetProductById(productId));
    }

    [HttpGet]
    public async Task<IActionResult> Cart(int bulkId)
    {
      if (bulkId != 0)
      {
        _cart.Clear();
        List<Product> products = await _productrepo.GetProductsAsync();
        switch (bulkId)
        {
          case 1:
            products = products.Where(p => p.Name == "Cake shop" || p.Name == "Candy wagon" || p.Name == "Fruit wagon" || p.Name == "Fruit wagon" || p.Name == "Bear family" || p.Name == "Rabbit family" || p.Name == "Squirrel family").ToList();
            foreach (var p in products)
            {
              _cart.AddItem(p, 1);
            }
            break;
          case 2:
            products = products.Where(p => p.Name == "Rabbit family" || p.Name == "Cruise car" || p.Name == "Town house").ToList();
            foreach (var p in products)
            {
              _cart.AddItem(p, 1);
            }
            break;
          case 3:
            products = products.Where(p => p.Name == "Cat family" || p.Name == "Log cabin" || p.Name == "Rabbit family" || p.Name == "Cruise car" || p.Name == "Squirrel family" || p.Name == "Tree house").ToList();
            foreach (var p in products)
            {
              _cart.AddItem(p, 1);
            }
            break;
          case 4:
            products = products.Where(p => p.Name == "Bath room set" || p.Name == "Children's bedroom" || p.Name == "Dining room set" || p.Name == "Cat family" || p.Name == "Squirrel family" || p.Name == "Living room set" || p.Name == "Rabbit family" || p.Name == "Town house").ToList();
            foreach (var p in products)
            {
              _cart.AddItem(p, 1);
            }
            break;
          default:
            return View(_cart);
        }
      }
      return View(_cart);
    }
    [HttpPost]
    public IActionResult Cart(Product product)
    {
      _cart.AddItem(product, 1);
      return View(_cart);
    }
    public IActionResult CartRemove(int productId)
    {
      _cart.RemoveLine(_cart.Lines.FirstOrDefault(p => p.Product.ProductId == productId).Product);
      return Redirect("/Home/Cart");
    }

    [HttpGet] 
    public IActionResult Order()
    {
      return View();
    }
    [HttpPost]
    public IActionResult Order(Order order)
    {
      _orderrepo.AddOrder(_cart, order);
      _cart.Clear();
      return View(order);
    }

    public IActionResult OrderConfirm()
    {
      return View();
    }



    public IActionResult Privacy()
    {
      return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
