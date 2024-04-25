using Encryption_System.Logic.Services;
using Encryption_System.Model;
using Encryption_System.Views.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_System.Logic.Presenter
{
    public class HomePresenter
    {
        IHome  view;
        EncryptedModel model = new EncryptedModel();

        public HomePresenter( IHome view)
        {
            this.view = view;
            this.view.EncryptedEvent += EncryptMethod;

            loadData();
        }

        private void loadData()
        {
            view.EncryptCount = HomeServices.GetCount().ToString();

            //Get Last Encrypt Date 
            var encryptedDates = HomeServices.GetDate();
            List<string> allDatesEncrypt = new List<string>();
            if (encryptedDates.Rows.Count > 0)
            {
                for (int i = 0; i < encryptedDates.Rows.Count; i++)
                {
                    allDatesEncrypt.Add(encryptedDates.Rows[i]["encrypteDate"].ToString());
                }

                view.LastDateEncrypt = allDatesEncrypt.Max();
            }
            else
                view.LastDateEncrypt = "---";
           
        }
        
        private void EncryptMethod(object sender, EventArgs e)
        {
            using (FileStream fileStream = new FileStream(view.PathFile, FileMode.Open,FileAccess.Read,FileShare.None,4096,FileOptions.Asynchronous))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (Aes aes = Aes.Create())
                    {
                        try
                        {
                            byte[] key = aes.Key;
                            byte[] iv = aes.IV;
                            
                            fileStream.CopyTo(memoryStream);
                            byte[] fileContent = memoryStream.ToArray();

                            byte[] encryptedContent = EncryptContent(fileContent, key, iv);
                            model.FileData = encryptedContent;
                            model.Key = Hashed(key,key.Length);
                            model.IV = Hashed(iv,iv.Length);
                            model.Path = view.PathFile;
                            model.FileName = view.FileName;
                            model.EncryptedDate = DateTime.Now.ToString("d");
                            //sava Encrypted file in Database
                            HomeServices.Add(model.FileName, model.Path, model.FileData, model.Key, model.IV, model.EncryptedDate);
                            view.IsEncrypted = true;
                            view.Message = "Encrypted Successfully";
                            loadData();
                           
                        }
                        catch(Exception ex)
                        {
                            view.IsEncrypted = false;
                            view.Message = ex.Message;
                        }

                    }
                }
            }
            DecryptedPresenter.loadAllData();
            //delete file
            File.Delete(view.PathFile);

        }
        private static byte[] EncryptContent(byte[] content, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(content, 0, content.Length);
                        cryptoStream.FlushFinalBlock();

                        return memoryStream.ToArray();
                    }
                }
            }
        }


        private string Hashed(byte[] value,int length)
        {
            float[] f = new float[length];

            value.CopyTo(f, 0);

            for (int i = 0; i < f.Length; i++)
            {
                f[i] /= 2; 
            }

            for (int i = 0; i < (int)f.Length/2; i++)
            {
                float temp = f[i];
                f[i] = f[f.Length - 1 - i];
                f[f.Length - 1 - i] = temp;
            }

            string val = null;
            foreach (var item in f)
            {
                val += item + ",";
            }

            byte[] valBytes = System.Text.Encoding.UTF8.GetBytes(val);
            string valEncoded = Convert.ToBase64String(valBytes);


            return valEncoded;
        }
    }
}
