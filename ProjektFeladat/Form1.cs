﻿using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormV xd = new FormV();
            xd.ShowDialog(this);
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormP xd = new FormP();
            xd.ShowDialog(this);
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormV xd = new FormV();
            xd.ShowDialog(this);
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormV xd = new FormV();
            xd.ShowDialog(this);
            this.Show();
        }
    }
}