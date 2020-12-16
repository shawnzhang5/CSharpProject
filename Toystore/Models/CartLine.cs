using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Toystore.Models
{
  [Table("CartLine")]
  public class CartLine
  {
    public CartLine()
    {
      this.Orders = new HashSet<Order>();
    }
    public int CartLineId { get; set; }
    public int Quantity { get; set; }
    public bool IsShipped { get; set; } = false;

    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
  }
}
