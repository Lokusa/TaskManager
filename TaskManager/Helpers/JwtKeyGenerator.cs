using System;

namespace TaskManager.Helpers
{
    public class JwtKeyGenerator
    {
        public static string GenerateKey()
        {
            // Create a random byte array
            var randomBytes = new byte[64]; // 64-byte array (for example)
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            // Convert it to a base64 string
            return Convert.ToBase64String(randomBytes);
        }

        public static void Main()
        {
            // Generate and print the key
            string key = GenerateKey();
            Console.WriteLine("Generated Key: " + key);
        }
    }
}
