using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CustomDictionary;

namespace ConsoleApp1 {
    class Program
    {
        static void Main(string[] args)
        {
            CustomDictionary<string, int> dict = new CustomDictionary<string, int>();

            dict.Add("Pasha", 1);
            dict.Add("Serega", 2);

            foreach (var item in dict)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }

            dict.Remove("Pasha");
            
            Console.WriteLine();
            foreach (var item in dict)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }
            
            dict.Add("Pasha", 1);

            dict.Clear();
            
            Console.WriteLine();
            foreach (var item in dict)
            {
                Console.WriteLine(item.Key.ToString() + " " + item.Value.ToString());
            }
            
            dict.Add("Pasha", 1);
            dict.Add("Serega", 2);
            dict.Add("Levan", 3);
            dict.Add("Danil", 4);
            
            Console.WriteLine();
            foreach (var item in dict)
            {
                Console.WriteLine(item.Key.ToString() + " " + item.Value.ToString());
            }

            Console.WriteLine(dict["Pasha"]);
            Console.WriteLine(dict["Serega"]);
            Console.WriteLine(dict["Levan"]);
            Console.WriteLine(dict.ContainsKey("Daniil"));
            Console.WriteLine(dict.ContainsKey("Danil"));

            dict["Rauf"] = 10;
            
            Console.WriteLine(dict.ContainsKey("Rauf"));
        }
    }
}