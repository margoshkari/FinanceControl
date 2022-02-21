using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FinanceControl
{
    public class SqlOperation
    {
        private SqlConnection connection;
        private SqlCommand command;
        public SqlOperation()
        {
            connection = Connection.GetConnection();
        }
        public bool AddCategory(string name)
        {
            using (command = new SqlCommand($"INSERT INTO Category VALUES('{name}')", connection))
            {
                if (command.ExecuteNonQuery() != -1)
                    return true;
            }

            return false;
        }
        public bool DeleteCategory(string name)
        {
            using (command = new SqlCommand($"DELETE FROM Category WHERE Name = '{name}'", connection))
            {
                if (command.ExecuteNonQuery() != -1)
                    return true;
            }

            return false;
        }
        public List<string> GetCategories()
        {
            command = new SqlCommand($"SELECT * FROM Category", connection);
            List<string> names = new List<string>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    names.Add(reader["name"].ToString());
                }
            }
            return names;
        }
        private int GetCategoryId(string categoryName)
        {
            command = new SqlCommand($"SELECT ID FROM Category WHERE Name = '{categoryName}'", connection);
            int id = 0;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    id = int.Parse(reader["ID"].ToString());
                }
            }
            return id;
        }
        public bool AddIncome(int count, string categoryName)
        {
            int id = GetCategoryId(categoryName);

            using (command = new SqlCommand($"INSERT INTO Info VALUES({count}, '{DateTime.Now.ToString("yyyy'-'MM'-'dd")}', {id}, 1)", connection))
            {
                if (command.ExecuteNonQuery() != -1)
                    return true;
            }

            return false;
        }
        public bool AddExpense(int count, string categoryName)
        {
            int id = GetCategoryId(categoryName);

            using (command = new SqlCommand($"INSERT INTO Info VALUES({count}, '{DateTime.Now.ToString("yyyy'-'MM'-'dd")}', {id}, 2)", connection))
            {
                if (command.ExecuteNonQuery() != -1)
                    return true;
            }

            return false;
        }
        public IEnumerable<FinanceInfo> FilterByDate(DateTime date)
        {
            command = new SqlCommand($"SELECT * FROM Info WHERE Date = '{date.ToString("yyyy'-'MM'-'dd")}'", connection);
            return Filter();
        }
        public IEnumerable<FinanceInfo> FilterByCost(int from, int to)
        {
            command = new SqlCommand($"SELECT * FROM Info WHERE Count > {from} AND Count < {to}", connection);
            return Filter();
        }
        public IEnumerable<FinanceInfo> FilterByCategory(string categoryName)
        {
            int id = GetCategoryId(categoryName);
            command = new SqlCommand($"SELECT * FROM Info WHERE IdCategory = {id}", connection);
            return Filter();
        }
        private IEnumerable<FinanceInfo> Filter()
        {
            List<FinanceInfo> info = new List<FinanceInfo>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    info.Add(new FinanceInfo((int)reader["Count"], (DateTime)reader["Date"], (int)reader["IdCategory"], (int)reader["IdManagement"]));
                }
            }
            return info;
        }
    }
}
