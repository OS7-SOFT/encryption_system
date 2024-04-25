using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encryption_System.Views.Interface
{
    public interface IDecryptedView
    {

        //Properties
        int Id { get; }
        string Value { get; set; }
        bool IsDecrypted { set; }
        string Message { set; }

        //event 
        event EventHandler SearchEvent;
        event EventHandler DecryptedEvent;
        event EventHandler DeleteEvent;

        //method
        void SetDataFileEncrypted(BindingSource fileDataList);
    }
}
