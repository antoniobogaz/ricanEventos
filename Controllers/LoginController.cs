using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ricanEventos.Models;
using ricanEventos.Helpers;
using ricanEventos.Repositories;

namespace ricanEventos.Controllers;

public class LoginController : Controller
{

  UserRepository userRepository = new UserRepository();

  public IActionResult Index()
  {
    if (HttpContext.Session.Get("Auth") != null)
      HttpContext.Response.Redirect("/");

    return View();
  }

  [HttpPost]
  public AjaxResponse Index(User user)
  {
    var response = userRepository.Read(Email: user.Email, Password: user.Password);

    if (response.DataLength == 1)
    {
      response.Success = true;
      HttpContext.Session.SetString("Auth", "True");
      HttpContext.Session.SetInt32("UserId", response.UserId);
    }

    return response;
  }

  public void Logout()
  {
    HttpContext.Session.Clear();
    HttpContext.Response.Redirect("/");
  }
}