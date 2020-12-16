using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Toystore.Models
{
  [Table("Order")]
  public class Order
  {
    public Order()
    {
      this.Lines = new HashSet<CartLine>();
    }
    public int OrderId { get; set; }

    [ForeignKey("MyUser")]
    public int? UserId { get; set; }
    public MyUser MyUser { get; set; }
    public DateTime OrderTime { get; set; }
    //DateTime.Now.ToString("yyyyMMddHHmmss")
    public virtual ICollection<CartLine> Lines { get; set; }
  }
}
