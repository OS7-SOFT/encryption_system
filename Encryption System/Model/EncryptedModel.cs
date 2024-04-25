using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_System.Model
{
    public class EncryptedModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public byte[] FileData { get; set; }
        public string Key { get; set; }
        public string IV { get; set; }
        public string EncryptedDate { get; set; }

    }
}
