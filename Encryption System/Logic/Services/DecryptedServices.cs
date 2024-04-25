using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_System.Logic.Services
{
    public static class DecryptedServices
    {
        //Get All Data
        public static DataTable GetAllData()
        {
            return Database.GetAllData("Get_All_Data");
        }

        //Get data by value (Search)
        public static DataTable GetDataByValue(string value)
        {
            int id = int.TryParse(value, out id) ? Convert.ToInt32(value) : 0 ;
            string fileName = value;
            return Database.GetDataByValue("Get_Data_By_Value", () => ParameterSearch(Database.command,id,fileName));
        }
        private static void ParameterSearch(SqlCommand command,int id ,string fileName)
        {
            command.Parameters.Add("@fileName", SqlDbType.VarChar).Value = fileName;
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
        }

        //Get Data by id
        public static DataTable GetDataById(int id)
        {
            return Database.GetDataByValue("Get_data_by_id", () => ParameterId(Database.command, id));
        }

        //Delete
        public static void Delete(int id)
        {
            Database.DealingData("Delete_Data", () => ParameterId(Database.command, id));
        }

        private static void ParameterId(SqlCommand command, int id)
        {
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
        }


    }
}
