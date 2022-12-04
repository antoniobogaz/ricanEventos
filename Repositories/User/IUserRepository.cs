using ricanEventos.Helpers;
using ricanEventos.Models;

namespace ricanEventos.Repositories;

public interface IUserRepository
{

  public AjaxResponse Create(User user);
  public AjaxResponse Read(int? userId, string? Email, string? Password = null, string? userIdOperation = "=");
  public AjaxResponse Update(User user);
}