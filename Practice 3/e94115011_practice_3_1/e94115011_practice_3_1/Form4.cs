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
    public partial class Form4 : Form
    {
        private Form3 mainForm;
        public Form4(Form3 form)
        {
            InitializeComponent();
            mainForm = form;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Text = "輸入完數量後，點選對應的商品按鈕，並按送出";
        }

        private string productName = "";
        private void button1_Click(object sender, EventArgs e)
        {
            productName = "企鵝";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            productName = "炸豬排";
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            productName = "炸蝦";
        }

        private void SubmitOrder(string product)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && int.TryParse(textBox1.Text, out int quantity))
            {
                string orderDetails = $"{quantity} 個 {product}";
                mainForm.AddOrder(orderDetails);
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (productName != "")
            {
                SubmitOrder(productName);
            }
        }
    }
}
