using System.Data.SqlClient;
using ricanEventos.Models;
using ricanEventos.Helpers;

namespace ricanEventos.Repositories;

public class LocationRepository : DBContext, ILocationRepository
{
  private AjaxResponse response = new AjaxResponse();
  private string query = " ";
  private List<string> parameters = new List<string>();
  public AjaxResponse ReadState(int? idState = null)
  {
    try
    {
      if (idState != null)
        query = "WHERE id = @id";

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = "SELECT * FROM states ORDER BY states.nome " + query;
      cmd.Parameters.Clear();

      if (idState != null)
        cmd.Parameters.AddWithValue("@id", idState);

      SqlDataReader reader = cmd.ExecuteReader();

      List<State> states = new List<State>();

      while (reader.Read())
      {
        State state = new State
        {
          Id = (int)reader["id"],
          Nome = reader["nome"].ToString(),
          SiglaUf = reader["sigla_uf"].ToString()
        };
        response.UserId = (int)reader["id"];
        states.Add(state);
      }

      response.DataLength = states.Count;
      response.Data.Add("states", states);

      reader.Close();
    }
    catch (System.Exception ex)
    {
      response.Success = false;
      response.Message = "teste";
      Console.Write(ex);
    }

    return response;
  }


  public State ReadStateById(int? idState = null)
  {
    State state = new State();
    try
    {
      if (idState != null)
        query = "WHERE id = @id";

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = "SELECT * FROM states " + query;
      cmd.Parameters.Clear();

      if (idState != null)
        cmd.Parameters.AddWithValue("@id", idState);

      SqlDataReader reader = cmd.ExecuteReader();

      while (reader.Read())
      {
        state.Id = (int)reader["id"];
        state.Nome = reader["nome"].ToString();
        state.SiglaUf = reader["sigla_uf"].ToString();
      }

      reader.Close();

    }
    catch (System.Exception ex)
    {
      response.Success = false;
    }

    return state;
  }

  public State ReadStateByCityId(int? cityId = null)
  {
    State state = new State();
    try
    {
      if (cityId != null)
        query = "WHERE id = (SELECT cities.state_id FROM cities WHERE cities.id = @id)";

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = "SELECT * FROM states " + query;
      cmd.Parameters.Clear();

      if (cityId != null)
        cmd.Parameters.AddWithValue("@id", cityId);

      SqlDataReader reader = cmd.ExecuteReader();

      while (reader.Read())
      {
        state.Id = (int)reader["id"];
        state.Nome = reader["nome"].ToString();
        state.SiglaUf = reader["sigla_uf"].ToString();
      }

      reader.Close();

    }
    catch (System.Exception ex)
    {
      response.Success = false;
      Console.WriteLine("aqui");
    }

    return state;
  }

  public AjaxResponse ReadCity(int idState)
  {
    try
    {

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = "SELECT * FROM cities WHERE state_id = @idState ORDER BY cities.nome";
      cmd.Parameters.Clear();
      cmd.Parameters.AddWithValue("@idState", idState);

      SqlDataReader reader = cmd.ExecuteReader();

      List<City> cities = new List<City>();

      while (reader.Read())
      {
        City city = new City
        {
          Id = (int)reader["id"],
          Nome = reader["nome"].ToString(),
          State = ReadStateById((int)reader["state_id"])
        };
        response.UserId = (int)reader["id"];
        cities.Add(city);
      }

      response.DataLength = cities.Count;
      response.Data.Add("cities", cities);

      reader.Close();
    }
    catch (System.Exception ex)
    {
      response.Success = false;
      response.Message = "teste";
      Console.Write(ex);
    }

    return response;
  }

}