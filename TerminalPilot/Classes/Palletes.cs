using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalPilot.Enums;
using TerminalPilot.Parser;

namespace TerminalPilot.Classes
{
    //base class
    public class Palletes {
        public class PalleteClass
        {
            public  Color LargeColor { get; set; }
            public  Color SmallColor1 { get; set; }
            public  Color SmallColor2 { get; set; }
            public Color SmallColor3 { get; set; }
        }


        public static Color GetCurrentPallete(PalleteType type)
        {
            if (ConfigManager.GetAny("Theme") == "")
            {
                ConfigManager.SetAny("Theme", "Neon Purple (Defualt)");
            }
            PalleteClass currentpallete = IncludedPalletes[ConfigManager.GetAny("Theme")];
            if (type == PalleteType.Large)
            {
                return currentpallete.LargeColor;
            }
            else if (type == PalleteType.Small1)
            {
                return currentpallete.SmallColor1;
            }
            else if (type == PalleteType.Small2)
            {
                return currentpallete.SmallColor2;
            }
            else if (type == PalleteType.Small3)
            {
                return currentpallete.SmallColor3;
            }
            return Color.White;
        }
        public static void SetCurrentPallete(string pallete)
        {
            ConfigManager.SetAny("Theme", pallete);
        }
        public static Dictionary<string, PalleteClass> IncludedPalletes = new Dictionary<string, PalleteClass>()
        {
            //neon purple (defualt and my favorite)
            {"Neon Purple (Defualt)", new PalleteClass()
            {
                LargeColor = Color.FromArgb(0, 101, 40, 247),
                SmallColor1 = Color.FromArgb(0, 160, 118, 249),
                SmallColor2 = Color.FromArgb(0, 215, 187, 245),
                SmallColor3 = Color.FromArgb(0, 237, 228, 255),
            }},
            // sunset pastel
            {"Sunset Pastel", new PalleteClass()
            {
                LargeColor = Color.FromArgb(0, 228, 147, 179),
                SmallColor1 = Color.FromArgb(0, 183, 132, 183),
                SmallColor2 = Color.FromArgb(0, 142, 122, 181),
                SmallColor3 = Color.FromArgb(0, 238, 165, 166)
            } },
            {"Ocean Blue", new PalleteClass()
            {
                LargeColor = Color.FromArgb(0, 182, 255, 250),
                SmallColor1 = Color.FromArgb(0, 128, 179, 255), 
                SmallColor2 = Color.FromArgb(0, 152, 228, 255), 
                SmallColor3 = Color.FromArgb(0, 104, 126, 255),
            }}
        };
    }
}
