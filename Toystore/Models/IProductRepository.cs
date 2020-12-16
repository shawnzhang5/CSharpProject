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

    void UpdateProduct(Product product);

    Task<Product> AddProductAsync(Product2 product);
  }
}
