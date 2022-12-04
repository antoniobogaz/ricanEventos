
namespace ricanEventos.Models;

public class Event
{
  public int Id { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public string Link { get; set; }
  public int Online { get; set; } = 0;

  public string Photo { get; set; } = string.Empty;

  public DateTime DateTime { get; set; }

  public User? User { get; set; } = new User();

  public Address? Address { get; set; } = new Address();
  public List<Category>? Categories { get; set; } = new List<Category>();

  public List<Price>? Prices { get; set; } = new List<Price>();
  public bool deleted { get; set; } = false;
}