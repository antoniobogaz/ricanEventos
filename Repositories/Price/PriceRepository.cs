using System.Data.SqlClient;
using ricanEventos.Models;
using ricanEventos.Helpers;

namespace ricanEventos.Repositories;

public class PriceRepository : DBContext, IPriceRepository
{
  public void Create(int idEvent, Price price)
  {
    try
    {
      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = @"INSERT INTO event_prices(id_event, value, category) 
                               VALUES (@id_event, @value, @category);";
      cmd.Parameters.Clear();
      cmd.Parameters.AddWithValue("@id_event", idEvent);
      cmd.Parameters.AddWithValue("@value", price.Value);
      cmd.Parameters.AddWithValue("@category", "DEFAULT");
      cmd.ExecuteNonQuery();
    }
    catch (System.Exception ex)
    {

      Console.Write(ex);
    }

  }

  public List<Price> Read(int idEvent)
  {
    var prices = new List<Price>();

    try
    {

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = "SELECT * FROM event_prices WHERE id_event = @id";
      cmd.Parameters.Clear();

      cmd.Parameters.AddWithValue("@id", idEvent);

      SqlDataReader reader = cmd.ExecuteReader();

      while (reader.Read())
      {
        Price price = new Price()
        {
          Value = (decimal)reader["value"]
        };
        prices.Add(
          price
        );
      }
      reader.Close();
    }
    catch (System.Exception ex)
    {
      Console.WriteLine("Error ao buscar preço");
    }

    return prices;
  }
}