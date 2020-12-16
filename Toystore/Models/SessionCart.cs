using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Toystore.Infrastructure;

namespace Toystore.Models
{
  public class SessionCart : Cart
  {
    [JsonIgnore]
    public ISession Session { get; set; }
    public static Cart GetCart(IServiceProvider services)
    {
      ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
      SessionCart cart = session?.GetJson<SessionCart>("Cart")
          ?? new SessionCart();
      cart.Session = session;
      return cart;
    }


    public override void AddItem(Product product, int quantity)
    {
      base.AddItem(product, quantity);
      Session.SetJson("Cart", this);
    }

    public override void RemoveLine(Product product)
    {
      base.RemoveLine(product);
      Session.SetJson("Cart", this);
    }

    public override void Clear()
    {
      base.Clear();
      Session.Remove("Cart");
    }

    public override void SetSession(MyUser user)
    {
      base.SetSession(user);
      Session.SetInt32("userId", user.UserId);
      Session.SetString("userName", user.Name);
      if (user.Vendor == true)
      {
        Session.SetInt32("isVendor", 1);
      }
      else
      {
        Session.SetInt32("isVendor", 0);
      }
    }
    public override void RemoveSession()
    {
      base.RemoveSession();
      Session.Remove("userId");
      Session.Remove("userName");
      Session.Remove("isVendor");
    }
    public override string GetSessionName()
    {
      base.GetSessionName();
      return Session.GetString("userName");
    }
    public override int? GetSessionId()
    {
      base.GetSessionId();
      return Session.GetInt32("userId");
    }
    public override int? GetSessionVendor()
    {
      base.GetSessionVendor();
      return Session.GetInt32("isVendor");
    }
  }
}
