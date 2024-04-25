using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_System.Views.Interface
{
    public interface IHome
    {
        string FileName { get; }
        string EncryptCount { set; }
        string LastDateEncrypt { set; }
        
        //Properties
        bool IsEncrypted { get; set; }
        string Message { get; set; }
        string PathFile { get;  }

        //event 
        event EventHandler EncryptedEvent; 
    }
}
