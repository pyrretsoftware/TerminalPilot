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
                Console.WriteLine(message.Pastel(Color.CornflowerBlue));
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
            string _tempflag_direditorwinput = "";
            string _tempflag_commandwinput = "";
            Console.WriteLine();
            Console.Write(parseconfig.linefeed.Replace("{PATH}", instance.Workingdirectory.FullName));
            _tempflag_deletelimit = Console.CursorLeft;
            while (instance.alive == true)
            {
                char key = Console.ReadKey(true).KeyChar;
                if (key == '\r')
                {
                    if (instance.InDirectoryEditor)
                    {
                        instance.InDirectoryEditor = false;
                        RemoveConsoleLine(_tempflag_messagetipline);
                        RemoveConsoleLine(Console.CursorTop);
                        DirectoryInfo dirinf = new DirectoryInfo(instance.Workingdirectory.FullName + @"\" + _tempflag_direditorwinput);
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
                    }
                    Console.WriteLine();
                    Interpreter.interprete(_tempflag_commandwinput, instance);
                    _tempflag_commandwinput = "";
                    Console.Write(parseconfig.linefeed.Replace("{PATH}", instance.Workingdirectory.FullName));
                    _tempflag_deletelimit = Console.CursorLeft;
                }
                else if (key == '\b')
                {
                    if (Console.CursorLeft <= _tempflag_deletelimit)
                    {
                        //jump back to directory editor
                        if (instance.InDirectoryEditor)
                        {
                            //go back one directory
                            instance.Workingdirectory = instance.Workingdirectory.Parent;
                            RemoveConsoleLine(Console.CursorTop);
                            Console.Write(parseconfig.linefeed.Replace("{PATH}", instance.Workingdirectory.FullName).Replace(">", @"\"));
                            _tempflag_deletelimit = Console.CursorLeft;
                        }
                        else
                        {
                            int _disposableflag_cursorpos = Console.CursorLeft;
                            instance.InDirectoryEditor = true;
                            //enter dir editor
                            messagetip("You are in directory editor. to exit, press tab.");
                            RemoveConsoleLine(Console.CursorTop);
                            Console.Write(parseconfig.linefeed.Replace("{PATH}", instance.Workingdirectory.FullName).Replace(">", @""));
                            Console.CursorLeft = _disposableflag_cursorpos - 1;
                        }
                    }
                    else
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
                        Console.Write("\b \b");
                    }
                }
                else if (key == '\t')
                {
                    if (instance.InDirectoryEditor)
                    {
                        instance.InDirectoryEditor = false;
                        RemoveConsoleLine(_tempflag_messagetipline);
                        RemoveConsoleLine(Console.CursorTop);
                        DirectoryInfo dirinf = new DirectoryInfo(instance.Workingdirectory.FullName + @"\" + _tempflag_direditorwinput);
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
                        }
                        Console.Write(key);

                    }
                }
            }
        }
    }
}