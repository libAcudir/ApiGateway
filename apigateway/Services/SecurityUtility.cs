using Common.Interfaces;
using NLog;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class SecurityUtility : ISecurityUtility
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private static ISecurityUtility instance;
        public static ISecurityUtility Instance
        {
            get
            {
                return instance ?? (instance = new SecurityUtility());
            }
        }

        private const string _key = "12SVA5%5f$";

        public string Encrypt(string plainStr)
        {
            try
            {
                var keyString = GenerateAPassKey(_key);
                RijndaelManaged aesEncryption = new RijndaelManaged();
                aesEncryption.KeySize = 256;
                aesEncryption.BlockSize = 128;
                aesEncryption.Mode = CipherMode.ECB;
                aesEncryption.Padding = PaddingMode.ISO10126;
                byte[] KeyInBytes = Encoding.UTF8.GetBytes(keyString);
                aesEncryption.Key = KeyInBytes;
                byte[] plainText = ASCIIEncoding.UTF8.GetBytes(plainStr);
                ICryptoTransform crypto = aesEncryption.CreateEncryptor();
                byte[] cipherText = crypto.TransformFinalBlock(plainText, 0, plainText.Length);
                return Convert.ToBase64String(cipherText);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error al cifrar el texto.");
                throw; // Lanza la excepción para manejo adicional
            }

        }

        public string Decrypt(string encryptedText)
        {
            try
            {
                var keyString = GenerateAPassKey(_key);
                RijndaelManaged aesEncryption = new RijndaelManaged();
                aesEncryption.KeySize = 256;
                aesEncryption.BlockSize = 128;
                aesEncryption.Mode = CipherMode.ECB;
                aesEncryption.Padding = PaddingMode.ISO10126;
                byte[] KeyInBytes = Encoding.UTF8.GetBytes(keyString);
                aesEncryption.Key = KeyInBytes;
                ICryptoTransform decrypto = aesEncryption.CreateDecryptor();
                byte[] encryptedBytes = Convert.FromBase64CharArray(encryptedText.ToCharArray(), 0, encryptedText.Length);
                return ASCIIEncoding.UTF8.GetString(decrypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length));
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error al desencriptar contraseña.");
                throw;
            }
        }

        public string GenerateAPassKey(string passphrase)
        {
            try
            {
                string passPhrase = passphrase;
                string saltValue = passphrase;
                string hashAlgorithm = "SHA1";
                int passwordIterations = 2;
                int keySize = 256;
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
                byte[] Key = pdb.GetBytes(keySize / 11);
                String KeyString = Convert.ToBase64String(Key);
                return KeyString;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error al generar la passkey");
                throw;
            }
        }

    }
}