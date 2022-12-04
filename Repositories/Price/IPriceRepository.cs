using ricanEventos.Helpers;
using ricanEventos.Models;

namespace ricanEventos.Repositories;

public interface IPriceRepository
{

  public void Create(int idEvent, Price price);
}