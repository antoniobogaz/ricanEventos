using ricanEventos.Helpers;
using ricanEventos.Models;

namespace ricanEventos.Repositories;

public interface IAddressRepository
{

  public void Create(int idEvent, Address address);
  public Address Read(int idEvent);
}