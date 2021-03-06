using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektFeladat
{
    public partial class FormA : Form
    {
        public FormA()
        {
            InitializeComponent();
            this.Shown += FormA_Load;
        }

        private const int size = 10;
        private const int buttonSize = 30;
        private const int spaceSize = 0;
        private const int bombs = 10;

        private bool[,] hidden = new bool[size, size];
        private bool[,] bomb = new bool[size, size];
        private bool[,] flagged = new bool[size, size];
        private int[,] nums = new int[size, size];
        private CheckBox flag = new CheckBox();
        private Label lbl = new Label();
        private List<List<Button>> buttons = new List<List<Button>>(); // eredetileg sima arrayt akartam hasznalni itt is de abban nem lehet pointerkent tarolni oket a list meg automatikusan pointerkent tarolja

        private bool running = true;
        private Random rand = new Random();

        private void buttonClick(object sender, EventArgs e)
        {
            if (running)
            {
                Button pressedButton = sender as Button;
                int x = (pressedButton.Location.X - 5) / (buttonSize + spaceSize);
                int y = (pressedButton.Location.Y - 25) / (buttonSize + spaceSize);
                if (flag.Checked) // Ha jeloles
                {
                    if (hidden[x, y])
                    {
                        if (flagged[x, y])
                        {
                            buttons[x][y].Text = "";
                        }
                        else
                        {
                            buttons[x][y].Text = "\u03D3";
                        }
                        flagged[x, y] = !(flagged[x, y]);
                        lbl.Text = "Bomba: " + Convert.ToString(countFlags()) + "/" + Convert.ToString(bombs);
                    }
                }
                else
                {
                    if (!flagged[x, y])
                    {
                        if (hidden[x, y])
                        {
                            if (bomb[x, y])
                            {
                                MessageBox.Show("Veszitettel");
                                buttons[x][y].Text = "✸";
                                for (int px = 0; px < size; px++)
                                {
                                    for (int py = 0; py < size; py++)
                                    {
                                        if (bomb[px, py])
                                        {
                                            if (flagged[px, py])
                                            {
                                                buttons[px][py].ForeColor = Color.Green;
                                            } else
                                            {
                                                buttons[px][py].Text = "✸";
                                                buttons[px][py].ForeColor = Color.Red;
                                            }
                                        } else if (flagged[px,py])
                                        {
                                            buttons[px][py].ForeColor = Color.Red;
                                        }
                                    }
                                }
                                running = false;
                            } else
                            {
                                felfedes(x, y);
                            }
                        }
                    }
                }
                if (checkWin())
                {
                    MessageBox.Show("Nyertel");
                    running = false;
                }
            }

        }
        private bool checkWin()
        {
            bool winning = true;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (bomb[x,y])
                    {
                        if (!flagged[x,y])
                        {
                            winning = false;
                        }
                    } else
                    {
                        if (flagged[x,y])
                        {
                            winning = false;
                        }
                        if (hidden[x,y])
                        {
                            winning = false;
                        }
                    }
                }
            }
            return winning;
        }
        private void felfedes(int x, int y)
        {
            if (flagged[x,y])
            {
                flagged[x, y] = false;
                lbl.Text = "Bomba: " + Convert.ToString(countFlags()) + "/" + Convert.ToString(bombs);
            }
            if (hidden[x,y])
            {
                if (nums[x, y] == 0)
                {
                    buttons[x][y].BackColor = Color.White;
                    hidden[x, y] = false;
                    for (int xp = -1; xp <= 1; xp++)
                    {
                        for (int yp = -1; yp <= 1; yp++)
                        {
                            if (x + xp >= 0 && y + yp >= 0 && x + xp < size && y + yp < size && !(xp == 0 && yp == 0))
                            {
                                felfedes(x + xp, y + yp);
                            }
                        }
                    }
                }
                else
                {
                    hidden[x, y] = false;
                    buttons[x][y].Text = Convert.ToString(nums[x, y]);
                    buttons[x][y].ForeColor = getColorFromInt(nums[x, y]);
                    buttons[x][y].BackColor = Color.White;
                }
            }
        }
        private int countFlags()
        {
            int flags = 0;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (flagged[x,y])
                    {
                        flags++;
                    }
                }
            }
            return flags;
        }
        private void quitButton(object sender, EventArgs e)
        {
            this.Close();
        }

        private Color getColorFromInt(int i)
        {
            switch (i)
            {
                default:
                    return Color.White;
                case 1:
                    return Color.Blue;
                case 2:
                    return Color.Green;
                case 3:
                    return Color.Red;
                case 4:
                    return Color.DarkBlue;
                case 5:
                    return Color.DarkRed;
                case 6:
                    return Color.Cyan;
                case 7:
                    return Color.Purple;
                case 8:
                    return Color.Black;
            }
        }

        private void FormA_Load(object sender, EventArgs e)
        {
            this.Size = new Size(size * (buttonSize + spaceSize) + 100, size * (buttonSize + spaceSize) + 28);
            this.CenterToScreen();

            Button quit = new Button();
            quit.Text = "X";
            quit.Location = new Point(size * (buttonSize + spaceSize) + 75, 0);
            quit.Size = new Size(25, 25);
            quit.BackColor = Color.Red;
            quit.FlatStyle = FlatStyle.Flat;
            quit.ForeColor = Color.White;
            quit.UseCompatibleTextRendering = true;
            quit.TextAlign = ContentAlignment.MiddleCenter;
            quit.Click += new System.EventHandler(this.quitButton);
            this.Controls.Add(quit);

            flag.Text = "Flag";
            flag.Location = new Point(size * (buttonSize + spaceSize) + 10, 30);
            this.Controls.Add(flag);

            lbl.Text = "Bomba: 0/" + Convert.ToString(bombs);
            lbl.Location = new Point(size * (buttonSize + spaceSize) + 10, 60);
            this.Controls.Add(lbl);

            // gombok letrehozasa
            for (int x = 0; x < size; x++)
            {
                List<Button> gombsor = new List<Button>();
                for (int y = 0; y < size; y++)
                {
                    Button btn = new Button();
                    btn.Location = new Point(x * (buttonSize + spaceSize) + 5, y * (buttonSize + spaceSize) + 25);
                    btn.Size = new Size(buttonSize, buttonSize);
                    btn.BackColor = Color.DarkGray;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Text = "";
                    btn.UseCompatibleTextRendering = true;
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold);
                    System.EventArgs xd = new System.EventArgs();
                    btn.Click += new System.EventHandler(this.buttonClick);
                    this.Controls.Add(btn);
                    gombsor.Add(btn);
                }
                buttons.Add(gombsor);
            }

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    bomb[x, y] = false;
                }
            }

            //bombak generalasa
            int bombakszama = 0;
            while (bombakszama < bombs)
            {
                int bx = rand.Next(size);
                int by = rand.Next(size);
                if (!bomb[bx, by])
                {
                    bomb[bx, by] = true;
                    buttons[bx][by].Text = "";
                    bombakszama = bombakszama + 1;
                }
            }

            //Szamok generalasa
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    int bombCount = 0;
                    for (int xp = -1; xp <= 1; xp++)
                    {
                        for (int yp = -1; yp <= 1; yp++)
                        {
                            if (x + xp >= 0 && y + yp >= 0 && x + xp < size && y + yp < size)
                            {
                                if (bomb[x+xp, y+yp])
                                {
                                    bombCount++;
                                }
                            }
                        }
                    }
                    nums[x, y] = bombCount;
                }
            }

            //Minden hidden
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    hidden[x, y] = true;
                }
            }

            //Semmise flagged
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    flagged[x, y] = false;
                }
            }
        }
    }
}
