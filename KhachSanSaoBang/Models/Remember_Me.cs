using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KhachSanSaoBang.Models
{
    public static class Remember_Me
    {
        private static readonly string Key = KhachSanSaoBang.Properties.Settings.Default.AES_Key;
        private static readonly string IV = KhachSanSaoBang.Properties.Settings.Default.AES_IV;

        public static string MaHoaThongTin(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = Encoding.UTF8.GetBytes(IV);
                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    byte[] data = Encoding.UTF8.GetBytes(plainText);
                    byte[] encrypted = encryptor.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(encrypted);
                }
            }
        }

        public static string GiaiMaThongTin(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = Encoding.UTF8.GetBytes(IV);
                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    byte[] data = Convert.FromBase64String(cipherText);
                    byte[] decrypted = decryptor.TransformFinalBlock(data, 0, data.Length);
                    return Encoding.UTF8.GetString(decrypted);
                }
            }
        }
    }
}
