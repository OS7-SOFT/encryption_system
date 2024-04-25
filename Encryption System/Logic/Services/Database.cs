using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_System.Logic.Services
{
    public static class Database
    {

        public static SqlCommand command;

        //Get Connection from Database
        private static SqlConnection GetConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = Properties.Settings.Default.ServicesName;
            builder.InitialCatalog = Properties.Settings.Default.DbName;
            builder.IntegratedSecurity = true;


            return new SqlConnection(builder.ConnectionString);
        }



        // To get all data
        public static DataTable GetAllData(string spName)
        {
            DataTable tbl = new DataTable();
            SqlDataAdapter adapter;

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                command = new SqlCommand(spName, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.ExecuteNonQuery();

                adapter = new SqlDataAdapter(command);
                adapter.Fill(tbl);
                adapter.Dispose();

                connection.Close();

            }

            return tbl;
        }

        // To get data by value
        public static DataTable GetDataByValue(string spName, Action addParameter)
        {
            DataTable tbl = new DataTable();
            SqlDataAdapter adapter;

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                command = new SqlCommand(spName, connection);
                command.CommandType = CommandType.StoredProcedure;

                //invok Method
                addParameter.Invoke();

                command.ExecuteNonQuery();

                adapter = new SqlDataAdapter(command);
                adapter.Fill(tbl);
                adapter.Dispose();

                connection.Close();

            }

            return tbl;
        }

        //To add edit delete data
        public static void DealingData(string spName, Action addParameter)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                command = new SqlCommand(spName, connection);
                command.CommandType = CommandType.StoredProcedure;

                //execute method 
                addParameter.Invoke();

                command.ExecuteNonQuery();

                connection.Close();
            }

        }

        // To get number of table
        public static int getCount(string spName)
        {
            int count = 0;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                command = new SqlCommand(spName, connection);
                count = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
            return count;
        }

        // To get Larg number of table
        public static double getLargNumber(string spName)
        {
            double count = 0;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                command = new SqlCommand(spName, connection);
                count = Convert.ToDouble(command.ExecuteScalar());
                connection.Close();
            }
            return count;
        }


        // To get Sum values of table
        public static double getSum(string spName, Action addParameter)
        {
            double count = 0;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                command = new SqlCommand(spName, connection);
                command.CommandType = CommandType.StoredProcedure;
                //execute method 
                addParameter.Invoke();
                count = Convert.ToDouble(command.ExecuteScalar());
                connection.Close();
            }
            return count;
        }
    }
}
