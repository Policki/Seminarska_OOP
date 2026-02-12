using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using knjiznica;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private List<knjiznica.Parkirisce> seznam = new List<knjiznica.Parkirisce>();
        private Dictionary<string, (knjiznica.Parkirisce p, Seja s)> aktivno
            = new Dictionary<string, (knjiznica.Parkirisce, Seja)>();

        public Form1()
        {
            InitializeComponent();
            Napolni();
            Nastavi();
            Prikazi();
        }

        private void Napolni()
        {
            seznam.Add(new Zunanje("Zunanje", 50, 1.50m));
            seznam.Add(new Hisa("Hiša", 120, 2.20m, 2.00));
            seznam.Add(new Kamp("Kamp", 20, 3.00m));
        }

        private void Nastavi()
        {
            Parkirisce.DataSource = seznam;
            TipVozila.DataSource = Enum.GetValues(typeof(TipVozila));

            Cas.Minimum = 1;
            Cas.Maximum = 240;
            Cas.Value = 1;

            Visina.Minimum = 1.0m;
            Visina.Maximum = 4.0m;
            Visina.DecimalPlaces = 2;
            Visina.Increment = 0.1m;
            Visina.Value = 1.60m;

            Cena.Text = "Cena: -";
        }

        private knjiznica.Parkirisce Izbrano()
        {
            return (knjiznica.Parkirisce)Parkirisce.SelectedItem;
        }

        private void Prihod_Click(object sender, EventArgs e)
        {
            try
            {
                string reg = Registrska.Text.Trim();
                var tip = (TipVozila)TipVozila.SelectedItem;
                double vis = (double)Visina.Value;
                decimal cena=Cas.Value;

                Vozilo v = new Vozilo(reg, tip, vis);
                
                if (aktivno.ContainsKey(v.Registrska))
                {
                    MessageBox.Show("Vozilo je že notri.");
                    return;
                }

                Seja s;
                double ure = (double)Cas.Value;
                bool ok = Izbrano().Vstopi(v, ure, out s);

                if (!ok || s == null)
                {
                    MessageBox.Show("Ni mogoèe vstopiti (ni prostora ali omejitev).");
                    return;
                }

                aktivno[v.Registrska] = (Izbrano(), s);
                Aktivno.Items.Add(s);

                Cena.Text = "Cena: -";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Prikazi();
            Pocisti();
        }

        private void Izhod_Click(object sender, EventArgs e)
        {
            try
            {
                if (Aktivno.SelectedItem == null)
                {
                    MessageBox.Show("Izberi sejo.");
                    return;
                }

                Seja s = (Seja)Aktivno.SelectedItem;
                string reg = s.Vozilo.Registrska;

                if (!aktivno.ContainsKey(reg))
                {
                    MessageBox.Show("Seje ni v slovarju.");
                    return;
                }

                var zapis = aktivno[reg];
                var p = zapis.p;

                double ure = s.Ure;   
                Denar c = p.Cena(ure);

                Cena.Text = "Cena: " + c.ToString();

                p.Izstopi();
                aktivno.Remove(reg);
                Aktivno.Items.Remove(s);
                s.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka: " + ex.Message);
            }
        }

        private void Prikazi()
        {
            if (Parkirisce.SelectedItem == null) return;

            var p = Izbrano();
            int prosto = knjiznica.Parkirisce.Prosto(p);

            Stanje.Text =
                "Izbrano: " + p.ToString() + Environment.NewLine +
                "Prosto: " + prosto + Environment.NewLine +
                "Parkirišèa: " + knjiznica.Parkirisce.StevecParkirisc + Environment.NewLine +
                "Seje: " + Seja.StevecSej;
        }

        private void Pocisti()
        {
            Registrska.Clear();
            Visina.Value = 1.60m;
            Cas.Value = 1;
        }

        private void Parkirisce_SelectedIndexChanged(object sender, EventArgs e)
        {
            Prikazi();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void Cena_Click(object sender, EventArgs e)
        {
            
        }
    }
}