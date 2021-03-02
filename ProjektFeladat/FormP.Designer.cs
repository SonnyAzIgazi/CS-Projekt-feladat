namespace ProjektFeladat
{
    partial class FormP
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.hint = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timer_label = new System.Windows.Forms.Label();
            this.segitseg_label = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // hint
            // 
            this.hint.BackColor = System.Drawing.Color.Black;
            this.hint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hint.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.hint.ForeColor = System.Drawing.Color.White;
            this.hint.Location = new System.Drawing.Point(352, 12);
            this.hint.Name = "hint";
            this.hint.Size = new System.Drawing.Size(94, 35);
            this.hint.TabIndex = 0;
            this.hint.Text = "Hint";
            this.hint.UseVisualStyleBackColor = false;
            this.hint.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Black;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(54, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 35);
            this.button2.TabIndex = 1;
            this.button2.Text = "<-";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // timer_label
            // 
            this.timer_label.AutoSize = true;
            this.timer_label.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.timer_label.Location = new System.Drawing.Point(357, 513);
            this.timer_label.Name = "timer_label";
            this.timer_label.Size = new System.Drawing.Size(107, 37);
            this.timer_label.TabIndex = 2;
            this.timer_label.Text = "00:00";
            // 
            // segitseg_label
            // 
            this.segitseg_label.AutoSize = true;
            this.segitseg_label.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.segitseg_label.Location = new System.Drawing.Point(623, 32);
            this.segitseg_label.Name = "segitseg_label";
            this.segitseg_label.Size = new System.Drawing.Size(154, 15);
            this.segitseg_label.TabIndex = 3;
            this.segitseg_label.Text = "Megmaradt segítség: 3";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 559);
            this.Controls.Add(this.segitseg_label);
            this.Controls.Add(this.timer_label);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.hint);
            this.Name = "Sudoku";
            this.Text = "Sudoku";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button hint;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label timer_label;
        private System.Windows.Forms.Label segitseg_label;
        private System.Windows.Forms.Timer timer1;
    }
}