using System;
using System.Text;

namespace LR2.Functions {
    public class TextOperations {
        public static void ExampleText() {
            Console.WriteLine("===[ Example of using System.Text ]===");


            ASCIIEncoding ascii = new ASCIIEncoding();
            String unicodeString = "Hello, I am Max Bukator and this is second lab for adaptive web";
            Console.WriteLine($"Original string: {unicodeString}");


            Byte[] encodedBytes = ascii.GetBytes(unicodeString);
            Console.WriteLine("Encoded Bytes: ");
            foreach (Byte b in encodedBytes) {
                Console.Write($"{b} ");
            }


            String decodedString = ascii.GetString(encodedBytes);
            Console.WriteLine($"\n\nDecoded bytes: {decodedString}");


            Console.WriteLine("\n");
        }
    }
}
