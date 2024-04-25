using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_System.Views.Interface
{
    public interface IForgetPasswordView
    {
        string Favorite { get; set; }
        string UserName { get; }
        string newPassword { get; set; }
        string ConfirmPass { get; set; }
        string Message {  set; }
        bool IsValid {  set; }
        bool IsEdited {  set; }

        //event
        event EventHandler CheckEvent;
        event EventHandler EditEvent; 


    }
}
