using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalPilot.OSSupport
{
    public class UserShell
    {
        public string DisplayName = "";
        public string ShellName = "";
        public string Arguments = "";

    } 

    
    public class ShellManager
    {
        public static List<UserShell> IncludedShellsWindows = new List<UserShell>()
        { 
            new UserShell() { DisplayName = "Cmd (Command Prompt)", ShellName = "cmd.exe", Arguments = "/c \"{COMMAND}\"" },
            new UserShell() { DisplayName = "Powershell", ShellName = "pwsh.exe", Arguments = "/c \"{COMMAND}\"" },
        };
        public static List<UserShell> IncludedShellsLinux = new List<UserShell>()
        {
            new UserShell() { DisplayName = "Bash", ShellName = "/bin/bash", Arguments = "-c \"{COMMAND}\"" }
        };
        public static List<UserShell> IncludedShellsMac = new List<UserShell>()
        {
            new UserShell() { DisplayName = "Zsh", ShellName = "/bin/zsh", Arguments = "-c \"{COMMAND}\"" },
            new UserShell() { DisplayName = "Bash", ShellName = "/bin/bash", Arguments = "-c \"{COMMAND}\"" }
        };

        public static List<UserShell> GetShells()
        {
            if (Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                return IncludedShellsMac;
            }
            else
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                return IncludedShellsLinux;
            }
            else
            {
                return IncludedShellsWindows;
            }
        }
    }
}
