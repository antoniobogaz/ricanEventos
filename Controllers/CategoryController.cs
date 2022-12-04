
using Microsoft.AspNetCore.Mvc;
using ricanEventos.Helpers;
using ricanEventos.Repositories;

namespace ricanEventos.Controllers;

public class CategoryController : Controller
{

  CategoryRepository categoryRepository = new CategoryRepository();


  [HttpPost]
  public AjaxResponse Index()
  {
    return categoryRepository.Read();
  }
}