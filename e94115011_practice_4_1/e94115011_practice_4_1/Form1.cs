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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace e94115011_practice_4_1
{
    public partial class Form1 : Form
    {
        private Color Chose;
        private Color[] tabColors = new Color[2];

        public Form1()
        {
            InitializeComponent();
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;

            richTextBox1.ReadOnly = true;
            richTextBox2.ReadOnly = true;
            richTextBox1.BackColor = this.BackColor;
            richTextBox2.BackColor = this.BackColor;

            if (tabControl1.SelectedIndex == 0)
            {
                Tab1DisplayMode();
            }

            richTextBox1.DoubleClick += Form1_DoubleClick;
            richTextBox2.DoubleClick += Form1_DoubleClick;
            this.DoubleClick += Form1_DoubleClick;
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                Tab1DisplayMode();
            }
            else
            {
                Tab2DisplayMode();
            }
        }

        private void Tab1DisplayMode()
        {
            textBox1.Enabled = false;
            button1.Enabled = false;
            richTextBox1.Visible = true;
            richTextBox2.Visible = false;
            this.BackColor = tabColors[0];
        }

        private void Tab2DisplayMode()
        {
            textBox1.Enabled = true;
            button1.Enabled = true;
            richTextBox1.Visible = false;
            richTextBox2.Visible = true;
            this.BackColor = tabColors[1];
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                string message = textBox1.Text;
                textBox1.Clear();

                if (tabControl1.SelectedIndex == 0)
                {
                    return;
                }
                else
                {
                    richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                    richTextBox1.AppendText($"{tabPage2.Text}: {message}" + Environment.NewLine);
                    richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                    richTextBox1.AppendText($"{tabPage1.Text}: 汪！" + Environment.NewLine);
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                    richTextBox1.ScrollToCaret();

                    richTextBox2.SelectionAlignment = HorizontalAlignment.Right;
                    richTextBox2.AppendText($"{tabPage2.Text}: {message}" + Environment.NewLine);
                    richTextBox2.SelectionAlignment = HorizontalAlignment.Left;
                    richTextBox2.AppendText($"{tabPage1.Text}: 汪！" + Environment.NewLine);
                    richTextBox2.SelectionStart = richTextBox1.Text.Length;
                    richTextBox2.ScrollToCaret();
                }
            }
        }


        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            顔色選擇 form2 = new 顔色選擇();
            if (form2.ShowDialog() == DialogResult.OK)
            {
                Chose = form2.SelectedColor;
                tabColors[tabControl1.SelectedIndex] = Chose;
                if (tabControl1.SelectedIndex == 0)
                {
                    richTextBox1.BackColor = tabColors[0];
                    this.BackColor = tabColors[0];
                }
                else
                {
                    richTextBox2.BackColor = tabColors[1];
                    this.BackColor = tabColors[1];
                }
            }
        }
    }
}