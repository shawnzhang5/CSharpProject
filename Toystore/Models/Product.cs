using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Toystore.Models
{
  [Table("Product")]
  public class Product
  {
    public int ProductId { get; set; }

    public string Name { get; set; }

    public string Category { get; set; }

    public string Description { get; set; }

    [Range(0,1000.0)]
    public double Price { get; set; }
    public string Photo { get; set; }

    [ForeignKey("MyUser")]
    public int UserId { get; set; }
    public MyUser MyUser { get; set; }
    public ICollection<CartLine> Lines { get; set; }
  }
}