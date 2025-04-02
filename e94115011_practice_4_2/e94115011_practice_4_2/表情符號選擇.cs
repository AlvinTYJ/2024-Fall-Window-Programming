using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e94115011_practice_4_2
{
    public partial class 表情符號選擇 : Form
    {
        private Form1 form1;
        public string SelectedFace{ get; private set; }

        public 表情符號選擇(Form1 parentForm)
        {
            InitializeComponent();
            form1 = parentForm;
        }

        private void 表情符號選擇_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;

            pictureBox1.Image = form1.Face0;
            pictureBox2.Image = form1.Face1;
            pictureBox3.Image = form1.Face2;
            pictureBox4.Image = form1.Face3;
            pictureBox5.Image = form1.Face4;
            pictureBox6.Image = form1.Face5;

            radioButton1.CheckedChanged += RadioButton1_CheckedChanged;
            radioButton2.CheckedChanged += RadioButton2_CheckedChanged;
            radioButton3.CheckedChanged += RadioButton3_CheckedChanged;
            radioButton4.CheckedChanged += RadioButton4_CheckedChanged;
            radioButton5.CheckedChanged += RadioButton5_CheckedChanged;
            radioButton6.CheckedChanged += RadioButton6_CheckedChanged;
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                SelectedFace = "Face0";
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                SelectedFace = "Face1";
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                SelectedFace = "Face2";
        }

        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
                SelectedFace = "Face3";
        }

        private void RadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
                SelectedFace = "Face4";
        }

        private void RadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
                SelectedFace = "Face5";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}