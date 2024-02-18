using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Pastel;
using TerminalPilot.Classes;
using TerminalPilot.OSSupport;
namespace TerminalPilot
{
    internal class Program
    {
        //TerminalPilot, pyrret 2023.
        static void Main(string[] args)
        {
            

            TerminalInstance instance = new TerminalInstance();
            Configuration config = new Configuration();
            OSVariables os = new OSVariables();
            
            if (ConfigManager.GetShell() == "")
            {
                AutomaticConfigConfigurer.StartupConfigure();
                instance.Shell = ConfigManager.GetShell();
                instance.ShellCommandArgument = ConfigManager.GetShellArgument();
            }
            //handle defualt shell


            instance.Workingdirectory = new DirectoryInfo(config.StartUpPath);
            Console.Clear();
            instance.alive = true;
            instance.name = "Terminal";
            Console.WriteLine("TerminalPilot".Pastel(Palletes.GetCurrentPallete(Enums.PalleteType.Large)) + ", Version " + Assembly.GetExecutingAssembly().GetName().Version);
            Console.WriteLine("From pyrret, Under MIT License.");
            Console.WriteLine();
            Console.WriteLine("Current Shell: " + instance.Shell.Pastel(Palletes.GetCurrentPallete(Enums.PalleteType.Small1)) + ". You can change it with " +  "'pilot shell'".Pastel(Palletes.GetCurrentPallete(Enums.PalleteType.Small1)));
            Parser.Parser parser = new Parser.Parser();
            parser.StartParse(instance);





        }
    }
}
