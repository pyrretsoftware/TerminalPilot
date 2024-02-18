using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Configuration;
using TerminalPilot.Enums;

namespace TerminalPilot.Classes
{
    public class ConfigManager
    {
        
        //color palletes
 

        //startup

        public static void SetShell(string shell, string shellargument)
        {
            Properties.Settings.Default["Shell"] = shell;
            Properties.Settings.Default["ShellArgument"] = shellargument;

        }
        public static string GetShell()
        {
            return (string)Properties.Settings.Default["Shell"];
        }
        public static string GetAny(string key)
        {
            return (string)Properties.Settings.Default[key];
        }
        public static void SetAny(string key, string value)
        {
            Properties.Settings.Default[key] = value;
        }
        public static string GetShellArgument()
        {
            return (string)Properties.Settings.Default["ShellArgument"];
        }
        //set the user token
        public static void SetUserToken(string usertoken)
        {
            Properties.Settings.Default["UserToken"] = usertoken;
        }
        //get the user token
        public static string GetUserToken()
        {
            return (string)Properties.Settings.Default["UserToken"];
        }
    }
}
