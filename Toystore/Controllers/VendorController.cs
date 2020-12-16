using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Toystore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Toystore.Controllers
{
  public class VendorController : Controller
  {
    private IMyUserRepository _userrepo;
    private IProductRepository _productrepo;
    private IOrderRepository _orderrepo;
    private Cart _cart;
    public VendorController(IMyUserRepository userrepo, IProductRepository productrepo, IOrderRepository orderrepo, Cart cartservice)
    {
      _userrepo = userrepo;
      _productrepo = productrepo;
      _orderrepo = orderrepo;
      _cart = cartservice;
    }
    public IActionResult Index()
    {
      if(_cart.GetSessionVendor() == 1)
      {
        MyUser vendor = _userrepo.GetUserByName(_cart.GetSessionName());
        return View("Index",vendor);
      }
      else if(_cart.GetSessionVendor() == 0)
      {
        MyUser customer = _userrepo.GetUserByName(_cart.GetSessionName());
        return View("Customer",customer);
      }
      else
      {
        return RedirectToAction("Login","Account");
      }
    }
    
    [HttpGet]
    public IActionResult UpdateProduct(int productId)
    {
      if (_cart.GetSessionVendor() == 1)
      {
        return View(_productrepo.GetProductById(productId));
      }
      else
      {
        return RedirectToAction("Index", "Vendor");
      }
    }
    [HttpPost]
    public IActionResult UpdateProduct(Product p)
    {
      if (ModelState.IsValid)
      {
        _productrepo.UpdateProduct(p);
        return Redirect("/Vendor");
      }
      else 
      {
        return View(p);
      }
    }
    [HttpGet]
    public IActionResult AddProduct(int userId)
    {
      if (_cart.GetSessionVendor() == 1)
      {
        return View(new Product2 { UserId = userId });
      }
      else
      {
        return RedirectToAction("Index", "Vendor");
      }
    }
    [HttpPost]
    public async Task<IActionResult> AddProduct(Product2 p2)
    {
      if (ModelState.IsValid && p2.Upload != null && p2.Upload.Length > 0)
      {
        await _productrepo.AddProductAsync(p2);
        return RedirectToAction("Product", "Home");
      }
      else
      {
        return View(p2);
      }
    }
    
    public IActionResult DelOrder(int orderId)
    {
      _orderrepo.DelOrder(orderId);
      return RedirectToAction("Index", "Vendor");
    }

    public IActionResult ShipProduct(int productId)
    {
      if (_cart.GetSessionVendor() == 1)
      {
        Product p1 = _productrepo.GetProductById(productId);
        return View(p1);
      }
      else
      {
        return RedirectToAction("Index", "Vendor");
      }
    }
    public IActionResult Ship(int cartlineId)
    {
      return RedirectToAction("ShipProduct", "Vendor", new {productId = _orderrepo.SetIsShipped(cartlineId)});
    }
  }
}
