using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalPilot.Classes
{
    public class OSVariables
    {
        public string[] PathVariable = Environment.GetEnvironmentVariable("PATH").Split(';');
        public string[] ProgramFileTypes; //.exe for windows
    }
    public class OSVariablesMethods
    {
        public static OSVariables GetOSVariables()
        {
            OSVariables ReturnValue = new OSVariables();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                ReturnValue.ProgramFileTypes = new string[] { "exe", "msi" };
            }
            return ReturnValue;
        }
    }
}
