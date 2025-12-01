using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace NightlyTree
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>

        // 【更新】用 List<Form1> 替代單個 Form1 實例，用於支援多螢幕
        public static List<Form1> MainInstances = new List<Form1>();

        // 保留原有的靜態配置變數
        public static bool actived = true;
        public static int colorG = 175;
        public static double opacity = 0.15;
        public static notifyForm NotifyInstances = new notifyForm();

        [STAThread]
        static void Main()
        {
            // 配置檔案讀取邏輯
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
                Form1.IsFirstTime = true; // 這裡依賴 Form1 的靜態變數
            }


            Application.EnableVisualStyles();

            // 【多螢幕】遍歷所有螢幕並創建 Form1 實例
            Form1 primaryForm = null;
            foreach (Screen screen in Screen.AllScreens)
            {
                // 呼叫 Form1(Screen screen) 建構子
                Form1 screenForm = new Form1(screen);
                MainInstances.Add(screenForm);
                if (screen.Primary)
                {
                    primaryForm = screenForm;
                }
                else
                {
                    // 非主螢幕的 Form 需要手動顯示
                    screenForm.Show();
                }
            }

            if (primaryForm != null)
            {
                Application.Run(primaryForm);
            }
            else
            {
                Application.Exit();
            }
        }

        // 【新增】更新所有遮罩的顯示狀態 (Show/Hide)
        public static void UpdateAllVisibility()
        {
            foreach (Form1 form in MainInstances)
            {
                if (actived)
                {
                    form.Show();
                }
                else
                {
                    form.Hide();
                }
            }
        }

        // 【新增】更新所有遮罩的顏色和透明度
        public static void UpdateAllStyles()
        {
            foreach (Form1 form in MainInstances)
            {
                form.BackColor = Color.FromArgb(255, 255, colorG, 0);

                // 檢查 timer 狀態，只有 timer 沒在運行才直接設定 Opacity
                if (!form.IsTimerRunning)
                {
                    form.Opacity = opacity;
                }
            }
        }
    }
}