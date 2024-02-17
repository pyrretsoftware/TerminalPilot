﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Pastel;
using TerminalPilot.Classes;
using TerminalPilot.Parser;
using TerminalPilot.Enums;
using TerminalPilot.OSSupport;
namespace TerminalPilot.Parser
{
    internal class Parser
    {
        public static void RemoveConsoleLine(int cursorline)
        {
            int oldccursorline = Console.CursorTop;
            int oldccursorleft = Console.CursorLeft;

            Console.SetCursorPosition(0, cursorline);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, oldccursorline);
        }

        public static void NewLine(TerminalInstance instance)
        {
            ParserConfig config = new ParserConfig();
            Console.WriteLine();
            string AfterUserPath = instance.Workingdirectory.FullName.Replace(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "");
            string UserPathAndBefore = instance.Workingdirectory.FullName.Replace(AfterUserPath, "");
            Console.Write(config.linefeed.Replace("{PATH}", UserPathAndBefore.Pastel(Palletes.GetCurrentPallete(PalleteType.Small1))) + AfterUserPath.Pastel(Palletes.GetCurrentPallete(PalleteType.Large)));
        }
        public static string GetNewLine(TerminalInstance instance)
        {
            ParserConfig config = new ParserConfig();
            string AfterUserPath = instance.Workingdirectory.FullName.Replace(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "");
            string UserPathAndBefore = instance.Workingdirectory.FullName.Replace(AfterUserPath, "");
            return config.linefeed.Replace("{USERPATH}", UserPathAndBefore.Pastel(Palletes.GetCurrentPallete(PalleteType.Large))).Replace("{AFTERUSERPATH}", AfterUserPath.Pastel(Palletes.GetCurrentPallete(PalleteType.Small1)));
        }
        public void StartParse(TerminalInstance instance)
        {
            Console.CancelKeyPress += (sender, e) => {
                e.Cancel = true; // prevent the process from terminating.
                if (Interpreter.RunningProcess != null)
                {
                    //Interpreter.RunningProcess.Kill();
                }
            };



            int _tempflag_messagetipline =
              default;
            void messagetip(string message)
            {
                if (_tempflag_messagetipline !=
                  default)
                {
                    RemoveConsoleLine(_tempflag_messagetipline);
                }
                int oldcursortop = Console.CursorTop;
                int oldcursorleft = Console.CursorLeft;
                Console.CursorLeft = 0;
                Console.CursorTop = Console.WindowTop + Console.WindowHeight - 2;
                _tempflag_messagetipline = Console.CursorTop;
                Console.WriteLine(message.Pastel(ConfigManager.GetColor(ColorSchemeColors.Special1)));
                Console.SetCursorPosition(oldcursorleft, oldcursortop);

            }
            Dictionary<char, string> keydict = new Dictionary<char, string>()
            {

            };
            Console.Title = instance.name;
            ParserConfig parseconfig = new ParserConfig();
            string currentword;
            int _tempflag_gobackwriteline = default;
            int _tempflag_deletelimit = 0;
            int _tempflag_deletelimity = 0;
            int _tempflag_cursorpos = 0;
            string _tempflag_direditorwinput = "";
            string _tempflag_commandwinput = "";
            string _tempflag_pathbeforedireditor = "";
            string _tempflag_linecursorleft = "";

            string WrittenUserInputLine = "";

            Console.WriteLine();
            string AfterUserPath = instance.Workingdirectory.FullName.Replace(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "");
            string UserPathAndBefore = instance.Workingdirectory.FullName.Replace(AfterUserPath, "");
            Console.Write(GetNewLine(instance));
            _tempflag_deletelimit = Console.CursorLeft;
            _tempflag_deletelimity = Console.CursorTop;

            void ReRenderUserInputLine()
            {
                // Calculate the number of lines
                int inputStartPos = _tempflag_deletelimit;
                int firstLineWidth = Console.WindowWidth - inputStartPos;
                int remainingLinesWidth = _tempflag_commandwinput.Length - firstLineWidth;
                int numberOfLines = 1 + (remainingLinesWidth > 0 ? (remainingLinesWidth / Console.WindowWidth) + ((remainingLinesWidth % Console.WindowWidth == 0) ? 0 : 1) : 0);

                // Store the original cursor position
                int originalCursorTop = Console.CursorTop;
                int originalCursorLeft = Console.CursorLeft;

                // Clear each line
                for (int i = 0; i < numberOfLines; i++)
                {
                    int currentLineStartPos = (i == 0) ? inputStartPos : 0;
                    Console.SetCursorPosition(currentLineStartPos, originalCursorTop - i);
                    Console.Write(new string(' ', Console.WindowWidth - currentLineStartPos));
                }

                // Reset the cursor position
                Console.SetCursorPosition(originalCursorLeft, originalCursorTop - numberOfLines + 1);

                // Write the new input
                Console.Write(_tempflag_commandwinput);
            }
            int GetCommandWInputPos()
            {
                int consoleWidth = Console.WindowWidth;

                int position = (Console.CursorTop - _tempflag_deletelimity) * consoleWidth + (Console.CursorLeft - _tempflag_deletelimit);

                if (position > _tempflag_commandwinput.Length)
                {
                    position = _tempflag_commandwinput.Length;
                }
                return position;
            }
            void InsertIntoUserInput(string insertstring)
            {
                //also support overflows
                if (Console.CursorLeft == Console.WindowWidth - 1)
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop++;
                }
                _tempflag_commandwinput = _tempflag_commandwinput.Insert(GetCommandWInputPos(), insertstring);
                ReRenderUserInputLine();
                Console.SetCursorPosition(Console.CursorLeft + insertstring.Length, Console.CursorTop);
            }


