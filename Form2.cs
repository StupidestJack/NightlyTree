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

        private void Form2_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Program.actived;
            trackBar1.Value = (Program.colorG - 55) / 2;
            trackBar2.Value = (int)(Program.opacity * 100);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked) { 
                Program.main.Hide();
            }
            else 
            {
                Program.main.Show();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Program.colorG = 55 + (trackBar1.Value * 2);
            Program.main.BackColor = Color.FromArgb(255, 255, 55 + (trackBar1.Value*2), 0);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Program.opacity = trackBar2.Value / 200.0;
            Program.main.Opacity = trackBar2.Value / 200.0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.opacity = 0.1;
            Program.colorG = 175;
            Program.main.Opacity = 0.15;
            Program.main.BackColor = Color.FromArgb(255, 255, 225, 0);
            checkBox1.Checked = Program.actived;
            trackBar1.Value = (Program.colorG - 55) / 2;
            trackBar2.Value = (int)(Program.opacity * 100);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("NightlyTree ver.1.0.0\nby Hsueh Jack UwU\n此軟體以MIT協議在GitHub開源。", "關於 NightlyTree", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            int activeInt = checkBox1.Checked ? 1 : 0;
            File.WriteAllText("config.txt", activeInt + "\r\n" + Program.colorG.ToString() + "\r\n" + Program.opacity.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string texts = "Nightly Tree 是一款免費且開源的抗藍光遮罩工具，如您是付費取得，那麼您大概被騙了。\r\n\r\n";
            texts += "已知問題：\r\n";
            texts += "1. 此軟體無法使用UIAccess權限，因此螢幕小鍵盤、工作管理員等程式可能會蓋住此軟體的遮罩。\r\n";
            texts += "2. 此軟體會被截圖擷取，可能導致螢幕擷取圖片泛黃。\r\n";
            texts += "3. 此軟體不建議在多重螢幕下使用，此版本尚未對多螢幕延伸模式進行相容。\r\n";
            texts += "\r\n若您有能力，歡迎到此專案的 GitHub 協助更新。\r\n";
            MessageBox.Show(texts, "使用須知", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
