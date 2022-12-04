
using Microsoft.AspNetCore.Mvc;
using ricanEventos.Helpers;
using ricanEventos.Repositories;

namespace ricanEventos.Controllers;

public class LocationController : Controller
{

  LocationRepository locationRepository = new LocationRepository();


  [HttpPost]
  public AjaxResponse States()
  {
    return locationRepository.ReadState();
  }

  [HttpPost]
  public AjaxResponse Cities(int idState)
  {
    return locationRepository.ReadCity(idState);
  }
}