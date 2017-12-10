using System.Configuration;

namespace MP.Framework.Security.Encryption
{
    public class EncryptionConfig : ConfigurationSection
    {
        public static readonly EncryptionConfig DefaultInstance = (EncryptionConfig)ConfigurationManager.GetSection("MP/encryption");

        public EncryptionConfig()
        {
        }

        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get { return (string)this["key"]; }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("iv", IsRequired = true)]
        public string IV
        {
            get { return (string)this["iv"]; }
            set { this["iv"] = value; }
        }

        [ConfigurationProperty("KeySize", IsRequired = true)]
        public int KeySize
        {
            get { return (int)this["KeySize"]; }
            set { this["KeySize"] = value; }
        }

        [ConfigurationProperty("BlockSize", IsRequired = true)]
        public int BlockSize
        {
            get { return (int)this["BlockSize"]; }
            set { this["BlockSize"] = value; }
        }
    }
}