            void RemoveFromUserInput()
            {
                int position = GetCommandWInputPos();
                if (position > 0)
                {
                    _tempflag_commandwinput = _tempflag_commandwinput.Remove(position - 1, 1);
                    ReRenderUserInputLine();

                    // Adjust the cursor position
                    if (Console.CursorLeft > 0)
                    {
                        Console.CursorLeft--;
                    }
                    else if (Console.CursorTop > _tempflag_deletelimity)
                    {
                        Console.CursorLeft = Console.WindowWidth - 1;
                        Console.CursorTop--;
                    }
                }
            }
            _tempflag_deletelimity = Console.CursorTop;

            while (instance.alive == true)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                char key = consoleKeyInfo.KeyChar;
                //preserve the current cursor position
                _tempflag_cursorpos = Console.CursorLeft;

                #region HandleSpecialCharacters
                if (key == '\r')
                {
                    _tempflag_linecursorleft = "";
                    if (instance.InDirectoryEditor)
                    {
                        instance.InDirectoryEditor = false;
                        RemoveConsoleLine(_tempflag_messagetipline);
                        RemoveConsoleLine(Console.CursorTop);
                        _tempflag_direditorwinput = "";
                        instance.Workingdirectory = new DirectoryInfo(_tempflag_pathbeforedireditor);
                        _tempflag_pathbeforedireditor = "";
                        instance.InDirectoryEditor = false;
                        Console.Write("Exited directory editor without making any changes".Pastel(ConfigManager.GetColor(ColorSchemeColors.Special1)));
                    }
                    Console.WriteLine();
                    Interpreter.InterpreteCommand(_tempflag_commandwinput, instance);
                    _tempflag_commandwinput = "";
                    WrittenUserInputLine = "";
                    Console.Write(GetNewLine(instance));
                    _tempflag_deletelimit = Console.CursorLeft;
                    _tempflag_deletelimity = Console.CursorTop;

                }
                else if (key == '\x1b')
                {
                    if (Console.CursorLeft <= _tempflag_deletelimit && Console.CursorTop == _tempflag_deletelimity)
                    {
                        //jump back to directory editor
                        if (instance.InDirectoryEditor)
                        {
                            //go back one directory
                            if (instance.Workingdirectory.Parent != null)
                            {

                                instance.Workingdirectory = instance.Workingdirectory.Parent;
                                RemoveConsoleLine(Console.CursorTop);
                                Console.WriteLine();
                                AfterUserPath = instance.Workingdirectory.FullName.Replace(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "");
                                UserPathAndBefore = instance.Workingdirectory.FullName.Replace(AfterUserPath, "");
                                Console.Write(GetNewLine(instance).Replace(">", OsDirectoryManager.GetOsBackSpaceString()));
                                _tempflag_deletelimit = Console.CursorLeft;
                                _tempflag_deletelimity = Console.CursorTop;

                            }
                        }
                        else
                        {
                            int _disposableflag_cursorpos = Console.CursorLeft;
                            instance.InDirectoryEditor = true;
                            _tempflag_pathbeforedireditor = instance.Workingdirectory.FullName;
                            //enter dir editor
                            messagetip("You are in directory editor. to exit, press tab.");
                            RemoveConsoleLine(Console.CursorTop);
                            Console.Write(GetNewLine(instance).Replace(">", @""));
                            Console.CursorLeft = _disposableflag_cursorpos - 1;

                        }
                    }
                }
                else if (consoleKeyInfo.Key == ConsoleKey.Backspace)
                {
                    if (Console.CursorLeft > _tempflag_deletelimit | _tempflag_deletelimity != Console.CursorTop)
                    {
                        if (instance.InDirectoryEditor)
                        {
                            _tempflag_direditorwinput =
_tempflag_direditorwinput.Remove(_tempflag_direditorwinput.Length - 1, 1);
                        }
                        else if (_tempflag_commandwinput.Length > 0)
                        {
                            RemoveFromUserInput();
                        }
                        if (Console.CursorLeft == 0)
                        {
                            Console.CursorLeft = _tempflag_commandwinput.Length + _tempflag_deletelimit;
                            Console.CursorTop--;

                            _tempflag_deletelimity = Console.CursorTop;
                            //Console.SetCursorPosition(Console.CursorTop - 1, _tempflag_deletelimit);
                        }
                    }


                }

