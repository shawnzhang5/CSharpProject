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
    private IUserRepository _userrepo;
    private IProductRepository _productrepo;

    public HomeController(ILogger<HomeController> logger, IUserRepository userrepo, IProductRepository productrepo)
    {
      _logger = logger;
      _userrepo = userrepo;
      _productrepo = productrepo;
    }

    [HttpGet]
    public IActionResult Index()
    {
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> Index(User user)
    {
      if (ModelState.IsValid)
      {
        if (user.Vendor == true)
        {
          ViewBag.UserName = user.UserName;
          List<Product> products = await _productrepo.GetProductsAsync();
          return View("VendorHome", products
            .Where(p => p.Vendor == user.UserName));
        }
        else if (user.Vendor == false)
        {
          return RedirectToAction("Product");
        }
        else
        {
          return View(user);
        }
      }
      else
      {
        return View(user);
      }
    }

    public async Task<IActionResult> Product(int productPage = 1)
    {
      List<Product> products = await _productrepo.GetProductsAsync();
      int PageSize = 4;
      return View(new ProductsListViewModel
      {
        Products = products
                   .OrderBy(p => p.id)
                   .Skip((productPage - 1) * PageSize)
                   .Take(PageSize),
        PagingInfo = new PagingInfo
        {
          CurrentPage = productPage,
          ItemsPerPage = PageSize,
          TotalItems = products.Count
        }
      });
    }

    public IActionResult Details(int id = 1)
    {
      return View(_productrepo.GetProductById(id));
    }

    [HttpGet]
    public IActionResult UpdateProduct(int id)
    {
      return View(_productrepo.GetProductById(id));
    }
    [HttpPost]
    public IActionResult UpdateProduct(Product p)
    {
      return View("Details",_productrepo.UpdateProduct(p));
    }
    [HttpGet]
    public IActionResult AddProduct()
    {
      return View();
    }
    [HttpPost]
    public IActionResult AddProduct(Product p)
    {
      return View("Details", _productrepo.AddProduct(p));
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
