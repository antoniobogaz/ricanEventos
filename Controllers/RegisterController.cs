using ricanEventos.Repositories;
using ricanEventos.Models;
using ricanEventos.Helpers;
using Microsoft.AspNetCore.Mvc;


namespace ricanEventos.Controllers;

public class RegisterController : Controller
{

  UserRepository repository = new UserRepository();
  AjaxResponse response = new AjaxResponse();

  [HttpGet]
  public IActionResult Index()
  {
    return View();
  }


  [HttpPost]
  public AjaxResponse Index(User user)
  {
    var currentUser = repository.Read(Email: user.Email);

    if (currentUser.DataLength > 0)
    {
      response.Message = "Este E-mail já está sendo utilizado tente outro.";
      return response;
    }

    response = repository.Create(user);
    HttpContext.Session.SetString("Auth", "True");
    HttpContext.Session.SetInt32("UserId", response.UserId);
    return response;
  }
}