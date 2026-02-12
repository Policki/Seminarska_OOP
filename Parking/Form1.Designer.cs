namespace Parking
{
    partial class Form1
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
            this.Parkirisce = new System.Windows.Forms.ComboBox();
            this.Registrska = new System.Windows.Forms.TextBox();
            this.TipVozila = new System.Windows.Forms.ComboBox();
            this.Visina = new System.Windows.Forms.NumericUpDown();
            this.Cas = new System.Windows.Forms.NumericUpDown();
            this.Kwh = new System.Windows.Forms.NumericUpDown();
            this.Voda = new System.Windows.Forms.NumericUpDown();
            this.Prihod = new System.Windows.Forms.Button();
            this.Izhod = new System.Windows.Forms.Button();
            this.Aktivno = new System.Windows.Forms.ListBox();
            this.Stanje = new System.Windows.Forms.Label();
            this.Cena = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Visina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Kwh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Voda)).BeginInit();
            this.SuspendLayout();
            // 
            // Parkirisce
            // 
            this.Parkirisce.FormattingEnabled = true;
            this.Parkirisce.Location = new System.Drawing.Point(250, 54);
            this.Parkirisce.Name = "Parkirisce";
            this.Parkirisce.Size = new System.Drawing.Size(121, 21);
            this.Parkirisce.TabIndex = 0;
            // 
            // Registrska
            // 
            this.Registrska.Location = new System.Drawing.Point(251, 81);
            this.Registrska.Name = "Registrska";
            this.Registrska.Size = new System.Drawing.Size(121, 20);
            this.Registrska.TabIndex = 1;
            // 
            // TipVozila
            // 
            this.TipVozila.FormattingEnabled = true;
            this.TipVozila.Location = new System.Drawing.Point(250, 25);
            this.TipVozila.Name = "TipVozila";
            this.TipVozila.Size = new System.Drawing.Size(121, 21);
            this.TipVozila.TabIndex = 2;
            // 
            // Visina
            // 
            this.Visina.Location = new System.Drawing.Point(251, 107);
            this.Visina.Name = "Visina";
            this.Visina.Size = new System.Drawing.Size(120, 20);
            this.Visina.TabIndex = 3;
            // 
            // Cas
            // 
            this.Cas.Location = new System.Drawing.Point(250, 133);
            this.Cas.Name = "Cas";
            this.Cas.Size = new System.Drawing.Size(122, 20);
            this.Cas.TabIndex = 4;
            // 
            // Kwh
            // 
            this.Kwh.Location = new System.Drawing.Point(250, 159);
            this.Kwh.Name = "Kwh";
            this.Kwh.Size = new System.Drawing.Size(122, 20);
            this.Kwh.TabIndex = 5;
            // 
            // Voda
            // 
            this.Voda.Location = new System.Drawing.Point(250, 185);
            this.Voda.Name = "Voda";
            this.Voda.Size = new System.Drawing.Size(122, 20);
            this.Voda.TabIndex = 6;
            // 
            // Prihod
            // 
            this.Prihod.Location = new System.Drawing.Point(405, 23);
            this.Prihod.Name = "Prihod";
            this.Prihod.Size = new System.Drawing.Size(75, 23);
            this.Prihod.TabIndex = 7;
            this.Prihod.Text = "Prihod";
            this.Prihod.UseVisualStyleBackColor = true;
            // 
            // Izhod
            // 
            this.Izhod.Location = new System.Drawing.Point(405, 52);
            this.Izhod.Name = "Izhod";
            this.Izhod.Size = new System.Drawing.Size(75, 23);
            this.Izhod.TabIndex = 8;
            this.Izhod.Text = "Izhod";
            this.Izhod.UseVisualStyleBackColor = true;
            // 
            // Aktivno
            // 
            this.Aktivno.FormattingEnabled = true;
            this.Aktivno.Location = new System.Drawing.Point(12, 25);
            this.Aktivno.Name = "Aktivno";
            this.Aktivno.Size = new System.Drawing.Size(233, 186);
            this.Aktivno.TabIndex = 9;
            // 
            // Stanje
            // 
            this.Stanje.AutoSize = true;
            this.Stanje.Location = new System.Drawing.Point(402, 124);
            this.Stanje.Name = "Stanje";
            this.Stanje.Size = new System.Drawing.Size(35, 13);
            this.Stanje.TabIndex = 10;
            this.Stanje.Text = "stanje";
            // 
            // Cena
            // 
            this.Cena.AutoSize = true;
            this.Cena.Location = new System.Drawing.Point(402, 159);
            this.Cena.Name = "Cena";
            this.Cena.Size = new System.Drawing.Size(31, 13);
            this.Cena.TabIndex = 11;
            this.Cena.Text = "cena";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Cena);
            this.Controls.Add(this.Stanje);
            this.Controls.Add(this.Aktivno);
            this.Controls.Add(this.Izhod);
            this.Controls.Add(this.Prihod);
            this.Controls.Add(this.Voda);
            this.Controls.Add(this.Kwh);
            this.Controls.Add(this.Cas);
            this.Controls.Add(this.Visina);
            this.Controls.Add(this.TipVozila);
            this.Controls.Add(this.Registrska);
            this.Controls.Add(this.Parkirisce);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Visina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Kwh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Voda)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Parkirisce;
        private System.Windows.Forms.TextBox Registrska;
        private System.Windows.Forms.ComboBox TipVozila;
        private System.Windows.Forms.NumericUpDown Visina;
        private System.Windows.Forms.NumericUpDown Cas;
        private System.Windows.Forms.NumericUpDown Kwh;
        private System.Windows.Forms.NumericUpDown Voda;
        private System.Windows.Forms.Button Prihod;
        private System.Windows.Forms.Button Izhod;
        private System.Windows.Forms.ListBox Aktivno;
        private System.Windows.Forms.Label Stanje;
        private System.Windows.Forms.Label Cena;
    }
}

