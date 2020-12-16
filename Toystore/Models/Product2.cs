using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Toystore.Models
{
  public class Product2
  {

    [Required(ErrorMessage = "Enter a name!")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Select a category!")]
    public string Category { get; set; }

    public string Description { get; set; }

    [Required(ErrorMessage = "Enter a price!")]
    [Range(0, 1000.0)]
    public double Price { get; set; }

    [BindProperty]
    public IFormFile Upload { get; set; }

    public int UserId { get; set; }
  }
}
