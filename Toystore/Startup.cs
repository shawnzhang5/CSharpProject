using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toystore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Toystore
{
  public class Startup
  {
    public IConfiguration _configuration { get; }
    public Startup(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<StoreDbContext>(options =>
      {
        options.UseSqlServer(
          _configuration["ConnectionStrings:AzureConnection"]);
      });
      services.AddScoped<Cart>(options => SessionCart.GetCart(options));
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddScoped<IMyUserRepository, EFMyUserRepository>();
      services.AddScoped<IProductRepository, EFProductRepository>();
      services.AddScoped<IOrderRepository, EFOrderRepository>();
      services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<StoreDbContext>();
      services.AddSingleton<Settings>(_configuration.Get<Settings>());
      services.AddSingleton<HttpClient>(new HttpClient());
      services.AddDistributedMemoryCache(); //Session
      services.AddSession(options =>
      {
        options.IdleTimeout = TimeSpan.FromMinutes(20);
      });   //Session
      services.AddControllersWithViews();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseSession();   //Session
      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute("catpage", "{category}/Page{productPage}", new { Controller = "Home", Action = "Product" });
        endpoints.MapControllerRoute("page", "Page{productPage}", new { Controller = "Home", Action = "Product"});
        endpoints.MapControllerRoute("category", "{category}", new { Controller = "Home", Action = "Product"});
        endpoints.MapControllerRoute("pagination", "Product/Page{productPage}", new { Controller = "Home", Action = "Product"});
        endpoints.MapDefaultControllerRoute();
      });

      //SeedData.EnsurePopulated(app);
    }
  }
}
