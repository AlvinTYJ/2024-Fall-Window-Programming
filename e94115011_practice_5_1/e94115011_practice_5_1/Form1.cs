using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e94115011_practice_5_1
{
    public partial class Form1 : Form
    {
        private int plateSpeed = 10;
        private int caughtFruits = 0;
        private int missedFruits = 0;
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            plate.Visible = true;
            label1.Visible = true;
            StartFruitFall();
            UpdateLabel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            plate.Visible = false;
            label1.Visible = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift == true)
            {
                plateSpeed = 20;
            }
            else
            {
                plateSpeed = 10;
            }
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                if (plate.Left - plateSpeed >= 0)
                {
                    plate.Left -= plateSpeed;
                }
                else
                {
                    plate.Left = 0;
                }
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                if (plate.Right + plateSpeed <= this.ClientSize.Width)
                {
                    plate.Left += plateSpeed;
                }
                else
                {
                    plate.Left = this.ClientSize.Width - plate.Width;
                }
            }
        }

        Random rand = new Random();
        private void StartFruitFall()
        {
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void UpdateLabel()
        {
            label1.Text = $"{caughtFruits}/{missedFruits}";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Panel fruit = new Panel
            {
                Size = new Size(20, 20),
                BackColor = Color.Green,
                Location = new Point(rand.Next(this.ClientSize.Width - 20), 0)
            };
            this.Controls.Add(fruit);

            Timer fallTimer = new Timer();
            fallTimer.Interval = 30;
            fallTimer.Tick += (s, ev) =>
            {
                fruit.Top += 5;

                if (fruit.Location.X + fruit.Width > plate.Location.X &&
                    fruit.Location.X < plate.Location.X + plate.Width &&
                    fruit.Location.Y + fruit.Height > plate.Location.Y &&
                    fruit.Location.Y < plate.Location.Y + plate.Height)
                {
                    this.Controls.Remove(fruit);
                    caughtFruits++;
                    UpdateLabel();
                    fallTimer.Stop();
                }
                else if (fruit.Top > this.ClientSize.Height)
                {
                    this.Controls.Remove(fruit);
                    missedFruits++;
                    UpdateLabel();
                    fallTimer.Stop();
                }
            };
            fallTimer.Start();
        }
    }
}
