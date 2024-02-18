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
        public static Color GetColor(ColorSchemeColors color)
        {
            string configcolor = default;
            if (color == ColorSchemeColors.Standard)
            {
                configcolor = (string?)Properties.Settings.Default["StandardTextColor"];
            } else if (color == ColorSchemeColors.Background)
            {
                configcolor = (string?)Properties.Settings.Default["BackgroundColor"];
            }
            else if (color == ColorSchemeColors.Special1)
            {
                configcolor = (string?)Properties.Settings.Default["SpecialColor1"];
            }
            else if (color == ColorSchemeColors.Special2)
            {
                configcolor = (string?)Properties.Settings.Default["SpecialColor2"];
            }
            else if (color == ColorSchemeColors.Special3)
            {
                configcolor = (string?)Properties.Settings.Default["SpecialColor3"];
            }
            return Color.FromArgb(0, Int32.Parse(configcolor.Split(',')[0]), Int32.Parse(configcolor.Split(',')[1]), Int32.Parse(configcolor.Split(',')[2]));
        }
        public static void SetColor(ColorSchemeColors color, Color finalcolor)
        {
            string colortoset = finalcolor.R.ToString() + "," + finalcolor.G.ToString() + "," + finalcolor.B.ToString() + ",";
            if (color == ColorSchemeColors.Standard)
            {
                Properties.Settings.Default["StandardTextColor"] = colortoset;
            }
            else if (color == ColorSchemeColors.Background)
            {
                Properties.Settings.Default["BackgroundColor"] = colortoset;
            }
            else if (color == ColorSchemeColors.Special1)
            {
                Properties.Settings.Default["SpecialColor1"] = colortoset;
            }
            else if (color == ColorSchemeColors.Special2)
            {
               Properties.Settings.Default["SpecialColor2"] = colortoset;
            }
            else if (color == ColorSchemeColors.Special3)
            {
                Properties.Settings.Default["SpecialColor3"] = colortoset;
            }
        }

        //startup

        public static void StartUp()
        {
            SetColor(ColorSchemeColors.Standard, Color.White);
            SetColor(ColorSchemeColors.Special1, Color.Aqua);
            SetColor(ColorSchemeColors.Special2, Color.Purple);
        }
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
