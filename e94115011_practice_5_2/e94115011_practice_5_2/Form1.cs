using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace e94115011_practice_5_2
{
    public partial class Form1 : Form
    {
        private Panel panelWin;
        private Panel panelLose;
        private Label winLabel;
        private Label loseLabel;

        bool isDragging = false;
        int startX, startY;
        int cardiganOriginalX, cardiganOriginalY;
        int myrtleOriginalX, myrtleOriginalY;
        int melanthaOriginalX, melanthaOriginalY;

        bool[,] cellAvailability = new bool[3, 11];
        Color[,] cellColors = new Color[3, 11];

        public int deploymentPoints = 10;
        private List<Character> deployedCharacters = new List<Character>();
        private Dictionary<Panel, Character> panelCharacterMap = new Dictionary<Panel, Character>();

        private Cardigan cardigan;
        private Label cardiganInfoLabel;
        private bool isCardiganDeployed = false;

        private Myrtle myrtle;
        private Label myrtleInfoLabel;
        private bool isMyrtleDeployed = false;

        private Melantha melantha;
        private Label melanthaInfoLabel;
        private bool isMelanthaDeployed = false;

        int playerHealth = 3;
        int enemyCount = 10;
        int currentEnemyCount = 10;
        private List<EnemyUI> enemyUIs = new List<EnemyUI>();

        public Form1()
        {
            InitializeComponent();

            panel1.Location = new Point(8, 8);
            panel1.BringToFront();
            panel2.Visible = false;
            InitializeWinLosePanels();

            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;

            panel4.Visible = false;
            panel4.Enabled = false;
            panel5.Visible = false;
            panel5.Enabled = false;
            panel6.Visible = false;
            panel6.Enabled = false;

            label1.Text = "";
            label2.Text = "";
            label3.Text = "";

            panel4.MouseDown += Cardigan_MouseDown;
            panel4.MouseMove += Cardigan_MouseMove;
            panel4.MouseUp += Cardigan_MouseUp;
            panel4.MouseHover += (s, e) => ShowCharacterInfo(cardigan);

            panel5.MouseDown += Myrtle_MouseDown;
            panel5.MouseMove += Myrtle_MouseMove;
            panel5.MouseUp += Myrtle_MouseUp;
            panel5.MouseHover += (s, e) => ShowCharacterInfo(myrtle);

            panel6.MouseDown += Melantha_MouseDown;
            panel6.MouseMove += Melantha_MouseMove;
            panel6.MouseUp += Melantha_MouseUp;
            panel6.MouseHover += (s, e) => ShowCharacterInfo(melantha);

            tableLayoutPanel1.CellPaint += tableLayoutPanel1_CellPaint;
            InitializeCellAvailabilityAndColors();

            cardigan = new Cardigan();
            panelCharacterMap[panel4] = cardigan;
            cardiganInfoLabel = new Label
            {
                AutoSize = true,
            };
            cardiganInfoLabel.Enabled = false;
            panel4.Controls.Add(cardiganInfoLabel);
            UpdateCardiganInfo(cardiganInfoLabel);

            myrtle = new Myrtle(this);
            panelCharacterMap[panel5] = myrtle;
            myrtleInfoLabel = new Label
            {
                AutoSize = true,
            };
            myrtleInfoLabel.Enabled = false;
            panel5.Controls.Add(myrtleInfoLabel);
            UpdateMyrtleInfo(myrtleInfoLabel);

            melantha = new Melantha();
            panelCharacterMap[panel6] = melantha;
            melanthaInfoLabel = new Label
            {
                AutoSize = true,
            };
            melanthaInfoLabel.Enabled = false;
            panel6.Controls.Add(melanthaInfoLabel);
            UpdateMelanthaInfo(melanthaInfoLabel);

            timer1.Interval = 1000;
            EnemySpawnTimer.Interval = 15000;
        }

        private void InitializeWinLosePanels()
        {
            panelWin = new Panel
            {
                Size = new Size(1000, 500),
                Location = new Point(0, 0),
                BackColor = SystemColors.Control,
                Visible = false
            };

            winLabel = new Label
            {
                Text = "成功通關",
                AutoSize = true,
            };
            panelWin.Controls.Add(winLabel);
            winLabel.Location = new Point(
            (panelWin.Width - winLabel.Width) / 3,
            (panelWin.Height - winLabel.Height) / 3
            );

            panelLose = new Panel
            {
                Size = new Size(1000, 500),
                Location = new Point(0, 0),
                BackColor = SystemColors.Control,
                Visible = false
            };

            loseLabel = new Label
            {
                Text = "通關失敗",
                AutoSize = true,
            };
            panelLose.Controls.Add(loseLabel);
            loseLabel.Location = new Point(
            (panelLose.Width - loseLabel.Width) / 3,
            (panelLose.Height - loseLabel.Height) / 3
            );

            this.Controls.Add(panelWin);
            this.Controls.Add(panelLose);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Location = new Point(0, 0);
            panel2.BringToFront();
            panel2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked)
            {
                MessageBox.Show("Please select at least one character.");
                return;
            }
            panel3.Location = new Point(0, 0);
            panel3.BringToFront();
            timer1.Start();
            SpawnEnemy();
            UpdateEnemyInfo();
            EnemySpawnTimer.Start();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            panel4.Visible = checkBox1.Checked;
            panel4.Enabled = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            panel5.Visible = checkBox2.Checked;
            panel5.Enabled = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            panel6.Visible = checkBox3.Checked;
            panel6.Enabled = checkBox3.Checked;
        }

        private void InitializeCellAvailabilityAndColors()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i == 1 && j == 0)
                    {
                        cellAvailability[i, j] = false;
                        cellColors[i, j] = Color.Red;
                    }
                    else if (i == 1 && j == 10)
                    {
                        cellAvailability[i, j] = false;
                        cellColors[i, j] = Color.SkyBlue;
                    }
                    else
                    {
                        cellAvailability[i, j] = true;
                        cellColors[i, j] = Color.Empty;
                    }
                }
            }
            tableLayoutPanel1.Invalidate();
        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (!cellAvailability[e.Row, e.Column])
            {
                using (Brush brush = new SolidBrush(cellColors[e.Row, e.Column]))
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                }
            }
        }

        private void CenterControlInPanel(Control control, Panel panel)
        {
            Point centerPoint = new Point(panel.Width / 2, panel.Height / 2);
            int x = centerPoint.X - (control.Width / 2);
            int y = centerPoint.Y - (control.Height / 2);
            control.Location = new Point(x, y);
        }

        private void ShowCharacterInfo(Character character)
        {
            if (character != null)
            {
                bool isCharacterDeployed = deployedCharacters.Any(c => c.Name == character.Name);

                if (isCharacterDeployed)
                {
                    label3.Text = $"{character.Name}\n{character.HP}/{character.MaxHP}\n{character.RemainingCooldown}";
                }
                else
                {
                    label3.Text = $"{character.Name}\n{character.DeploymentCost}";
                }
            }
        }

        private void Cardigan_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !isCardiganDeployed)
            {
                isDragging = true;
                startX = e.X;
                startY = e.Y;
                cardiganOriginalX = panel4.Left;
                cardiganOriginalY = panel4.Top;
            }
            else if (e.Button == MouseButtons.Right && isCardiganDeployed)
            {
                RemoveCardigan();
            }
        }

        private void RemoveCardigan()
        {
            tableLayoutPanel1.Controls.Remove(panel4);
            panel3.Controls.Add(panel4);
            panel4.BringToFront();
            panel4.Location = new Point(cardiganOriginalX, cardiganOriginalY);
            isCardiganDeployed = false;
            deployedCharacters.Remove(cardigan);
            panel4.BackColor = Color.LightGray;
            UpdateCardiganInfo(cardiganInfoLabel);
            ResumeMoveEnemy();
        }

        private void Cardigan_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                panel4.Left += e.X - startX;
                panel4.Top += e.Y - startY;
            }
        }

        private void Cardigan_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
                Point dropPoint = tableLayoutPanel1.PointToClient(Cursor.Position);
                int colIndex = dropPoint.X / (tableLayoutPanel1.Width / tableLayoutPanel1.ColumnCount);
                int rowIndex = dropPoint.Y / (tableLayoutPanel1.Height / tableLayoutPanel1.RowCount);

                if (!isCardiganDeployed)
                {
                    if ((deploymentPoints >= cardigan.DeploymentCost) && tableLayoutPanel1.ClientRectangle.Contains(dropPoint) && cellAvailability[rowIndex, colIndex])
                    {
                        deploymentPoints -= cardigan.DeploymentCost;
                        label1.Text = deploymentPoints.ToString();

                        if (panel4.Parent != null)
                        {
                            panel4.Parent.Controls.Remove(panel4);
                        }
                        tableLayoutPanel1.Controls.Add(panel4, colIndex, rowIndex);

                        if (!deployedCharacters.Contains(cardigan))
                        {
                            deployedCharacters.Add(cardigan);
                        }
                        isCardiganDeployed = true;
                        panel4.BackColor = Color.Gray;
                        UpdateCardiganInfo(cardiganInfoLabel);
                    }
                    else
                    {
                        panel4.Location = new Point(cardiganOriginalX, cardiganOriginalY);
                    }
                }
                else if (isCardiganDeployed)
                {
                    if (cardigan.IsCooldownComplete)
                    {
                        cardigan.UseSkill();
                        panel4.BackColor = Color.Gray;
                        UpdateCardiganInfo(cardiganInfoLabel);
                    }
                }
            }
        }

        private void UpdateCardiganInfo(Label label)
        {
            label.Font = new Font(label.Font.FontFamily, 6, label.Font.Style);
            label.TextAlign = ContentAlignment.MiddleCenter;
            if (isCardiganDeployed)
            {
                label.Text = $"{cardigan.Name}\n{cardigan.HP}/{cardigan.MaxHP}\n{cardigan.RemainingCooldown}";
            }
            else
            {
                label.Text = $"{cardigan.Name}\n{cardigan.DeploymentCost}";

            }
            CenterControlInPanel(label, panel4);
        }

        private void Myrtle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !isMyrtleDeployed)
            {
                isDragging = true;
                startX = e.X;
                startY = e.Y;
                myrtleOriginalX = panel5.Left;
                myrtleOriginalY = panel5.Top;
            }
            else if (e.Button == MouseButtons.Right && isMyrtleDeployed)
            {
                RemoveMyrtle();
            }
        }

        private void RemoveMyrtle()
        {
            tableLayoutPanel1.Controls.Remove(panel5);
            panel3.Controls.Add(panel5);
            panel5.BringToFront();
            panel5.Location = new Point(myrtleOriginalX, myrtleOriginalY);
            isMyrtleDeployed = false;
            deployedCharacters.Remove(myrtle);
            panel5.BackColor = Color.LightGray;
            UpdateMyrtleInfo(myrtleInfoLabel);
            ResumeMoveEnemy();
        }

        private void Myrtle_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                panel5.Left += e.X - startX;
                panel5.Top += e.Y - startY;
            }
        }

        private void Myrtle_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
                Point dropPoint = tableLayoutPanel1.PointToClient(Cursor.Position);
                int colIndex = dropPoint.X / (tableLayoutPanel1.Width / tableLayoutPanel1.ColumnCount);
                int rowIndex = dropPoint.Y / (tableLayoutPanel1.Height / tableLayoutPanel1.RowCount);

                if (!isMyrtleDeployed)
                {
                    if ((deploymentPoints >= myrtle.DeploymentCost) && tableLayoutPanel1.ClientRectangle.Contains(dropPoint) && cellAvailability[rowIndex, colIndex])
                    {
                        deploymentPoints -= myrtle.DeploymentCost;
                        label1.Text = deploymentPoints.ToString();

                        if (panel5.Parent != null)
                        {
                            panel5.Parent.Controls.Remove(panel5);
                        }
                        tableLayoutPanel1.Controls.Add(panel5, colIndex, rowIndex);

                        if (!deployedCharacters.Contains(myrtle))
                        {
                            deployedCharacters.Add(myrtle);
                        }
                        isMyrtleDeployed = true;
                        panel5.BackColor = Color.Gray;
                        UpdateMyrtleInfo(myrtleInfoLabel);
                    }
                    else
                    {
                        panel5.Location = new Point(myrtleOriginalX, myrtleOriginalY);
                    }
                }
                else if (isMyrtleDeployed)
                {
                    if (myrtle.IsCooldownComplete)
                    {
                        myrtle.UseSkill();
                        label1.Text = deploymentPoints.ToString();
                        panel5.BackColor = Color.Gray;
                        UpdateMyrtleInfo(myrtleInfoLabel);
                    }
                }
            }
        }

        private void UpdateMyrtleInfo(Label label)
        {
            label.Font = new Font(label.Font.FontFamily, 6, label.Font.Style);
            label.TextAlign = ContentAlignment.MiddleCenter;
            if (isMyrtleDeployed)
            {
                label.Text = $"{myrtle.Name}\n{myrtle.HP}/{myrtle.MaxHP}\n{myrtle.RemainingCooldown}";
            }
            else
            {
                label.Text = $"{myrtle.Name}\n{myrtle.DeploymentCost}";

            }
            CenterControlInPanel(label, panel5);
        }

        private void Melantha_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !isMelanthaDeployed)
            {
                isDragging = true;
                startX = e.X;
                startY = e.Y;
                melanthaOriginalX = panel6.Left;
                melanthaOriginalY = panel6.Top;
            }
            else if (e.Button == MouseButtons.Right && isMelanthaDeployed)
            {
                RemoveMelantha();
            }
        }

        private void RemoveMelantha()
        {
            tableLayoutPanel1.Controls.Remove(panel6);
            panel3.Controls.Add(panel6);
            panel6.BringToFront();
            panel6.Location = new Point(melanthaOriginalX, melanthaOriginalY);
            isMelanthaDeployed = false;
            deployedCharacters.Remove(melantha);
            panel6.BackColor = Color.LightGray;
            UpdateMelanthaInfo(melanthaInfoLabel);
            ResumeMoveEnemy();
        }

        private void Melantha_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                panel6.Left += e.X - startX;
                panel6.Top += e.Y - startY;
            }
        }

        private void Melantha_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
                Point dropPoint = tableLayoutPanel1.PointToClient(Cursor.Position);
                int colIndex = dropPoint.X / (tableLayoutPanel1.Width / tableLayoutPanel1.ColumnCount);
                int rowIndex = dropPoint.Y / (tableLayoutPanel1.Height / tableLayoutPanel1.RowCount);

                if (!isMelanthaDeployed)
                {
                    if ((deploymentPoints >= melantha.DeploymentCost) && tableLayoutPanel1.ClientRectangle.Contains(dropPoint) && cellAvailability[rowIndex, colIndex])
                    {
                        deploymentPoints -= melantha.DeploymentCost;
                        label1.Text = deploymentPoints.ToString();

                        if (panel6.Parent != null)
                        {
                            panel6.Parent.Controls.Remove(panel6);
                        }
                        tableLayoutPanel1.Controls.Add(panel6, colIndex, rowIndex);

                        if (!deployedCharacters.Contains(melantha))
                        {
                            deployedCharacters.Add(melantha);
                        }
                        isMelanthaDeployed = true;
                        panel6.BackColor = Color.Gray;
                        UpdateMelanthaInfo(melanthaInfoLabel);
                    }
                    else
                    {
                        panel6.Location = new Point(melanthaOriginalX, melanthaOriginalY);
                    }
                }
                else if (isMelanthaDeployed)
                {
                    if (melantha.IsCooldownComplete)
                    {
                        List<EnemyUI> enemiesInRange = new List<EnemyUI>();

                        foreach (var enemyUI in enemyUIs.ToList())
                        {
                            Panel enemyPanel = enemyUI.Panel;
                            int enemyCenterX = (enemyPanel.Left + (enemyPanel.Width / 2)) - tableLayoutPanel1.Left;
                            int enemyCenterY = (enemyPanel.Top + (enemyPanel.Height / 2)) - tableLayoutPanel1.Top;

                            if (IsEnemyInMelanthaRange(colIndex, rowIndex, enemyCenterX, enemyCenterY))
                            {
                                enemiesInRange.Add(enemyUI);
                            }
                        }

                        if (enemiesInRange.Count > 0)
                        {
                            EnemyUI firstEnemy = enemiesInRange[0];
                            melantha.UseSkill(firstEnemy);

                            if (firstEnemy.Enemy.IsDead)
                            {
                                RemoveEnemy(firstEnemy);
                                CheckWinCondition();
                            }
                            panel6.BackColor = Color.Gray;
                            UpdateMelanthaInfo(melanthaInfoLabel);
                        }
                    }
                }
            }
        }

        private bool IsEnemyInMelanthaRange(int colIndex, int rowIndex, int enemyCenterX, int enemyCenterY)
        {
            int melanthaLeftEdge = GetColumnLeftEdge(colIndex);
            int melanthaRightEdge = GetColumnRightEdge(colIndex);
            int melanthaTopEdge = GetRowTopEdge(rowIndex);
            int melanthaBottomEdge = GetRowBottomEdge(rowIndex);

            if (colIndex > 0)
            {
                int leftLeftEdge = GetColumnLeftEdge(colIndex - 1);
                int leftRightEdge = GetColumnRightEdge(colIndex - 1);
                int leftTopEdge = GetRowTopEdge(rowIndex);
                int leftBottomEdge = GetRowBottomEdge(rowIndex);

                if (enemyCenterX >= leftLeftEdge && enemyCenterX <= leftRightEdge &&
                    enemyCenterY >= leftTopEdge && enemyCenterY <= leftBottomEdge)
                {
                    return true;
                }
            }

            if (rowIndex > 0)
            {
                int topLeftEdge = GetColumnLeftEdge(colIndex);
                int topRightEdge = GetColumnRightEdge(colIndex);
                int topTopEdge = GetRowTopEdge(rowIndex - 1);
                int topBottomEdge = GetRowBottomEdge(rowIndex - 1);

                if (enemyCenterX >= topLeftEdge && enemyCenterX <= topRightEdge &&
                    enemyCenterY >= topTopEdge && enemyCenterY <= topBottomEdge)
                {
                    return true;
                }
            }

            if (rowIndex < (tableLayoutPanel1.RowCount - 1))
            {
                int bottomLeftEdge = GetColumnLeftEdge(colIndex);
                int bottomRightEdge = GetColumnRightEdge(colIndex);
                int bottomTopEdge = GetRowTopEdge(rowIndex + 1);
                int bottomBottomEdge = GetRowBottomEdge(rowIndex + 1);

                if (enemyCenterX >= bottomLeftEdge && enemyCenterX <= bottomRightEdge &&
                    enemyCenterY >= bottomTopEdge && enemyCenterY <= bottomBottomEdge)
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdateMelanthaInfo(Label label)
        {
            label.Font = new Font(label.Font.FontFamily, 6, label.Font.Style);
            label.TextAlign = ContentAlignment.MiddleCenter;
            if (isMelanthaDeployed)
            {
                label.Text = $"{melantha.Name}\n{melantha.HP}/{melantha.MaxHP}\n{melantha.RemainingCooldown}";
            }
            else
            {
                label.Text = $"{melantha.Name}\n{melantha.DeploymentCost}";

            }
            CenterControlInPanel(label, panel6);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            deploymentPoints = Math.Min(deploymentPoints + 1, 99);
            label1.Text = deploymentPoints.ToString();
            label2.Text = $"{playerHealth.ToString()}/{currentEnemyCount.ToString()}";

            foreach (var character in deployedCharacters)
            {
                character.ReduceCooldown();
                if (character.IsCooldownComplete)
                {
                    if (character.Name == "Cardigan")
                    {
                        panel4.BackColor = Color.LimeGreen;
                    }
                    else if (character.Name == "Myrtle")
                    {
                        panel5.BackColor = Color.LimeGreen;
                    }
                    else if (character.Name == "Melantha")
                    {
                        panel6.BackColor = Color.LimeGreen;
                    }
                }
            }

            foreach (var enemyUI in enemyUIs)
            {
                if (IsBlockedByCharacter(enemyUI.Panel))
                {
                    Character blockingCharacter = GetBlockingCharacter(enemyUI.Panel);
                    if (blockingCharacter != null)
                    {
                        enemyUI.Enemy.Attack(blockingCharacter);
                    }
                }
            }

            if (deployedCharacters.Contains(cardigan) && enemyUIs.Count > 0)
            {
                var panel = panel4;
                int rowIndex = tableLayoutPanel1.GetRow(panel);
                int colIndex = tableLayoutPanel1.GetColumn(panel);

                int cardiganLeftEdge = GetColumnLeftEdge(colIndex);
                int cardiganRightEdge = GetColumnRightEdge(colIndex);
                int cardiganTopEdge = GetRowTopEdge(rowIndex);
                int cardiganBottomEdge = GetRowBottomEdge(rowIndex);
                bool canAttack = true;

                foreach (var enemyUI in enemyUIs.ToList())
                {
                    Panel enemyPanel = enemyUI.Panel;
                    int enemyCenterX = (enemyPanel.Left + (enemyPanel.Width / 2)) - tableLayoutPanel1.Left;
                    int enemyCenterY = (enemyPanel.Top + (enemyPanel.Height / 2)) - tableLayoutPanel1.Top;

                    if (colIndex > 0 && canAttack)
                    {
                        int leftLeftEdge = GetColumnLeftEdge(colIndex - 1);
                        int leftRightEdge = GetColumnRightEdge(colIndex - 1);
                        int leftTopEdge = GetRowTopEdge(rowIndex);
                        int leftBottomEdge = GetRowBottomEdge(rowIndex);

                        if (enemyCenterX >= leftLeftEdge && enemyCenterX <= leftRightEdge &&
                            enemyCenterY >= leftTopEdge && enemyCenterY <= leftBottomEdge)
                        {
                            cardigan.Attack(enemyUI.Enemy);
                            canAttack = false;
                        }
                    }

                    if (rowIndex > 0 && canAttack)
                    {
                        int topLeftEdge = GetColumnLeftEdge(colIndex);
                        int topRightEdge = GetColumnRightEdge(colIndex);
                        int topTopEdge = GetRowTopEdge(rowIndex - 1);
                        int topBottomEdge = GetRowBottomEdge(rowIndex - 1);

                        if (enemyCenterX >= topLeftEdge && enemyCenterX <= topRightEdge &&
                            enemyCenterY >= topTopEdge && enemyCenterY <= topBottomEdge)
                        {
                            cardigan.Attack(enemyUI.Enemy);
                            canAttack = false;
                        }
                    }

                    if (rowIndex < (tableLayoutPanel1.RowCount - 1) && canAttack)
                    {
                        int bottomLeftEdge = GetColumnLeftEdge(colIndex);
                        int bottomRightEdge = GetColumnRightEdge(colIndex);
                        int bottomTopEdge = GetRowTopEdge(rowIndex + 1);
                        int bottomBottomEdge = GetRowBottomEdge(rowIndex + 1);

                        if (enemyCenterX >= bottomLeftEdge && enemyCenterX <= bottomRightEdge &&
                            enemyCenterY >= bottomTopEdge && enemyCenterY <= bottomBottomEdge)
                        {
                            cardigan.Attack(enemyUI.Enemy);
                            canAttack = false;
                        }
                    }
                    if (enemyUI.Enemy.IsDead)
                    {
                        RemoveEnemy(enemyUI);
                        CheckWinCondition();
                    }
                }
            }

            if (deployedCharacters.Contains(myrtle) && enemyUIs.Count > 0)
            {
                var panel = panel5;
                int rowIndex = tableLayoutPanel1.GetRow(panel);
                int colIndex = tableLayoutPanel1.GetColumn(panel);

                int myrtleLeftEdge = GetColumnLeftEdge(colIndex);
                int myrtleRightEdge = GetColumnRightEdge(colIndex);
                int myrtleTopEdge = GetRowTopEdge(rowIndex);
                int myrtleBottomEdge = GetRowBottomEdge(rowIndex);
                bool canAttack = true;

                foreach (var enemyUI in enemyUIs.ToList())
                {
                    Panel enemyPanel = enemyUI.Panel;
                    int enemyCenterX = (enemyPanel.Left + (enemyPanel.Width / 2)) - tableLayoutPanel1.Left;
                    int enemyCenterY = (enemyPanel.Top + (enemyPanel.Height / 2)) - tableLayoutPanel1.Top;

                    if (colIndex > 0 && canAttack)
                    {
                        int leftLeftEdge = GetColumnLeftEdge(colIndex - 1);
                        int leftRightEdge = GetColumnRightEdge(colIndex - 1);
                        int leftTopEdge = GetRowTopEdge(rowIndex);
                        int leftBottomEdge = GetRowBottomEdge(rowIndex);

                        if (enemyCenterX >= leftLeftEdge && enemyCenterX <= leftRightEdge &&
                            enemyCenterY >= leftTopEdge && enemyCenterY <= leftBottomEdge)
                        {
                            myrtle.Attack(enemyUI.Enemy);
                            canAttack = false;
                        }
                    }

                    if (rowIndex > 0 && canAttack)
                    {
                        int topLeftEdge = GetColumnLeftEdge(colIndex);
                        int topRightEdge = GetColumnRightEdge(colIndex);
                        int topTopEdge = GetRowTopEdge(rowIndex - 1);
                        int topBottomEdge = GetRowBottomEdge(rowIndex - 1);

                        if (enemyCenterX >= topLeftEdge && enemyCenterX <= topRightEdge &&
                            enemyCenterY >= topTopEdge && enemyCenterY <= topBottomEdge)
                        {
                            myrtle.Attack(enemyUI.Enemy);
                            canAttack = false;
                        }
                    }

                    if (rowIndex < (tableLayoutPanel1.RowCount - 1) && canAttack)
                    {
                        int bottomLeftEdge = GetColumnLeftEdge(colIndex);
                        int bottomRightEdge = GetColumnRightEdge(colIndex);
                        int bottomTopEdge = GetRowTopEdge(rowIndex + 1);
                        int bottomBottomEdge = GetRowBottomEdge(rowIndex + 1);

                        if (enemyCenterX >= bottomLeftEdge && enemyCenterX <= bottomRightEdge &&
                            enemyCenterY >= bottomTopEdge && enemyCenterY <= bottomBottomEdge)
                        {
                            myrtle.Attack(enemyUI.Enemy);
                            canAttack = false;
                        }
                    }
                    if (enemyUI.Enemy.IsDead)
                    {
                        RemoveEnemy(enemyUI);
                        CheckWinCondition();
                    }
                }
            }

            if (deployedCharacters.Contains(melantha) && enemyUIs.Count > 0)
            {
                var panel = panel6;
                int rowIndex = tableLayoutPanel1.GetRow(panel);
                int colIndex = tableLayoutPanel1.GetColumn(panel);

                int melanthaLeftEdge = GetColumnLeftEdge(colIndex);
                int melanthaRightEdge = GetColumnRightEdge(colIndex);
                int melanthaTopEdge = GetRowTopEdge(rowIndex);
                int melanthaBottomEdge = GetRowBottomEdge(rowIndex);
                bool canAttack = true;

                foreach (var enemyUI in enemyUIs.ToList())
                {
                    Panel enemyPanel = enemyUI.Panel;
                    int enemyCenterX = (enemyPanel.Left + (enemyPanel.Width / 2)) - tableLayoutPanel1.Left;
                    int enemyCenterY = (enemyPanel.Top + (enemyPanel.Height / 2)) - tableLayoutPanel1.Top;

                    if (colIndex > 0 && canAttack)
                    {
                        int leftLeftEdge = GetColumnLeftEdge(colIndex - 1);
                        int leftRightEdge = GetColumnRightEdge(colIndex - 1);
                        int leftTopEdge = GetRowTopEdge(rowIndex);
                        int leftBottomEdge = GetRowBottomEdge(rowIndex);

                        if (enemyCenterX >= leftLeftEdge && enemyCenterX <= leftRightEdge &&
                            enemyCenterY >= leftTopEdge && enemyCenterY <= leftBottomEdge)
                        {
                            melantha.Attack(enemyUI.Enemy);
                            canAttack = false;
                        }
                    }

                    if (rowIndex > 0 && canAttack)
                    {
                        int topLeftEdge = GetColumnLeftEdge(colIndex);
                        int topRightEdge = GetColumnRightEdge(colIndex);
                        int topTopEdge = GetRowTopEdge(rowIndex - 1);
                        int topBottomEdge = GetRowBottomEdge(rowIndex - 1);

                        if (enemyCenterX >= topLeftEdge && enemyCenterX <= topRightEdge &&
                            enemyCenterY >= topTopEdge && enemyCenterY <= topBottomEdge)
                        {
                            melantha.Attack(enemyUI.Enemy);
                            canAttack = false;
                        }
                    }

                    if (rowIndex < (tableLayoutPanel1.RowCount - 1) && canAttack)
                    {
                        int bottomLeftEdge = GetColumnLeftEdge(colIndex);
                        int bottomRightEdge = GetColumnRightEdge(colIndex);
                        int bottomTopEdge = GetRowTopEdge(rowIndex + 1);
                        int bottomBottomEdge = GetRowBottomEdge(rowIndex + 1);

                        if (enemyCenterX >= bottomLeftEdge && enemyCenterX <= bottomRightEdge &&
                            enemyCenterY >= bottomTopEdge && enemyCenterY <= bottomBottomEdge)
                        {
                            melantha.Attack(enemyUI.Enemy);
                            canAttack = false;
                        }
                    }
                    if (enemyUI.Enemy.IsDead)
                    {
                        RemoveEnemy(enemyUI);
                        CheckWinCondition();
                    }
                }
            }

            if (cardigan.HP <= 0)
            {
                RemoveCardigan();
            }
            if (myrtle.HP <= 0)
            {
                RemoveMyrtle();
            }
            if (melantha.HP <= 0)
            {
                RemoveMelantha();
            }

            UpdateCardiganInfo(cardiganInfoLabel);
            UpdateMyrtleInfo(myrtleInfoLabel);
            UpdateMelanthaInfo(melanthaInfoLabel);
            UpdateEnemyInfo();
        }

        private void UpdateEnemyInfo()
        {
            foreach (var enemyUI in enemyUIs)
            {
                if (enemyUI.Enemy != null && this.Controls.Contains(enemyUI.Panel))
                {
                    enemyUI.InfoLabel.Text = $"{enemyUI.Enemy.HP}";
                    CenterControlInPanel(enemyUI.InfoLabel, enemyUI.Panel);
                }
            }
        }

        private void SpawnEnemy()
        {
            enemyCount--;
            Enemy enemy = new Enemy(3000, 500, 200);

            Panel enemyPanel = new Panel
            {
                Size = new Size(30, 30),
                BackColor = Color.Yellow,
                Enabled = false
            };

            this.Controls.Add(enemyPanel);
            enemyPanel.BringToFront();
            Point cellMidpointLocationOnForm = GetCellCenter(tableLayoutPanel1, 1, 0);
            int startLocationX = cellMidpointLocationOnForm.X - (enemyPanel.Width / 2);
            int startLocationY = cellMidpointLocationOnForm.Y - (enemyPanel.Height / 2);
            enemyPanel.Location = new Point(startLocationX, startLocationY);

            Label enemyInfoLabel = new Label
            {
                AutoSize = true,
                Enabled = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 6),
            };
            enemyPanel.Controls.Add(enemyInfoLabel);

            Timer enemyMoveTimer = new Timer
            {
                Interval = 50
            };
            enemyMoveTimer.Tick += (s, args) => MoveEnemy(enemyPanel, enemyMoveTimer);
            enemyMoveTimer.Start();

            enemyUIs.Add(new EnemyUI(enemy, enemyPanel, enemyInfoLabel, enemyMoveTimer));
            UpdateEnemyInfo();
        }

        private void EnemySpawnTimer_Tick(object sender, EventArgs e)
        {
            if (enemyCount > 0)
            {
                SpawnEnemy();
            }
        }

        private Point GetCellCenter(TableLayoutPanel table, int row, int col)
        {
            int cellWidth = table.Width / table.ColumnCount;
            int cellHeight = table.Height / table.RowCount;

            int x = col * cellWidth;
            int y = row * cellHeight;

            int midX = x + cellWidth / 2;
            int midY = y + cellHeight / 2;

            Point cellMidpointLocation = table.PointToScreen(new Point(midX, midY));
            return this.PointToClient(cellMidpointLocation);
        }

        private void MoveEnemy(Panel enemyPanel, Timer moveTimer)
        {
            enemyPanel.Left += 1;

            if (IsBlockedByCharacter(enemyPanel))
            {
                moveTimer.Stop();
                return;
            }

            int column11Midpoint = GetCellCenter(tableLayoutPanel1, 1, 10).X;
            int enemyMidpoint = enemyPanel.Left + (enemyPanel.Width / 2);

            if (enemyMidpoint >= column11Midpoint)
            {
                var enemyUI = enemyUIs.FirstOrDefault(e => e.Panel == enemyPanel);
                if (enemyUI != null)
                {
                    RemoveEnemy(enemyUI);
                }
                playerHealth--;
                CheckWinCondition();
            }
        }

        private void RemoveEnemy(EnemyUI enemyUI)
        {
            currentEnemyCount--;
            this.Controls.Remove(enemyUI.Panel);
            enemyUI.MoveTimer.Stop();
            enemyUI.MoveTimer.Dispose();
            label2.Text = $"{playerHealth.ToString()}/{currentEnemyCount.ToString()}";
            enemyUIs.Remove(enemyUI);
        }

        private void ResumeMoveEnemy()
        {
            foreach (var enemyUI in enemyUIs)
            {
                if (!IsBlockedByCharacter(enemyUI.Panel))
                {
                    Timer enemyMoveTimer = enemyUI.MoveTimer;
                    if (enemyMoveTimer != null && !enemyMoveTimer.Enabled)
                    {
                        enemyMoveTimer.Start();
                    }
                }
            }
        }

        private bool IsBlockedByCharacter(Panel enemyPanel)
        {
            int middleRow = 1;
            int enemyCenterX = (enemyPanel.Left + (enemyPanel.Width / 2)) - tableLayoutPanel1.Left;
            int totalColumns = tableLayoutPanel1.ColumnCount;

            for (int col = 0; col < totalColumns; col++)
            {
                Panel characterPanel = GetCharacterPanel(middleRow, col);

                if (characterPanel != null)
                {
                    int columnLeftEdge = GetColumnLeftEdge(col);
                    int columnRightEdge = GetColumnRightEdge(col);
                    if (enemyCenterX >= columnLeftEdge && enemyCenterX <= columnRightEdge)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private Character GetBlockingCharacter(Panel enemyPanel)
        {
            int middleRow = 1;
            int enemyCenterX = (enemyPanel.Left + (enemyPanel.Width / 2)) - tableLayoutPanel1.Left;
            int totalColumns = tableLayoutPanel1.ColumnCount;

            for (int col = 0; col < totalColumns; col++)
            {
                Panel characterPanel = GetCharacterPanel(middleRow, col);

                if (characterPanel != null)
                {
                    int columnLeftEdge = GetColumnLeftEdge(col);
                    int columnRightEdge = GetColumnRightEdge(col);

                    if (enemyCenterX >= columnLeftEdge && enemyCenterX <= columnRightEdge)
                    {
                        return GetCharacterFromPanel(characterPanel);
                    }
                }
            }
            return null;
        }

        private Panel GetCharacterPanel(int rowIndex, int colIndex)
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                int row = tableLayoutPanel1.GetRow(control);
                int col = tableLayoutPanel1.GetColumn(control);
                if (row == rowIndex && col == colIndex)
                {
                    return control as Panel;
                }
            }
            return null;
        }

        private Character GetCharacterFromPanel(Panel panel)
        {
            if (panelCharacterMap.TryGetValue(panel, out Character character))
            {
                return character;
            }
            return null;
        }

        private int GetColumnLeftEdge(int col)
        {
            int leftEdge = 0;
            for (int i = 0; i < col; i++)
            {
                leftEdge += tableLayoutPanel1.GetColumnWidths()[i];
            }
            return leftEdge;
        }

        private int GetColumnRightEdge(int col)
        {
            int rightEdge = 0;
            for (int i = 0; i <= col; i++)
            {
                rightEdge += tableLayoutPanel1.GetColumnWidths()[i];
            }
            return rightEdge;
        }

        private int GetRowTopEdge(int row)
        {
            int topEdge = 0;
            for (int i = 0; i < row; i++)
            {
                topEdge += tableLayoutPanel1.GetRowHeights()[i];
            }
            return topEdge;
        }

        private int GetRowBottomEdge(int row)
        {
            int bottomEdge = 0;
            for (int i = 0; i <= row; i++)
            {
                bottomEdge += tableLayoutPanel1.GetRowHeights()[i];
            }
            return bottomEdge;
        }

        private void CheckWinCondition()
        {
            if (currentEnemyCount == 0 && playerHealth > 0)
            {
                panelWin.Visible = true;
                panelWin.BringToFront();
                panel3.Enabled = false;
                EnemySpawnTimer.Stop();
            }
            else if (playerHealth <= 0)
            {
                panelLose.Visible = true;
                panelLose.BringToFront();
                panel3.Enabled = false;
                EnemySpawnTimer.Stop();
            }
        }
    }
}