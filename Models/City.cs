
namespace ricanEventos.Models;

public class City
{
  public int Id { get; set; }
  public string Nome { get; set; }
  public string Ibge { get; set; } = string.Empty;
  public State? State { get; set; } = new State();
}