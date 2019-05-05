using System.Security.Cryptography;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;
using UnityEngine;

public class HashController : Singleton {

    public static HashController instance {
        get { return (HashController) _instance; }
    }

    public string ToHash = "";

    public int GetUniqueHashNumber(string name, int i) {
        string subHash = ComputeSha256Hash(ToHash + name);
        List<int> subHashNumbers = new List<int>();
        foreach (string hashFramgent in Split(subHash, 7)) {
            subHashNumbers.Add(int.Parse(hashFramgent, System.Globalization.NumberStyles.HexNumber));
        }

        return subHashNumbers[i % subHashNumbers.Count];
    }

    static IEnumerable<string> Split(string str, int chunkSize) {
        return Enumerable.Range(0, str.Length / chunkSize)
            .Select(i => str.Substring(i * chunkSize, chunkSize));
    }

    static string ComputeSha256Hash(string rawData) {
        // Create a SHA256   
        using (SHA256 sha256Hash = SHA256.Create()) {
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++) {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
