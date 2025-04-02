using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e94115011_practice_6_2
{
    public partial class Form1 : Form
    {
        private Panel panel1;
        private Button button1;
        private PictureBox pictureBox1;
        private Panel panel2;
        private TableLayoutPanel map;
        private VScrollBar vScrollBar;
        private HScrollBar hScrollBar;
        private Panel panel3;
        private PictureBox selectedPictureBox;
        private PictureBox stevePictureBox;
        private Button button2;
        private Button button3;
        private TransparentPanel settingsPanel;
        private Button button4;
        private Button button5;
        private Button button6;

        Image Background = Image.FromFile(@"..\..\Practice 6 Images\background.jpg");
        Image GrassImage = Image.FromFile(@"..\..\Practice 6 Images\block1.png");
        Image DirtImage = Image.FromFile(@"..\..\Practice 6 Images\block2.png");
        Image StoneImage = Image.FromFile(@"..\..\Practice 6 Images\block3.png");
        Image WoolImage = Image.FromFile(@"..\..\Practice 6 Images\block4.png");
        Image HeadImage = Image.FromFile(@"..\..\Practice 6 Images\head.png");
        Image InventoryImage = Image.FromFile(@"..\..\Practice 6 Images\inventory.png");
        Image SelectedImage = Image.FromFile(@"..\..\Practice 6 Images\selected.png");
        Image SteveImage = Image.FromFile(@"..\..\Practice 6 Images\steve.png");
        Image TitleImage = Image.FromFile(@"..\..\Practice 6 Images\title.png");

        private float scaleFactor;
        BlockType selectedBlockType;

        public Form1()
        {
            InitializeComponent();

            this.Icon = new Icon(@"..\..\Practice 6 Images\head.ico");
            scaleFactor = 2.3f;
            selectedBlockType = BlockType.Grass;

            panel1 = new Panel
            {
                Size = new Size(1000, 700),
                Location = new Point(0, 0),
                Dock = DockStyle.Fill,
                BackgroundImage = Background,
                BackgroundImageLayout = ImageLayout.Stretch
            };
            this.Controls.Add(panel1);
            panel1.Resize += new EventHandler(Panel1_Resize);

            button1 = new Button
            {
                Text = "開新游戲",
                Size = new Size(220, 30),
                BackColor = Color.Black,
                ForeColor = Color.White,
                AutoSize = true
            };
            button1.Click += new EventHandler(button1_Click);

            button2 = new Button
            {
                Text = "開啓存檔",
                Size = new Size(220, 30),
                BackColor = Color.Black,
                ForeColor = Color.White,
                AutoSize = true
            };
            button2.Click += new EventHandler(button2_Click);

            button3 = new Button
            {
                Text = "離開游戲",
                Size = new Size(220, 30),
                BackColor = Color.Black,
                ForeColor = Color.White,
                AutoSize = true
            };
            button3.Click += new EventHandler(button3_Click);

            pictureBox1 = new PictureBox
            {
                Size = new Size(150, 100),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = TitleImage,
                BackColor = Color.Transparent
            };

            panel1.Controls.Add(button1);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(pictureBox1);
            CenterControlsByRatio();
            ////////////////////
            this.Resize += new EventHandler(Form1_Resize);

            map = new TableLayoutPanel
            {
                RowCount = 15,
                ColumnCount = 30,
                Size = new Size(30 * 40, 15 * 40),
                BackColor = Color.Black,
                //CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                Margin = new Padding(0)
            };
            for (int i = 0; i < 15; i++)
            {
                map.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            }
            for (int i = 0; i < 30; i++)
            {
                map.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40));
            }

            panel2 = new Panel
            {
                Size = new Size(map.Width, map.Height),
                Location = new Point(0, 0),
                AutoScroll = false
            };
            this.Controls.Add(panel2);
            panel2.Controls.Add(map);
            map.Location = new Point(0, 0);

            vScrollBar = new VScrollBar
            {
                SmallChange = 100,
                LargeChange = 100,
            };
            vScrollBar.Scroll += VScrollBar_Scroll;
            this.Controls.Add(vScrollBar);

            hScrollBar = new HScrollBar
            {
                SmallChange = 300,
                LargeChange = 300,
            };
            hScrollBar.Scroll += HScrollBar_Scroll;
            this.Controls.Add(hScrollBar);

            UpdateScrollBarSizes();

            panel3 = new Panel
            {
                Size = new Size(414, 50),
                BackColor = Color.Transparent
            };
            this.Controls.Add(panel3);
            Image originalImage = InventoryImage;
            int newWidth = (int)(originalImage.Width * scaleFactor);
            int newHeight = (int)(originalImage.Height * scaleFactor);
            Bitmap scaledImage = new Bitmap(newWidth, newHeight);

            using (Graphics g = Graphics.FromImage(scaledImage))
            {
                g.DrawImage(originalImage, 0, 0, newWidth, newHeight);
            }
            panel3.BackgroundImage = scaledImage;
            originalImage.Dispose();
            Panel2CenterControlsByRatio();

            AddImageToPanel3(GrassImage, new Point(12, 12), new Size(26, 26));
            AddImageToPanel3(DirtImage, new Point(58, 12), new Size(26, 26));
            AddImageToPanel3(StoneImage, new Point(104, 12), new Size(26, 26));
            AddImageToPanel3(WoolImage, new Point(150, 12), new Size(26, 26));

            AddSelectedImage(GrassImage);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.KeyUp += new KeyEventHandler(Form1_KeyUp);
            map.MouseDown += map_MouseDown;

            stevePictureBox = new PictureBox
            {
                Size = new Size(35, 70),
                Image = SteveImage,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Black,
            };
            Point cellLocation = new Point(40, 90);
            stevePictureBox.Location = cellLocation;
            panel2.Controls.Add(stevePictureBox);

            CreateMap();

            Timer gameTimer = new Timer();
            gameTimer.Interval = 20;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
            ////////////////////
            settingsPanel = new TransparentPanel
            {
                Size = new Size(1000, 700),
                Location = new Point(0, 0),
                Dock = DockStyle.Fill,
                Visible = false,
                BackColor = Color.Black,
            };
            this.Controls.Add(settingsPanel);
            settingsPanel.Resize += new EventHandler(settingsPanel_Resize);

            button4 = new Button
            {
                Text = "回到游戲",
                Size = new Size(220, 30),
                BackColor = Color.Black,
                ForeColor = Color.White,
                AutoSize = true
            };
            button4.Click += new EventHandler(button4_Click);

            button5 = new Button
            {
                Text = "儲存並回到主頁面",
                Size = new Size(220, 30),
                BackColor = Color.Black,
                ForeColor = Color.White,
                AutoSize = true
            };
            button5.Click += new EventHandler(button5_Click);

            button6 = new Button
            {
                Text = "回到標題畫面",
                Size = new Size(220, 30),
                BackColor = Color.Black,
                ForeColor = Color.White,
                AutoSize = true
            };
            button6.Click += new EventHandler(button6_Click);

            settingsPanel.Controls.Add(button4);
            settingsPanel.Controls.Add(button5);
            settingsPanel.Controls.Add(button6);
            settingsPanelCenterControlsByRatio();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            UpdateScrollBarSizes();
            Panel2CenterControlsByRatio();
            this.MaximumSize = new Size(map.Width + vScrollBar.Width, map.Height + hScrollBar.Height);
        }

        private void UpdateScrollBarSizes()
        {
            vScrollBar.Location = new Point(this.ClientSize.Width - vScrollBar.Width, 0);
            vScrollBar.Height = this.ClientSize.Height - hScrollBar.Height;
            vScrollBar.Maximum = Math.Max(0, (map.Height - this.ClientSize.Height) * 3 / 2);
            vScrollBar.Minimum = 0;

            hScrollBar.Location = new Point(0, this.ClientSize.Height - hScrollBar.Height);
            hScrollBar.Width = this.ClientSize.Width - vScrollBar.Width;
            hScrollBar.Maximum = Math.Max(0, (map.Width - this.ClientSize.Width) * 3 / 2);
            hScrollBar.Minimum = 0;
        }

        private void Panel1_Resize(object sender, EventArgs e)
        {
            CenterControlsByRatio();
        }

        private void CenterControlsByRatio()
        {
            ResizeTitleControls();
            int button1_XPosition = (panel1.Width - button1.Width) / 2;
            int button1_YPosition = (panel1.Height - button1.Height) * 3 / 6;

            int button2_XPosition = (panel1.Width - button2.Width) / 2;
            int button2_YPosition = (panel1.Height - button2.Height) * 4 / 6;

            int button3_XPosition = (panel1.Width - button3.Width) / 2;
            int button3_YPosition = (panel1.Height - button3.Height) * 5 / 6;

            int pictureBoxXPosition = (panel1.Width - pictureBox1.Width) / 2;
            int pictureBoxYPosition = (panel1.Height - pictureBox1.Height) / 4;

            button1.Location = new Point(button1_XPosition, button1_YPosition);
            button2.Location = new Point(button2_XPosition, button2_YPosition);
            button3.Location = new Point(button3_XPosition, button3_YPosition);
            pictureBox1.Location = new Point(pictureBoxXPosition, pictureBoxYPosition);
        }

        private void ResizeTitleControls()
        {
            float factor1 = 0.4f;
            float factor2 = 0.6f;

            int originalButtonWidth = button1.Width;
            int originalButtonHeight = button1.Height;

            float aspectRatio1 = (float)originalButtonWidth / originalButtonHeight;

            int newWidth1 = (int)(panel1.Width * factor1);
            int newHeight1 = (int)(newWidth1 / aspectRatio1);

            button1.Size = new Size(newWidth1, newHeight1);
            button2.Size = new Size(newWidth1, newHeight1);
            button3.Size = new Size(newWidth1, newHeight1);

            int originalImageWidth = TitleImage.Width;
            int originalImageHeight = TitleImage.Height;

            float aspectRatio2 = (float)originalImageWidth / originalImageHeight;

            int newWidth2 = (int)(panel1.Width * factor2);
            int newHeight2 = (int)(newWidth2 / aspectRatio2);

            pictureBox1.Size = new Size(newWidth2, newHeight2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewGame();
            selectedBlockType = BlockType.Grass;
            UpdateSelectedImagePosition(GrassImage);
            panel1.Visible = false;
            hScrollBar.BringToFront();
            vScrollBar.BringToFront();
            panel3.BringToFront();
            stevePictureBox.BringToFront();
            settingsPanel.BringToFront();
            this.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(@"..\..\savefile.dat"))
                {
                    LoadGame(@"..\..\savefile.dat");
                    switch (selectedBlockType)
                    {
                        case BlockType.Grass:
                            UpdateSelectedImagePosition(GrassImage);
                            break;
                        case BlockType.Dirt:
                            UpdateSelectedImagePosition(DirtImage);
                            break;
                        case BlockType.Stone:
                            UpdateSelectedImagePosition(StoneImage);
                            break;
                        case BlockType.Wool:
                            UpdateSelectedImagePosition(WoolImage);
                            break;
                    }
                    panel1.Visible = false;
                    hScrollBar.BringToFront();
                    vScrollBar.BringToFront();
                    panel3.BringToFront();
                    stevePictureBox.BringToFront();
                    settingsPanel.BringToFront();
                    this.Focus();
                }
                else
                {
                    MessageBox.Show("Save file does not exist.", "Load Game Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading the game: " + ex.Message, "Load Game Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            panel2.Location = new Point(panel2.Location.X, -vScrollBar.Value);
        }

        private void HScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            panel2.Location = new Point(-hScrollBar.Value, panel2.Location.Y);
        }

        private void Panel2CenterControlsByRatio()
        {
            int panel3_XPosition = (this.Width - panel3.Width - vScrollBar.Width) / 2;
            int panel3_YPosition = (this.Height - panel3.Height) * 5 / 6;
            panel3.Location = new Point(panel3_XPosition, panel3_YPosition);
        }

        private void settingsPanel_Resize(object sender, EventArgs e)
        {
            settingsPanelCenterControlsByRatio();
        }

        private void settingsPanelCenterControlsByRatio()
        {
            float factor1 = 0.4f;

            int originalButtonWidth = button4.Width;
            int originalButtonHeight = button4.Height;

            float aspectRatio1 = (float)originalButtonWidth / originalButtonHeight;

            int newWidth1 = (int)(settingsPanel.Width * factor1);
            int newHeight1 = (int)(newWidth1 / aspectRatio1);

            button4.Size = new Size(newWidth1, newHeight1);
            button5.Size = new Size(newWidth1, newHeight1);
            button6.Size = new Size(newWidth1, newHeight1);

            int button4_XPosition = (settingsPanel.Width - button4.Width) / 2;
            int button4_YPosition = (settingsPanel.Height - button4.Height) * 2 / 6;

            int button5_XPosition = (settingsPanel.Width - button5.Width) / 2;
            int button5_YPosition = (settingsPanel.Height - button5.Height) * 3 / 6;

            int button6_XPosition = (settingsPanel.Width - button6.Width) / 2;
            int button6_YPosition = (settingsPanel.Height - button6.Height) * 4 / 6;

            button4.Location = new Point(button4_XPosition, button4_YPosition);
            button5.Location = new Point(button5_XPosition, button5_YPosition);
            button6.Location = new Point(button6_XPosition, button6_YPosition);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            settingsPanel.Visible = !settingsPanel.Visible;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                SaveGame(@"..\..\savefile.dat");
                MessageBox.Show("Game saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panel1.Visible = true;
                panel1.BringToFront();
                settingsPanel.Visible = !settingsPanel.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving game: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel1.BringToFront();
            settingsPanel.Visible = !settingsPanel.Visible;
        }

        private void AddImageToPanel3(Image image, Point location, Size size)
        {
            PictureBox pictureBox = new PictureBox
            {
                Image = image,
                Location = location,
                Size = size,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            panel3.Controls.Add(pictureBox);
        }

        private void AddSelectedImage(Image image)
        {
            PictureBox blockPictureBox = panel3.Controls.OfType<PictureBox>().FirstOrDefault(p => p.Image == image);

            if (blockPictureBox != null)
            {
                Point blockLocation = blockPictureBox.Location;
                Size blockSize = blockPictureBox.Size;

                Point selectedImageLocation = new Point(
                    blockLocation.X + (blockSize.Width - 55) / 2,
                    blockLocation.Y + (blockSize.Height - 55) / 2
                );

                selectedPictureBox = new PictureBox
                {
                    Image = SelectedImage,
                    Location = selectedImageLocation,
                    Size = new Size(55, 55),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                };

                panel3.Controls.Add(selectedPictureBox);
                blockPictureBox.BringToFront();
            }
        }

        private void UpdateSelectedImagePosition(Image image)
        {
            PictureBox blockPictureBox = panel3.Controls.OfType<PictureBox>().FirstOrDefault(p => p.Image == image);

            if (blockPictureBox != null)
            {
                Point blockLocation = blockPictureBox.Location;
                Size blockSize = blockPictureBox.Size;

                Point selectedImageLocation = new Point(
                    blockLocation.X + (blockSize.Width - 55) / 2,
                    blockLocation.Y + (blockSize.Height - 55) / 2
                );

                selectedPictureBox.Location = selectedImageLocation;
                blockPictureBox.BringToFront();
            }
        }

        enum BlockType
        {
            Grass,
            Dirt,
            Stone,
            Wool,
        }

        bool movingLeft = false;
        bool movingRight = false;
        bool isJumping = false;
        bool isGrounded = false;

        int jumpSpeed = 0;
        int gravity = 3;
        int jumpHeight = 9;
        int playerSpeed = 5;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                movingLeft = true;
            }
            if (e.KeyCode == Keys.D)
            {
                movingRight = true;
            }
            if (e.KeyCode == Keys.Space && !isJumping && isGrounded)
            {
                isJumping = true;
                jumpSpeed = jumpHeight;
            }
            if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
            {
                UpdateSelectedImagePosition(GrassImage);
                selectedBlockType = BlockType.Grass;
            }
            if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
            {
                UpdateSelectedImagePosition(DirtImage);
                selectedBlockType = BlockType.Dirt;
            }
            if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
            {
                UpdateSelectedImagePosition(StoneImage);
                selectedBlockType = BlockType.Stone;
            }
            if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
            {
                UpdateSelectedImagePosition(WoolImage);
                selectedBlockType = BlockType.Wool;
            }

            if (e.KeyCode == Keys.Escape)
            {
                settingsPanel.Visible = !settingsPanel.Visible;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                movingLeft = false;
            }
            if (e.KeyCode == Keys.D)
            {
                movingRight = false;
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            bool isOnBlock = false;

            if (movingLeft && stevePictureBox.Left > 0)
            {
                bool canMoveLeft = true;
                int maxAllowedMoveLeft = playerSpeed;

                foreach (Control block in map.Controls)
                {
                    if (block is Panel)
                    {
                        bool horizontallyAligned = (stevePictureBox.Bottom > block.Top && stevePictureBox.Top < block.Bottom);

                        if (horizontallyAligned && stevePictureBox.Left >= block.Right && stevePictureBox.Left - playerSpeed < block.Right)
                        {
                            maxAllowedMoveLeft = stevePictureBox.Left - block.Right;
                            canMoveLeft = false;
                            break;
                        }
                    }
                }
                if (canMoveLeft)
                {
                    stevePictureBox.Left -= playerSpeed;
                }
                else
                {
                    stevePictureBox.Left -= maxAllowedMoveLeft;
                }
            }

            if (movingRight && stevePictureBox.Right < this.ClientSize.Width)
            {
                bool canMoveRight = true;
                int maxAllowedMoveRight = playerSpeed;

                foreach (Control block in map.Controls)
                {
                    if (block is Panel)
                    {
                        bool horizontallyAligned = (stevePictureBox.Bottom > block.Top && stevePictureBox.Top < block.Bottom);

                        if (horizontallyAligned && stevePictureBox.Right <= block.Left && stevePictureBox.Right + playerSpeed > block.Left)
                        {
                            maxAllowedMoveRight = block.Left - stevePictureBox.Right;
                            canMoveRight = false;
                            break;
                        }
                    }
                }
                if (canMoveRight)
                {
                    stevePictureBox.Left += playerSpeed;
                }
                else
                {
                    stevePictureBox.Left += maxAllowedMoveRight;
                }
            }

            if (!isJumping)
            {
                stevePictureBox.Top += gravity;
            }
            if (isJumping)
            {
                stevePictureBox.Top -= jumpSpeed;
                jumpSpeed -= 1;

                if (jumpSpeed <= 0)
                {
                    isJumping = false;
                }
            }

            foreach (Control block in map.Controls)
            {
                if (block is Panel)
                {
                    bool verticallyAligned = (stevePictureBox.Right > block.Left && stevePictureBox.Left < block.Right);
                    bool isFalling = !isJumping && jumpSpeed <= 0;

                    if (verticallyAligned && stevePictureBox.Bottom >= block.Top && stevePictureBox.Top < block.Top && isFalling)
                    {
                        isOnBlock = true;
                        isGrounded = true;
                        stevePictureBox.Top = block.Top - stevePictureBox.Height;
                        jumpSpeed = 0;
                        break;
                    }

                    bool isJumpingAndColliding = (stevePictureBox.Top < block.Bottom &&
                                       stevePictureBox.Top > block.Top && jumpSpeed > 0);

                    if (verticallyAligned && isJumpingAndColliding)
                    {
                        jumpSpeed = -jumpSpeed;
                        stevePictureBox.Top = block.Bottom;
                        break;
                    }
                }
            }

            if (!isOnBlock && !isJumping)
            {
                isGrounded = false;
                stevePictureBox.Top += gravity;
            }

            if (stevePictureBox.Bottom >= map.Height)
            {
                isGrounded = true;
                stevePictureBox.Top = map.Height - stevePictureBox.Height;
                jumpSpeed = 0;
            }
        }

        private void map_MouseDown(object sender, MouseEventArgs e)
        {
            Point dropPoint = map.PointToClient(Cursor.Position);
            int colIndex = dropPoint.X / (map.Width / map.ColumnCount);
            int rowIndex = dropPoint.Y / (map.Height / map.RowCount);

            if (colIndex >= 0 && rowIndex >= 0 && colIndex < map.ColumnCount && rowIndex < map.RowCount)
            {
                if (e.Button == MouseButtons.Right)
                {
                    Control existingControl = map.GetControlFromPosition(colIndex, rowIndex);
                    if (existingControl == null)
                    {
                        Panel newBlock = new Panel
                        {
                            Size = new Size(40, 40),
                            Enabled = false,
                            Margin = Padding.Empty,
                            Dock = DockStyle.Fill,
                            Tag = selectedBlockType
                        };

                        switch (selectedBlockType)
                        {
                            case BlockType.Grass:
                                newBlock.BackgroundImage = GrassImage;
                                newBlock.BackgroundImageLayout = ImageLayout.Stretch;
                                break;
                            case BlockType.Dirt:
                                newBlock.BackgroundImage = DirtImage;
                                newBlock.BackgroundImageLayout = ImageLayout.Stretch;
                                break;
                            case BlockType.Stone:
                                newBlock.BackgroundImage = StoneImage;
                                newBlock.BackgroundImageLayout = ImageLayout.Stretch;
                                break;
                            case BlockType.Wool:
                                newBlock.BackgroundImage = WoolImage;
                                newBlock.BackgroundImageLayout = ImageLayout.Stretch;
                                break;
                        }
                        map.Controls.Add(newBlock, colIndex, rowIndex);
                    }
                }
                else if (e.Button == MouseButtons.Left)
                {
                    Control controlToRemove = map.GetControlFromPosition(colIndex, rowIndex);
                    if (controlToRemove != null)
                    {
                        map.Controls.Remove(controlToRemove);
                    }
                }
            }
        }

        private void CreateMap()
        {
            int[][] grassCells = new int[][]
            {
                new int[] { 0, 5 },
                new int[] { 1, 4 },
                new int[] { 2, 4 },
                new int[] { 3, 4 },
                new int[] { 4, 5 },
                new int[] { 5, 6 },
                new int[] { 6, 6 },
                new int[] { 7, 5 },
                new int[] { 8, 5 },
                new int[] { 9, 5 },
                new int[] { 10, 5 },
                new int[] { 11, 4 },
                new int[] { 12, 4 },
                new int[] { 13, 4 },
                new int[] { 14, 5 },
                new int[] { 15, 6 },
                new int[] { 16, 6 },
                new int[] { 17, 5 },
                new int[] { 18, 5 },
                new int[] { 19, 5 },
                new int[] { 20, 5 },
                new int[] { 21, 4 },
                new int[] { 22, 4 },
                new int[] { 23, 4 },
                new int[] { 24, 5 },
                new int[] { 25, 6 },
                new int[] { 26, 6 },
                new int[] { 27, 5 },
                new int[] { 28, 5 },
                new int[] { 29, 5 }
            };

            int[][] dirtCells = new int[][]
            {
                new int[] { 0, 6 }, new int[] { 0, 7 },
                new int[] { 1, 5 }, new int[] { 1, 6 },
                new int[] { 2, 5 }, new int[] { 2, 6 },
                new int[] { 3, 5 }, new int[] { 3, 6 },
                new int[] { 4, 6 }, new int[] { 4, 7 },
                new int[] { 5, 7 }, new int[] { 5, 8 },
                new int[] { 6, 7 }, new int[] { 6, 8 },
                new int[] { 7, 6 }, new int[] { 7, 7 },
                new int[] { 8, 6 }, new int[] { 8, 7 },
                new int[] { 9, 6 }, new int[] { 9, 7 },
                new int[] { 10, 6 }, new int[] { 10, 7 },
                new int[] { 11, 5 }, new int[] { 11, 6 },
                new int[] { 12, 5 }, new int[] { 12, 6 },
                new int[] { 13, 5 }, new int[] { 13, 6 },
                new int[] { 14, 6 }, new int[] { 14, 7 },
                new int[] { 15, 7 }, new int[] { 15, 8 },
                new int[] { 16, 7 }, new int[] { 16, 8 },
                new int[] { 17, 6 }, new int[] { 17, 7 },
                new int[] { 18, 6 }, new int[] { 18, 7 },
                new int[] { 19, 6 }, new int[] { 19, 7 },
                new int[] { 20, 6 }, new int[] { 20, 7 },
                new int[] { 21, 5 }, new int[] { 21, 6 },
                new int[] { 22, 5 }, new int[] { 22, 6 },
                new int[] { 23, 5 }, new int[] { 23, 6 },
                new int[] { 24, 6 }, new int[] { 24, 7 },
                new int[] { 25, 7 }, new int[] { 25, 8 },
                new int[] { 26, 7 }, new int[] { 26, 8 },
                new int[] { 27, 6 }, new int[] { 27, 7 },
                new int[] { 28, 6 }, new int[] { 28, 7 },
                new int[] { 29, 6 }, new int[] { 29, 7 },
            };

            for (int i = 1; i <= grassCells.Length; i++)
            {
                int colIndex = grassCells[i - 1][0];
                int rowIndex = grassCells[i - 1][1];

                Panel grassBlock = new Panel
                {
                    Size = new Size(40, 40),
                    Enabled = false,
                    Margin = Padding.Empty,
                    Dock = DockStyle.Fill,
                    BackgroundImage = GrassImage,
                    BackgroundImageLayout = ImageLayout.Stretch,
                    Tag = BlockType.Grass
                };
                map.Controls.Add(grassBlock, colIndex, rowIndex);
            }

            for (int i = 1; i <= dirtCells.Length; i++)
            {
                int colIndex = dirtCells[i - 1][0];
                int rowIndex = dirtCells[i - 1][1];

                Panel dirtBlock = new Panel
                {
                    Size = new Size(40, 40),
                    Enabled = false,
                    Margin = Padding.Empty,
                    Dock = DockStyle.Fill,
                    BackgroundImage = DirtImage,
                    BackgroundImageLayout = ImageLayout.Stretch,
                    Tag = BlockType.Dirt
                };

                map.Controls.Add(dirtBlock, colIndex, rowIndex);

                if (i % 2 == 0)
                {
                    for (int row = rowIndex + 1; row < map.RowCount; row++)
                    {
                        Panel stoneBlock = new Panel
                        {
                            Size = new Size(40, 40),
                            Enabled = false,
                            Margin = Padding.Empty,
                            Dock = DockStyle.Fill,
                            BackgroundImage = StoneImage,
                            BackgroundImageLayout = ImageLayout.Stretch,
                            Tag = BlockType.Stone
                        };
                        map.Controls.Add(stoneBlock, colIndex, row);
                    }
                }
            }
        }
        private void NewGame()
        {
            map.Controls.Clear();
            selectedBlockType = BlockType.Grass;
            stevePictureBox.Location = new Point(40, 90);
            CreateMap();
        }

        private void SaveGame(string filePath)
        {
            // Create a GameData object to store game state
            GameData gameData = new GameData
            {
                CharacterPosition = new Point(stevePictureBox.Left, stevePictureBox.Top),
                Blocks = new List<BlockData>(),
                selected = selectedBlockType.ToString(),
            };

            // Save all block data from the map
            foreach (Control control in map.Controls)
            {
                if (control is Panel)
                {
                    // Calculate the grid position
                    int colIndex = map.GetColumn(control);
                    int rowIndex = map.GetRow(control);
                    BlockType blockType = GetBlockTypeFromControl(control); // Get block type from Tag

                    BlockData blockData = new BlockData
                    {
                        X = colIndex * 40,
                        Y = rowIndex * 40,
                        BlockType = blockType.ToString() // Store block type as string or enum
                    };
                    gameData.Blocks.Add(blockData);
                }
            }

            // Serialize the game data to a file
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, gameData);
            }
        }

        private BlockType GetBlockTypeFromControl(Control control)
        {
            // Ensure that the control's Tag is of type BlockType
            if (control.Tag is BlockType blockType)
            {
                return blockType; // Return the block type directly
            }

            throw new InvalidOperationException("Unknown block type");
        }

        private void LoadGame(string filePath)
        {
            // Deserialize the game data from the file
            GameData gameData;
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                gameData = (GameData)formatter.Deserialize(fs);
                selectedBlockType = (BlockType)Enum.Parse(typeof(BlockType), gameData.selected);
            }

            // Restore the character position
            stevePictureBox.Left = gameData.CharacterPosition.X;
            stevePictureBox.Top = gameData.CharacterPosition.Y;

            // Clear the existing blocks
            map.Controls.Clear();

            // Restore all block data to the map
            foreach (BlockData block in gameData.Blocks)
            {
                Panel newBlock = new Panel
                {
                    Size = new Size(40, 40),
                    Enabled = false,
                    Margin = Padding.Empty,
                    Dock = DockStyle.Fill,
                    Tag = Enum.Parse(typeof(BlockType), block.BlockType) // Set the Tag to the block type
                };

                // Set the background image based on the block type
                switch (block.BlockType)
                {
                    case "Grass":
                        newBlock.BackgroundImage = GrassImage;
                        newBlock.BackgroundImageLayout = ImageLayout.Stretch;
                        break;
                    case "Dirt":
                        newBlock.BackgroundImage = DirtImage;
                        newBlock.BackgroundImageLayout = ImageLayout.Stretch;
                        break;
                    case "Stone":
                        newBlock.BackgroundImage = StoneImage;
                        newBlock.BackgroundImageLayout = ImageLayout.Stretch;
                        break;
                    case "Wool":
                        newBlock.BackgroundImage = WoolImage;
                        newBlock.BackgroundImageLayout = ImageLayout.Stretch;
                        break;
                }

                // Calculate the grid position based on the saved X and Y values
                int colIndex = block.X / 40; // Assuming each block is 40x40
                int rowIndex = block.Y / 40;

                // Add the block to the map at the calculated position
                map.Controls.Add(newBlock, colIndex, rowIndex);
            }
        }

    }

    public class TransparentPanel : Panel
    {
        private int opacity = 120;
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(opacity, Color.DarkGray)))
            {
                e.Graphics.FillRectangle(brush, e.ClipRectangle);
            }
        }
    }

    [Serializable]
    public class BlockData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string BlockType { get; set; }
    }

    [Serializable]
    public class GameData
    {
        public Point CharacterPosition { get; set; }
        public List<BlockData> Blocks { get; set; }
        public string selected { get; set; }
    }
}
