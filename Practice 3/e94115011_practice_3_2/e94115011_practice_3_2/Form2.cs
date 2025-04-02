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
    public partial class Form2 : Form
    {
        public static Dictionary<string, Consumer> consumers = new Dictionary<string, Consumer>();

        public Form2()
        {
            InitializeComponent();
            consumers["admin"] = new Consumer
            {
                Password = "admin"
            };
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Text = "歡迎光臨！請登入";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (consumers.ContainsKey(textBox1.Text) && consumers[textBox1.Text].Password == textBox2.Text)
            {
                Form3 form3 = new Form3(textBox1.Text);
                textBox1.Clear();
                textBox2.Clear();
                form3.ShowDialog();
            }
            else
            {
                label1.Text = "帳號或密碼錯誤";
                textBox1.Clear();
                textBox2.Clear();
            }
        }
    }
    public class Consumer
    {
        public string Password;
    }
}