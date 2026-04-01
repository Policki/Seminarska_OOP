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
        private List<Parkirisce> seznam = new List<Parkirisce>();
        private Dictionary<string, (Parkirisce p, Seja s)> aktivno =
            new Dictionary<string, (Parkirisce, Seja)>();

        public Form1()
        {
            InitializeComponent();
            Napolni();
            Nastavi();
            PoveziObvestila();
            Prikazi();
        }

        private void Napolni()
        {
            seznam.Add(new Zunanje("Zunanje", 50, 1.5m));
            seznam.Add(new Hisa("Hiša", 120, 2.2m, 2.0));
            seznam.Add(new Kamp("Kamp", 20, 3.0m));
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
            Visina.Value = 1.6m;

            Cena.Text = "Cena: -";
        }

        private void PoveziObvestila()
        {
            foreach (Parkirisce p in seznam)
            {
                p.Obvestilo += Obvestilo;
            }
        }

        private void Obvestilo(string s)
        {
            MessageBox.Show(s);
        }

        private Parkirisce Izbrano()
        {
            return (Parkirisce)Parkirisce.SelectedItem;
        }

        private void Prihod_Click(object sender, EventArgs e)
        {
            try
            {
                string reg = Registrska.Text.Trim();
                TipVozila tip = (TipVozila)TipVozila.SelectedItem;
                double vis = (double)Visina.Value;
                double ure = (double)Cas.Value;

                Vozilo v = new Vozilo(reg, tip, vis);

                if (aktivno.ContainsKey(v.Registrska))
                {
                    MessageBox.Show("To vozilo je že noter.");
                    return;
                }

                Parkirisce p = Izbrano();
                Seja s;

                bool ok = p.Vstopi(v, ure, out s);

                if (!ok || s == null)
                {
                    MessageBox.Show("Ni možno vstopiti.");
                    return;
                }

                aktivno[v.Registrska] = (p, s);
                Aktivno.Items.Add(s);

                MessageBox.Show("Dodano vozilo. ID: " + s.Id);

                Cena.Text = "Cena: -";

                Prikazi();
                Pocisti();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Izhod_Click(object sender, EventArgs e)
        {
            try
            {
                if (Aktivno.SelectedItem == null)
                {
                    MessageBox.Show("Izberi nekaj.");
                    return;
                }

                Seja s = (Seja)Aktivno.SelectedItem;
                string reg = s.Vozilo.Registrska;

                if (!aktivno.ContainsKey(reg))
                {
                    MessageBox.Show("Napaka.");
                    return;
                }

                var zapis = aktivno[reg];
                Parkirisce p = zapis.p;

                ICena cenik = p;
                Denar c = cenik.Cena(s.Ure);

                Cena.Text = "Cena: " + c;

                string podatki =
                    "Parkiranje:\n" +
                    "Reg: " + s.Vozilo.Registrska + "\n" +
                    "Ure: " + s.Ure + "\n" +
                    "Cena: " + c + "\n";

                p.Izstopi(reg);
                aktivno.Remove(reg);
                Aktivno.Items.Remove(s);
                s.Dispose();

                MessageBox.Show(podatki);

                Prikazi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka: " + ex.Message);
            }
        }

        private void Prikazi()
        {
            if (Parkirisce.SelectedItem == null) return;

            Parkirisce p = Izbrano();
            int prosto = knjiznica.Parkirisce.Prosto(p);

            string txt =
                "Parkirišèe: " + p.Ime + "\n" +
                "Zasedeno: " + p.Zasedeno + "/" + p.Kapaciteta + "\n" +
                "Prosto: " + prosto + "\n" +
                "Prihodek: " + p.Prihodek + "\n" +
                "Seje: " + Seja.StevecSej;

            if (p is Hisa h)
            {
                txt += "\nMax višina: " + h.MaxVisina;
            }

            Stanje.Text = txt;
        }

        private void Pocisti()
        {
            Registrska.Clear();
            Visina.Value = 1.6m;
            Cas.Value = 1;
        }

        private void Parkirisce_SelectedIndexChanged(object sender, EventArgs e)
        {
            Prikazi();
        }

        private void Aktivno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Aktivno.SelectedItem == null) return;

            Seja s = (Seja)Aktivno.SelectedItem;

            Cena.Text = "Izbrano: " + s.Vozilo.Registrska;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Prikazi();
        }

        private void Cena_Click(object sender, EventArgs e)
        {
        }
    }
}