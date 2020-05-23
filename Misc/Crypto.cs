using System;
using System.IO;
using System.Security.Cryptography;

namespace CC_Functions.Misc
{
    /// <summary>
    /// Contains cryptographic functions
    /// </summary>
    public static class Crypto
    {
        /// <summary>
        /// Encrypts an array of bytes using SHA512. Use with <see cref="Decrypt">Decrypt</see>
        /// </summary>
        /// <param name="data">The array of bytes to encrypt</param>
        /// <param name="key">The key for encryption, later required to decrypt</param>
        /// <returns>The encrypted data</returns>
        /// <exception cref="ArgumentException">Thrown if provided data is invalid</exception>
        public static byte[] Encrypt(byte[] data, byte[] key)
        {
            if (key is null)
                throw new ArgumentException("Key must have valid value.", nameof(key));
            if (data is null)
                throw new ArgumentException("The text must have valid value.", nameof(data));

            byte[] buffer = data;
            SHA512CryptoServiceProvider hash = new SHA512CryptoServiceProvider();
            byte[] aesKey = new byte[24];
            Buffer.BlockCopy(hash.ComputeHash(key), 0, aesKey, 0, 24);

            using Aes aes = Aes.Create();
            if (aes == null)
                throw new ArgumentException("Parameter must not be null.", nameof(aes));

            aes.Key = aesKey;

            using ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using MemoryStream resultStream = new MemoryStream();
            using (CryptoStream aesStream = new CryptoStream(resultStream, encryptor, CryptoStreamMode.Write))
            {
                using MemoryStream plainStream = new MemoryStream(buffer);
                plainStream.CopyTo(aesStream);
            }

            byte[] result = resultStream.ToArray();
            byte[] combined = new byte[aes.IV.Length + result.Length];
            Array.ConstrainedCopy(aes.IV, 0, combined, 0, aes.IV.Length);
            Array.ConstrainedCopy(result, 0, combined, aes.IV.Length, result.Length);

            return combined;
        }
        /// <summary>
        /// Decrypts an SHA512-encrypted byte array. Use with <see cref="Encrypt">Encrypt</see>
        /// </summary>
        /// <param name="encrypted">The array of bytes to decrypt</param>
        /// <param name="key">The key the data was encrypted with</param>
        /// <returns>The decrypted data</returns>
        /// <exception cref="ArgumentException">Thrown if provided data is invalid</exception>
        public static byte[] Decrypt(byte[] encrypted, byte[] key)
        {
            if (key is null)
                throw new ArgumentException("Key must have valid value.", nameof(key));
            if (encrypted is null)
                throw new ArgumentException("The encrypted text must have valid value.", nameof(encrypted));

            byte[] combined = encrypted;
            byte[] buffer = new byte[combined.Length];
            SHA512CryptoServiceProvider hash = new SHA512CryptoServiceProvider();
            byte[] aesKey = new byte[24];
            Buffer.BlockCopy(hash.ComputeHash(key), 0, aesKey, 0, 24);

            using Aes aes = Aes.Create();
            if (aes == null)
                throw new ArgumentException("Parameter must not be null.", nameof(aes));

            aes.Key = aesKey;

            byte[] iv = new byte[aes.IV.Length];
            byte[] ciphertext = new byte[buffer.Length - iv.Length];

            Array.ConstrainedCopy(combined, 0, iv, 0, iv.Length);
            Array.ConstrainedCopy(combined, iv.Length, ciphertext, 0, ciphertext.Length);

            aes.IV = iv;

            using ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using MemoryStream resultStream = new MemoryStream();
            using (CryptoStream aesStream = new CryptoStream(resultStream, decryptor, CryptoStreamMode.Write))
            {
                using MemoryStream plainStream = new MemoryStream(ciphertext);
                plainStream.CopyTo(aesStream);
            }

            return resultStream.ToArray();
        }
    }
}