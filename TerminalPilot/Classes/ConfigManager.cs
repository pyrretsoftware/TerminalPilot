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
                configcolor = ConfigurationManager.AppSettings["StandardTextColor"];
            } else if (color == ColorSchemeColors.Background)
            {
                configcolor = ConfigurationManager.AppSettings["BackgroundColor"];
            }
            else if (color == ColorSchemeColors.Special1)
            {
                configcolor = ConfigurationManager.AppSettings["SpecialColor1"];
            }
            else if (color == ColorSchemeColors.Special2)
            {
                configcolor = ConfigurationManager.AppSettings["SpecialColor2"];
            }
            else if (color == ColorSchemeColors.Special3)
            {
                configcolor = ConfigurationManager.AppSettings["SpecialColor3"];
            }
            return Color.FromArgb(0, Int32.Parse(configcolor.Split(',')[0]), Int32.Parse(configcolor.Split(',')[1]), Int32.Parse(configcolor.Split(',')[2]));
        }
        public static void SetColor(ColorSchemeColors color, Color finalcolor)
        {
            string colortoset = finalcolor.R.ToString() + "," + finalcolor.G.ToString() + "," + finalcolor.B.ToString() + ",";
            if (color == ColorSchemeColors.Standard)
            {
                ConfigurationManager.AppSettings["StandardTextColor"] = colortoset;
            }
            else if (color == ColorSchemeColors.Background)
            {
                ConfigurationManager.AppSettings["BackgroundColor"] = colortoset;
            }
            else if (color == ColorSchemeColors.Special1)
            {
                ConfigurationManager.AppSettings["SpecialColor1"] = colortoset;
            }
            else if (color == ColorSchemeColors.Special2)
            {
               ConfigurationManager.AppSettings["SpecialColor2"] = colortoset;
            }
            else if (color == ColorSchemeColors.Special3)
            {
                ConfigurationManager.AppSettings["SpecialColor3"] = colortoset;
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
            ConfigurationManager.AppSettings["Shell"] = shell;
            ConfigurationManager.AppSettings["ShellArgument"] = shellargument;

        }
        public static string[] GetShell()
        {
            string[] shelldata = new string[1];
            shelldata.Append(ConfigurationManager.AppSettings["Shell"]);
            shelldata.Append(ConfigurationManager.AppSettings["ShellArgument"]);
            return shelldata;

        }
    }
}
