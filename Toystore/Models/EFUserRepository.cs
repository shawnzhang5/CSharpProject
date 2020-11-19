using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toystore.Models
{
  public class EFUserRepository : IUserRepository
  {
    private StoreDbContext _context;
    public EFUserRepository(StoreDbContext context)
    {
      _context = context;
    }

    public IQueryable<User> GetUsers() => _context.Users;
  }
}
