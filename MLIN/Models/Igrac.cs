using System.Linq;
using System.Windows.Media;
using ReactiveUI;
using System.Collections.Generic;

namespace MLIN.Models
{
    public class Igrac : ReactiveObject
    {
        private bool kompjuter = false;
        private bool naPotezu;
        private string ime;
        private int figuricaOstalo;
        private Brush background;
        private ReactiveCollection<Mlin> prijeMlinovi;
        private int moguceMicati;
        private int nevidljiveFigure;
        public static readonly Brush InactiveColor = Brushes.White;
        public static readonly Brush ActiveColor = Brushes.Red;

        public Igrac()
        {
            NevidljiveFigure = 9;
            FiguricaOstalo = 0;
            prijeMlinovi = new ReactiveCollection<Mlin>();
        }

        public string Ime
        {
            get { return ime; }
            set { this.RaiseAndSetIfChanged(ref ime, value); }
        }

        public bool NaPotezu
        {
            get { return naPotezu; }
            set
            {
                this.RaiseAndSetIfChanged(ref naPotezu, value);
                Background = NaPotezu ? ActiveColor : InactiveColor;
            }
        }

        public int Potez { get; set; }

        public Brush Background
        {
            get { return background; }
            set { this.RaiseAndSetIfChanged(ref background, value); }
        }

        public bool IsComputer
        {
            get { return kompjuter; }
            set { this.RaiseAndSetIfChanged(ref kompjuter, value); }
        }

        public int MoguceMicati
        {
            get { return moguceMicati; }
            set { this.RaiseAndSetIfChanged(ref moguceMicati, value); }
        }

        public int NevidljiveFigure
        {
            get { return nevidljiveFigure; }
            set { this.RaiseAndSetIfChanged(ref nevidljiveFigure, value); }
        }

        public int FiguricaOstalo
        {
            get { return figuricaOstalo; }
            set { this.RaiseAndSetIfChanged(ref figuricaOstalo, value); }
        }

        public ReactiveCollection<Mlin> PrijeMlinovi
        {
            get { return prijeMlinovi; }
            set { this.RaiseAndSetIfChanged(ref prijeMlinovi, value); }
        }

        private void provjeraMlin(int a, int b, int c, Krug movedTile, KrugStatus ts,ReactiveCollection<Krug> krugovi, ref Mlin tofill)
        {
            if (krugovi[a].Status == ts && krugovi[b].Status == ts && krugovi[c].Status == ts &&
                (movedTile == krugovi[a] ||
                 movedTile == krugovi[b] ||
                 movedTile == krugovi[c]))
                tofill = new Mlin(krugovi[a], krugovi[b], krugovi[c]);
        }

        /////////// Ploca //////////
        //
        //  [0]           [1]            [2]
        //      [8]       [9]       [10]
        //           [16] [17] [18]
        //  [3] [11] [19]      [20] [12] [4]
        //           [21] [22] [23]
        //      [13]      [14]      [15]
        //  [5]           [6]            [7]
        //
        public bool NoviMlinovi(ReactiveCollection<Krug> tiles, KrugStatus ts, Krug movedTile)
        {
            Mlin trenutniMlin = null;

            provjeraMlin(0, 1, 2, movedTile, ts, tiles, ref trenutniMlin);
            provjeraMlin(0, 3, 5, movedTile, ts, tiles, ref trenutniMlin);
            provjeraMlin(2, 4, 7, movedTile, ts, tiles, ref trenutniMlin);
            provjeraMlin(5, 6, 7, movedTile, ts, tiles, ref trenutniMlin);


            provjeraMlin(8, 9, 10, movedTile, ts, tiles, ref trenutniMlin);
            provjeraMlin(8, 11, 13, movedTile, ts, tiles, ref trenutniMlin);
            provjeraMlin(10, 12, 15, movedTile, ts, tiles, ref trenutniMlin);
            provjeraMlin(13, 14, 15, movedTile, ts, tiles, ref trenutniMlin);


            provjeraMlin(16, 17, 18, movedTile, ts, tiles, ref trenutniMlin);
            provjeraMlin(16, 19, 21, movedTile, ts, tiles, ref trenutniMlin);
            provjeraMlin(18, 20, 23, movedTile, ts, tiles, ref trenutniMlin);
            provjeraMlin(21, 22, 23, movedTile, ts, tiles, ref trenutniMlin);


            provjeraMlin(1, 9, 17, movedTile, ts, tiles, ref trenutniMlin);
            provjeraMlin(3, 11, 19, movedTile, ts, tiles, ref trenutniMlin);
            provjeraMlin(4, 12, 20, movedTile, ts, tiles, ref trenutniMlin);
            provjeraMlin(6, 14, 22, movedTile, ts, tiles, ref trenutniMlin);

            if (trenutniMlin == null) return false;
            trenutniMlin.Turn = Potez;

            if (trenutniMlin.First != movedTile &&
                trenutniMlin.Second != movedTile &&
                trenutniMlin.Third != movedTile)
                return false;

            if (PrijeMlinovi.Count > 1 && trenutniMlin.Equals(PrijeMlinovi[PrijeMlinovi.Count - 2]) && trenutniMlin.Turn - PrijeMlinovi[PrijeMlinovi.Count - 2].Turn <= 2)
                return false;

            PrijeMlinovi.Add(trenutniMlin);

            return true;
        }
    }
}
