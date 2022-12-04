using System.Data.SqlClient;
using ricanEventos.Models;
using ricanEventos.Helpers;
using ricanEventos.Repositories;

namespace ricanEventos.Repositories;

public class EventRepository : DBContext, IEventRepository
{
  private AjaxResponse response = new AjaxResponse();
  private string query = " ";
  private List<string> parameters = new List<string>();
  List<Event> events = new List<Event>();
  private PriceRepository priceRepository = new PriceRepository();
  private AddressRepository addressRepository = new AddressRepository();
  private CategoryRepository categoryRepository = new CategoryRepository();
  private UserRepository userRepository = new UserRepository();
  public AjaxResponse Create(Event evento)
  {
    try
    {
      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = @"INSERT INTO events(title, description, online, photo, user_id, date_time, link) 
                               VALUES (@title, @description, @online, @photo, @user_id, @date_time, @link);
                               SELECT SCOPE_IDENTITY()";
      cmd.Parameters.Clear();
      cmd.Parameters.AddWithValue("@title", evento.Title);
      cmd.Parameters.AddWithValue("@description", evento.Description);
      cmd.Parameters.AddWithValue("@online", evento.Online);
      cmd.Parameters.AddWithValue("@photo", evento.Photo);
      cmd.Parameters.AddWithValue("@link", evento.Link);
      cmd.Parameters.AddWithValue("@user_id", evento.User.Id);
      cmd.Parameters.AddWithValue("@date_time", evento.DateTime);

      int idEvent = Convert.ToInt32((decimal)cmd.ExecuteScalar());

      priceRepository.Create(idEvent, evento.Prices.First());

      if (evento.Online != 1)
        addressRepository.Create(idEvent, evento.Address);

      foreach (var category in evento.Categories)
      {
        categoryRepository.Create(idEvent, category);
      }

      response.Success = true;
      Dispose();
      return response;
    }
    catch (System.Exception ex)
    {
      response.Message = "Registro não realizado com sucesso!";
      Console.Write(ex);
    }

    return response;
  }
  public AjaxResponse Update(Event evento)
  {
    try
    {
      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;

      cmd.CommandText = @"DELETE FROM event_categories WHERE id_event = @id_event";
      cmd.Parameters.Clear();
      cmd.Parameters.AddWithValue("@id_event", evento.Id);
      cmd.ExecuteNonQuery();

      cmd.CommandText = @"DELETE FROM event_address WHERE id_event = @id_event";
      cmd.Parameters.Clear();
      cmd.Parameters.AddWithValue("@id_event", evento.Id);
      cmd.ExecuteNonQuery();

      cmd.CommandText = @"DELETE FROM event_prices WHERE id_event = @id_event";
      cmd.Parameters.Clear();
      cmd.Parameters.AddWithValue("@id_event", evento.Id);
      cmd.ExecuteNonQuery();


      cmd.CommandText = @"UPDATE events 
                             SET title = @title, 
                                 description = @description, 
                                 online = @online, 
                                 photo = @photo,
                                 date_time = @date_time,
                                 link = @link
                           WHERE events.id = @id
                             AND events.user_id = @user_id";
      cmd.Parameters.Clear();
      cmd.Parameters.AddWithValue("@id", evento.Id);
      cmd.Parameters.AddWithValue("@title", evento.Title);
      cmd.Parameters.AddWithValue("@description", evento.Description);
      cmd.Parameters.AddWithValue("@online", evento.Online);
      cmd.Parameters.AddWithValue("@link", evento.Link);
      cmd.Parameters.AddWithValue("@photo", evento.Photo);
      cmd.Parameters.AddWithValue("@user_id", evento.User.Id);
      cmd.Parameters.AddWithValue("@date_time", evento.DateTime);
      cmd.ExecuteNonQuery();
      int idEvent = evento.Id;

      priceRepository.Create(idEvent, evento.Prices.First());

      if (evento.Online != 1)
        addressRepository.Create(idEvent, evento.Address);

      foreach (var category in evento.Categories)
        categoryRepository.Create(idEvent, category);


      response.Success = true;
      Dispose();
      return response;
    }
    catch (System.Exception ex)
    {
      response.Message = "Registro não realizado com sucesso!";
      Console.Write(ex);
    }

    return response;
  }

  public AjaxResponse Read(int? idEvent = null, int? userId = null)
  {
    try
    {
      if (userId != null)
        parameters.Add("user_id = @user_id");

      if (idEvent != null)
        parameters.Add("id = @id");

      if (parameters.Count != 0)
        query += " WHERE " + String.Join(" AND ", parameters.ToArray()) + " AND events.deleted = 0";
      else
        query += " WHERE events.deleted = 0";

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = "SELECT * FROM events " + query + " ORDER BY id DESC";
      cmd.Parameters.Clear();

      if (userId != null)
        cmd.Parameters.AddWithValue("@user_id", userId);

      if (idEvent != null)
        cmd.Parameters.AddWithValue("@id", idEvent);

      SqlDataReader reader = cmd.ExecuteReader();


      while (reader.Read())
      {
        Event evento = new Event
        {
          Id = (int)reader["id"],
          Title = reader["title"].ToString(),
          Description = reader["description"].ToString(),
          Online = (int)reader["online"],
          Link = reader["link"].ToString(),
          Photo = reader["photo"].ToString(),
          DateTime = (DateTime)reader["date_time"],
          User = userRepository.GetUser((int)reader["user_id"]),
          Address = addressRepository.Read((int)reader["id"]),
          Prices = priceRepository.Read((int)reader["id"]),
          Categories = categoryRepository.ReadFromEvent((int)reader["id"])
        };

        events.Add(evento);
      }

      response.DataLength = events.Count;
      response.Data.Add("events", events);

      reader.Close();
    }
    catch (System.Exception)
    {

      throw;
    }

    return response;
  }


  public AjaxResponse Delete(int idEvent)
  {
    try
    {
      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = @"UPDATE events SET deleted = 1 WHERE events.id = @id";
      cmd.Parameters.Clear();
      cmd.Parameters.AddWithValue("@id", idEvent);

      cmd.ExecuteNonQuery();

      Dispose();
    }
    catch (System.Exception ex)
    {
      response.Message = "Registro não realizado com sucesso!";
      Console.Write(ex);
    }

    return response;
  }
}