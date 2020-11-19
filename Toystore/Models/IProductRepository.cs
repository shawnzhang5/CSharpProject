using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toystore.Models
{
  public interface IProductRepository
  {
    Task<List<Product>> GetProductsAsync();

    Product GetProductById(int id);

    Product UpdateProduct(Product product);

    Product AddProduct(Product product);
  }
}
