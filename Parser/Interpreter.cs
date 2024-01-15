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
        FileCommand, //a file thats typed as a command, example parameter
        BuiltInCommand, //a command that is built-in to terminalpilot and that all command interpreters have, mkdir.
        TerminalPilotCommand, //a command unique to terminalpilot, pilot whatever
        PilotedProgramCommand, //a terminalpilotcommand that adds functionality to an existing built-in command
        CouldNotDetermine //self-explanitory, often result of user error
    }
    public class Interpreter
    {
        public static void CommandInterpreter(string command)
        {

        }
        public static string[] Commands = new string[]
        {
            "echo",
            "cd",
            "mkdir"
        };
        public static string[] TerminalPilotCommands = new string[]
        {
            "pilot"
        };
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
                //terminalpilotcommand, builtincommand
                if (TerminalPilotCommands.Contains(startcommand))
                {
                    return InputType.TerminalPilotCommand;
                } else if (Commands.Contains(startcommand))
                {
                    return InputType.BuiltInCommand;
                } else 
                {
                    foreach (string path in os.PathVariable)
                    {
                        if (Directory.GetFiles(path + @"/", startcommand + ".*").Length > 0)
                        {
                            return InputType.FileCommand;
                        }
                    }
                    if (Directory.GetFiles(instance.Workingdirectory + @"/", startcommand + ".*").Length > 0)
                    {
                        return InputType.FileCommand;
                    }
                }

            } 
            return InputType.CouldNotDetermine;
        }
        public static void InterpreteCommand(string command, TerminalInstance instance)
        {
            if (command != "")
            {
                string filename = command.Split(' ')[0];
                InputType inputType = DetermineInputType(filename, instance);
                if (inputType != InputType.CouldNotDetermine)
                {
                    if (inputType == InputType.Program | inputType == InputType.File)
                    {
                        ProcessStartInfo startinfo = new ProcessStartInfo();
                        string disposableflag_firstbestfilepath;
                        if (File.Exists(instance.Workingdirectory + @"\" + filename));
                        {
                            disposableflag_firstbestfilepath = 
                        }
                        setvariable:
                        startinfo.WorkingDirectory = instance.Workingdirectory.FullName;
                        startinfo.FileName = filename;
                        startinfo.UseShellExecute = false;
                        if (command.Split(' ').Length > 1)
                        {
                            startinfo.Arguments = command.Split(' ')[1];
                        }
                        Process.Start(startinfo);
                    } else if (inputType == InputType.TerminalPilotCommand | inputType == InputType.BuiltInCommand)
                    {
                        CommandInterpreter(command);
                    }
                } else
                {
                    Console.WriteLine("'" + filename + "' is not a valid program nor is it a valid terminalpilot command.");
                    goto done;
                }
                

            }

        done:;
        }
    }
}
