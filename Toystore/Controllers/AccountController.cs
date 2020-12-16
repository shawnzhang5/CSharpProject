using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Toystore.Models;
using Toystore.Models.ViewModels;

namespace Toystore.Controllers
{

  public class AccountController : Controller
  {
    private IMyUserRepository _userrepo;
    private Cart _cart;
    public AccountController(IMyUserRepository userrepo, Cart cartservice)
    {
      _userrepo = userrepo;
      _cart = cartservice;
    }
    [HttpGet]
    public ViewResult Login()
    {
      return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginModel loginModel)
    {
      if (ModelState.IsValid)
      {
        MyUser dbuser = _userrepo.GetUserByName(loginModel.Name);
        if(dbuser != null && _userrepo.PasswordHash(loginModel.Password) == dbuser.Password)
        {
          _cart.SetSession(dbuser);
          return Redirect(Settings.ReturnUrl ?? "/Vendor");
        }
        ModelState.AddModelError("", "Invalid name or password");
      }
      return View(loginModel);
    }
    
    [HttpGet]
    public ViewResult Register()
    {
      return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterModel registerModel)
    {
      if (ModelState.IsValid)
      {
        if(_userrepo.AddUser(registerModel))
        {
          MyUser dbuser = _userrepo.GetUserByName(registerModel.Name);
          _cart.SetSession(dbuser);
          return Redirect(Settings.ReturnUrl ?? "/Vendor");
        }    
        ModelState.AddModelError("", "This username or email already exist in DB!");
        return View(registerModel);
      }
      return View(registerModel);
    }
    public RedirectResult Logout()
    {
      _cart.RemoveSession();
      _cart.Clear();
      return Redirect(Settings.ReturnUrl ?? "/Home/Product");
    }
    
  }
}
