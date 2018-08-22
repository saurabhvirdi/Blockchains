using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace SampleBlockchain
{
    public class BlockUtility
    {
        /// <summary>
        /// Gets the sha256 hash.
        /// </summary>
        /// <param name="rawData">The raw data.</param>
        /// <returns></returns>
        public static string GetSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash() - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                var sb = new StringBuilder();
                foreach (var bt in bytes)
                {
                    sb.Append(String.Format("{0:X2}", bt));
                }

                return sb.ToString();
            }
        }
    }
}
