using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pastel;
namespace TerminalPilot.Classes
{
    public class ParserConfig
    {
        public string linefeed = "{USERPATH}{AFTERUSERPATH}" + ">".Pastel(Palletes.GetCurrentPallete(Enums.PalleteType.Large));
    }
}
