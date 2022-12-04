using ricanEventos.Helpers;
using ricanEventos.Models;

namespace ricanEventos.Repositories;

public interface ICategoryRepository
{

  public void Create(int idEvent, Category category);
  public AjaxResponse Read();
  public List<Category> ReadFromEvent(int eventId);

  public void Delete(int eventId);
}