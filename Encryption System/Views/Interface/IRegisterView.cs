using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_System.Views.Interface
{
    public interface IRegisterView
    {
        //propertes
        string UserName { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        string Favorite { get; set; }

        string Message { get; set; }
        bool isSuccessful { get; set; }

        //event
        event EventHandler RegisterEvent;
        event EventHandler CancelEvent;
    }
}
