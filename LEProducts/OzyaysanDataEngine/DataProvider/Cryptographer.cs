using System;
using System.Security.Cryptography;
using System.Web;
using System.IO;
using System.Text;
using System.Security;
using System.Web.Security;
namespace OzyaysanDataEngine.DataProvider
{

    public sealed class Cryptographer
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Cryptographer()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// <para>Description: Generates Random Password</para>
        /// </summary>
        /// <param name="PasswordLength">Length of The Password</param>
        /// <returns>String Includes Password</returns>
        public static string CreateRandomPassword(int PasswordLength)
        {
            System.String _allowedChars = "bcdfghjkmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ23456789";
            Byte[] randomBytes = new Byte[PasswordLength];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)randomBytes[i] % allowedCharCount];
            }

            return new string(chars);
        }


        /// <summary>
        /// <para>Description: Generates Salt Which  Will Be Used To Generate Hash.</para>
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string CreateSalt(int size)
        {
            // Generate a cryptographic random number using the cryptographic
            // service provider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// <para>Description: Generates Passsword Hash.</para>
        /// </summary>
        /// <param name="pwd">Password To Be Hashed.</param>
        /// <param name="salt">SaltString Which Will Be Used In Hashing.</param>
        /// <returns></returns>
        public static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd =
                FormsAuthentication.HashPasswordForStoringInConfigFile(
                saltAndPwd, "SHA1");
            return hashedPwd;
        }


        /// <summary>
        /// <para>Description: Encrypts String By Using Base String(Symmetric Encryption)</para>
        /// </summary>
        /// <param name="InputText">String Which Will Be Encrypted.</param>
        /// <param name="Password">Base String Which Will Be Used In Encryption. (Key To Encrypt, Decrypt)</param>
        /// <returns>Encrypted String</returns>
        public static string EncryptString(string InputText, string Password)
        {

            // We are now going to create an instance of the 

            // Rihndael class.  

            RijndaelManaged RijndaelCipher = new RijndaelManaged();



            // First we need to turn the input strings into a byte array.

            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);



            // We are using salt to make it harder to guess our key

            // using a dictionary attack.

            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());



            // The (Secret Key) will be generated from the specified 

            // password and salt.

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);



            // Create a encryptor from the existing SecretKey bytes.

            // We use 32 bytes for the secret key 

            // (the default Rijndael key length is 256 bit = 32 bytes) and

            // then 16 bytes for the IV (initialization vector),

            // (the default Rijndael IV length is 128 bit = 16 bytes)

            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));



            // Create a MemoryStream that is going to hold the encrypted bytes 

            MemoryStream memoryStream = new MemoryStream();



            // Create a CryptoStream through which we are going to be processing our data. 

            // CryptoStreamMode.Write means that we are going to be writing data 

            // to the stream and the output will be written in the MemoryStream

            // we have provided. (always use write mode for encryption)

            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);



            // Start the encryption process.

            cryptoStream.Write(PlainText, 0, PlainText.Length);



            // Finish encrypting.

            cryptoStream.FlushFinalBlock();



            // Convert our encrypted data from a memoryStream into a byte array.

            byte[] CipherBytes = memoryStream.ToArray();







            // Close both streams.

            memoryStream.Close();

            cryptoStream.Close();



            // Convert encrypted data into a base64-encoded string.

            // A common mistake would be to use an Encoding class for that. 

            // It does not work, because not all byte values can be

            // represented by characters. We are going to be using Base64 encoding

            // That is designed exactly for what we are trying to do. 

            string EncryptedData = Convert.ToBase64String(CipherBytes);



            // Return encrypted string.

            return EncryptedData;

        }

        /// <summary>
        /// <para>Description: Decrypts String By Using Base String(Symmetric Encryption)</para>
        /// </summary>
        /// <param name="InputText">String Which Will Be Decrypted.</param>
        /// <param name="Password">Base String Which Will Be Used In Decryption. (Key To Encrypt, Decrypt)</param>
        /// <returns>Decrypted Plain String</returns>
        public static string DecryptString(string InputText, string Password)
        {

            RijndaelManaged RijndaelCipher = new RijndaelManaged();



            byte[] EncryptedData = Convert.FromBase64String(InputText);

            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());



            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);



            // Create a decryptor from the existing SecretKey bytes.

            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));



            MemoryStream memoryStream = new MemoryStream(EncryptedData);



            // Create a CryptoStream. (always use Read mode for decryption).

            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);



            // Since at this point we don't know what the size of decrypted data

            // will be, allocate the buffer long enough to hold EncryptedData;

            // DecryptedData is never longer than EncryptedData.

            byte[] PlainText = new byte[EncryptedData.Length];



            // Start decrypting.

            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);



            memoryStream.Close();

            cryptoStream.Close();



            // Convert decrypted data into a string. 

            string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);



            // Return decrypted string.   

            return DecryptedData;

        }

        /// <summary>
        /// <para>Description: Encrypts File By Using Base String(Symmetric Encryption)</para>
        /// </summary>
        /// <param name="FileLocation">File Location</param>
        /// <param name="FileDestination">File Destination</param>
        /// <param name="Password">Base String Which Will Be Used In Encryption. (Key To Encrypt, Decrypt)</param>
        private static void EncryptFile(string FileLocation, string FileDestination, string Password)
        {

            RijndaelManaged RijndaelCipher = new RijndaelManaged();



            // First we are going to open the file streams 

            FileStream fsIn = new FileStream(FileLocation, FileMode.Open, FileAccess.Read);

            FileStream fsOut = new FileStream(FileDestination, FileMode.Create, FileAccess.Write);



            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());



            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);



            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));



            CryptoStream cryptoStream = new CryptoStream(fsOut, Encryptor, CryptoStreamMode.Write);



            int ByteData;

            while ((ByteData = fsIn.ReadByte()) != -1)
            {

                cryptoStream.WriteByte((byte)ByteData);

            }



            cryptoStream.Close();

            fsIn.Close();

            fsOut.Close();

        }

    }

}
