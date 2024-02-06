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
using TerminalPilot.Commands;
namespace TerminalPilot.Parser
{
    public class Command
    {
        public string StartIdentifier = "";
        public string FunctionName = "";
        public string Tip = "";
        public string Example = "";
    }
    public class Interpreter
    {
        
        public static Command[] TerminalPilotCommands = new Command[]
        {
            new Command()
            {
                StartIdentifier = "serial",
                FunctionName = "pilotserialcommand",
                Tip = "Use the command '[pilot] serial [Port] [Baudrate]' to open a terminal serial connection.",
                Example = "pilot serial COM10 9600"
            }    
        };
        public static InputType DetermineInputType(string startcommand, TerminalInstance instance)
        {
                foreach (char illegalcharacter in Path.GetInvalidFileNameChars())
                {
                    if (startcommand.Contains(illegalcharacter)) {
                        return InputType.CouldNotDetermine;
                    }

                }

                //terminalpilotcommand, builtincommand, filecommand
                if ("pilot" == startcommand)
                {
                    return InputType.TerminalPilotCommand;
                } else /*if (Commands.Contains(startcommand))*/
                {
                    return InputType.BuiltInCommand;
                }/* else 
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
                }*/
            return InputType.CouldNotDetermine;
        }
        
        public static void InterpreteCommand(string command, TerminalInstance instance)
        {
            OSVariables os = OSVariablesMethods.GetOSVariables();
                string filename = command.Split(' ')[0];
                InputType inputType = DetermineInputType(filename, instance);
                if (inputType != InputType.CouldNotDetermine)
                {
                    if (inputType == InputType.TerminalPilotCommand)
                    {
                        if (command.Split(' ')[0] == "pilot")
                        {
                            if (command == "pilot")
                            {
                                PilotCommands.pilothelp();
                                goto _temp_done;
                            }
                            string _tempflag_firstbestflag = String.Empty;
                            foreach (Command icommand in TerminalPilotCommands)
                            {
                                if (icommand.StartIdentifier == command.Split(' ')[1])
                                {
                                    _tempflag_firstbestflag = icommand.StartIdentifier;
                                    break;
                                }
                            }
                            if (_tempflag_firstbestflag == "serial")
                            {

                            }
                            if (_tempflag_firstbestflag == "")
                            {
                                Console.WriteLine();
                                Console.WriteLine("That pilot command does not exists. if you want information on pilot commands, visit https://rb.gy/o1k4ns.");
                            };
                        _temp_done:;
                    } else
                    {

                    }
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
