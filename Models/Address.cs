
namespace ricanEventos.Models;

public class Address
{
  public string Cep { get; set; }
  public string Logradouro { get; set; } = string.Empty;
  public string NrLocal { get; set; } = string.Empty;
  public string Bairro { get; set; } = string.Empty;
  public string Longitude { get; set; } = string.Empty;
  public string Latitude { get; set; } = string.Empty;
  public string Complemento { get; set; } = string.Empty;
  public int? idCity { get; set; } = null;

  public State? state { get; set; } = new State();
}