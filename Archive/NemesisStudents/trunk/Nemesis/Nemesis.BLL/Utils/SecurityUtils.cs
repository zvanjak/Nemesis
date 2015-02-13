using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.BLL.Utils
{
  public class SecurityUtils
  {

    private const int SALT_BYTES_NUM = 8;

    public static string Base64Encode(byte[] plainText) 
    {
      return System.Convert.ToBase64String(plainText);
    }

    public static byte[] Base64Decode(string base64String)
    {
      return System.Convert.FromBase64String(base64String);
    }

    public static byte[] GenerateSalt()
    {
      RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

      byte[] saltBytes = new byte[SALT_BYTES_NUM];
      rng.GetNonZeroBytes(saltBytes);
      return saltBytes;
    }

    public static byte[] ComputeSaltedPasswordHash(string password, byte[] salt)
    {
      byte[] passwordBytes = UTF8Encoding.UTF8.GetBytes(password);

      byte[] preHashed = new byte[salt.Length + passwordBytes.Length];
      System.Buffer.BlockCopy(passwordBytes, 0, preHashed, 0, passwordBytes.Length);
      System.Buffer.BlockCopy(salt, 0, preHashed, passwordBytes.Length, salt.Length);

      SHA256 sha256 = SHA256.Create();

      return sha256.ComputeHash(preHashed);
    }

    public static bool ValidatePassword(string inputPassword, byte[] salt, byte[] validPassword)
    {
      byte[] hashedPassword = ComputeSaltedPasswordHash(inputPassword, salt);

      return hashedPassword.SequenceEqual(validPassword);
    }

  }
}
