using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e94115011_practice_4_2
{
    public partial class Form1 : Form
    {
        private Color Chose;
        private Color[] tabColors = new Color[2];
        private Image dogImage;
        private Image catImage;
        public Image Face0 { get; private set; }
        public Image Face1 { get; private set; }
        public Image Face2 { get; private set; }
        public Image Face3 { get; private set; }
        public Image Face4 { get; private set; }
        public Image Face5 { get; private set; }
        private Dictionary<string, Image> emojiImages;
        private string SelectedEmoji;
        private Random random;
        private bool isPlayingGame = false;

        public Form1()
        {
            InitializeComponent();
            dogImage = Image.FromFile(@"..\..\Practice4_Images\dog.png");
            catImage = Image.FromFile(@"..\..\Practice4_Images\cat.png");

            Face0 = Image.FromFile(@"..\..\Practice4_Images\0.png");
            Face1 = Image.FromFile(@"..\..\Practice4_Images\1.png");
            Face2 = Image.FromFile(@"..\..\Practice4_Images\2.png");
            Face3 = Image.FromFile(@"..\..\Practice4_Images\3.png");
            Face4 = Image.FromFile(@"..\..\Practice4_Images\4.png");
            Face5 = Image.FromFile(@"..\..\Practice4_Images\5.png");

            emojiImages = new Dictionary<string, Image>()
            {
                { "Face0", Face0},
                { "Face1", Face1},
                { "Face2", Face2},
                { "Face3", Face3},
                { "Face4", Face4},
                { "Face5", Face5}
            };

            richTextBox1.ReadOnly = true;
            richTextBox2.ReadOnly = true;
            richTextBox1.BackColor = this.BackColor;
            richTextBox2.BackColor = this.BackColor;

            if (tabControl1.SelectedIndex == 0)
            {
                Tab1DisplayMode();
            }

            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
            richTextBox1.DoubleClick += Form1_DoubleClick;
            richTextBox2.DoubleClick += Form1_DoubleClick;
            this.DoubleClick += Form1_DoubleClick;

            random = new Random();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Image button = Image.FromFile(@"..\..\Practice4_Images\button.png");
            Image resizedImage = ResizeImage(button, 40, 40);
            button2.Image = resizedImage;
            button2.ImageAlign = ContentAlignment.MiddleCenter;
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGamingState();
            if (tabControl1.SelectedIndex == 0)
            {
                Tab1DisplayMode();
            }
            else
            {
                Tab2DisplayMode();
            }
        }

        private void UpdateGamingState()
        {
            if (isPlayingGame && tabControl1.SelectedIndex == 1)
            {
                textBox1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else if (isPlayingGame && tabControl1.SelectedIndex == 0)
            {
                textBox1.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
            }
        }

        private void Tab1DisplayMode()
        {
            richTextBox1.Visible = true;
            richTextBox2.Visible = false;
            this.BackColor = tabColors[0];
        }

        private void Tab2DisplayMode()
        {
            richTextBox1.Visible = false;
            richTextBox2.Visible = true;
            this.BackColor = tabColors[1];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                string message = textBox1.Text;
                textBox1.Clear();

                if (isPlayingGame)
                {
                    if (message == "剪刀" || message == "石頭" || message == "布")
                    {
                        InsertMessageWithAvatarRight(richTextBox1, tabPage1.Text, message, dogImage);
                        InsertMessageWithAvatarLeft(richTextBox2, tabPage1.Text, message, dogImage);

                        string kaibwibo = GetRandomRPS();
                        InsertMessageWithAvatarLeft(richTextBox1, tabPage2.Text, kaibwibo, catImage);
                        InsertMessageWithAvatarRight(richTextBox2, tabPage2.Text, kaibwibo, catImage);

                        isPlayingGame = false;
                        button2.Enabled = true;
                    }
                    else
                    {
                        textBox1.Clear();
                    }
                }
                else if (tabControl1.SelectedIndex == 0 && message == "猜拳")
                {
                    isPlayingGame = true;
                    button2.Enabled = false;
                    InsertMessageWithAvatarRight(richTextBox1, tabPage1.Text, message, dogImage);
                    InsertMessageWithAvatarLeft(richTextBox2, tabPage1.Text, message, dogImage);

                    string reply = "游戲開始";
                    InsertMessageWithAvatarLeft(richTextBox1, tabPage2.Text, reply, catImage);
                    InsertMessageWithAvatarRight(richTextBox2, tabPage2.Text, reply, catImage);
                }
                else if (tabControl1.SelectedIndex == 0)
                {
                    InsertMessageWithAvatarRight(richTextBox1, tabPage1.Text, message, dogImage);
                    InsertMessageWithAvatarLeft(richTextBox2, tabPage1.Text, message, dogImage);
                    if (message == "汪！")
                    {
                        string reply = "喵";
                        InsertMessageWithAvatarLeft(richTextBox1, tabPage2.Text, reply, catImage);
                        InsertMessageWithAvatarRight(richTextBox2, tabPage2.Text, reply, catImage);
                    }
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    InsertMessageWithAvatarLeft(richTextBox1, tabPage2.Text, message, catImage);
                    InsertMessageWithAvatarRight(richTextBox2, tabPage2.Text, message, catImage);
                    if (message == "喵！")
                    {
                        string reply = "汪";
                        InsertMessageWithAvatarRight(richTextBox1, tabPage1.Text, reply, catImage);
                        InsertMessageWithAvatarLeft(richTextBox2, tabPage1.Text, reply, catImage);
                    }
                }
            }
        }


        private void InsertMessageWithAvatarLeft(RichTextBox richTextBox, string userName, string message, Image avatar)
        {
            richTextBox.ReadOnly = false;
            richTextBox.SelectionAlignment = HorizontalAlignment.Left;
            Image resizedImage = ResizeImage(avatar, 15, 15);
            Image newBackgroundImage = SetImageBackgroundColor(resizedImage, SystemColors.Control);
            Clipboard.SetImage(newBackgroundImage);
            richTextBox.Paste();
            richTextBox.AppendText($"{userName}: {message}" + Environment.NewLine);
            richTextBox.SelectionStart = richTextBox.Text.Length;
            richTextBox.ScrollToCaret();
            richTextBox.ReadOnly = true;
        }

        private void InsertMessageWithAvatarRight(RichTextBox richTextBox, string userName, string message, Image avatar)
        {
            richTextBox.ReadOnly = false;
            richTextBox.SelectionAlignment = HorizontalAlignment.Right;
            Image resizedImage = ResizeImage(avatar, 15, 15);
            Image newBackgroundImage = SetImageBackgroundColor(resizedImage, SystemColors.Control);
            Clipboard.SetImage(newBackgroundImage);
            richTextBox.Paste();
            richTextBox.AppendText($"{userName}: {message}" + Environment.NewLine);
            richTextBox.SelectionStart = richTextBox.Text.Length;
            richTextBox.ScrollToCaret();
            richTextBox.ReadOnly = true;
        }

        private void InsertEmojiWithAvatarLeft(RichTextBox richTextBox, string userName, string emoji, Image avatar)
        {
            richTextBox.ReadOnly = false;
            richTextBox.SelectionAlignment = HorizontalAlignment.Left;

            Image resizedImage = ResizeImage(avatar, 15, 15);
            Image newBackgroundImage = SetImageBackgroundColor(resizedImage, SystemColors.Control);
            Clipboard.SetImage(newBackgroundImage);
            richTextBox.Paste();

            richTextBox.AppendText($"{userName}: ");

            Image resizedEmoji = ResizeImage(emojiImages[emoji], 15, 15);
            Image newBackgroundEmoji = SetImageBackgroundColor(resizedEmoji, SystemColors.Control);
            Clipboard.SetImage(newBackgroundEmoji);
            richTextBox.Paste();

            richTextBox.AppendText(Environment.NewLine);
            richTextBox.SelectionStart = richTextBox.Text.Length;
            richTextBox.ScrollToCaret();
            richTextBox.ReadOnly = true;
        }

        private void InsertEmojiWithAvatarRight(RichTextBox richTextBox, string userName, string emoji, Image avatar)
        {
            richTextBox.ReadOnly = false;
            richTextBox.SelectionAlignment = HorizontalAlignment.Right;

            Image resizedImage = ResizeImage(avatar, 15, 15);
            Image newBackgroundImage = SetImageBackgroundColor(resizedImage, SystemColors.Control);
            Clipboard.SetImage(newBackgroundImage);
            richTextBox.Paste();

            richTextBox.AppendText($"{userName}: ");

            Image resizedEmoji = ResizeImage(emojiImages[emoji], 15, 15);
            Image newBackgroundEmoji = SetImageBackgroundColor(resizedEmoji, SystemColors.Control);
            Clipboard.SetImage(newBackgroundEmoji);
            richTextBox.Paste();

            richTextBox.AppendText(Environment.NewLine);
            richTextBox.SelectionStart = richTextBox.Text.Length;
            richTextBox.ScrollToCaret();
            richTextBox.ReadOnly = true;
        }

        private Image ResizeImage(Image image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, width, height);
            }

            return resizedImage;
        }

        private Image SetImageBackgroundColor(Image image, Color backgroundColor)
        {
            Bitmap newImage = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.Clear(backgroundColor);
                g.DrawImage(image, 0, 0, image.Width, image.Height);
            }

            return newImage;
        }

        private string GetRandomRPS()
        {
            string[] choices = {"剪刀", "石頭", "布"};
            int index = random.Next(choices.Length);
            return choices[index];
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

        private void button2_Click(object sender, EventArgs e)
        {
            表情符號選擇 form3 = new 表情符號選擇(this);
            if (form3.ShowDialog() == DialogResult.OK)
            {
                SelectedEmoji = form3.SelectedFace;
                if (tabControl1.SelectedIndex == 0)
                {
                    InsertEmojiWithAvatarRight(richTextBox1, tabPage1.Text, SelectedEmoji, dogImage);
                    InsertEmojiWithAvatarLeft(richTextBox2, tabPage1.Text, SelectedEmoji, dogImage);
                }
                else
                {
                    InsertEmojiWithAvatarLeft(richTextBox1, tabPage2.Text, SelectedEmoji, catImage);
                    InsertEmojiWithAvatarRight(richTextBox2, tabPage2.Text, SelectedEmoji, catImage);
                }
            }
        }
    }
}