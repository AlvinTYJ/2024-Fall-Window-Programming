using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e94115011_practice_6_1
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

        private float scaleFactor = 2.3f;
        BlockType selectedBlockType = BlockType.Grass;

        public Form1()
        {
            InitializeComponent();

            this.Icon = new Icon(@"..\..\Practice 6 Images\head.ico");

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

            pictureBox1 = new PictureBox
            {
                Size = new Size(150, 100),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = TitleImage,
                BackColor = Color.Transparent
            };

            panel1.Controls.Add(button1);
            panel1.Controls.Add(pictureBox1);
            CenterControlsByRatio();
            //////////
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
            map.MouseDown += map_MouseDown;

            stevePictureBox = new PictureBox
            {
                Size = new Size(40, 80),
                Image = SteveImage,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Black,
            };
            Point cellLocation = new Point(40, 80);
            stevePictureBox.Location = cellLocation;
            panel2.Controls.Add(stevePictureBox);

            CreateMap();

            Debug.WriteLine($"Current DPI: {this.DeviceDpi}");
            Debug.WriteLine($"Scale Factor: {this.DeviceDpi / 96f}");
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
            int buttonXPosition = (panel1.Width - button1.Width) / 2;
            int buttonYPosition = (panel1.Height - button1.Height) * 2 / 3;

            int pictureBoxXPosition = (panel1.Width - pictureBox1.Width) / 2;
            int pictureBoxYPosition = (panel1.Height - pictureBox1.Height) / 4;

            button1.Location = new Point(buttonXPosition, buttonYPosition);
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

            int originalImageWidth = TitleImage.Width;
            int originalImageHeight = TitleImage.Height;

            float aspectRatio2 = (float)originalImageWidth / originalImageHeight;

            int newWidth2 = (int)(panel1.Width * factor2);
            int newHeight2 = (int)(newWidth2 / aspectRatio2);

            pictureBox1.Size = new Size(newWidth2, newHeight2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.SendToBack();
            hScrollBar.BringToFront();
            vScrollBar.BringToFront();
            panel3.BringToFront();
            stevePictureBox.BringToFront();
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
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
                            Size = new Size(50, 50),
                            Enabled = false,
                            Margin = Padding.Empty,
                            Dock = DockStyle.Fill
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
                    Dock = DockStyle.Fill,
                    BackgroundImage = GrassImage,
                    BackgroundImageLayout = ImageLayout.Stretch,
                    Margin = Padding.Empty,
                    Enabled = false
                };
                map.Controls.Add(grassBlock, colIndex, rowIndex);
            }

            for (int i = 1; i <= dirtCells.Length; i++)
            {
                int colIndex = dirtCells[i - 1][0];
                int rowIndex = dirtCells[i - 1][1];

                Panel dirtBlock = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackgroundImage = DirtImage,
                    BackgroundImageLayout = ImageLayout.Stretch,
                    Margin = Padding.Empty,
                    Enabled = false
                };

                map.Controls.Add(dirtBlock, colIndex, rowIndex);

                if (i % 2 == 0)
                {
                    for (int row = rowIndex + 1; row < map.RowCount; row++)
                    {
                        Panel stoneBlock = new Panel
                        {
                            Dock = DockStyle.Fill,
                            BackgroundImage = StoneImage,
                            BackgroundImageLayout = ImageLayout.Stretch,
                            Margin = Padding.Empty,
                            Enabled = false
                        };
                        map.Controls.Add(stoneBlock, colIndex, row);
                    }
                }
            }
        }
    }
}
