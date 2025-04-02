using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e94115011_practice_7_1
{
    public partial class text : Form
    {
        public RichTextBox richTextBox1;
        private string filePath = string.Empty;

        public text()
        {
            InitializeComponent();

            richTextBox1 = new RichTextBox();
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Both;
            richTextBox1.WordWrap = true;
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            richTextBox1.Location = new Point(0, 36);
            richTextBox1.Size = new Size(this.ClientSize.Width, this.ClientSize.Height-36);
            richTextBox1.Font = new Font("Arial", 10);
            this.Controls.Add(richTextBox1);

            saveFileDialog1.Filter = "自訂文字檔 (*.mytxt)|*.mytxt|文字檔 (*.txt)|*.txt";
        }

        public void SetFilePath(string path)
        {
            filePath = path;
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                if (this.Text.EndsWith(".mytxt", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        string fontData = $"{richTextBox1.Font.FontFamily.Name},{richTextBox1.Font.Size},{richTextBox1.Font.Style}";
                        string colorData = $"{richTextBox1.ForeColor.R},{richTextBox1.ForeColor.G},{richTextBox1.ForeColor.B}";
                        string content = fontData + Environment.NewLine + colorData + Environment.NewLine + richTextBox1.Text;
                        File.WriteAllText(filePath, content);
                        MessageBox.Show("檔案儲存成功!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("儲存檔案時出錯: " + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        File.WriteAllText(filePath, richTextBox1.Text);
                        MessageBox.Show("檔案儲存成功!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("儲存檔案時出錯: " + ex.Message);
                    }
                }
            }
            else if (string.IsNullOrEmpty(filePath))
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if(saveFileDialog1.FilterIndex == 1)
                    {
                        try
                        {
                            string fontData = $"{richTextBox1.Font.FontFamily.Name},{richTextBox1.Font.Size},{richTextBox1.Font.Style}";
                            string colorData = $"{richTextBox1.ForeColor.R},{richTextBox1.ForeColor.G},{richTextBox1.ForeColor.B}";
                            string content = fontData + Environment.NewLine + colorData + Environment.NewLine + richTextBox1.Text;
                            File.WriteAllText(saveFileDialog1.FileName, content);
                            SetFilePath(saveFileDialog1.FileName);
                            this.Text = saveFileDialog1.FileName;
                            MessageBox.Show("檔案儲存成功!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("儲存檔案時出錯: " + ex.Message);
                        }
                    }
                    else if (saveFileDialog1.FilterIndex == 2)
                    {
                        try
                        {
                            File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
                            SetFilePath(saveFileDialog1.FileName);
                            this.Text = saveFileDialog1.FileName;
                            MessageBox.Show("檔案儲存成功!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("儲存檔案時出錯: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                saveFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(filePath);
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FilterIndex == 1)
                    {
                        try
                        {
                            string fontData = $"{richTextBox1.Font.FontFamily.Name},{richTextBox1.Font.Size},{richTextBox1.Font.Style}";
                            string colorData = $"{richTextBox1.ForeColor.R},{richTextBox1.ForeColor.G},{richTextBox1.ForeColor.B}";
                            string content = fontData + Environment.NewLine + colorData + Environment.NewLine + richTextBox1.Text;
                            File.WriteAllText(saveFileDialog1.FileName, content);
                            SetFilePath(saveFileDialog1.FileName);
                            this.Text = saveFileDialog1.FileName;
                            MessageBox.Show("檔案儲存成功!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("儲存檔案時出錯: " + ex.Message);
                        }
                    }
                    else if (saveFileDialog1.FilterIndex == 2)
                    {
                        try
                        {
                            File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
                            SetFilePath(saveFileDialog1.FileName);
                            this.Text = saveFileDialog1.FileName;
                            MessageBox.Show("檔案儲存成功!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("儲存檔案時出錯: " + ex.Message);
                        }
                    }
                }
            }
            else if (string.IsNullOrEmpty(filePath))
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FilterIndex == 1)
                    {
                        try
                        {
                            string fontData = $"{richTextBox1.Font.FontFamily.Name},{richTextBox1.Font.Size},{richTextBox1.Font.Style}";
                            string colorData = $"{richTextBox1.ForeColor.R},{richTextBox1.ForeColor.G},{richTextBox1.ForeColor.B}";
                            string content = fontData + Environment.NewLine + colorData + Environment.NewLine + richTextBox1.Text;
                            File.WriteAllText(saveFileDialog1.FileName, content);
                            SetFilePath(saveFileDialog1.FileName);
                            this.Text = saveFileDialog1.FileName;
                            MessageBox.Show("檔案儲存成功!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("儲存檔案時出錯: " + ex.Message);
                        }
                    }
                    else if (saveFileDialog1.FilterIndex == 2)
                    {
                        try
                        {
                            File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
                            SetFilePath(saveFileDialog1.FileName);
                            this.Text = saveFileDialog1.FileName;
                            MessageBox.Show("檔案儲存成功!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("儲存檔案時出錯: " + ex.Message);
                        }
                    }
                }
            }   
        }

        private void mnuCut_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                richTextBox1.Cut();
            }
        }

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                richTextBox1.Copy();
            }
        }

        private void mnuPaste_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void mnuFont_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fontDialog1.Font;
            }
        }

        private void mnuColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.ForeColor = colorDialog1.Color;
            }
        }

        private void mnuExit2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
