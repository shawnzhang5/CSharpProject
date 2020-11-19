using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Toystore.Models
{
  [Table("User")]
  public class User
  {
    public int UserId { get; set; }

    [Required(ErrorMessage = "Enter Username!")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Enter Password!")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Enter Password Again!")]
    public string RePassword { get; set; }

    [Required(ErrorMessage = "Enter Email Address!")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Are you a toy vendor?")]
    public bool? Vendor { get; set; }

    [Range(1, 100)]
    public int? Age { get; set; }

    public bool? Gender { get; set; }
  }
}
