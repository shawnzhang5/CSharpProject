using System.Collections.Generic;
using System.Linq;

namespace Toystore.Models
{

  public class Cart
  {
    public List<CartLine> Lines { get; set; } = new List<CartLine>();

    public virtual void AddItem(Product product, int quantity)
    {
      CartLine line = Lines
          .Where(p => p.Product.ProductId == product.ProductId)
          .FirstOrDefault();

      if (line == null)
      {
        Lines.Add(new CartLine
        {
          Product = product,
          Quantity = quantity
        });
      }
      else
      {
        line.Quantity += quantity;
      }
    }

    public virtual void RemoveLine(Product product) =>
        Lines.RemoveAll(l => l.Product.ProductId == product.ProductId);

    public double ComputeTotalValue() =>
        Lines.Sum(e => e.Product.Price * e.Quantity);


    public virtual void Clear() => Lines.Clear();
    public virtual void SetSession(MyUser user) { }
    public virtual void RemoveSession() { }
    public virtual string GetSessionName() { return ""; }
    public virtual int? GetSessionId() { return null; }
    public virtual int? GetSessionVendor() { return null; }
  }


}
