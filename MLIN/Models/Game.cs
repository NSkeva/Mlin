using ReactiveUI;

namespace MLIN.Models
{
    public class Game : ReactiveObject
    {
        private string winner = "";
        private GameState state;
        private ReactiveCollection<Krug> krugovi;

        public Krug CurrentlyMovingPiece { get; set; }

        public Game()
        {
            state = GameState.Postavljanje;
            StvoriKrugove();
        }

        public GameState State
        {
            get { return state; }
            set { this.RaiseAndSetIfChanged(ref state, value); }
        }

        public ReactiveCollection<Krug> Krugovi
        {
            get { return krugovi; }
            set { this.RaiseAndSetIfChanged(ref krugovi, value); }
        }

        public string Winner
        {
            get { return winner; }
            set { this.RaiseAndSetIfChanged(ref winner, value);  }
        }

        protected void StvoriKrugove()
        {
            Krugovi = new ReactiveCollection<Krug>(
                new[]
                {
                    // Vanjske
                    new Krug { KrugIme = "OuterTopLeft", Row = 0, Column = 0 },
                    new Krug { KrugIme = "OuterTopMiddle", Row = 0, Column = 3 },
                    new Krug { KrugIme = "OuterTopRight", Row = 0, Column = 6 },
                    new Krug { KrugIme = "OuterMiddleLeft", Row = 3, Column = 0 },
                    new Krug { KrugIme = "OuterMiddleRight", Row = 3, Column = 6 },
                    new Krug { KrugIme = "OuterBottomLeft", Row = 6, Column = 0 },
                    new Krug { KrugIme = "OuterBottomMiddle", Row = 6, Column = 3 },
                    new Krug { KrugIme = "OuterBottomRight", Row = 6, Column = 6 },
                    // Sredina
                    new Krug { KrugIme = "MiddleTopLeft", Row = 1, Column = 1 },
                    new Krug { KrugIme = "MiddleTopMiddle", Row = 1, Column = 3 },
                    new Krug { KrugIme = "MiddleTopRight", Row = 1, Column = 5 },
                    new Krug { KrugIme = "MiddleMiddleLeft", Row = 3, Column = 1 },
                    new Krug { KrugIme = "MiddleMiddleRight", Row = 3, Column = 5 },
                    new Krug { KrugIme = "MiddleBottomLeft", Row = 5, Column = 1 },
                    new Krug { KrugIme = "MiddleBottomMiddle", Row = 5, Column = 3 },
                    new Krug { KrugIme = "MiddleBottomRight", Row = 5, Column = 5 },
                    // Unutra
                    new Krug { KrugIme = "InnerTopLeft", Row = 2, Column = 2 },
                    new Krug { KrugIme = "InnerTopMiddle", Row = 2, Column = 3 },
                    new Krug { KrugIme = "InnerTopRight", Row = 2, Column = 4 },
                    new Krug { KrugIme = "InnerMiddleLeft", Row = 3, Column = 2 },
                    new Krug { KrugIme = "InnerMiddleRight", Row = 3, Column = 4 },
                    new Krug { KrugIme = "InnerBottomLeft", Row = 4, Column = 2 },
                    new Krug { KrugIme = "InnerBottomMiddle", Row = 4, Column = 3 },
                    new Krug { KrugIme = "InnerBottomRight", Row = 4, Column = 4 }
                });

            // Poveznice

            // Vanjske
            Krugovi[0].SusjedniKrugovi = new[] { Krugovi[1], Krugovi[3] };
            Krugovi[1].SusjedniKrugovi = new[] { Krugovi[0], Krugovi[2], Krugovi[9] };
            Krugovi[2].SusjedniKrugovi = new[] { Krugovi[1], Krugovi[4] };
            Krugovi[3].SusjedniKrugovi = new[] { Krugovi[0], Krugovi[5], Krugovi[11] };
            Krugovi[4].SusjedniKrugovi = new[] { Krugovi[2], Krugovi[7], Krugovi[12] };
            Krugovi[5].SusjedniKrugovi = new[] { Krugovi[3], Krugovi[6] };
            Krugovi[6].SusjedniKrugovi = new[] { Krugovi[5], Krugovi[7], Krugovi[14] };
            Krugovi[7].SusjedniKrugovi = new[] { Krugovi[4], Krugovi[6] };
            // Sredina
            Krugovi[8].SusjedniKrugovi = new[] { Krugovi[9], Krugovi[11] };
            Krugovi[9].SusjedniKrugovi = new[] { Krugovi[1], Krugovi[8], Krugovi[10], Krugovi[17] };
            Krugovi[10].SusjedniKrugovi = new[] { Krugovi[9], Krugovi[12] };
            Krugovi[11].SusjedniKrugovi = new[] { Krugovi[3], Krugovi[8], Krugovi[13], Krugovi[19] };
            Krugovi[12].SusjedniKrugovi = new[] { Krugovi[4], Krugovi[10], Krugovi[15], Krugovi[20] };
            Krugovi[13].SusjedniKrugovi = new[] { Krugovi[11], Krugovi[14] };
            Krugovi[14].SusjedniKrugovi = new[] { Krugovi[6], Krugovi[13], Krugovi[15], Krugovi[22] };
            Krugovi[15].SusjedniKrugovi = new[] { Krugovi[12], Krugovi[14] };
            // Unutra
            Krugovi[16].SusjedniKrugovi = new[] { Krugovi[17], Krugovi[19] };
            Krugovi[17].SusjedniKrugovi = new[] { Krugovi[9], Krugovi[16], Krugovi[18] };
            Krugovi[18].SusjedniKrugovi = new[] { Krugovi[17], Krugovi[20] };
            Krugovi[19].SusjedniKrugovi = new[] { Krugovi[11], Krugovi[16], Krugovi[21] };
            Krugovi[20].SusjedniKrugovi = new[] { Krugovi[18], Krugovi[12], Krugovi[23] };
            Krugovi[21].SusjedniKrugovi = new[] { Krugovi[19], Krugovi[22] };
            Krugovi[22].SusjedniKrugovi = new[] { Krugovi[14], Krugovi[21], Krugovi[23] };
            Krugovi[23].SusjedniKrugovi = new[] { Krugovi[20], Krugovi[22] };

            this.RaisePropertyChanged(x => x.Krugovi);
        }
    }
}