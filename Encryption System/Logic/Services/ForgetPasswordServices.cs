using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_System.Logic.Services
{
    public static class ForgetPasswordServices
    {
        public static void EditPassword(int id , string newPassword)
        {
            Database.DealingData("Edit_Password", () => Parameters(Database.command, id, newPassword));
        }

        private static void Parameters(SqlCommand command ,int id,string newPassword  ) 
        {
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            command.Parameters.Add("@newPassword", SqlDbType.VarChar).Value = newPassword;
        } 
    }
}
