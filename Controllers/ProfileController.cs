using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ricanEventos.Models;
using ricanEventos.Helpers;
using ricanEventos.Repositories;

namespace ricanEventos.Controllers;

public class ProfileController : Controller
{

  UserRepository userRepository = new UserRepository();

  public IActionResult Index()
  {
    return View();
  }

  public IActionResult Event()
  {
    return View();
  }
  public IActionResult Favorite()
  {
    return View();
  }
}