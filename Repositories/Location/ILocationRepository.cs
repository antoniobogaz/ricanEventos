using ricanEventos.Helpers;
using ricanEventos.Models;

namespace ricanEventos.Repositories;

public interface ILocationRepository
{

  public AjaxResponse ReadState(int? idState = null);
  public AjaxResponse ReadCity(int idState);
  public State ReadStateById(int? idState = null);

  public State ReadStateByCityId(int? cityId = null);
}