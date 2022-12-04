namespace ricanEventos.Helpers;


public class AjaxResponse
{

  public bool Success { get; set; } = false;
  public int DataLength { get; set; } = 0;
  public int UserId { get; set; } = 0;

  public string Message { get; set; } = string.Empty;

  public Dictionary<string, object>? Data { get; set; } = new Dictionary<string, object> { };
}