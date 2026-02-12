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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Parking
{
    public partial class Form1 : Form
    {
        private readonly List<Parkirisce> parkirisca = new();
        private readonly Dictionary<string, (Parkirisce parkirisce, ParkirnaSeja seja)> _aktivneSeje = new();

        public Form1()
        {
            InitializeComponent();
            InicializirajPodatke();
            InicializirajVmesnik();
            OsveziStanje();
        }

        private void InicializirajPodatke()
        {
            parkirisca.Add(new ParkirisceNaProstem("Zunanje - Center", 50, 1.50m));
            parkirisca.Add(new ParkirnaHisa("Parkirna hiša - City", 120, 2.20m, 2.00));
            parkirisca.Add(new ParkirisceZaAvtodome("Avtodomi - Kamp", 20, 15m, 0.40m, 0.005m));
        }

        private void InicializirajVmesnik()
        {
            Parkirisce.DataSource = parkirisca;

            TipVozila.DataSource = Enum.GetValues(typeof(TipVozila));

            Cas.Minimum = 1;
            Cas.Maximum = 100000;

            Kwh.Minimum = 0;
            Kwh.Maximum = 500;

            Voda.Minimum = 0;
            Voda.Maximum = 50000;

            Visina.Minimum = 1.0m;
            Visina.Maximum = 4.0m;
            Visina.DecimalPlaces = 2;
            Visina.Increment = 0.01m;
            Visina.Value = 1.60m;
        }

        private Parkirisce IzbranoParkirisce => (Parkirisce)Parkirisce.SelectedItem;

        private void Prihod_Click(object sender, EventArgs e)
        {
            try
            {
                var registrska = Registrska.Text.Trim();
                var tip = (Vozila)TipVozila.SelectedItem!;
                var visina = (double)Visina.Value;

                var vozilo = new Vozilo(registrska, tip, visina);

                if (_aktivneSeje.ContainsKey(vozilo.RegistrskaOznaka))
                {
                    MessageBox.Show("To vozilo je že parkirano.", "Opozorilo");
                    return;
                }

                if (IzbranoParkirisce.PoskusiVstopi(vozilo, out var seja) && seja != null)
                {
                    _aktivneSeje[vozilo.RegistrskaOznaka] = (IzbranoParkirisce, seja);
                    Aktivno.Items.Add(seja);
                    Cena.Text = "Cena: -";
                }
                else
                {
                    MessageBox.Show("Vstop ni dovoljen (ni prostora ali omejitve).", "Zavrnjeno");
                }
            }
            catch (Exception napaka)
            {
                MessageBox.Show(napaka.Message, "Napaka");
            }
            finally
            {
                OsveziStanje();
            }
        }

        private void Izhod_Click(object sender, EventArgs e)
        {
            if (Aktivno.SelectedItem is not ParkirnaSeja izbranaSeja)
            {
                MessageBox.Show("Izberi aktivno sejo.", "Info");
                return;
            }

            var registrska = izbranaSeja.Vozilo.RegistrskaOznaka;

            if (!_aktivneSeje.TryGetValue(registrska, out var zapis))
                return;

            var parkirisce = zapis.parkirisce;
            var seja = zapis.seja;

            // zapremo sejo
            seja.Zapri(DateTime.Now);

            // trajanje vzamemo iz dejanske seje ali pa iz numMinute (za demonstracijo lahko uporabiš numMinute)
            var trajanje = TimeSpan.FromMinutes((double)Minute.Value);

            // priključki (uporabno za avtodome)
            var kwh = (double)Kwh.Value;
            var voda = (double)Voda.Value;

            // POLIMORFIZEM: isti klic, različna implementacija glede na tip parkirišča
            Denar cena = parkirisce.IzracunajCeno(seja.Vozilo, trajanje, kwh, voda);

            Cena.Text = $"Cena: {cena}";

            // pospravimo
            parkirisce.OznaciIzstop();
            _aktivneSeje.Remove(registrska);
            Aktivno.Items.Remove(izbranaSeja);
            izbranaSeja.Dispose();

            OsveziStanje();
        }

        private void OsveziStanje()
        {
            var parkirisce = IzbranoParkirisce;

            int prostaMesta = Parkirisce.IzracunajProstaMesta(parkirisce); // static metoda

            Stanje.Text =
                $"Izbrano: {parkirisce}\n" +
                $"Prosta mesta: {prostaMesta}\n" +
                $"Ustvarjenih parkirišč: {Parkirisce.SteviloUstvarjenihParkirisc}\n" +
                $"Ustvarjenih sej: {ParkirnaSeja.SteviloUstvarjenihSej}";
        }

        private void cmbParkirisce_SelectedIndexChanged(object sender, EventArgs e)
        {
            OsveziStanje();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
