using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toystore.Models.ViewModels;

namespace Toystore.Models
{
  public interface IMyUserRepository
  {
    MyUser GetUserByName(string name);
    string PasswordHash(string password);
    bool AddUser(RegisterModel registerModel);
  }
}
