using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_System.Views.Interface
{
    public interface ILoginView
    {
        string UserName { get; set; }
        string Password { get; set; }
        string Message { get; set; }
        bool IsLogged { get; set; }

        //event
        event EventHandler LoginEvent;


        
    }
}
