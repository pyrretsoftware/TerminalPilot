using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalPilot.OSSupport
{
    public class OsDirectoryManager
    {
        public static string GetOsSlash()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                return "/";
            }
            else
            {
                return @"\";
            }
        }
        public static char GetOsBackSpaceChar()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                return '?';
            }
            else
            {
                return '\b';
            }
        }

        public static string GetOsBackSpaceString()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                return "/";
            }
            else
            {
                return @"\";
            }
        }
    } 
}
