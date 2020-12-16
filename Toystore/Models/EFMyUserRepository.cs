using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Toystore.Models.ViewModels;

namespace Toystore.Models
{
  public class EFMyUserRepository : IMyUserRepository
  {
    private StoreDbContext _context;
    public EFMyUserRepository(StoreDbContext context)
    {
      _context = context;
    }
    public MyUser GetUserByName(string name)
    {
      return _context.MyUsers
          .Include(u => u.Products)
            .ThenInclude(p => p.Lines).DefaultIfEmpty()
          .Include(u => u.Orders)
            .ThenInclude(o => o.Lines)
              .ThenInclude(l => l.Product).DefaultIfEmpty()
          .FirstOrDefault(u => u.Name == name || u.Email == name);
    }

    public string PasswordHash(string password)
    {
      //Encrypt password Hashing Algorithm
      SHA256 myHashingVar = SHA256.Create();
      byte[] passwordByteArray = Encoding.ASCII.GetBytes(password);
      passwordByteArray[0] += 1;
      passwordByteArray[1] += 2;
      passwordByteArray[2] += 3;
      byte[] hashedPasswordByteArray = myHashingVar.ComputeHash(passwordByteArray);
      string hashedPassword = "";
      foreach (byte b in hashedPasswordByteArray)
      {
        hashedPassword += b.ToString("x2");
      }
      return hashedPassword;
    }
    public bool AddUser(RegisterModel registerModel)
    {
      MyUser user = new MyUser
      {
        Name = registerModel.Name,
        Email = registerModel.Email,
        Password = PasswordHash(registerModel.Password),
        Vendor = registerModel.Vendor,
        Age = registerModel.Age,
        Gender = registerModel.Gender
      };
      try
      {
        _context.MyUsers.Add(user);
        _context.SaveChanges();
      }
      catch(Exception e)
      {
        return false;
      }
      return true;
    }
  }
}
