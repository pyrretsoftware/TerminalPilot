using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TerminalPilot.Classes;

namespace TerminalPilot.OSSupport
{
    public class AutomaticConfigConfigurer
    {
        public static void StartupConfigure()
        {
            //check if were on a unix system
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                //options are 
                // /bin/bash
                // custom
                ConfigManager.SetShell("/bin/bash", "-c \"{COMMAND}\"");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                //options are 
                // cmd
                // powershell
                // custom
                ConfigManager.SetShell("cmd.exe", "/c \"{COMMAND}\"");
            } else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                //options are 
                // zsh
                // /bin/bash
                // custom
                ConfigManager.SetShell("/bin/zsh", "-c \"{COMMAND}\"");
            }
        }
    }
}
