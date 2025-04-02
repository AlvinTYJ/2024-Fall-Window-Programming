using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace e94115011_practice_3_2
{
    public partial class Form3 : Form
    {
        private int orderNumber = 1000;
        String username;
        public Form3(String s = "")
        {
            InitializeComponent();
            listBox1.ForeColor = listBox1.BackColor;
            listBox1.HorizontalScrollbar = true;
            username = s;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Text = "歡迎登入！" + username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(this);
            form4.ShowDialog();
        }

        public void AddOrder(string orderDetails)
        {
            listBox1.Items.Add($"訂單編號: {orderNumber} 購買了 {orderDetails}，此訂單由 {username} 新增");
            label1.Text = "新增訂單成功，訂單編號 " + orderNumber;
            orderNumber++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.ForeColor = Color.Black;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(this);
            form5.ShowDialog();
        }

        public void SetUsername(String newUsername)
        {
            username = newUsername;
            label1.Text = "歡迎登入！" + username;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}