using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;

namespace Digital.Common
{
    public  enum SerurityType
    {
        UserInfoImage,
        UserInfoFolder,
        CaseId
    }

    public class CryptoService
    {
        internal const string PasswordKey = "MvcS0lk$";
        private const string HashKey = "SHA256Managed";

        internal const string PasswordImageKey = "我的宝贝龙玥涵";
        internal const string PasswordUserFolder = "我的宝贝小小";
        internal const string PasswordCaseId = "佛学阿閦如来陀罗尼";

        public static string HashEncrypt(string input)
        {
            string hashed = Cryptographer.CreateHash(HashKey, input);
            return hashed;
        }

        public static string MD5Encrypt(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var keyBytes = Encoding.UTF8.GetBytes(PasswordKey);

            var des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(keyBytes);
            des.Mode = CipherMode.ECB;
            var encrypted = des.CreateEncryptor().TransformFinalBlock(inputBytes, 0, inputBytes.Length);
            return Convert.ToBase64String(encrypted);
        }

        public static string MD5Encrypt(string input, SerurityType Types)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var des = new TripleDESCryptoServiceProvider();
            if (Types == SerurityType.UserInfoImage)
            {
                var keyBytes = Encoding.UTF8.GetBytes(PasswordImageKey);
                des.Key = MakeMD5(keyBytes);
            }
            else if (Types == SerurityType.UserInfoFolder)
            {
                var keyBytes = Encoding.UTF8.GetBytes(PasswordUserFolder);
                des.Key = MakeMD5(keyBytes);
            }
            des.Mode = CipherMode.ECB;
            var encrypted = des.CreateEncryptor().TransformFinalBlock(inputBytes, 0, inputBytes.Length);
            return Convert.ToBase64String(encrypted);

        }

        public static string MD5Decrypt(string encodedString, SerurityType Types)
        {
            var encodedBytes = Convert.FromBase64String(encodedString);
            var des = new TripleDESCryptoServiceProvider();
            if (Types == SerurityType.UserInfoImage)
            {
                var keyBytes = Encoding.UTF8.GetBytes(PasswordImageKey);
                des.Key = MakeMD5(keyBytes);
            }
            else if (Types == SerurityType.UserInfoFolder)
            {
                var keyBytes = Encoding.UTF8.GetBytes(PasswordUserFolder);
                des.Key = MakeMD5(keyBytes);
            }
            des.Mode = CipherMode.ECB;
            var decrypted = des.CreateDecryptor().TransformFinalBlock(encodedBytes, 0, encodedBytes.Length);
            return Encoding.Default.GetString(decrypted);
        }

        public static string MD5Decrypt(string encodedString)
        {
            var encodedBytes = Convert.FromBase64String(encodedString);
            var keyBytes = Encoding.UTF8.GetBytes(PasswordKey);

            var des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(keyBytes);
            des.Mode = CipherMode.ECB;
            var decrypted = des.CreateDecryptor().TransformFinalBlock(encodedBytes, 0, encodedBytes.Length);
            return Encoding.Default.GetString(decrypted);
        }

        #region private methods

        private static byte[] MakeMD5(byte[] original)
        {
            using (var hashmd5 = new MD5CryptoServiceProvider())
            {
                byte[] keyhash = hashmd5.ComputeHash(original);
                return keyhash;
            }
        }

        #endregion
    }
}
