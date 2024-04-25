using Encryption_System.Logic.Services;
using Encryption_System.Model;
using Encryption_System.Views.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encryption_System.Logic.Presenter
{
    public class DecryptedPresenter
    {
        IDecryptedView View;
        DecryptedModel model = new DecryptedModel();
        static BindingSource fileDataList;


        public DecryptedPresenter(IDecryptedView view)
        {
            fileDataList = new BindingSource();
            this.View = view;
            this.View.SearchEvent += SearchCurrentFile;
            this.View.DecryptedEvent += DecryptedMethod;
            this.View.DeleteEvent += DeleteMethod;
            this.View.SetDataFileEncrypted(fileDataList);
            loadAllData();
        }

        public static void loadAllData()
        {
            fileDataList.DataSource = DecryptedServices.GetAllData();
        }
        private void SearchCurrentFile(object sender, EventArgs e)
        {
            model.Value = View.Value;
            bool isEmpty = string.IsNullOrEmpty(model.Value);

            if (isEmpty)
                fileDataList.DataSource = DecryptedServices.GetAllData();
            else
                fileDataList.DataSource = DecryptedServices.GetDataByValue(model.Value);
        }


        private void DecryptedMethod(object sender, EventArgs e)
        {
            try
            {
                model.Id = View.Id;
                DataTable data = DecryptedServices.GetDataById(model.Id);
                

                string decryptedFilePath = data.Rows[0][2].ToString();

                byte[] encryptedContent = data.Rows[0]["fileData"] as byte[];
                string keyString = data.Rows[0][4].ToString();
                string ivString = data.Rows[0][5].ToString();
                byte[] key = UnHashed(keyString, 32);
                byte[] iv = UnHashed(ivString, 16);
                byte[] decryptedContent = DecryptContent(encryptedContent, key,iv );

                using (FileStream fileStream = File.Create(decryptedFilePath,4096,FileOptions.Asynchronous))
                {
                    fileStream.Write(decryptedContent, 0, decryptedContent.Length);
                }
                View.IsDecrypted = true;
                View.Message = "Decrypted Successfully";
            }
            catch(Exception ex)
            {
                View.IsDecrypted = false;
                View.Message = ex.Message;
            }
        }

        private static byte[] DecryptContent(byte[] content,byte[] key , byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Write))
                    {
                        
                        cryptoStream.Write(content, 0, content.Length);
                        cryptoStream.FlushFinalBlock();

                        return memoryStream.ToArray();
                    }
                }
            }
        }

        //Unhashed key and iv
        private byte[] UnHashed(string value, int length)
        {
            byte[] decodedBytes = Convert.FromBase64String(value);
            string decodedValue = System.Text.Encoding.UTF8.GetString(decodedBytes);

            string[] listString = decodedValue.Split(',');
            
            byte[] newValue = new byte[length];

            for (int i = 0; i < length; i++)
            {
                if(listString[i] != "")
                {
                    newValue[i] = (byte)(float.Parse(listString[i]) * 2);
                }
            }

            for (int i = 0; i < (int)newValue.Length / 2; i++)
            {
                byte temp = newValue[i];
                newValue[i] = newValue[newValue.Length - 1 - i];
                newValue[newValue.Length - 1 - i] = temp;
            }


            return newValue;
        }

        //Delete file from database after decrypted
        private void DeleteMethod(object sender, EventArgs e)
        {
            model.Id = View.Id;
            DecryptedServices.Delete(model.Id);
            loadAllData();
        }

    }
}
