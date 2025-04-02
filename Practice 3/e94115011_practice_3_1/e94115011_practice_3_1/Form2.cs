using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e94115011_practice_3_1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Text = "歡迎光臨！請登入";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                Form3 listForm = new Form3(textBox1.Text);
                listForm.Show();
            }
            else
            {
                label1.Text = "帳號或密碼錯誤";
                textBox1.Clear();
                textBox2.Clear();
            }
        }
    }
}
