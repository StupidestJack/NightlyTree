using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; // 確保有這個引用
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms; // 確保有這個引用

namespace NightlyTree
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr window, int index, int
        value);
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr window, int index);


        const int GWL_EXSTYLE = -20;
        const int WS_EX_TOOLWINDOW = 0x00000080;
        const int WS_EX_APPWINDOW = 0x00040000;
        public static bool IsFirstTime = false;

        // 【新增】標記此實例是否為系統圖示的控制者 (雖然不用圖示了，但isPrimaryForm對後續邏輯可能有用)
        private readonly bool isPrimaryForm;

        // 屬性：用於檢查 timer1 是否正在運行 (用於 Program.cs 的同步邏輯)
        public bool IsTimerRunning => timer1.Enabled;

        // 默認建構子 (必須保留給 Designer)
        public Form1()
        {
            InitializeComponent();
            this.isPrimaryForm = false;
            int windowStyle = GetWindowLong(Handle, GWL_EXSTYLE);
            SetWindowLong(Handle, GWL_EXSTYLE, windowStyle | WS_EX_TOOLWINDOW);
        }

        // 【多螢幕建構子】
        public Form1(Screen screen) : this()
        {
            this.isPrimaryForm = screen.Primary;

            // 1. 設定位置和大小
            this.Size = screen.Bounds.Size;
            this.Location = screen.Bounds.Location;
            this.StartPosition = FormStartPosition.Manual;

            // 2. 處理 Timer 和 Opacity
            if (this.isPrimaryForm)
            {
                // 主螢幕：啟用漸變效果
                this.Opacity = 0D; // 從透明開始
                this.timer1.Enabled = true; // 啟用計時器開始漸變
            }
            else
            {
                // 非主螢幕：直接設為最終透明度，無需漸變
                this.timer1.Enabled = false;
                this.Opacity = Program.opacity;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 刪除：所有 notifyIcon1 和 IsFirstTime 的判斷邏輯

            this.BackColor = Color.FromArgb(255, 255, Program.colorG, 0);

            if (!Program.actived)
            {
                this.Hide();
            }
        }

        // 【保留】用於穿透和透明效果的核心方法
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

        // 【保留】Timer 邏輯用於漸變效果
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < Program.opacity)
            {
                this.Opacity += 0.01;
            }
            else
            {
                this.timer1.Stop();
            }
        }

        // 【刪除】notifyIcon1_MouseClick 等所有圖示和選單相關方法
        // private void notifyIcon1_MouseClick(...)
        // private void 開啟設定ToolStripMenuItem_Click(...)
        // private void 退出ToolStripMenuItem_Click(...)

        // 程式結束方法，供 Form2 的退出按鈕使用 (如果需要的話)
        // private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        // {
        //     Application.Exit();
        // }
    }
}