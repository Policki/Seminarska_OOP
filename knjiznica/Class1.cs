using System;

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

    public readonly struct Denar
    {
        public const string Valuta = "EUR";
        public decimal Znesek { get; }

        public Denar(decimal znesek)
        {
            if (znesek < 0) Znesek = 0;
            else Znesek = znesek;
        }

        public static Denar operator +(Denar a, Denar b)
        {
            return new Denar(a.Znesek + b.Znesek);
        }

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

        public Vozilo(string registrska, TipVozila tip, double visina)
        {
            Registrska = registrska;
            Tip = tip;
            if (visina <= 0) Visina = 1.6;
            else Visina = visina;
        }

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

        public readonly Guid Id = Guid.NewGuid();

        private bool koncano;
        public double Ure { get; private set; }

        public Seja(Vozilo vozilo, double ure)
        {
            Vozilo = vozilo;
            Vstop = DateTime.Now;
            Ure = ure;
            StevecSej++;
        }

        ~Seja()
        {
            StevecSej--;
        }

        public void Dispose()
        {
            if (koncano) return;
            koncano = true;
            GC.SuppressFinalize(this);
        }

        public override string ToString()
        {
            return Vozilo.Registrska + " | vstop: " + Vstop.ToString("HH:mm:ss");
        }
    }

    public abstract class Parkirisce
    {
        public const int MaxKapaciteta = 2000;
        public static int StevecParkirisc { get; private set; }

        public readonly Guid Id = Guid.NewGuid();

        private int zasedeno;
        private Denar prihodek;

        public string Ime { get; private set; }
        public int Kapaciteta { get; private set; }

        public int Zasedeno
        {
            get { return zasedeno; }
            protected set
            {
                if (value < 0) zasedeno = 0;
                else if (value > Kapaciteta) zasedeno = Kapaciteta;
                else zasedeno = value;
            }
        }

        public Denar Prihodek
        {
            get { return prihodek; }
            protected set { prihodek = value; }
        }

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

        public virtual bool Lahko(Vozilo vozilo)
        {
            return Zasedeno < Kapaciteta;
        }

        public bool Vstopi(Vozilo vozilo, double ure, out Seja seja)
        {
            seja = null;

            if (!Lahko(vozilo))
                return false;

            Zasedeno = Zasedeno + 1;
            seja = new Seja(vozilo, ure);
            return true;
        }

        public void Izstopi()
        {
            if (Zasedeno > 0) Zasedeno = Zasedeno - 1;
        }
        public abstract Denar Cena(double ure);

        protected void Dodaj(Denar z)
        {
            Prihodek = Prihodek + z;
        }

        public static int Prosto(Parkirisce p)
        {
            return p.Kapaciteta - p.Zasedeno;
        }

        public override string ToString()
        {
            return Ime + " (" + Zasedeno + "/" + Kapaciteta + ")";
        }
    }

    public class Zunanje : Parkirisce
    {
        private decimal naUro;

        public Zunanje(string ime, int kapaciteta, decimal naUro)
            : base(ime, kapaciteta)
        {
            this.naUro = naUro;
        }

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

        public Hisa(string ime, int kapaciteta, decimal naUro, double maxVisina)
            : base(ime, kapaciteta)
        {
            this.naUro = naUro;
            MaxVisina = maxVisina;
        }

        public override bool Lahko(Vozilo vozilo)
        {
            if (!base.Lahko(vozilo)) return false;
            return vozilo.Visina <= MaxVisina;
        }

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

        public Kamp(string ime, int kapaciteta, decimal naUro)
            : base(ime, kapaciteta)
        {
            this.naUro = naUro;
        }

        public override Denar Cena(double ure)
        {
            int u = (int)Math.Ceiling(ure);
            Denar z = new Denar(u * naUro);
            Dodaj(z);
            return z;
        }
    }
}