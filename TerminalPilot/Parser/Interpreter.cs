using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalPilot.Classes;
using TerminalPilot.Enums;
using TerminalPilot.Flags;
namespace TerminalPilot.Parser
{
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
            OSVariables os = OSVariablesMethods.GetOSVariables();
                string filename = command.Split(' ')[0];
                InputType inputType = DetermineInputType(filename, instance);
                if (inputType != InputType.CouldNotDetermine)
                {
                    if (inputType == InputType.Program | inputType == InputType.File)
                    {
                        string disposableflag_firstbestfilepath = FlagHandler.GetFlagString(FlagType.DisposableFlag);
                        //note that the file extension is included here
                        ProcessStartInfo startinfo = new ProcessStartInfo();

                        if (File.Exists(instance.Workingdirectory + @"\" + filename))
                        {
                            disposableflag_firstbestfilepath = instance.Workingdirectory + @"\" + filename;
                        } 
                        else
                        {
                            foreach (string path in os.PathVariable)
                            {
                                if (File.Exists(path + @"\" + filename))
                                {
                                    disposableflag_firstbestfilepath = path + @"\" + filename;
                                    break;
                                }
                            }
                        }
                        
                        startinfo.WorkingDirectory = instance.Workingdirectory.FullName;
                        startinfo.FileName = disposableflag_firstbestfilepath;
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
                if (!string.IsNullOrEmpty(command))
                {
                    Console.WriteLine("'" + filename + "' is not a valid program nor is it a valid terminalpilot command.");
                    goto done;
                }
                }
        done:;
        }
    }
}
