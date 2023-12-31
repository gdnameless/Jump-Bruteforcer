﻿using System.Globalization;
using System.Text.RegularExpressions;

namespace Jump_Bruteforcer
{
    public class Parser
    {
        public static Map Parse(string Text) => Parse(".jmap", Text);

        public static Map Parse(string Extension, string Text)
        {
            Extension = Extension.ToLower();
            List<Object> objects = new List<Object>();
            if (Extension == ".cmap" || Extension == ".jmap")
            {
                int datalinenum = 5;
                string[] args = Text.Split('\n')[datalinenum - 1].Trim().Split(' ');

                for (int i = 0; i < args.Length; i += 3)
                {
                    (int x, int y, int objectid) = (int.Parse(args[i]), int.Parse(args[i + 1]), int.Parse(args[i + 2]));
                    ObjectType o = Enum.IsDefined(typeof(ObjectType), objectid) ? (ObjectType)objectid : ObjectType.Unknown;
                    objects.Add(new(x, y, o, i / 3));
                }
            }
            else //Extension == ".txt"
            {
                const int MinParams = 10;
                const NumberStyles Style = NumberStyles.Float;

                static double ParseDouble(string s) => double.Parse(s, Style, CultureInfo.InvariantCulture);

                string[] Lines = Text.Split('\n');

                for (int i = 0; i < Lines.Length; i++)
                {
                    if (Lines[i].Trim() == string.Empty)
                    {
                        continue;
                    }
                    string[] Parameters = Lines[i].Split(',');
                    if (Parameters.Length < MinParams)
                    {
                        throw new Exception($"Expected {MinParams} parameters, found {Parameters.Length} (Line {i + 1})");
                    }
                    string name = Parameters[0].ToLower();
                    ObjectType o = ObjectNames.GetValueOrDefault(name);
                    int x = (int)Math.Round(ParseDouble(Parameters[1]));
                    int y = (int)Math.Round(ParseDouble(Parameters[2]));

                    objects.Add(new(x, y, o, i));
                }
            }
            return new Map(objects);

        }

