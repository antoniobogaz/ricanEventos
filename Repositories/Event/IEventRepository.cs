using ricanEventos.Helpers;
using ricanEventos.Models;

namespace ricanEventos.Repositories;

public interface IEventRepository
{

  public AjaxResponse Create(Event evento);
  public AjaxResponse Read(int? idEvent = null, int? userId = null);
  public AjaxResponse Delete(int idEvent);
}