using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toystore.Models
{
  public class EFOrderRepository : IOrderRepository
  {
    private StoreDbContext _context;

    public EFOrderRepository(StoreDbContext context)
    {
      _context = context;
    }
    public void AddOrder(Cart cart, Order order)
    {
      //Remember dbobject is not object, pointer!!!
      Order dborder = new Order
      {
        UserId = cart.GetSessionId(),
        OrderTime = order.OrderTime
      };
      foreach (var line in cart.Lines)
      {
        CartLine dbline = new CartLine
        {
          Quantity = line.Quantity,
          ProductId = line.Product.ProductId
        };
        dbline.Orders.Add(dborder);
        dborder.Lines.Add(dbline);
        _context.Lines.Add(dbline);
      }
      _context.Orders.Add(dborder);
      _context.SaveChanges();
    }

    public void DelOrder(int orderId)
    {
      Order dborder = _context.Orders
        .Include(o => o.Lines).DefaultIfEmpty()
        .FirstOrDefault(o => o.OrderId == orderId); //include is mandatory for cascading delete
      foreach(var line in dborder.Lines)
      {
        _context.Entry(line).State = EntityState.Deleted;
      }
      _context.Entry(dborder).State = EntityState.Deleted;
      _context.SaveChanges();
    }
    public bool CheckFirstOrder(int? userId)
    {
      if(userId == null || _context.Orders.FirstOrDefault(o => o.UserId == userId) == null)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
    public int SetIsShipped(int cartlineId)
    {
      CartLine dbcartLine = _context.Lines.FirstOrDefault(l => l.CartLineId == cartlineId);
      if (dbcartLine != null)
      {
        dbcartLine.IsShipped = true;
        _context.SaveChanges();
      }
      return dbcartLine.ProductId;
    }
  }
}
