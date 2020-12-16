using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Toystore.Models
{
  [Table("CartLineOrder")]
  public class CartLineOrder
  {
    [ForeignKey("CartLine")]
    public int CartLineId { get; set; }
    public virtual CartLine CartLine { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }
  }
}
