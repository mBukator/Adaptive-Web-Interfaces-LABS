using System;
using System.IO;

namespace LR2.Functions {
    public class ExampleIO {
        public static void Example_IO() {
            Console.WriteLine("===[ Example of using System.IO ]===");
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Salute.txt");
            if (!File.Exists(path)) {
                using (StreamWriter sw = File.CreateText(path)) {
                    sw.WriteLine("Hello");
                    sw.WriteLine("my name");
                    sw.WriteLine("Is");
                    sw.WriteLine("Max Bukator");

                }
                using (StreamReader sr = File.OpenText(path)) {
                    string s;
                    while ((s = sr.ReadLine()) != null) {
                        Console.WriteLine(s);
                    }
                }
            } else {
                using (StreamReader sr = File.OpenText(path)) {
                    string s;
                    while ((s = sr.ReadLine()) != null) {
                        Console.WriteLine(s);
                    }
                }
            }
            Console.WriteLine($"\nPath to file: {path}");
            Console.WriteLine("\n");
        }
    }
}
