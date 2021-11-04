using System;
using System.IO;

namespace MinecraftModRenamingTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome! Minecraft Mod Renaming Tool v.0.1");
            
            string directory;
            if (args.Length != 0 && !string.IsNullOrEmpty(args[0]))
            {
                directory = args[0];
            }
            else
            {
                Console.WriteLine("Directory not passed in args.");
                Console.WriteLine("Please, paste full path to your minecraft mods directory >");
                directory = Console.ReadLine();
            }

            var workingPath = !string.IsNullOrEmpty(directory) ? directory :
                Path.Combine(Environment.CurrentDirectory, "mods");
            
            var r = new RenamingTool(workingPath).DoRenaming();
            
            Console.WriteLine(r.GetResult());

            Console.WriteLine("\n\t -> To exit, press Enter ⏎!");
            Console.WriteLine("\n\t -> To undo all renaming, press Backspace ⌫!");
            
            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.Enter)
            {
                r.UndoRenaming();
            }

            if (key == ConsoleKey.Enter)
            {
                Environment.Exit(0);
            }
        }
    }
}