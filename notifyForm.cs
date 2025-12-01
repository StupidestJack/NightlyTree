using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NightlyTree
{
    public partial class notifyForm : Form
    {
        public notifyForm()
        {
            InitializeComponent();
        }

        private void notify_Load(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            new Form2().Show();
        }
    }
}
