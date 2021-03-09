using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjektFeladat
{
    public partial class FormP : Form
    {    
        Random rnd = new Random();

        //Változók
        static Button[,] btns = new Button[9, 9]; //A tábla gombjai
        static int[,] szamok = new int[9, 9]; //Random generált számok
        static List<int> egytol_kilencig = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 }; //Lehetséges számok

        static DateTime now = DateTime.Now;

        static bool nyert = false;

        static int seconds = 300; //5 perc van a megoldásra

        static int remaining_hints = 3; //Megmaradt segítségek

        //Tábla betöltése
        public FormP()
        {
            InitializeComponent();

            Tablakeszitese();
            //Vonalak "festese" esztétikai miatt
            this.Paint += Form1_Paint;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gs = this.CreateGraphics();

            Pen pen = new Pen(Color.FromArgb(189, 185, 185));
            pen.Width = 3;

            //Vertikális
            gs.DrawLine(pen, new Point(324, 54), new Point(324, 506));
            gs.DrawLine(pen, new Point(474, 54), new Point(474, 506));

            //Horizontális
            gs.DrawLine(pen, new Point(175, 205), new Point(625, 205));
            gs.DrawLine(pen, new Point(175, 355), new Point(625, 355));
        }
        private void Tablakeszitese()
        {
            int kezdox = 180;
            int kezdoy = 60;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    btns[i, j] = new Button();

                    //Méret és hely
                    btns[i, j].Size = new Size(40, 40);
                    if (j == 0 && i == 0)
                    {
                        btns[i, j].Location = new Point(kezdox, kezdoy);
                    }
                    else if (i == 0 && j != 0)
                    {
                        kezdox += 50; //X tengelyen 10 van a gombok között 
                        btns[i, j].Location = new Point(kezdox, kezdoy);
                    }
                    else if (i != 0 && j == 0)
                    {
                        btns[i, j].Location = new Point(kezdox, kezdoy);
                    }
                    else
                    {
                        kezdox += 50;
                        btns[i, j].Location = new Point(kezdox, kezdoy);
                    }

                    //Kinézet
                    btns[i, j].ForeColor = Color.Black;
                    btns[i, j].BackColor = Color.FromArgb(221, 221, 221);
                    btns[i, j].FlatAppearance.BorderSize = 0;
                    btns[i, j].FlatStyle = FlatStyle.Flat;

                    //Funkcionalitás
                    btns[i, j].KeyPress += btn_KeyPress;

                    //Formhoz hozzáadás
                    this.Controls.Add(btns[i, j]);
                }

                kezdox = 180;
                kezdoy += 50; //Y tengelyen is 10 van gombok között

            }

            SzamokkalKitoltes();

        }

        //Játél előkészítés és maga a játék
        private void SzamokkalKitoltes()
        {
            for (int i = 0; i < 9; i++)
            {
                int sor_probak = 0;
                for (int j = 0; j < 9; j++)
                {
                    egytol_kilencig = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                    bool kenyszerkilepes = false;
                    int oszlop_probak = 0;
                    int szam;

                    while (!Helyes_e(szam = egytol_kilencig[rnd.Next(0, egytol_kilencig.Count)], i, j) && kenyszerkilepes == false)
                    {
                        egytol_kilencig.Remove(szam); //Ha nem jó a szám akkor eltávolítja a listából, hogy véletlenül nem válassza újra

                        oszlop_probak++;
                        if (oszlop_probak > 7)
                        {
                            //7-nél több lépésnél kényszerkilépés
                            kenyszerkilepes = true;
                        }
                    }

                    if (!kenyszerkilepes)
                    {
                        szamok[i, j] = szam;
                    }
                    else
                    {
                        j = 0; //egész sor újra csinálása
                        sor_probak++;
                        if (sor_probak > 2)
                        {
                            if (i != 0)
                            {
                                //Ha nem lehetséges egy következő sornyi számot elő állítani, akkor az előző sort kezdő előről
                                i--;
                                sor_probak = 0;
                                if (DateTime.Now - now > new TimeSpan(0, 0, 5))
                                {
                                    //Ha 5-nél több másodpercig megy már az egészet újra kezdi előről.
                                    SzamokkalKitoltes();
                                }
                            }
                        }
                    }
                }
            }

            Jatekinditasa();
        }
        private void Jatekinditasa()
        {
            SzamokMutatasa(55);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (btns[i, j].Text != "")
                    {
                        btns[i, j].Enabled = false; //55 kirakott számon ne lehessen változtatni
                    }
                    else
                    {
                        btns[i, j].Enabled = true;
                        btns[i, j].ForeColor = Color.White;
                        btns[i, j].Font = new Font(btns[i, j].Font, FontStyle.Bold);
                    }
                }
            }
        }

        //Kiegészítő funkciók
        private bool Helyes_e(int szam, int i, int j)
        {
            //Megnézi, hogy a sorban van-e már ilyen szám
            for (int k = 0; k < 9; k++)
            {
                if (j != k && szamok[i, k] == szam)
                {
                    return false;
                }
            }

            //Megnézi, hogy az oszlopban van-e már ilyen szám
            for (int l = 0; l < 9; l++)
            {
                if (i != l && szamok[l, j] == szam)
                {
                    return false;
                }
            }

            //Megnézi, hogy a 3x3 részben van-e már ilyen szám
            if (i == 2 || i == 5 || i == 8)
            {
                if (j == 0 || j == 3 || j == 6)
                {
                    if (szamok[i - 1, j + 1] == szam || szamok[i - 1, j + 2] == szam || szamok[i - 2, j + 1] == szam || szamok[i - 2, j + 2] == szam)
                    {
                        return false;
                    }
                }
                else if (j == 1 || j == 4 || j == 7)
                {
                    if (szamok[i - 1, j + 1] == szam || szamok[i - 2, j + 1] == szam || szamok[i - 1, j - 1] == szam || szamok[i - 2, j - 1] == szam)
                    {
                        return false;
                    }
                }
                else if (j == 2 || j == 5 || j == 8)
                {
                    if (szamok[i - 1, j - 1] == szam || szamok[i - 2, j - 1] == szam || szamok[i - 1, j - 2] == szam || szamok[i - 2, j - 2] == szam)
                    {
                        return false;
                    }
                }
            }
            else if (i == 1 || i == 4 || i == 7)
            {
                if (j == 0 || j == 3 || j == 6)
                {
                    if (szamok[i - 1, j + 1] == szam || szamok[i - 1, j + 2] == szam)
                    {
                        return false;
                    }
                }
                else if (j == 1 || j == 4 || j == 7)
                {
                    if (szamok[i - 1, j + 1] == szam || szamok[i - 1, j - 1] == szam)
                    {
                        return false;
                    }
                }
                else if (j == 2 || j == 5 || j == 8)
                {
                    if (szamok[i - 1, j - 1] == szam || szamok[i - 1, j - 2] == szam)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private bool Check_e()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (btns[i, j].Text == "")
                    {
                        return false;
                    }
                }
            }

            hint.Text = "Check";
            return true;
        }
        private void SzamokMutatasa(int darab)
        {
            for (int i = 0; i < darab; i++)
            {
                int sor = rnd.Next(0, 9);
                int oszlop = rnd.Next(0, 9);
                if (btns[sor, oszlop].Text != "")
                {
                    i--;
                }
                else if (btns[sor, oszlop].Text == "" || btns[sor, oszlop].ForeColor == Color.Red)
                {
                    btns[sor, oszlop].Text = Convert.ToString(szamok[sor, oszlop]);
                }
            }
        }

        //Eventek
        private void btn_KeyPress(object sender, KeyPressEventArgs e)
        {
            Button btn = sender as Button;
            btn.Text = e.KeyChar.ToString();
            btn.ForeColor = Color.White;

            Check_e();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool nyert_e = true;
            if (hint.Text == "Check")
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (btns[i, j].Text != Convert.ToString(szamok[i, j]))
                        {
                            nyert_e = false;
                            btns[i, j].ForeColor = Color.Red;
                            if (remaining_hints != 0)
                            {
                                hint.Text = "Hint";
                            }
                        }
                    }
                }
                if (nyert_e)
                {
                    nyert = true;
                    insert_to_sql(seconds,remaining_hints);
                    MessageBox.Show("Gratulálok!Nyert!" + Environment.NewLine + "Pontszáma: " + seconds);
                }
            }
            else
            {
                if (remaining_hints != 0 && hint.Text != "Check")
                {
                    bool sikeres_hint_adas = false;
                    while (!sikeres_hint_adas)
                    {
                        int sor = rnd.Next(0, 9);
                        int oszlop = rnd.Next(0, 9);
                        if (btns[sor, oszlop].Text == "" || btns[sor, oszlop].ForeColor == Color.Red)
                        {
                            btns[sor, oszlop].Text = Convert.ToString(szamok[sor, oszlop]);
                            btns[sor, oszlop].ForeColor = Color.White;
                            sikeres_hint_adas = true;
                        }
                    }
                    remaining_hints--;
                    segitseg_label.Text = "Megmaradt segítség: " + remaining_hints;
                }
                else
                {
                    hint.Text = "Check";
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (seconds > 0)
            {
                timer_label.Text = "0" + (seconds / 60) + ":" + ((seconds % 60) < 10 ? "0" + Convert.ToString(seconds % 60) : Convert.ToString(seconds % 60));
                seconds--;
            }
            else if (seconds <= 0 && !nyert)
            {
                MessageBox.Show("Sajnos vesztett");
                Application.Exit();
            }
        }
        
        //SQL
        private void insert_to_sql(int seconds, int help)
		{
			string connStr = "server=35.207.89.236;user=game;database=statistics;password='F^zL!&5TN00@!lhpOxngxNs1K9iJur'";
			MySqlConnection conn = new MySqlConnection(connStr);
			conn.Open();
			string sql = $"INSERT INTO sudoku (seconds, help) VALUES ({seconds}, {help})";
			MySqlCommand cmd = new MySqlCommand(sql, conn);
			MySqlDataReader rdr = cmd.ExecuteReader();

			conn.Close();
		}
    }
}
