using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EEmlakApi.Utilities
{
    public class Encryption
    {
        //Newlenmesini istemiyorum o yüzden constructorı Private yaptım.
        private Encryption()
        {
        }

        //SHA256 Sifrele yaparken bu fonksiyondan yardım alınır.Sifreleme için kullanılamaz.
        private static byte[] ByteDonustur(string deger)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            return ByteConverter.GetBytes(deger);
        }

        //DES Sifreleme ve Geri Çözmek için bu fonksiyondan yardım alıyoruz.Şifre için kullanılmaz.
        private static byte[] Byte8(string deger)
        {
            char[] arrayChar = deger.ToCharArray();
            byte[] arrayByte = new byte[arrayChar.Length];
            for (int i = 0; i < arrayByte.Length; i++)
            {
                arrayByte[i] = Convert.ToByte(arrayChar[i]);
            }
            return arrayByte;
        }

        public static string SHA256(string strGiris)
        {
            if (string.IsNullOrEmpty(strGiris))
            {
                throw new ArgumentNullException("Şifrelenecek Veri Yok");
            }
            else
            {
                SHA256Managed sifre = new SHA256Managed();
                byte[] arySifre = ByteDonustur(strGiris);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }

        //Key1 ve Key2 degerleri sadece 8 karakter deger alır.
        public static string DESSifrele(string strGiris, string Key1, string Key2)
        {
            string sonuc = "";
            if (string.IsNullOrEmpty(strGiris))
            {
                throw new ArgumentNullException("Şifrelenecek veri yok");
            }
            else
            {
                byte[] aryKey = Byte8(Key1); // BURAYA 8 bit string DEĞER GİRİN
                byte[] aryIV = Byte8(Key2); // BURAYA 8 bit string DEĞER GİRİN
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(aryKey, aryIV), CryptoStreamMode.Write);
                StreamWriter writer = new StreamWriter(cs);
                writer.Write(strGiris);
                writer.Flush();
                cs.FlushFinalBlock();
                writer.Flush();
                sonuc = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
                writer.Dispose();
                cs.Dispose();
                ms.Dispose();
            }
            return sonuc;
        }

        //Key1 ve Key2 degerleri sadece 8 karakter deger alır.
        public static string DESCoz(string strGiris, string Key1, string Key2)
        {
            string strSonuc = "";
            if (string.IsNullOrEmpty(strGiris))
            {
                throw new ArgumentNullException("Şifrelenecek veri yok.");
            }
            else
            {
                byte[] aryKey = Byte8(Key1);
                byte[] aryIV = Byte8(Key2);
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream(Convert.FromBase64String(strGiris));
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(aryKey, aryIV), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cs);
                strSonuc = reader.ReadToEnd();
                reader.Dispose();
                cs.Dispose();
                ms.Dispose();
            }
            return strSonuc;
        }
    }
}