using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalPilot.Classes;

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
