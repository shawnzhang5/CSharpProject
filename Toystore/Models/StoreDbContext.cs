using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toystore.Models
{
  public class StoreDbContext : DbContext
  {
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public StoreDbContext(DbContextOptions<StoreDbContext> options)
      : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<User>().HasData
        (new User
        {
          UserId = 1,
          UserName = "aaa",
          Password = "111",
          RePassword = "111",
          Email = "aaa@example.com",
          Vendor = true,
          Age = 30,
          Gender = false
        });
      modelBuilder.Entity<User>().HasData
        (new User
        {
          UserId = 2,
          UserName = "bbb",
          Password = "111",
          RePassword = "111",
          Email = "bbb@example.com",
          Vendor = true,
          Age = 30,
          Gender = false
        });
      modelBuilder.Entity<User>().HasData
        (new User
        {
          UserId = 3,
          UserName = "ccc",
          Password = "111",
          RePassword = "111",
          Email = "ccc@example.com",
          Vendor = false,
          Age = 30,
          Gender = false
        });
      modelBuilder.Entity<Product>().HasData
        (new Product
        {
          id = 1,
          Name = "Polar Bear",
          Category = "Animals",
          Description = "",
          Photo = ""
        });
      modelBuilder.Entity<Product>().HasData
        (new Product
        {
          id = 2,
          Name = "Piano lesson",
          Category = "MISC",
          Description = "",
          Photo = ""
        });
      modelBuilder.Entity<Product>().HasData
        (new Product
        {
          id = 3,
          Name = "Gardening",
          Category = "MISC",
          Description = "",
          Photo = ""
        });
      modelBuilder.Entity<Product>().HasData
        (new Product
        {
          id = 4,
          Name = "Workbench",
          Category = "Furnitures",
          Description = "",
          Photo = ""
        });
    }
  }
}
