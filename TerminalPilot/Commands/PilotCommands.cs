using Pastel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalPilot.Classes;
using TerminalPilot.OSSupport;
using TerminalPilot.Enums;
namespace TerminalPilot.Commands
{
    public class PilotCommands
    {
        public static void pilothelp()
        {
            Console.WriteLine("Please provide a command. for help on terminalpilot commands, visit https://rb.gy/o1k4ns");
        }
        public static void pilotserialcommand(string command)
        {
            Console.WriteLine("This command is not yet implemented.");
        }
        public static void pilotshellcommand(string command)
        {
            //display a menu with all available shells
            int OptionCount = 1;
            Console.WriteLine("Tip: if you want to use a custom shell, use the 'pilot config set Shell' command.".Pastel(Palletes.GetCurrentPallete(PalleteType.Small1)));
            Console.WriteLine();
            Console.WriteLine("Available shells:".Pastel(Palletes.GetCurrentPallete(PalleteType.Small1)));
            foreach (UserShell shell in ShellManager.GetShells())
            {
                string finished = OptionCount + ". " + shell.DisplayName;
                Console.WriteLine(finished.Pastel(Palletes.GetCurrentPallete(PalleteType.Small2)));
                OptionCount++;
            }
            Console.WriteLine("Please select a shell by typing its number:".Pastel(Palletes.GetCurrentPallete(PalleteType.Large)));
            string selectedshell = Console.ReadLine();
            if (int.TryParse(selectedshell, out int result))
            {
                if (result > 0 && result < OptionCount)
                {
                    ConfigManager.SetAny("Shell", (ShellManager.GetShells()[result - 1].ShellName));
                    ConfigManager.SetAny("ShellArgument", (ShellManager.GetShells()[result - 1].Arguments));
                    Console.WriteLine("Set shell to " + ShellManager.GetShells()[result - 1].DisplayName);
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
            
        }
        public static void pilotreloadcommand(string command, TerminalInstance instance)
        {
            
        }
        public static void pilotconfigcommand(string config)
        {
            if (config == "pilot config")
            {
                Console.WriteLine("Please provide an action command. for help on terminalpilot commands, visit https://rb.gy/o1k4ns");
            }
            else
            {
                if (config.Split(' ')[2] == "set")
                {
                    if (config.Split(' ').Length < 5)
                    {
                        Console.WriteLine("Please provide a value to set.");
                        return;
                    }
                    ConfigManager.SetAny(config.Split(' ')[3], config.Split(' ')[4]);
                    Console.WriteLine("Set '" + config.Split(' ')[3] + "' to '" + config.Split(' ')[4] + "'");
                } else if (config.Split(' ')[2] == "get")
                {
                    if (config.Split(' ').Length < 4)
                    {
                        Console.WriteLine("Please provide a value to get.");
                        return;
                    }
                    Console.WriteLine(ConfigManager.GetAny(config.Split(' ')[3]));
                }
            }
        }
        public static void pilotauthcommand(string command)
        {
            Classes.AuthMethods.InitAuth(command);
        }
    }
}
