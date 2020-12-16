using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toystore.Models;
using Toystore.Models.ViewModels;

namespace Toystore.Components
{
  public class CartSummaryViewComponent : ViewComponent
  {
    private Cart _cart;

    public CartSummaryViewComponent(Cart cartservice)
    {
      _cart = cartservice;
    }

    public IViewComponentResult Invoke()
    {
      return View(_cart);
    }
  }
}
