using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalPilot.Classes;

namespace TerminalPilot.Parser
{
    public enum InputType
    {
        Program, //a program, example.exe
        File, //a file, example.txt.
        ProgramCommand, //a program thats typed as a built-in command, example parameter
        BuiltInCommand, //a command that is built-in to terminalpilot and that all command interpreters have, mkdir.
        TerminalPilotCommand, //a command unique to terminalpilot, pilot whatever
        PilotedProgramCommand, //a terminalpilotcommand that adds functionality to an existing built-in command
        CouldNotDetermine //self-explanitory, often result of user error
    }
    public class CommandInterpreter
    {
        public static InputType DetermineInputType(string startcommand, TerminalInstance instance)
        {
            OSVariables os = OSVariablesMethods.GetOSVariables();
            if (startcommand.Contains('.'))
            {
                //either program or fileasprogram
                if (os.ProgramFileTypes.Contains(startcommand.Split('.')[1]))
                {
                    return InputType.Program;
                } else
                {
                    return InputType.File;
                }
            } else
            {
                //terminalpilotcommand & 
            }
            return InputType.CouldNotDetermine;
        }
        public static void interprete(string command, TerminalInstance instance)
        {
            
            if (command != "")
            {
                string filename = command.Split(' ')[0];
                if (!File.Exists(@"C:\Windows\System32\" + filename))
                {
                    Console.WriteLine("'" + filename + "' is not a valid program nor is it a valid terminalpilot command.");
                    goto done;
                }
                ProcessStartInfo
                    commandinfo = new ProcessStartInfo();
                commandinfo.WorkingDirectory = instance.Workingdirectory.FullName;
                commandinfo.FileName = filename;
                commandinfo.UseShellExecute = false;
                if (command.Split(' ').Length > 1)
                {
                    commandinfo.Arguments = command.Split(' ')[1];
                }
                Process.Start(commandinfo);

            }

        done:;
        }
    }
}
