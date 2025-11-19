using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace NightlyTree
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>

        
        public static Form1 main = new Form1();
        public static bool actived = true;
        public static int colorG = 175;
        public static double opacity = 0.15;

        

        [STAThread]
        static void Main()
        {
            try
            {
                string conf = File.ReadAllText("config.txt");
                string[] lines = conf.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                if (lines.Length >= 3 &&
                    int.TryParse(lines[1], out int g) &&
                    double.TryParse(lines[2], out double o))
                {
                    actived = (lines[0] == "1");
                    colorG = g;
                    opacity = o;
                }
                else
                {
                    throw new Exception("Config file format is incorrect.");
                }
            }
            catch (Exception)
            {
                actived = true;
                colorG = 175;
                opacity = 0.15;
                File.WriteAllText("config.txt", "1\r\n175\r\n0.15");
                Form1.first_time = true;
            }


            Application.EnableVisualStyles();
            Application.Run(main);
        }
    }
}
