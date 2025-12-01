using System;
using System.IO;

namespace NightlyTree
{
    // 將配置變數從 Program.cs 移到這裡
    internal class ConfigClass
    {
        // 預設配置值
        private const string ConfigFileName = "config.txt";
        private const bool DefaultIsActived = true;
        private const int DefaultColorG = 175;
        private const double DefaultOpacity = 0.15;

        // 配置屬性 (使用 PascalCase 命名規範)
        public bool IsActived { get; set; } = DefaultIsActived;
        public int ColorGValue { get; set; } = DefaultColorG;
        public double OpacityValue { get; set; } = DefaultOpacity;

        public ConfigClass()
        {
            // 建構子：嘗試載入配置
            LoadConfig();
        }

        // 嘗試從檔案載入配置
        public void LoadConfig()
        {
            try
            {
                string conf = File.ReadAllText(ConfigFileName);
                // 允許處理 \r\n (Windows) 和 \n (Unix/其他) 換行符
                string[] lines = conf.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                if (lines.Length >= 3 &&
                    int.TryParse(lines[1], out int g) &&
                    double.TryParse(lines[2], out double o))
                {
                    ColorGValue = g;
                    OpacityValue = o;
                }
                else
                {
                    // 檔案格式錯誤，使用預設值並儲存正確格式
                    throw new Exception("Config file format is incorrect.");
                }
            }
            catch (Exception)
            {
                // 檔案不存在或讀取失敗，使用預設值
                IsActived = DefaultIsActived;
                ColorGValue = DefaultColorG;
                OpacityValue = DefaultOpacity;

                // 首次執行或檔案損壞時，寫入正確的預設配置
                SaveConfig();
                // 設置標誌，用於 Form1 顯示初次使用訊息
                Form1.IsFirstTime = true;
            }
        }

        // 將當前配置儲存到檔案
        public void SaveConfig()
        {
            int activeInt = IsActived ? 1 : 0;
            string content = activeInt + "\r\n" + ColorGValue.ToString() + "\r\n" + OpacityValue.ToString();
            File.WriteAllText(ConfigFileName, content);
        }

        // 將所有配置重置為預設值
        public void ResetToDefaults()
        {
            IsActived = DefaultIsActived;
            ColorGValue = DefaultColorG;
            OpacityValue = DefaultOpacity;
        }
    }
}