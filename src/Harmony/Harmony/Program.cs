using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime;
using System.Runtime.InteropServices;

namespace Harmony
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("> -------------------------------------------------");
            Console.WriteLine("> Harmony: In-Game Black Ops III Alias Compiler UwU Edition");
            Console.WriteLine("> Developed by Scobalula");
            Console.WriteLine("> Version {0}", Assembly.GetExecutingAssembly().GetName().Version);
            Console.WriteLine("> Donate     @ https://paypal.com/Scobalula");
            Console.WriteLine("> Discord    @ https://discord.gg/fGVpV39");
            Console.WriteLine("> -------------------------------------------------");

            foreach(var arg in args)
                Instance.AliasFileNames.Add(Path.GetFileNameWithoutExtension(arg).ToLower());

            try
            {
                Console.WriteLine("> Initializing, please wait...");
                Instance.Initialize();

                Console.WriteLine("> Watching for changes....");

                while (true)
                {

                }
            }
            catch(Exception e)
            {
                Instance.PrintException("Error", e);
                Console.ReadLine();
            }
        }
    }
}
