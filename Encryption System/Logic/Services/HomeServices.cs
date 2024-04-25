using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_System.Logic.Services
{
    public static class HomeServices
    {

        //Add Encrypt file 
        public static void Add(string fileName, string path, byte[] fileData, string key, string iv, string encryptedDate)
        {
            Database.DealingData("Add_encrypt_file", () => parameters(Database.command,fileName,path,fileData,key,iv,encryptedDate));
        }

        private static void parameters(SqlCommand command , string fileName , string path , byte[] fileData , string key , string iv ,string encryptedDate)
        {
            command.Parameters.Add("@fileName",SqlDbType.VarChar).Value = fileName;
            command.Parameters.Add("@path",SqlDbType.VarChar).Value = path;
            command.Parameters.Add("@fileData", SqlDbType.VarBinary).Value = fileData;
            command.Parameters.Add("@key", SqlDbType.VarChar).Value = key;
            command.Parameters.Add("@iv", SqlDbType.VarChar).Value = iv;
            command.Parameters.Add("@encrypteDate", SqlDbType.VarChar).Value = encryptedDate;
        }
        
        //Get Encrypt date 
        public static DataTable GetDate()
        {
            return Database.GetAllData("Encrypt_Date");
        }

        //Get Encrypt Count
        public static int GetCount()
        {
            return Database.getCount("file_Count");
        }

    }
}
