using System.Data.SqlClient;
using ricanEventos.Models;
using ricanEventos.Helpers;

namespace ricanEventos.Repositories;

public class UserRepository : DBContext, IUserRepository
{
  private AjaxResponse response = new AjaxResponse();
  private string query = " ";
  private List<string> parameters = new List<string>();
  public AjaxResponse Create(User user)
  {
    try
    {
      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = @"INSERT INTO users(name, email, password) VALUES (@name, @email, @password)";
      cmd.Parameters.Clear();
      cmd.Parameters.AddWithValue("@name", user.Name);
      cmd.Parameters.AddWithValue("@email", user.Email);
      cmd.Parameters.AddWithValue("@password", user.Password);
      cmd.ExecuteNonQuery();

      response.Success = true;

      return response;
    }
    catch (System.Exception ex)
    {
      response.Message = "Registro não realizado com sucesso!";
      Console.Write(ex);
    }

    return response;
  }
  public AjaxResponse Read(int? userId = null, string? Email = null, string? Password = null, string? userIdOperation = "=")
  {
    try
    {
      if (userId != null)
        parameters.Add("id " + userIdOperation + " @user_id");

      if (Email != null)
        parameters.Add("email = @email");

      if (Password != null)
        parameters.Add("password = @password");

      if (parameters.Count != 0)
        query += " WHERE " + String.Join(" AND ", parameters.ToArray());

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = "SELECT * FROM users" + query;
      cmd.Parameters.Clear();

      if (userId != null)
        cmd.Parameters.AddWithValue("@user_id", userId);

      if (Email != null)
        cmd.Parameters.AddWithValue("@email", Email.Trim());

      if (Password != null)
        cmd.Parameters.AddWithValue("@password", Password.Trim());

      SqlDataReader reader = cmd.ExecuteReader();

      List<User> users = new List<User>();

      while (reader.Read())
      {
        User user = new User
        {
          Id = (int)reader["id"],
          Name = reader["Name"].ToString(),
          Email = reader["Email"].ToString()
        };
        response.UserId = (int)reader["id"];
        users.Add(user);
      }

      response.DataLength = users.Count;
      response.Data.Add("users", users);

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

  public AjaxResponse Update(User user)
  {
    try
    {
      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = @"UPDATE users SET email = @email, name = @name, password = @password WHERE id = @id";
      cmd.Parameters.Clear();
      cmd.Parameters.AddWithValue("@id", user.Id);
      cmd.Parameters.AddWithValue("@name", user.Name);
      cmd.Parameters.AddWithValue("@email", user.Email);
      cmd.Parameters.AddWithValue("@password", user.Password);
      cmd.ExecuteNonQuery();

      response.Success = true;

      return response;
    }
    catch (System.Exception ex)
    {
      response.Message = "Registro não realizado com sucesso!";
      Console.Write(ex);
    }

    return response;
  }


  public User GetUser(int userId)
  {
    User user = new User();
    try
    {

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = connection;
      cmd.CommandText = "SELECT * FROM users WHERE users.id = @id";
      cmd.Parameters.Clear();

      cmd.Parameters.AddWithValue("@id", userId);

      SqlDataReader reader = cmd.ExecuteReader();



      while (reader.Read())
      {
        user.Id = (int)reader["id"];
        user.Name = reader["Name"].ToString();
        user.Email = reader["Email"].ToString();
      }

      reader.Close();
    }
    catch (System.Exception ex)
    {
      Console.WriteLine("Error ao buscar usuáriodfasdf");
      Console.WriteLine(ex);
    }

    return user;
  }

}