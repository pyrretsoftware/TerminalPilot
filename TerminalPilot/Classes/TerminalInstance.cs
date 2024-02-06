using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalPilot.Classes;
namespace TerminalPilot.Classes
{
    public class TerminalInstance
    {
        public bool alive { get; set; }
        public string name { get; set; }
        public DirectoryInfo Workingdirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        public bool InDirectoryEditor = false;
        public string Shell = ConfigManager.GetShell()[0];
        public string ShellCommandArgument = ConfigManager.GetShell()[1];
    }
}
