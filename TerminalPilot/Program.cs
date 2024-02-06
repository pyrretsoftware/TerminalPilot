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
            
            //handle defualt shell
           

            instance.Workingdirectory = new DirectoryInfo(config.StartUpPath);
            ConfigManager.StartUp();
            Console.Clear();
            instance.alive = true;
            instance.name = "Terminal";
            Console.WriteLine("TerminalPilot".Pastel(Color.Aqua) + ", Version " + Assembly.GetExecutingAssembly().GetName().Version);
            Console.WriteLine("From pyrret, Under MIT License.");
            Console.WriteLine();
            Console.WriteLine("Link your github account with the command " + "pilot".Pastel(Color.DarkViolet) + " link for extra features!");
            Parser.Parser parser = new Parser.Parser();
            parser.StartParse(instance);





        }
    }
}
