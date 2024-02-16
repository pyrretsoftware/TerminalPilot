﻿using System;
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
        public string Tip = "";
        public string Example = "";
    }
    public class Interpreter
    {

        public static Command[] TerminalPilotCommands = new Command[] {
      new Command() {
          StartIdentifier = "serial",
            Tip = "Use the command 'pilot serial [Port] [Baudrate]' to open a terminal serial connection.",
            Example = "pilot serial COM10 9600"
        },
        new Command() {
          StartIdentifier = "auth",
            Tip = "Use the command 'pilot auth' to authenticate with your GitHub account, and get access to tons of cool features.",
            Example = "pilot auth"
        },
        new Command() {
          StartIdentifier = "config",
            Tip = "use the command 'pilot config [Mode] [Key] [Value]' to set or get a config parameter.",
            Example = "pilot config get Shell"
        }
    };
        public static InputType DetermineInputType(string startcommand, TerminalInstance instance)
        {
            if ("pilot" == startcommand.Split(' ')[0])
            {
                return InputType.TerminalPilotCommand;
            }
            else
            {
                return InputType.BuiltInCommand;
            }
        }
        public static Process RunningProcess;
        public static async Task<bool> InterpreteCommand(string command, TerminalInstance instance)
        {
            OSVariables os = OSVariablesMethods.GetOSVariables();
            InputType inputType = DetermineInputType(command, instance);

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
                    switch (_tempflag_firstbestflag)
                    {
                        case "config":
                            PilotCommands.pilotconfigcommand(command);
                            break;
                        case "serial":
                            PilotCommands.pilotserialcommand(command);
                            break;
                        case "auth":
                            PilotCommands.pilotauthcommand(command);
                            break;
                        default:
                            Console.WriteLine("That pilot command does not exists. if you want information on pilot commands, visit https://rb.gy/o1k4ns.");
                            break;
                    }
                _temp_done:;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(command))
                {
                    //run the command in the specifed shell
                    string shell = ConfigManager.GetShell();
                    string arguments = ConfigManager.GetShellArgument();

                    //start the shell process
                    ProcessStartInfo psi = new ProcessStartInfo(shell, arguments.Replace("{COMMAND}", command));
                    psi.UseShellExecute = false;
                    RunningProcess = Process.Start(psi);
                    RunningProcess.WaitForExit();
                    Console.WriteLine("");
                }
            }
        done:;
            return true;
        }
    }
}