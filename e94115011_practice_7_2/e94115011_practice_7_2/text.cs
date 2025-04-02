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

namespace e94115011_practice_7_2
{
    public partial class text : Form
    {
        public RichTextBox richTextBox1;
        private string filePath = string.Empty;
        Timer autoSaveTimer;
        private bool isExiting = false;
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

            autoSaveTimer = new Timer();
            autoSaveTimer.Interval = 60000;
            autoSaveTimer.Tick += AutoSave_Tick;
            autoSaveTimer.Start();
            this.FormClosing += text_FormClosing;
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

        private void mnuUndo_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo)
            {
                richTextBox1.Undo();
            }
        }

        private void mnuRedo_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanRedo)
            {
                richTextBox1.Redo();
            }
        }

        private void mnuWordCount_Click(object sender, EventArgs e)
        {
            int wordCount = richTextBox1.Text.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
            MessageBox.Show($"字數: {wordCount}", "字數統計", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuFindReplace_Click(object sender, EventArgs e)
        {
            尋找與取代 findReplaceForm = new 尋找與取代(this);
            findReplaceForm.Show();
        }

        public void FindNext(string findText)
        {
            if (!string.IsNullOrEmpty(findText))
            {
                int currentSelectionStart = richTextBox1.SelectionStart;
                int currentSelectionLength = richTextBox1.SelectionLength;
                richTextBox1.SelectAll();
                richTextBox1.SelectionBackColor = richTextBox1.BackColor;
                richTextBox1.SelectionColor = richTextBox1.ForeColor;
                richTextBox1.Select(currentSelectionStart, currentSelectionLength);

                int startIndex = richTextBox1.SelectionStart + richTextBox1.SelectionLength;
                int nextIndex = richTextBox1.Text.IndexOf(findText, startIndex, StringComparison.OrdinalIgnoreCase);
                if (nextIndex != -1)
                {
                    richTextBox1.Select(nextIndex, findText.Length);
                    richTextBox1.SelectionBackColor = Color.Black;
                    richTextBox1.SelectionColor = Color.White;
                    richTextBox1.ScrollToCaret();
                }
                else
                {
                    richTextBox1.SelectionLength = 0;
                    MessageBox.Show($"已找不到更多匹配項目", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show($"請輸入要尋找的文字", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Replace(string findText, string replaceText)
        {
            if (richTextBox1.SelectedText == findText)
            {
                richTextBox1.SelectedText = replaceText;
            }
        }

        public void ReplaceAll(string findText, string replaceText)
        {
            richTextBox1.Text = richTextBox1.Text.Replace(findText, replaceText);
        }

        private void AutoSave_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                if (this.Text.EndsWith(".mytxt", StringComparison.OrdinalIgnoreCase))
                {
                    string fontData = $"{richTextBox1.Font.FontFamily.Name},{richTextBox1.Font.Size},{richTextBox1.Font.Style}";
                    string colorData = $"{richTextBox1.ForeColor.R},{richTextBox1.ForeColor.G},{richTextBox1.ForeColor.B}";
                    string content = fontData + Environment.NewLine + colorData + Environment.NewLine + richTextBox1.Text;
                    File.WriteAllText(filePath, content);
                }
                else
                {
                    File.WriteAllText(filePath, richTextBox1.Text);
                }
            }
        }

        private void mnuExit2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                DialogResult result = MessageBox.Show(
                    "檔案尚未儲存，是否確定要關閉",
                    "未儲存的變更",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    isExiting = true;
                    this.Close();
                    autoSaveTimer.Stop();
                }
                else if (result == DialogResult.No)
                {
                    //
                }
            }
        }

        private void text_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isExiting && string.IsNullOrEmpty(filePath))
            {
                DialogResult result = MessageBox.Show(
                    "檔案尚未儲存，是否確定要關閉",
                    "未儲存的變更",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
                autoSaveTimer.Stop();
            }
            isExiting = false;
        }
    }
}
