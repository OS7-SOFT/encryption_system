using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_System.Logic.Services
{
    public static class GetUseresServices
    {
        //Get All Data
        public static DataTable GetAllAccounts()
        {
            return Database.GetAllData("getAllAccounts");
        }

    }
}
