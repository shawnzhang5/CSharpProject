using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Toystore.Models
{

  public static class SeedData
  {
    private const string adminUser = "aaa";
    private const string adminPassword = "Secret123$";
    public static async void EnsurePopulated(IApplicationBuilder app)
    {
      StoreDbContext context = app.ApplicationServices
          .CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();

      if (context.Database.GetPendingMigrations().Any())
      {
        context.Database.Migrate();
      }
      
      UserManager<IdentityUser> userManager = app.ApplicationServices
          .CreateScope().ServiceProvider
          .GetRequiredService<UserManager<IdentityUser>>();

      IdentityUser user = await userManager.FindByIdAsync(adminUser);
      if (user == null)
      {
        user = new IdentityUser("aaa");
        user.Email = "aaa@example.com";
        user.PhoneNumber = "555-1234";
        await userManager.CreateAsync(user, adminPassword);
      }
      


      if (!context.Products.Any())
      {
        context.Products.AddRange(
            new Product
            {
              Name = "Bathroom set",
              Category = "Furniture",
              Description = "",
              Price = 19.95,
              Photo = ""
            }
        );
        context.SaveChanges();
      }
    }
  }
}
