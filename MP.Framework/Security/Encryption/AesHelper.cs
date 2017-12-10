using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using MP.Framework.Extensions;

namespace MP.Framework.Security.Encryption
{
    public class AesHelper
    {
        private readonly AesManaged _aesManaged;

        public AesHelper()
        {
            _aesManaged = CreateAesManaged(EncryptionConfig.DefaultInstance);
        }

        public AesHelper(EncryptionConfig config)
        {
            _aesManaged = CreateAesManaged(config);
        }

        public AesManaged CreateAesManaged(EncryptionConfig config)
        {
            AesManaged aesManaged = new AesManaged
            {
                KeySize = config.KeySize,
                BlockSize = config.BlockSize,
                Key = Convert.FromBase64String(config.Key),
                IV = Convert.FromBase64String(config.IV),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };

            return aesManaged;
        }

        public string Encrypt(string data)
        {
            if (String.IsNullOrWhiteSpace(data))
            {
                return data;
            }

            string encryptedData;

            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream();

                using (CryptoStream crypto = new CryptoStream(ms, _aesManaged.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] buffer = Encoding.Unicode.GetBytes(data);

                    crypto.Write(buffer, 0, buffer.Length);
                    crypto.FlushFinalBlock();

                    encryptedData = Convert.ToBase64String(ms.ToArray());
                }
            }
            finally
            {
                if (ms != null)
                {
                    ms.Dispose();
                }
            }

            return encryptedData;
        }

        public string Decrypt(string data)
        {
            if (!data.IsBase64String())
            {
                return data;
            }

            string decryptedData;

            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream();
                using (CryptoStream crypto = new CryptoStream(ms, _aesManaged.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    byte[] buffer = Convert.FromBase64String(data);

                    crypto.Write(buffer, 0, buffer.Length);
                    crypto.FlushFinalBlock();

                    decryptedData = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            finally
            {
                if (ms != null)
                {
                    ms.Dispose();
                }
            }

            return decryptedData;
        }
    }
}
