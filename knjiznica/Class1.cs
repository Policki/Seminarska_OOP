using System;

using System;
using System.Collections.Generic;

namespace knjiznica
{
    public enum TipVozila
    {
        Avto,
        Motor,
        Tovornjak,
        ElektricniAvto,
        Avtodom
    }

    public interface ICena
    {
        Denar Cena(double ure);
    }

    public delegate void ObvestiloHandler(string sporocilo);

    public readonly struct Denar
    {
        public const string Valuta = "EUR";
        public decimal Znesek { get; }

        /// <summary>
        /// Ustvari nov denarni znesek in prepreči negativno vrednost.
        /// </summary>
        public Denar(decimal znesek)
        {
            if (znesek < 0)
                Znesek = 0;
            else
                Znesek = znesek;
        }

        /// <summary>
        /// Sešteje dva denarna zneska.
        /// </summary>
        public static Denar operator +(Denar a, Denar b)
        {
            return new Denar(a.Znesek + b.Znesek);
        }

        /// <summary>
        /// Vrne nizovno predstavitev denarnega zneska z valuto.
        /// </summary>
        public override string ToString()
        {
            return Znesek.ToString("0.00") + " " + Valuta;
        }
    }

    public class Vozilo
    {
        private string registrska;

        public string Registrska
        {
            get { return registrska; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Registrska ne sme biti prazna.");

                registrska = value.Trim().ToUpper();
            }
        }

        public TipVozila Tip { get; private set; }
        public double Visina { get; private set; }

        /// <summary>
        /// Ustvari novo vozilo z registrsko oznako, tipom in višino.
        /// </summary>
        public Vozilo(string registrska, TipVozila tip, double visina)
        {
            Registrska = registrska;
            Tip = tip;

            if (visina <= 0)
                Visina = 1.6;
            else
                Visina = visina;
        }

        /// <summary>
        /// Vrne osnovni opis vozila.
        /// </summary>
        public override string ToString()
        {
            return Registrska + " (" + Tip + ")";
        }
    }

    public class Seja : IDisposable
    {
        public static int StevecSej { get; private set; }

        public Vozilo Vozilo { get; private set; }
        public DateTime Vstop { get; private set; }
        public double Ure { get; private set; }

        public readonly Guid Id = Guid.NewGuid();

        private bool koncano;

        /// <summary>
        /// Ustvari novo parkirno sejo za podano vozilo.
        /// </summary>
        public Seja(Vozilo vozilo, double ure)
        {
            Vozilo = vozilo;
            Vstop = DateTime.Now;
            Ure = ure;
            StevecSej++;
        }

        /// <summary>
        /// Sprosti vire seje ob uničenju objekta.
        /// </summary>
        ~Seja()
        {
            StevecSej--;
        }

        /// <summary>
        /// Zaključi sejo in prepreči ponovno čiščenje.
        /// </summary>
        public void Dispose()
        {
            if (koncano) return;

            koncano = true;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Vrne opis seje z registrsko oznako in časom vstopa.
        /// </summary>
        public override string ToString()
        {
            return Vozilo.Registrska + " | vstop: " + Vstop.ToString("HH:mm:ss");
        }
    }

    public abstract class Parkirisce : ICena
    {
        private List<Seja> seje = new List<Seja>();

        public const int MaxKapaciteta = 2000;
        public static int StevecParkirisc { get; private set; }

        public readonly Guid Id = Guid.NewGuid();

        private int zasedeno;
        private Denar prihodek;

        public string Ime { get; private set; }
        public int Kapaciteta { get; private set; }

        public event ObvestiloHandler Obvestilo;

        public Seja this[string registrska]
        {
            get
            {
                foreach (Seja seja in seje)
                {
                    if (seja.Vozilo.Registrska == registrska)
                        return seja;
                }

                return null;
            }
        }

        public int Zasedeno
        {
            get { return zasedeno; }
            protected set
            {
                if (value < 0)
                    zasedeno = 0;
                else if (value > Kapaciteta)
                    zasedeno = Kapaciteta;
                else
                    zasedeno = value;
            }
        }

        public Denar Prihodek
        {
            get { return prihodek; }
            protected set { prihodek = value; }
        }

        /// <summary>
        /// Ustvari novo parkirišče z imenom in kapaciteto.
        /// </summary>
        protected Parkirisce(string ime, int kapaciteta)
        {
            if (kapaciteta <= 0 || kapaciteta > MaxKapaciteta)
                throw new ArgumentOutOfRangeException("kapaciteta");

            Ime = ime;
            Kapaciteta = kapaciteta;
            Zasedeno = 0;
            Prihodek = new Denar(0);

            StevecParkirisc++;
        }

