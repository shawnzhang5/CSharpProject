using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toystore.Models;

namespace Toystore.Components
{
  public class RandomProductViewComponent : ViewComponent
  {
    private IProductRepository _productrepo;
    public RandomProductViewComponent(IProductRepository repo)
    {
      _productrepo = repo;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
      List<Product> products = await _productrepo.GetProductsAsync();
      Random dice = new Random();
      return View(products.ElementAt(dice.Next(0, products.Count)));
    }
  }
}
