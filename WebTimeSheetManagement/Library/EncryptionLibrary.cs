namespace EventApplicationCore.Library
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="EncryptionLibrary" />
    /// </summary>
    public class EncryptionLibrary
    {
        /// <summary>
        /// The AES_Encrypt
        /// </summary>
        /// <param name="bytesToBeEncrypted">The bytesToBeEncrypted<see cref="byte[]"/></param>
        /// <param name="passwordBytes">The passwordBytes<see cref="byte[]"/></param>
        /// <returns>The <see cref="byte[]"/></returns>
        public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        /// <summary>
        /// The AES_Decrypt
        /// </summary>
        /// <param name="bytesToBeDecrypted">The bytesToBeDecrypted<see cref="byte[]"/></param>
        /// <param name="passwordBytes">The passwordBytes<see cref="byte[]"/></param>
        /// <returns>The <see cref="byte[]"/></returns>
        public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            try
            {
                byte[] decryptedBytes = null;

                // Set your salt here, change it to meet your flavor:
                // The salt bytes must be at least 8 bytes.
                byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

                using (MemoryStream ms = new MemoryStream())
                {
                    using (RijndaelManaged AES = new RijndaelManaged())
                    {
                        AES.KeySize = 256;
                        AES.BlockSize = 128;

                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);

                        AES.Mode = CipherMode.CBC;

                        using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                            cs.Close();
                        }
                        decryptedBytes = ms.ToArray();
                    }
                }

                return decryptedBytes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The EncryptText
        /// </summary>
        /// <param name="input">The input<see cref="string"/></param>
        /// <param name="password">The password<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string EncryptText(string input, string password = "E6t187^D43%F")
        {
            try
            {
                // Get the bytes of the string
                byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Hash the password with SHA256
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

                string result = Convert.ToBase64String(bytesEncrypted);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The DecryptText
        /// </summary>
        /// <param name="input">The input<see cref="string"/></param>
        /// <param name="password">The password<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string DecryptText(string input, string password = "E6t187^D43%F")
        {
            try
            {
                // Get the bytes of the string
                byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

                string result = Encoding.UTF8.GetString(bytesDecrypted);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Defines the <see cref="KeyGenerator" />
        /// </summary>
        public static class KeyGenerator
        {
            /// <summary>
            /// The GetUniqueKey
            /// </summary>
            /// <param name="maxSize">The maxSize<see cref="int"/></param>
            /// <returns>The <see cref="string"/></returns>
            public static string GetUniqueKey(int maxSize = 15)
            {
                try
                {
                    char[] chars = new char[62];
                    chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
                    byte[] data = new byte[1];
                    using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
                    {
                        crypto.GetNonZeroBytes(data);
                        data = new byte[maxSize];
                        crypto.GetNonZeroBytes(data);
                    }
                    StringBuilder result = new StringBuilder(maxSize);
                    foreach (byte b in data)
                    {
                        result.Append(chars[b % (chars.Length)]);
                    }
                    return result.ToString();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
