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
            //parse json file
            instance.Workingdirectory = new DirectoryInfo(config.StartUpPath);
            Console.Clear();
            instance.alive = true;
            instance.name = "Terminal";
            Console.WriteLine("TerminalPilot".Pastel(Color.Aqua) + ", Version " + Assembly.GetExecutingAssembly().GetName().Version);
            Console.WriteLine("From pyrret, Under MIT License.");
            Console.WriteLine();
            Console.WriteLine("visit https://tp.axell.me/update for the latest TerminalPilot!");
            Parser.Parser parser = new Parser.Parser();
            parser.StartParse(instance);





        }
    }
}
