using System.Data.SqlClient;

namespace FinanceControl
{
    public class Connection
    {
        private static SqlConnection connection;
        private Connection() { }

        public static SqlConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new SqlConnection("Server=margo.database.windows.net;Initial Catalog=UsersData;User ID=margos;Password=Chimneys2578;");
                connection.Open();
            }

            return connection;
        }
    }
}
