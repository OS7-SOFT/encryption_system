using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_System.Logic.Services
{
    public static class RegisterServices
    {
        public static void Add(string userName, string password,string favorite)
        {
            Database.DealingData("createAccount", () => ParameterAdd(Database.command, userName, password,favorite));
        }

        private static void ParameterAdd(SqlCommand command, string userName, string password, string favorite)
        {
            command.Parameters.Add("@userName", SqlDbType.VarChar).Value = userName;
            command.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
            command.Parameters.Add("@favorite", SqlDbType.VarChar).Value = favorite;

        }

        //Delete Admin Account 
        public static void DeleteAdmin()
        {
            Database.DealingData("delete_admin_account", () => { });
        }
    }
}
