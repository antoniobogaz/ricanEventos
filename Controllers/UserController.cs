using ricanEventos.Repositories;
using ricanEventos.Models;
using ricanEventos.Helpers;
using Microsoft.AspNetCore.Mvc;


namespace ricanEventos.Controllers;

public class UserController : Controller
{

  UserRepository repository = new UserRepository();
  AjaxResponse response = new AjaxResponse();

  [HttpPost]
  public AjaxResponse Index(User user)
  {
    return repository.Read(userId: user.Id);
  }

  [HttpPost]
  public AjaxResponse Update(User user)
  {
    var currentUser = repository.Read(Email: user.Email, userId: user.Id, userIdOperation: "<>");

    if (currentUser.DataLength > 0)
    {
      response.Message = "Este E-mail já está sendo utilizado tente outro.";
      return response;
    }

    response = repository.Update(user);
    response.Success = true;
    response.Message = "Dados atualizados com sucesso.";
    return response;
  }
}