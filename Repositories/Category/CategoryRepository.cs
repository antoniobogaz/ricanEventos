using System.Data.SqlClient;
using ricanEventos.Models;
using ricanEventos.Helpers;

namespace ricanEventos.Repositories;

public class CategoryRepository : DBContext, ICategoryRepository
{
  private AjaxResponse response = new AjaxResponse();
  private string query = " ";
  private List<string> parameters = new List<string>();

  private List<Category> categories = new List<Category>();
  public void Create(int idEvent, Category category)
  {
    try
    {
      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = @"INSERT INTO event_categories(id_event, id_category) VALUES (@id_event, @id_category)";
      cmd.Parameters.Clear();
      cmd.Parameters.AddWithValue("@id_event", idEvent);
      cmd.Parameters.AddWithValue("@id_category", category.Id);
      cmd.ExecuteNonQuery();

    }
    catch (System.Exception ex)
    {

      Console.Write(ex);
    }
  }
  public AjaxResponse Read()
  {
    try
    {

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = "SELECT * FROM categories";
      cmd.Parameters.Clear();

      SqlDataReader reader = cmd.ExecuteReader();

      List<Category> categories = new List<Category>();

      while (reader.Read())
      {
        Category category = new Category
        {
          Id = (int)reader["id"],
          Name = reader["Name"].ToString(),
        };
        response.UserId = (int)reader["id"];
        categories.Add(category);
      }

      response.DataLength = categories.Count;
      response.Data.Add("categories", categories);

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

  public List<Category> ReadFromEvent(int eventId)
  {
    try
    {
      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = @"SELECT * 
                            FROM categories, event_categories 
                           WHERE event_categories.id_event = @id_event
                             AND event_categories.id_category = categories.id";
      cmd.Parameters.Clear();
      cmd.Parameters.AddWithValue("@id_event", eventId);

      SqlDataReader reader = cmd.ExecuteReader();

      while (reader.Read())
      {
        Category user = new Category
        {
          Id = (int)reader["id_category"],
          Name = reader["Name"].ToString()
        };
        response.UserId = (int)reader["id"];
        categories.Add(user);
      }

      reader.Close();
    }
    catch (System.Exception)
    {

      throw;
    }

    return categories;
  }

  public void Delete(int eventId)
  {
    try
    {
      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = @"DELETE FROM event_categories WHERE id_event = @id_event";
      cmd.Parameters.Clear();
      cmd.Parameters.AddWithValue("@id_event", eventId);
      cmd.ExecuteNonQuery();

    }
    catch (System.Exception ex)
    {

      Console.Write(ex);
    }
  }
}