using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NightlyTree
{
    public partial class Form1 : Form
    {
        private NotifyIcon notifyIcon1; // 新增這一行

        public static bool first_time = false;
        public Form1()
        {
            InitializeComponent();
            Size bounds = Screen.PrimaryScreen.Bounds.Size;
            this.Size = bounds;

            // 初始化 notifyIcon1
            notifyIcon1 = new NotifyIcon();
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.Visible = true;
            notifyIcon1.MouseClick += notifyIcon1_MouseClick;
        }   

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;

            if (first_time)
            {
                MessageBox.Show("歡迎使用 Nightly Tree！\r\n這是一個免費開源，適用於 Windows 2000 (SP3) 以上的抗藍光遮罩工具。\r\n\r\n若要調整設定，請點擊系統匣中的圖示。", "Nightly Tree - 初次使用", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x20;   // WS_EX_TRANSPARENT → 滑鼠事件穿透
                cp.ExStyle |= 0x80000; // WS_EX_LAYERED → 支援透明
                return cp;
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            new Form2().Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < Program.opacity)
            {
                this.Opacity += 0.01;
            }
            else
            {
                timer1.Stop();
            }
        }
    }
}
