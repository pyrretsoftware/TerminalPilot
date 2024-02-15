using System;
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
            Console.Write(config.linefeed.Replace("{PATH}", instance.Workingdirectory.FullName));
        }
        public void StartParse(TerminalInstance instance)
        {
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
            string _tempflag_direditorwinput = "";
            string _tempflag_commandwinput = "";
            string _tempflag_pathbeforedireditor = "";
            string _tempflag_linecursorleft = "";
            Console.WriteLine();
            Console.Write(parseconfig.linefeed.Replace("{PATH}", instance.Workingdirectory.FullName));
            _tempflag_deletelimit = Console.CursorLeft;
            _tempflag_deletelimity = Console.CursorTop;
            while (instance.alive == true)
            {
                char key = Console.ReadKey(true).KeyChar;
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
                    Console.Write(parseconfig.linefeed.Replace("{PATH}", instance.Workingdirectory.FullName));
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
                                Console.Write(parseconfig.linefeed.Replace("{PATH}", instance.Workingdirectory.FullName).Replace(">", OsDirectoryManager.GetOsBackSpaceChar()));
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
                            Console.Write(parseconfig.linefeed.Replace("{PATH}", instance.Workingdirectory.FullName).Replace(">", @""));
                            Console.CursorLeft = _disposableflag_cursorpos - 1;

                        }
                    }
                }
                else if (key == OsDirectoryManager.GetOsBackSpaceChar())
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
                            _tempflag_commandwinput =
                                _tempflag_commandwinput.Remove(_tempflag_commandwinput.Length - 1, 1);
                        }
                        if (Console.CursorLeft == 0)
                        {
                            Console.CursorLeft = _tempflag_commandwinput.Length + _tempflag_deletelimit;
                            Console.CursorTop--;

                            _tempflag_deletelimity = Console.CursorTop;
                            //Console.SetCursorPosition(Console.CursorTop - 1, _tempflag_deletelimit);
                        }
                        else
                        {
                            Console.Write("\b \b");
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
                                Console.WriteLine("Successfully created " + _disposableflag_dirscreated.ToString().Pastel(Color.Aqua) + _grammarflag_plural);
                                instance.Workingdirectory = dirinf;
                            }
                        }
                        else
                        {
                            instance.Workingdirectory = dirinf;
                        }
                        Console.Write(parseconfig.linefeed.Replace("{PATH}", instance.Workingdirectory.FullName));
                        _tempflag_deletelimit = Console.CursorLeft;
                    }
                    else
                    {
                        Console.Write(key);
                    }
                }
                else
                {
                    string towrite;
                    if (keydict.TryGetValue(key, out towrite))
                    {
                        Console.Write(towrite);
                    }
                    else
                    {
                        if (instance.InDirectoryEditor)
                        {
                            _tempflag_direditorwinput = _tempflag_direditorwinput + key;
                        }
                        else
                        {
                            _tempflag_commandwinput = _tempflag_commandwinput + key;
                            if (_tempflag_deletelimity != Console.CursorTop && _tempflag_linecursorleft == "")
                            {
                                _tempflag_linecursorleft = _tempflag_commandwinput;
                            }
                        }
                        Console.Write(key);

                    }
                }
            }
        }
    }
}