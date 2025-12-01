using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace NightlyTree
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string batPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\NightlyTree.bat";
        private void Form2_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Program.actived;
            trackBar1.Value = (Program.colorG - 55) / 2;
            // Opacity 的 Value 應該是乘以 200.0 (根據 trackBar2_Scroll)
            trackBar2.Value = (int)(Program.opacity * 200.0);
            // 檢查開機自啟動狀態 (批次檔方案)
            checkBox2.Checked = File.Exists(batPath);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Program.actived = checkBox1.Checked;
            // 【更新】同步所有 Form 的顯示狀態
            Program.UpdateAllVisibility();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定要退出 Nightly Tree 嗎？", "退出確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Program.colorG = 55 + (trackBar1.Value * 2);
            // 【更新】同步所有 Form 的顏色
            Program.UpdateAllStyles();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Program.opacity = trackBar2.Value / 200.0;
            // 【更新】同步所有 Form 的透明度
            Program.UpdateAllStyles();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 重置 Program 的靜態變數為預設值
            Program.opacity = 0.15;
            Program.colorG = 175;
            Program.actived = true;

            // 【更新】同步所有 Form 的樣式
            Program.UpdateAllStyles();
            // 【更新】同步所有 Form 的顯示狀態
            Program.UpdateAllVisibility();

            // 更新 Form2 控制項
            checkBox1.Checked = Program.actived;
            trackBar1.Value = (Program.colorG - 55) / 2;
            trackBar2.Value = (int)(Program.opacity * 200.0);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("NightlyTree ver.1.5.0\nby Hsueh Jack UwU\n此軟體以MIT協議在GitHub開源。", "關於 Nightly Tree", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 保持原本的儲存邏輯
            int activeInt = Program.actived ? 1 : 0;
            File.WriteAllText("config.txt", activeInt + "\r\n" + Program.colorG.ToString() + "\r\n" + Program.opacity.ToString());
        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                MessageBox.Show("為避免開機自啟動出現問題，請把此軟體放置於安全的位置或資料夾。", "開機自啟動", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                string fullPath = Application.ExecutablePath;
                File.WriteAllText(batPath, "title Nightly Tree - 自動啟動視窗\r\nstart \"\" \"" + fullPath + "\"");
            }
            else
            {

                if (File.Exists(batPath))
                {
                    File.Delete(batPath);
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string texts = "Nightly Tree 是一款免費且開源的抗藍光遮罩工具，如您是付費取得，那麼您大概被騙了。\r\n\r\n";
            texts += "已知問題：\r\n";
            texts += "1. 此軟體無法使用UIAccess權限，因此螢幕小鍵盤、工作管理員等程式可能會蓋住此軟體的遮罩，且新版本中無法覆蓋到開始選單。\r\n";
            texts += "2. 此軟體會被截圖擷取，可能導致螢幕擷取圖片泛黃。\r\n";
            texts += "\r\n若您有能力，歡迎到此專案的 GitHub 協助更新。\r\n";
            MessageBox.Show(texts, "使用須知", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}