                else if (key == '\t')
                {
                    if (instance.InDirectoryEditor)
                    {
                        instance.InDirectoryEditor = false;
                        RemoveConsoleLine(_tempflag_messagetipline);
                        RemoveConsoleLine(Console.CursorTop);
                        DirectoryInfo dirinf = new DirectoryInfo(instance.Workingdirectory.FullName + OsDirectoryManager.GetOsSlash() + _tempflag_direditorwinput);
                        _tempflag_direditorwinput = "";
                        if (!Directory.Exists(dirinf.FullName))
                        {
                            Console.WriteLine("the directory you set does not exist, do you want to create it? (yes, no)");
                            if (Console.ReadLine() == "yes")
                            {
                                List<DirectoryInfo> parentlist = new List<DirectoryInfo>();
                                parentlist.Add(dirinf);
                                while (parentlist.Last().Parent != null)
                                {
                                    parentlist.Add(parentlist.Last().Parent);
                                }

                                int _disposableflag_dirscreated = 0;
                                foreach (DirectoryInfo dir in parentlist.AsEnumerable().Reverse())
                                {
                                    if (!Directory.Exists(dir.FullName))
                                    {
                                        Directory.CreateDirectory(dir.FullName);
                                        _disposableflag_dirscreated++;
                                    }
                                }

                                string _grammarflag_plural = " directory";
                                if (_disposableflag_dirscreated > 1)
                                {
                                    _grammarflag_plural = " directories";
                                }
                                Console.WriteLine("Successfully created " + _disposableflag_dirscreated.ToString().Pastel(Palletes.GetCurrentPallete(Enums.PalleteType.Small1)) + _grammarflag_plural);
                                instance.Workingdirectory = dirinf;
                            }
                        }
                        else
                        {
                            instance.Workingdirectory = dirinf;
                        }
                        Console.Write(GetNewLine(instance));
                        _tempflag_deletelimit = Console.CursorLeft;
                    }
                    else
                    {
                        Console.Write(key);
                    }
                }
                else if (consoleKeyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (Console.CursorLeft > _tempflag_deletelimit)
                    {
                        Console.CursorLeft--;
                        _tempflag_cursorpos = Console.CursorLeft;
                    }
                }
                else if (consoleKeyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (Console.CursorLeft < _tempflag_commandwinput.Length + _tempflag_deletelimit)
                    {
                        Console.CursorLeft++;
                        _tempflag_cursorpos = Console.CursorLeft;
                    }
                }
                else
                #endregion
                {



                    if (instance.InDirectoryEditor)
                    {
                        _tempflag_direditorwinput = _tempflag_direditorwinput + key;
                    }
                    else
                    {
                        InsertIntoUserInput(key.ToString());
                        if (_tempflag_deletelimity != Console.CursorTop && _tempflag_linecursorleft == "")
                        {
                            _tempflag_linecursorleft = _tempflag_commandwinput;
                        }
                    }
                    //overflow if there is no room left
                    if (Console.CursorLeft == Console.WindowWidth - 1)
                    {
                        Console.CursorLeft = 0;
                        Console.CursorTop++;
                    } else
                    {
                    Console.CursorLeft = _tempflag_cursorpos + 1;

                    }


                }
            }
        }
    }
}