        static readonly Dictionary<string, ObjectType> ObjectNames = new()
        {
            {"block", ObjectType.Block },
            {"blockblock", ObjectType.Block },
            {"object1125", ObjectType.Block },
            {"object1126", ObjectType.Block },
            {"object767", ObjectType.Block },
            {"object856", ObjectType.Block },
            {"object857", ObjectType.Block },
            {"object858", ObjectType.Block },
            {"miniblock", ObjectType.MiniBlock},
            {"miniblockblock", ObjectType.MiniBlock },
            {"damageblock", ObjectType.KillerBlock},
            {"object510", ObjectType.KillerBlock },
            {"playerKiller", ObjectType.KillerBlock },
            {"apple", ObjectType.Apple},
            {"cherry", ObjectType.Apple},
            {"deliciousfruit", ObjectType.Apple},
            {"object1124", ObjectType.Apple },
            {"object1140", ObjectType.Apple },
            {"object1159", ObjectType.Apple },
            {"object511", ObjectType.Apple },
            {"objectazurecherry222", ObjectType.Apple },
            {"objectbeigecherry222", ObjectType.Apple },
            {"objectgreencherry222", ObjectType.Apple },
            {"objectgreycherry222", ObjectType.Apple },
            {"objectrottencherry222", ObjectType.Apple },
            {"objectstrangecherry", ObjectType.Apple },
            {"objectwhitecherry222", ObjectType.Apple },
            {"yellowcherry", ObjectType.Apple },
            {"deliciousfruit222", ObjectType.Apple },
            {"objvioletcherry222", ObjectType.Apple },
            {"warp", ObjectType.Warp},
            {"warpstart", ObjectType.Warp},
            {"object513", ObjectType.Warp },
            {"object545", ObjectType.Warp },
            {"warpcount", ObjectType.Warp },
            {"warpstartstart", ObjectType.Warp },
            {"warptext", ObjectType.Warp },
            {"warpwarp", ObjectType.Warp },
            {"playerstart", ObjectType.PlayerStart},
            {"playerstartstart", ObjectType.PlayerStart},
            {"water", ObjectType.Water1}, // yuuutu
            {"water1", ObjectType.Water1}, // renex
            {"objwater", ObjectType.Water1 },
            {"objwater1111111", ObjectType.Water1 },
            {"objwater2222222", ObjectType.Water2},
            {"water2", ObjectType.Water2}, // renex
            {"objWater2", ObjectType.Water2 },
            {"water3", ObjectType.Water3}, // renex
            {"spikedown", ObjectType.SpikeDown},
            {"object1020", ObjectType.SpikeDown },
            {"object1028", ObjectType.SpikeDown },
            {"object1034", ObjectType.SpikeDown },
            {"object1044", ObjectType.SpikeDown },
            {"object1056", ObjectType.SpikeDown },
            {"object1070", ObjectType.SpikeDown },
            {"object1078", ObjectType.SpikeDown },
            {"object1087", ObjectType.SpikeDown },
            {"object1103", ObjectType.SpikeDown },
            {"object1113", ObjectType.SpikeDown },
            {"object1120", ObjectType.SpikeDown },
            {"object1135", ObjectType.SpikeDown },
            {"object1138", ObjectType.SpikeDown },
            {"object1155", ObjectType.SpikeDown },
            {"object517", ObjectType.SpikeDown },
            {"object521", ObjectType.SpikeDown },
            {"object547", ObjectType.SpikeDown },
            {"object574", ObjectType.SpikeDown },
            {"object716", ObjectType.SpikeDown },
            {"object723", ObjectType.SpikeDown },
            {"object733", ObjectType.SpikeDown },
            {"object742", ObjectType.SpikeDown },
            {"object748", ObjectType.SpikeDown },
            {"object754", ObjectType.SpikeDown },
            {"object761", ObjectType.SpikeDown },
            {"object770", ObjectType.SpikeDown },
            {"object784", ObjectType.SpikeDown },
            {"object822", ObjectType.SpikeDown },
            {"object832", ObjectType.SpikeDown },
            {"object838", ObjectType.SpikeDown },
            {"object850", ObjectType.SpikeDown },
            {"object860", ObjectType.SpikeDown },
            {"object888", ObjectType.SpikeDown },
            {"object897", ObjectType.SpikeDown },
            {"object905", ObjectType.SpikeDown },
            {"object909", ObjectType.SpikeDown },
            {"object914", ObjectType.SpikeDown },
            {"object920", ObjectType.SpikeDown },
            {"object942", ObjectType.SpikeDown },
            {"spikedowndown", ObjectType.SpikeDown },
            {"spikeleft", ObjectType.SpikeLeft},
            {"object1021", ObjectType.SpikeLeft },
            {"object1035", ObjectType.SpikeLeft },
            {"object1045", ObjectType.SpikeLeft },
            {"object1057", ObjectType.SpikeLeft },
            {"object1071", ObjectType.SpikeLeft },
            {"object1079", ObjectType.SpikeLeft },
            {"object1088", ObjectType.SpikeLeft },
            {"object1097", ObjectType.SpikeLeft },
            {"object1104", ObjectType.SpikeLeft },
            {"object1114", ObjectType.SpikeLeft },
            {"object1121", ObjectType.SpikeLeft },
            {"object1136", ObjectType.SpikeLeft },
            {"object1139", ObjectType.SpikeLeft },
            {"object1156", ObjectType.SpikeLeft },
            {"object518", ObjectType.SpikeLeft },
            {"object522", ObjectType.SpikeLeft },
            {"object546", ObjectType.SpikeLeft },
            {"object573", ObjectType.SpikeLeft },
            {"object724", ObjectType.SpikeLeft },
            {"object727", ObjectType.SpikeLeft },
            {"object735", ObjectType.SpikeLeft },
            {"object743", ObjectType.SpikeLeft },
            {"object749", ObjectType.SpikeLeft },
            {"object755", ObjectType.SpikeLeft },
            {"object762", ObjectType.SpikeLeft },
            {"object771", ObjectType.SpikeLeft },
            {"object785", ObjectType.SpikeLeft },
            {"object823", ObjectType.SpikeLeft },
            {"object833", ObjectType.SpikeLeft },
            {"object839", ObjectType.SpikeLeft },
            {"object851", ObjectType.SpikeLeft },
            {"object862", ObjectType.SpikeLeft },
            {"object889", ObjectType.SpikeLeft },
            {"object898", ObjectType.SpikeLeft },
            {"object906", ObjectType.SpikeLeft },
            {"object910", ObjectType.SpikeLeft },
            {"object915", ObjectType.SpikeLeft },
            {"object921", ObjectType.SpikeLeft },
            {"object943", ObjectType.SpikeLeft },
            {"spikeleftleft", ObjectType.SpikeLeft },
            {"spikeright", ObjectType.SpikeRight},
            {"object1022", ObjectType.SpikeRight },
            {"object1033", ObjectType.SpikeRight },
            {"object1043", ObjectType.SpikeRight },
            {"object1055", ObjectType.SpikeRight },
            {"object1069", ObjectType.SpikeRight },
            {"object1077", ObjectType.SpikeRight },
            {"object1086", ObjectType.SpikeRight },
            {"object1096", ObjectType.SpikeRight },
            {"object1102", ObjectType.SpikeRight },
            {"object1112", ObjectType.SpikeRight },
            {"object1119", ObjectType.SpikeRight },
            {"object1134", ObjectType.SpikeRight },
            {"object1137", ObjectType.SpikeRight },
            {"object1154", ObjectType.SpikeRight },
            {"object516", ObjectType.SpikeRight },
            {"object524", ObjectType.SpikeRight },
            {"object548", ObjectType.SpikeRight },
            {"object572", ObjectType.SpikeRight },
            {"object717", ObjectType.SpikeRight },
            {"object722", ObjectType.SpikeRight },
            {"object728", ObjectType.SpikeRight },
            {"object734", ObjectType.SpikeRight },
            {"object741", ObjectType.SpikeRight },
            {"object753", ObjectType.SpikeRight },
            {"object760", ObjectType.SpikeRight },
            {"object769", ObjectType.SpikeRight },
            {"object783", ObjectType.SpikeRight },
            {"object821", ObjectType.SpikeRight },
            {"object831", ObjectType.SpikeRight },
            {"object837", ObjectType.SpikeRight },
            {"object849", ObjectType.SpikeRight },
            {"object861", ObjectType.SpikeRight },
            {"object887", ObjectType.SpikeRight },
            {"object896", ObjectType.SpikeRight },
            {"object904", ObjectType.SpikeRight },
            {"object908", ObjectType.SpikeRight },
            {"object913", ObjectType.SpikeRight },
            {"object919", ObjectType.SpikeRight },
            {"object941", ObjectType.SpikeRight },
            {"spikerightright", ObjectType.SpikeRight },
            {"spikeup", ObjectType.SpikeUp},
            {"newspike", ObjectType.SpikeUp },
            {"object1019", ObjectType.SpikeUp },
            {"object1023", ObjectType.SpikeUp },
            {"object1032", ObjectType.SpikeUp },
            {"object1042", ObjectType.SpikeUp },
            {"object1054", ObjectType.SpikeUp },
            {"object1068", ObjectType.SpikeUp },
            {"object1076", ObjectType.SpikeUp },
            {"object1085", ObjectType.SpikeUp },
            {"object1095", ObjectType.SpikeUp },
            {"object1101", ObjectType.SpikeUp },
            {"object1111", ObjectType.SpikeUp },
            {"object1118", ObjectType.SpikeUp },
            {"object1127", ObjectType.SpikeUp },
            {"object1133", ObjectType.SpikeUp },
            {"object1153", ObjectType.SpikeUp },
            {"object515", ObjectType.SpikeUp },
            {"object523", ObjectType.SpikeUp },
            {"object525", ObjectType.SpikeUp },
            {"object571", ObjectType.SpikeUp },
            {"object719", ObjectType.SpikeUp },
            {"object721", ObjectType.SpikeUp },
            {"object726", ObjectType.SpikeUp },
            {"object732", ObjectType.SpikeUp },
            {"object740", ObjectType.SpikeUp },
            {"object747", ObjectType.SpikeUp },
            {"object752", ObjectType.SpikeUp },
            {"object763", ObjectType.SpikeUp },
            {"object768", ObjectType.SpikeUp },
            {"object820", ObjectType.SpikeUp },
            {"object834", ObjectType.SpikeUp },
            {"object836", ObjectType.SpikeUp },
            {"object848", ObjectType.SpikeUp },
            {"object859", ObjectType.SpikeUp },
            {"object886", ObjectType.SpikeUp },
            {"object895", ObjectType.SpikeUp },
            {"object903", ObjectType.SpikeUp },
            {"object907", ObjectType.SpikeUp },
            {"object912", ObjectType.SpikeUp },
            {"object918", ObjectType.SpikeUp },
            {"object940", ObjectType.SpikeUp },
            {"playerkillerkiller", ObjectType.SpikeUp },
            {"spikeupup", ObjectType.SpikeUp },
            {"spiked", ObjectType.SpikeDown},
            {"spikel", ObjectType.SpikeLeft},
            {"spiker", ObjectType.SpikeRight},
            {"spikeu", ObjectType.SpikeUp},
            {"minispikedown", ObjectType.MiniSpikeDown},
            {"minispikedowndown", ObjectType.MiniSpikeDown },
            {"object1026", ObjectType.MiniSpikeDown },
            {"object1047", ObjectType.MiniSpikeDown },
            {"object1051", ObjectType.MiniSpikeDown },
            {"object1060", ObjectType.MiniSpikeDown },
            {"object1065", ObjectType.MiniSpikeDown },
            {"object1074", ObjectType.MiniSpikeDown },
            {"object1082", ObjectType.MiniSpikeDown },
            {"object1091", ObjectType.MiniSpikeDown },
            {"object1099", ObjectType.MiniSpikeDown },
            {"object1107", ObjectType.MiniSpikeDown },
            {"object1122", ObjectType.MiniSpikeDown },
            {"object1130", ObjectType.MiniSpikeDown },
            {"object1143", ObjectType.MiniSpikeDown },
            {"object1146", ObjectType.MiniSpikeDown },
            {"object1150", ObjectType.MiniSpikeDown },
            {"object1168", ObjectType.MiniSpikeDown },
            {"object725", ObjectType.MiniSpikeDown },
            {"object746", ObjectType.MiniSpikeDown },
            {"object750", ObjectType.MiniSpikeDown },
            {"object757", ObjectType.MiniSpikeDown },
            {"object773", ObjectType.MiniSpikeDown },
            {"object825", ObjectType.MiniSpikeDown },
            {"object842", ObjectType.MiniSpikeDown },
            {"object845", ObjectType.MiniSpikeDown },
            {"object854", ObjectType.MiniSpikeDown },
            {"object866", ObjectType.MiniSpikeDown },
            {"object892", ObjectType.MiniSpikeDown },
            {"object901", ObjectType.MiniSpikeDown },
            {"object946", ObjectType.MiniSpikeDown },
            {"object948", ObjectType.MiniSpikeDown },
            {"prismmini", ObjectType.MiniSpikeDown },
            {"whitemini", ObjectType.MiniSpikeDown },
            {"minispikeleft", ObjectType.MiniSpikeLeft},
            {"minispikeleftleft", ObjectType.MiniSpikeLeft },
            {"object1027", ObjectType.MiniSpikeLeft },
            {"object1052", ObjectType.MiniSpikeLeft },
            {"object1061", ObjectType.MiniSpikeLeft },
            {"object1066", ObjectType.MiniSpikeLeft },
            {"object1075", ObjectType.MiniSpikeLeft },
            {"object1083", ObjectType.MiniSpikeLeft },
            {"object1092", ObjectType.MiniSpikeLeft },
            {"object1093", ObjectType.MiniSpikeLeft },
            {"object1108", ObjectType.MiniSpikeLeft },
            {"object1115", ObjectType.MiniSpikeLeft },
            {"object1116", ObjectType.MiniSpikeLeft },
            {"object1131", ObjectType.MiniSpikeLeft },
            {"object1144", ObjectType.MiniSpikeLeft },
            {"object1147", ObjectType.MiniSpikeLeft },
            {"object1151", ObjectType.MiniSpikeLeft },
            {"object731", ObjectType.MiniSpikeLeft },
            {"object737", ObjectType.MiniSpikeLeft },
            {"object758", ObjectType.MiniSpikeLeft },
            {"object765", ObjectType.MiniSpikeLeft },
            {"object774", ObjectType.MiniSpikeLeft },
            {"object787", ObjectType.MiniSpikeLeft },
            {"object829", ObjectType.MiniSpikeLeft },
            {"object835", ObjectType.MiniSpikeLeft },
            {"object843", ObjectType.MiniSpikeLeft },
            {"object847", ObjectType.MiniSpikeLeft },
            {"object855", ObjectType.MiniSpikeLeft },
            {"object865", ObjectType.MiniSpikeLeft },
            {"object893", ObjectType.MiniSpikeLeft },
            {"object902", ObjectType.MiniSpikeLeft },
            {"object947", ObjectType.MiniSpikeLeft },
            {"minispikeright", ObjectType.MiniSpikeRight},
            {"minispikerightright", ObjectType.MiniSpikeRight },
            {"object1025", ObjectType.MiniSpikeRight },
            {"object1050", ObjectType.MiniSpikeRight },
            {"object1059", ObjectType.MiniSpikeRight },
            {"object1064", ObjectType.MiniSpikeRight },
            {"object1073", ObjectType.MiniSpikeRight },
            {"object1081", ObjectType.MiniSpikeRight },
            {"object1090", ObjectType.MiniSpikeRight },
            {"object1094", ObjectType.MiniSpikeRight },
            {"object1106", ObjectType.MiniSpikeRight },
            {"object1117", ObjectType.MiniSpikeRight },
            {"object1129", ObjectType.MiniSpikeRight },
            {"object1142", ObjectType.MiniSpikeRight },
            {"object1145", ObjectType.MiniSpikeRight },
            {"object1149", ObjectType.MiniSpikeRight },
            {"object745", ObjectType.MiniSpikeRight },
            {"object759", ObjectType.MiniSpikeRight },
            {"object764", ObjectType.MiniSpikeRight },
            {"object776", ObjectType.MiniSpikeRight },
            {"object788", ObjectType.MiniSpikeRight },
            {"object828", ObjectType.MiniSpikeRight },
            {"object830", ObjectType.MiniSpikeRight },
            {"object841", ObjectType.MiniSpikeRight },
            {"object853", ObjectType.MiniSpikeRight },
            {"object864", ObjectType.MiniSpikeRight },
            {"object891", ObjectType.MiniSpikeRight },
            {"object900", ObjectType.MiniSpikeRight },
            {"object945", ObjectType.MiniSpikeRight },
            {"prismmini2", ObjectType.MiniSpikeRight },
            {"whitemini2", ObjectType.MiniSpikeRight },
            {"minispikeup", ObjectType.MiniSpikeUp},
            {"minispikeupup", ObjectType.MiniSpikeUp },
            {"object1024", ObjectType.MiniSpikeUp },
            {"object1046", ObjectType.MiniSpikeUp },
            {"object1049", ObjectType.MiniSpikeUp },
            {"object1058", ObjectType.MiniSpikeUp },
            {"object1063", ObjectType.MiniSpikeUp },
            {"object1072", ObjectType.MiniSpikeUp },
            {"object1080", ObjectType.MiniSpikeUp },
            {"object1089", ObjectType.MiniSpikeUp },
            {"object1098", ObjectType.MiniSpikeUp },
            {"object1105", ObjectType.MiniSpikeUp },
            {"object1128", ObjectType.MiniSpikeUp },
            {"object1132", ObjectType.MiniSpikeUp },
            {"object1141", ObjectType.MiniSpikeUp },
            {"object1148", ObjectType.MiniSpikeUp },
            {"object736", ObjectType.MiniSpikeUp },
            {"object744", ObjectType.MiniSpikeUp },
            {"object751", ObjectType.MiniSpikeUp },
            {"object756", ObjectType.MiniSpikeUp },
            {"object772", ObjectType.MiniSpikeUp },
            {"object775", ObjectType.MiniSpikeUp },
            {"object786", ObjectType.MiniSpikeUp },
            {"object824", ObjectType.MiniSpikeUp },
            {"object840", ObjectType.MiniSpikeUp },
            {"object846", ObjectType.MiniSpikeUp },
            {"object852", ObjectType.MiniSpikeUp },
            {"object863", ObjectType.MiniSpikeUp },
            {"object867", ObjectType.MiniSpikeUp },
            {"object890", ObjectType.MiniSpikeUp },
            {"object899", ObjectType.MiniSpikeUp },
            {"object911", ObjectType.MiniSpikeUp },
            {"object922", ObjectType.MiniSpikeUp },
            {"object944", ObjectType.MiniSpikeUp },
            {"playerkiller", ObjectType.SpikeDown},
            {"object797", ObjectType.LineSpikeUp },
            {"object798", ObjectType.LineSpikeRight },
            {"object799", ObjectType.LineSpikeDown },
            {"object800", ObjectType.LineSpikeLeft },
            {"object801", ObjectType.LineMiniSpikeUp },
            {"object802", ObjectType.LineMiniSpikeRight },
            {"object803", ObjectType.LineMiniSpikeDown },
            {"object804", ObjectType.LineMiniSpikeLeft },
            {"object931", ObjectType.LineSpikeUp },
            {"object932", ObjectType.LineSpikeRight },
            {"object933", ObjectType.LineSpikeDown },
            {"object934", ObjectType.LineSpikeLeft },
            {"object935", ObjectType.LineMiniSpikeUp },
            {"object936", ObjectType.LineMiniSpikeRight },
            {"object937", ObjectType.LineMiniSpikeDown },
            {"object938", ObjectType.LineMiniSpikeLeft },
            {"walljumpl", ObjectType.VineLeft},
            {"walljumpll", ObjectType.VineLeft},
            {"walljumpr", ObjectType.VineRight},
            {"walljumprr", ObjectType.VineRight},
            {"platform", ObjectType.Platform}, // yuuutu platform hitbox is a lot smaller than the regular one, good luck distinguishing here
            {"movingplatform", ObjectType.Platform}, // like this one should have the regular hitbox but not the other one since its just supposed to be an object parent
            {"movingplatformform", ObjectType.Platform },
            {"object1169", ObjectType.Platform },
            {"object844", ObjectType.Platform },
            {"object923", ObjectType.Platform },
            {"platformarchfoe", ObjectType.Platform },
            {"platformballoons", ObjectType.Platform },
            {"platformblowgame", ObjectType.Platform },
            {"platformcrimson", ObjectType.Platform },
            {"platformrmj", ObjectType.Platform },
            {"platformsprites", ObjectType.Platform },
            {"platform2", ObjectType.Platform },
            {"catharsiswater", ObjectType.CatharsisWater},
            {"grav_up", ObjectType.GravityArrowUp },
            {"grav_down", ObjectType.GravityArrowDown },
            {"_ue", ObjectType.GravityArrowUp },
            {"_sita", ObjectType.GravityArrowDown },
        };
    }
}

