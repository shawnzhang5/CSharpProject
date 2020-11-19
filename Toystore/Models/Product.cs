using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Toystore.Models
{
  public class Product
  {
    [Key]
    public int id { get; set; }
    [Required(ErrorMessage = "Enter a name!")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Select a category!")]
    public string Category { get; set; }

    public string Description { get; set; }

    public string Vendor { get; set; }

    public string Photo { get; set; }

  }
}