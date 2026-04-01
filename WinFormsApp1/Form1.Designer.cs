namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Aktivno = new ListBox();
            TipVozila = new ComboBox();
            Parkirisce = new ComboBox();
            Registrska = new TextBox();
            Visina = new NumericUpDown();
            Cas = new NumericUpDown();
            Prihod = new Button();
            Izhod = new Button();
            Stanje = new Label();
            Cena = new Label();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)Visina).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Cas).BeginInit();
            SuspendLayout();
            // 
            // Aktivno
            // 
            Aktivno.FormattingEnabled = true;
            Aktivno.ItemHeight = 15;
            Aktivno.Location = new Point(12, 25);
            Aktivno.Name = "Aktivno";
            Aktivno.Size = new Size(263, 199);
            Aktivno.TabIndex = 0;
            // 
            // TipVozila
            // 
            TipVozila.FormattingEnabled = true;
            TipVozila.Location = new Point(281, 25);
            TipVozila.Name = "TipVozila";
            TipVozila.Size = new Size(204, 23);
            TipVozila.TabIndex = 1;
            // 
            // Parkirisce
            // 
            Parkirisce.FormattingEnabled = true;
            Parkirisce.Location = new Point(281, 54);
            Parkirisce.Name = "Parkirisce";
            Parkirisce.Size = new Size(204, 23);
            Parkirisce.TabIndex = 2;
            // 
            // Registrska
            // 
            Registrska.Location = new Point(281, 83);
            Registrska.Name = "Registrska";
            Registrska.Size = new Size(204, 23);
            Registrska.TabIndex = 3;
            // 
            // Visina
            // 
            Visina.Location = new Point(281, 133);
            Visina.Name = "Visina";
            Visina.Size = new Size(204, 23);
            Visina.TabIndex = 4;
            // 
            // Cas
            // 
            Cas.Location = new Point(281, 180);
            Cas.Name = "Cas";
            Cas.Size = new Size(204, 23);
            Cas.TabIndex = 5;
            // 
            // Prihod
            // 
            Prihod.Location = new Point(514, 25);
            Prihod.Name = "Prihod";
            Prihod.Size = new Size(75, 23);
            Prihod.TabIndex = 8;
            Prihod.Text = "Prihod";
            Prihod.UseVisualStyleBackColor = true;
            Prihod.Click += Prihod_Click;
            // 
            // Izhod
            // 
            Izhod.Location = new Point(514, 54);
            Izhod.Name = "Izhod";
            Izhod.Size = new Size(75, 23);
            Izhod.TabIndex = 9;
            Izhod.Text = "Izhod";
            Izhod.UseVisualStyleBackColor = true;
            Izhod.Click += Izhod_Click;
            // 
            // Stanje
            // 
            Stanje.AutoSize = true;
            Stanje.Location = new Point(514, 86);
            Stanje.Name = "Stanje";
            Stanje.Size = new Size(39, 15);
            Stanje.TabIndex = 10;
            Stanje.Text = "Stanje";
            // 
            // Cena
            // 
            Cena.AutoSize = true;
            Cena.Location = new Point(514, 232);
            Cena.Name = "Cena";
            Cena.Size = new Size(0, 15);
            Cena.TabIndex = 11;
            Cena.Click += Cena_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(282, 116);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 12;
            label1.Text = "Višina:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(281, 162);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 13;
            label2.Text = "Čas:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Cena);
            Controls.Add(Stanje);
            Controls.Add(Izhod);
            Controls.Add(Prihod);
            Controls.Add(Cas);
            Controls.Add(Visina);
            Controls.Add(Registrska);
            Controls.Add(Parkirisce);
            Controls.Add(TipVozila);
            Controls.Add(Aktivno);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)Visina).EndInit();
            ((System.ComponentModel.ISupportInitialize)Cas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox Aktivno;
        private ComboBox TipVozila;
        private ComboBox Parkirisce;
        private TextBox Registrska;
        private NumericUpDown Visina;
        private NumericUpDown Cas;
        private Button Prihod;
        private Button Izhod;
        private Label Stanje;
        private Label Cena;
        private Label label1;
        private Label label2;
    }
}
