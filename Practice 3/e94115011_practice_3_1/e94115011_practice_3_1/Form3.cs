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

namespace e94115011_practice_3_1
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
            Form4 menuForm = new Form4(this);
            menuForm.ShowDialog();
        }

        public void AddOrder(string orderDetails)
        {
            listBox1.Items.Add($"訂單編號: {orderNumber} 購買了 {orderDetails}");
            label1.Text = "新增訂單成功，訂單編號 " + orderNumber;
            orderNumber++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.ForeColor = Color.Black;
        }
    }
}