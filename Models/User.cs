
namespace ricanEventos.Models;

public class User
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string Password { get; set; } = string.Empty;

  public List<int> EventFavorites { get; set; } = new List<int>();
}