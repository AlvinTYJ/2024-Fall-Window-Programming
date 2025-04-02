using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace e94115011_practice_7_2
{
    public partial class Form1 : Form
    {
        int num = 0;
        public Form1()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            openFileDialog1.Filter = "Text files (*.txt;*.mytxt)|*.txt;*.mytxt";
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            text frmText = new text();
            frmText.MdiParent = this;
            frmText.Text = num.ToString() + ".mytxt";
            frmText.Show();
            num++;
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileName.EndsWith(".mytxt", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        string content = File.ReadAllText(openFileDialog1.FileName);
                        string[] lines = content.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                        text frmText = new text();
                        frmText.MdiParent = this;
                        frmText.Text = openFileDialog1.FileName;
                        frmText.SetFilePath(openFileDialog1.FileName);

                        if (lines.Length >= 2 && lines[0].Contains(",") && lines[1].Contains(","))
                        {
                            string[] fontData = lines[0].Split(',');
                            Font font = new Font(fontData[0], float.Parse(fontData[1]), (FontStyle)Enum.Parse(typeof(FontStyle), fontData[2]));
                            frmText.richTextBox1.Font = font;

                            string[] colorData = lines[1].Split(',');
                            Color color = Color.FromArgb(int.Parse(colorData[0]), int.Parse(colorData[1]), int.Parse(colorData[2]));
                            frmText.richTextBox1.ForeColor = color;

                            frmText.richTextBox1.Text = string.Join(Environment.NewLine, lines.Skip(2)); 
                        }
                        else
                        {
                            frmText.richTextBox1.Text = content;
                        }
                        frmText.Show();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        text frmText = new text();
                        frmText.MdiParent = this;
                        frmText.Text = openFileDialog1.FileName;
                        string content = File.ReadAllText(openFileDialog1.FileName);
                        frmText.richTextBox1.Text = content;
                        frmText.SetFilePath(openFileDialog1.FileName);
                        frmText.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
