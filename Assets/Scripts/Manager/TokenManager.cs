using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;

public static class TokenManager
{
    // private static string tokenKey = "authToken";
    // private static string tokenExpiryKey = "tokenExpiry";
    private static string tokenKey = "";

    /// <summary>
    /// Saves the authentication token securely.
    /// </summary>
    /// <param name="token">The JWT token string.</param>
    /// <param name="expiresIn">Token validity duration in seconds.</param>
    public static void SaveToken(string token, int expiresIn = 1440)
    {
        // Encrypt the token before saving
        // string encryptedToken = Encrypt(token);
        // PlayerPrefs.SetString(tokenKey, token);
        // PlayerPrefs.SetString(tokenExpiryKey, DateTime.UtcNow.AddMinutes(expiresIn).ToString());
        // PlayerPrefs.Save();
        tokenKey = token;
    }

    public static string GetToken()
    {
        // if (!PlayerPrefs.HasKey(tokenKey) || !PlayerPrefs.HasKey(tokenExpiryKey))
        //     return null;
        //
        // string token = PlayerPrefs.GetString(tokenKey, string.Empty);
        // string expiryString = PlayerPrefs.GetString(tokenExpiryKey, string.Empty);

        // if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(expiryString))
        //     return null;
        //
        // if (!DateTime.TryParse(expiryString, out DateTime expiry))
        //     return null;
        //
        // if (DateTime.UtcNow > expiry)
        // {
        //     // Token expired
        //     ClearToken();
        //     return null;
        // }

        // Decrypt the token
        return tokenKey;
    }

    /// <summary>
    /// Clears the stored token.
    /// </summary>
    public static void ClearToken()
    {
        // PlayerPrefs.DeleteKey(tokenKey);
        // PlayerPrefs.DeleteKey(tokenExpiryKey);
        // PlayerPrefs.Save();
        tokenKey = "";
    }

    /// <summary>
    /// Encrypts a string using AES.
    /// </summary>
    /// <param name="plainText">The plain text to encrypt.</param>
    /// <returns>The encrypted string in Base64 format.</returns>
    private static string Encrypt(string plainText)
    {
        // NOTE: For demonstration purposes only. Do NOT hard-code keys in production.
        string keyString = "your-32-byte-long-key-here!"; // Must be 32 bytes for AES-256
        string ivString = "your-16-byte-iv!"; // Must be 16 bytes for AES

        byte[] key = Encoding.UTF8.GetBytes(keyString);
        byte[] iv = Encoding.UTF8.GetBytes(ivString);

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                return Convert.ToBase64String(encryptedBytes);
            }
        }
    }

    /// <summary>
    /// Decrypts a Base64 string using AES.
    /// </summary>
    /// <param name="encryptedText">The encrypted string in Base64 format.</param>
    /// <returns>The decrypted plain text.</returns>
    private static string Decrypt(string encryptedText)
    {
        // NOTE: For demonstration purposes only. Do NOT hard-code keys in production.
        string keyString = "your-32-byte-long-key-here!"; // Must be 32 bytes for AES-256
        string ivString = "your-16-byte-iv!"; // Must be 16 bytes for AES

        byte[] key = Encoding.UTF8.GetBytes(keyString);
        byte[] iv = Encoding.UTF8.GetBytes(ivString);
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            {
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}