        /// <summary>
        /// Sproži obvestilo naročenim poslušalcem.
        /// </summary>
        protected void Sporoci(string sporocilo)
        {
            if (Obvestilo != null)
                Obvestilo(sporocilo);
        }

        /// <summary>
        /// Preveri, ali lahko vozilo vstopi na parkirišče.
        /// </summary>
        public virtual bool Lahko(Vozilo vozilo)
        {
            return Zasedeno < Kapaciteta;
        }

        /// <summary>
        /// Poskusi sprejeti vozilo na parkirišče in ustvari novo sejo.
        /// </summary>
        public bool Vstopi(Vozilo vozilo, double ure, out Seja seja)
        {
            seja = null;

            if (!Lahko(vozilo))
                return false;

            Zasedeno = Zasedeno + 1;
            seja = new Seja(vozilo, ure);

            seje.Add(seja);

            Sporoci("Vozilo " + vozilo.Registrska + " je vstopilo na parkirišče " + Ime + ".");

            return true;
        }

        /// <summary>
        /// Odstrani vozilo s parkirišča glede na registrsko oznako.
        /// </summary>
        public void Izstopi(string registrska)
        {
            if (Zasedeno > 0)
                Zasedeno = Zasedeno - 1;

            Seja s = this[registrska];

            if (s != null)
            {
                seje.Remove(s);
                Sporoci("Vozilo " + registrska + " je zapustilo parkirišče " + Ime + ".");
            }
        }

        /// <summary>
        /// Izračuna ceno parkiranja za podano število ur.
        /// </summary>
        public abstract Denar Cena(double ure);

        /// <summary>
        /// Doda znesek k skupnemu prihodku parkirišča.
        /// </summary>
        protected void Dodaj(Denar z)
        {
            Prihodek = Prihodek + z;
        }

        /// <summary>
        /// Vrne število prostih mest na parkirišču.
        /// </summary>
        public static int Prosto(Parkirisce p)
        {
            return p.Kapaciteta - p.Zasedeno;
        }

        /// <summary>
        /// Vrne osnovni opis parkirišča in njegove zasedenosti.
        /// </summary>
        public override string ToString()
        {
            return Ime + " (" + Zasedeno + "/" + Kapaciteta + ")";
        }
    }

    public class Zunanje : Parkirisce
    {
        private decimal naUro;

        /// <summary>
        /// Ustvari zunanje parkirišče z določeno urno postavko.
        /// </summary>
        public Zunanje(string ime, int kapaciteta, decimal naUro)
            : base(ime, kapaciteta)
        {
            this.naUro = naUro;
        }

        /// <summary>
        /// Izračuna ceno parkiranja na zunanjem parkirišču.
        /// </summary>
        public override Denar Cena(double ure)
        {
            int u = (int)Math.Ceiling(ure);
            Denar z = new Denar(u * naUro);
            Dodaj(z);
            return z;
        }
    }

    public class Hisa : Parkirisce
    {
        private decimal naUro;
        public double MaxVisina { get; private set; }

        /// <summary>
        /// Ustvari parkirno hišo z urno postavko in omejitvijo višine.
        /// </summary>
        public Hisa(string ime, int kapaciteta, decimal naUro, double maxVisina)
            : base(ime, kapaciteta)
        {
            this.naUro = naUro;
            MaxVisina = maxVisina;
        }

        /// <summary>
        /// Preveri, ali vozilo izpolnjuje pogoje za vstop v hišo.
        /// </summary>
        public override bool Lahko(Vozilo vozilo)
        {
            if (!base.Lahko(vozilo))
                return false;

            return vozilo.Visina <= MaxVisina;
        }

        /// <summary>
        /// Izračuna ceno parkiranja v parkirni hiši.
        /// </summary>
        public override Denar Cena(double ure)
        {
            int u = (int)Math.Ceiling(ure);
            Denar z = new Denar(u * naUro);
            Dodaj(z);
            return z;
        }
    }

    public class Kamp : Parkirisce
    {
        private decimal naUro;

        /// <summary>
        /// Ustvari kamp parkirišče z določeno urno postavko.
        /// </summary>
        public Kamp(string ime, int kapaciteta, decimal naUro)
            : base(ime, kapaciteta)
        {
            this.naUro = naUro;
        }

        /// <summary>
        /// Izračuna ceno parkiranja v kampu.
        /// </summary>
        public override Denar Cena(double ure)
        {
            int u = (int)Math.Ceiling(ure);
            Denar z = new Denar(u * naUro);
            Dodaj(z);
            return z;
        }
    }
}
