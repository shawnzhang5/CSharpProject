using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toystore.Models
{
  public interface IOrderRepository
  {
    void AddOrder(Cart cart, Order order);
    void DelOrder(int orderId);
    bool CheckFirstOrder(int? userId);
    int SetIsShipped(int cartlineId);
  }
}
