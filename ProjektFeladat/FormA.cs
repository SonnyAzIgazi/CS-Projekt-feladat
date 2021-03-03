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

        private bool[,] hidden;
        private bool[,] bomb;
        private int[,] nums;
        private CheckBox flag;
        private Button[,] buttons;

        private const int size = 10;
        private const int buttonSize = 30;
        private const int spaceSize = 0;

        private void buttonClick(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            int x = (pressedButton.Location.X - 5) / (buttonSize + spaceSize);
            int y = (pressedButton.Location.Y - 25) / (buttonSize + spaceSize);
            MessageBox.Show(Convert.ToString(x) + " " + Convert.ToString(y));

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
            bomb = new bool[100,100];

            

            buttons = new Button[size, size];

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

            flag = new CheckBox();
            flag.Text = "Flag";
            flag.Location = new Point(size * (buttonSize + spaceSize) + 10, 30);
            this.Controls.Add(flag);

            Label lbl = new Label();
            lbl.Text = "Bomba: 0/10";
            lbl.Location = new Point(size * (buttonSize + spaceSize) + 10, 60);
            this.Controls.Add(lbl);


            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Button btn = new Button();
                    btn.Location = new Point(x * (buttonSize + spaceSize) + 5, y * (buttonSize + spaceSize) + 25);
                    btn.Size = new Size(buttonSize, buttonSize);
                    btn.BackColor = Color.DarkGray;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Text = "1";
                    btn.AccessibleName = "csa";
                    btn.UseCompatibleTextRendering = true;
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold);
                    System.EventArgs xd = new System.EventArgs();
                    btn.Click += new System.EventHandler(this.buttonClick);
                    this.Controls.Add(btn);
                    buttons[x, y] = btn;
                }
            }


        }
    }
}
