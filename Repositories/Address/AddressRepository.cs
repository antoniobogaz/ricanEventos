using System.Data.SqlClient;
using ricanEventos.Models;
using ricanEventos.Helpers;

namespace ricanEventos.Repositories;

public class AddressRepository : DBContext, IAddressRepository
{

  LocationRepository locationRepository = new LocationRepository();
  public void Create(int idEvent, Address address)
  {
    try
    {
      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = @"INSERT INTO event_address(id_event, cep, logradouro, nr_local, bairro, complemento, id_city) 
                               VALUES (@id_event, @cep, @logradouro, @nr_local, @bairro, @complemento, @id_city);";
      cmd.Parameters.Clear();
      cmd.Parameters.AddWithValue("@id_event", idEvent);
      cmd.Parameters.AddWithValue("@cep", address.Cep);
      cmd.Parameters.AddWithValue("@logradouro", address.Logradouro);
      cmd.Parameters.AddWithValue("@nr_local", address.NrLocal);
      cmd.Parameters.AddWithValue("@bairro", address.Bairro);
      cmd.Parameters.AddWithValue("@complemento", address.Complemento);
      cmd.Parameters.AddWithValue("@id_city", address.idCity);
      cmd.ExecuteNonQuery();
    }
    catch (System.Exception ex)
    {

      Console.Write(ex);
    }
  }

  public Address Read(int idEvent)
  {
    Address address = new Address();
    try
    {

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = "SELECT * FROM event_address WHERE id_event = @id";
      cmd.Parameters.Clear();

      cmd.Parameters.AddWithValue("@id", idEvent);

      SqlDataReader reader = cmd.ExecuteReader();

      while (reader.Read())
      {
        address.Cep = reader["cep"].ToString();
        address.Logradouro = reader["logradouro"].ToString();
        address.Bairro = reader["bairro"].ToString();

        address.NrLocal = reader["nr_local"].ToString();
        address.Bairro = reader["bairro"].ToString();
        address.Complemento = reader["complemento"].ToString();
        address.idCity = (int)reader["id_city"];
        address.state = locationRepository.ReadStateByCityId(cityId: (int)reader["id_city"]);
      }

      reader.Close();
    }
    catch (System.Exception ex)
    {
      Console.WriteLine("Error ao buscar address");
      Console.WriteLine(ex);
    }

    return address;
  }
}