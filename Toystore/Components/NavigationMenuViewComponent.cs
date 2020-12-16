using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toystore.Models;

namespace Toystore.Components
{
  public class NavigationMenuViewComponent : ViewComponent
  {
    private IProductRepository _productrepo;
    public NavigationMenuViewComponent(IProductRepository repo)
    {
      _productrepo = repo;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
      List<Product> categories = await _productrepo.GetProductsAsync();
      return View(categories
        .Select(x => x.Category)
        .Distinct()
        .OrderBy(x => x));
    }
  }
}
