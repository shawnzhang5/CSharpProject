using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Toystore.Models
{
  [Table("MyUser")]
  public class MyUser
  {
    [Key]
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool? Vendor { get; set; }
    public int? Age { get; set; }
    public bool? Gender { get; set; }

    public ICollection<Product> Products { get; set; }
    public ICollection<Order> Orders { get; set; }
  }
}
