using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e94115011_practice_3_2
{
    public partial class Form5 : Form
    {
        private Form3 mainForm3;
        public Form5(Form3 form3)
        {
            InitializeComponent();
            mainForm3 = form3;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Text = "請輸入要新增的使用者帳號與密碼";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Form2.consumers.ContainsKey(textBox1.Text))
            {
                label1.Text = "此使用者已經存在";
                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {
                Form2.consumers[textBox1.Text] = new Consumer
                {
                    Password = textBox2.Text
                };
                mainForm3.SetUsername(textBox1.Text);
                this.Close();
            }
        }
    }
}