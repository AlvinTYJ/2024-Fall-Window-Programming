using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace e94115011_practice_4_2
{
    public partial class 顔色選擇 : Form
    {
        public Color SelectedColor { get; private set; }

        public 顔色選擇()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            this.Click += 顔色選擇_Click;
        }

        private void 顔色選擇_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            this.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }

        private void 顔色選擇_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            this.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedColor = this.BackColor;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}