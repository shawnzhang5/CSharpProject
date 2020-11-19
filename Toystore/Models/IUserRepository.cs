using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toystore.Models
{
  public interface IUserRepository
  {
    IQueryable<User> GetUsers();
  }
}
