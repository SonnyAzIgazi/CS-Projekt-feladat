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
    public partial class FormS : Form
    {
        static string connStr = "server=35.207.89.236;user=game;database=statistics;password='F^zL!&5TN00@!lhpOxngxNs1K9iJur'";
        private MySqlConnection conn;


        public FormS()
        {
            InitializeComponent();
        }

        private void FormS_Load(object sender, EventArgs e)
        {
            //this.BackColor = Color.FromArgb(92, 216, 90);
            //this.ForeColor = Color.FromArgb(8, 49, 58);
            label2.Visible = false;
            conn = new MySqlConnection(connStr);
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void update_DGV()
        {
            dataGridView1.Columns.Clear();

            label2.Visible = false;
            if (radioButton1.Checked)
            {
                conn.Open();
                string sql = "SELECT * FROM tilegame";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    dataGridView1.Columns.Add(rdr.GetName(i), rdr.GetName(i));
                }

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        object[] row = new object[rdr.FieldCount];

                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            row[i] = rdr.GetValue(i);
                        }

                        dataGridView1.Rows.Add(row);
                    }
                }
                else
                {
                    label2.Visible = true;
                }

                conn.Close();
            }
            else if (radioButton2.Checked)
            {
                conn.Open();
                string sql = "SELECT * FROM sudoku";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    dataGridView1.Columns.Add(rdr.GetName(i), rdr.GetName(i));
                }

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        object[] row = new object[rdr.FieldCount];

                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            row[i] = rdr.GetValue(i);
                        }

                        dataGridView1.Rows.Add(row);
                    }
                }
                else
                {
                    label2.Visible = true;
                }

                conn.Close();
            }
            else if (radioButton3.Checked)
            {
                conn.Open();
                string sql = "SELECT * FROM aknakereso";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    dataGridView1.Columns.Add(rdr.GetName(i), rdr.GetName(i));
                }

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        object[] row = new object[rdr.FieldCount];

                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            row[i] = rdr.GetValue(i);
                        }

                        dataGridView1.Rows.Add(row);
                    }
                }
                else
                {
                    label2.Visible = true;
                }

                conn.Close();
            }
        }

        

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            update_DGV();
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            update_DGV();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            update_DGV();
        }
    }
}
