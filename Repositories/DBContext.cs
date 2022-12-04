using System.Data.SqlClient;

namespace ricanEventos.Repositories
{
  public abstract class DBContext
  {
    // S3cur3P@ssW0rd! 
    // sa 
    string connectionString = "Server=localhost,1433;Database=ricanEventos;User Id=sa;Password=S3cur3P@ssW0rd!;";
    protected SqlConnection connection;

    public DBContext()
    {
      connection = new SqlConnection(connectionString);
      connection.Open();
    }

    public void Dispose()
    {
      if (connection != null)
        connection.Close();
      SqlConnection.ClearPool(connection);
    }
  }
}