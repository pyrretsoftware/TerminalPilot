namespace TerminalPilot.Enums
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
}