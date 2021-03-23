using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using MLIN.Models;
using MLIN.ViewModels;
using System;
using System.Collections;
using System.Diagnostics;

namespace MLIN.Views
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : UserControl
    {
        public Board()
        {
            InitializeComponent();
        }

        static Game game = ViewModelLocator.GameViewModel.Game;
        static PlayerViewModel igraci = ViewModelLocator.PlayerViewModel;

        enum OdabranoStanje
        {
            Neutralno,
            PostaviNovo,
            MicanjeFigure,
            MicanjePostojece
        }

        private static OdabranoStanje trenutnoStanje = OdabranoStanje.Neutralno;

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var ellipse = sender as Ellipse;
            if (ellipse == null) return;
            var krug = game.Krugovi.FirstOrDefault(t => ellipse.Tag as string == t.KrugIme);
            if (krug == null) return;

            if (trenutnoStanje == OdabranoStanje.MicanjeFigure)
            {
                if (igraci.Igrac1.NaPotezu && krug.Status == KrugStatus.P2)
                {
                    krug.Status = KrugStatus.Prazno;
                    igraci.Igrac2.FiguricaOstalo--;
                    if (igraci.Igrac2.FiguricaOstalo == 3 && igraci.Igrac2.NevidljiveFigure == 0)
                        game.State = GameState.Skakanje;
                    if (igraci.Igrac2.FiguricaOstalo == 2 && igraci.Igrac2.NevidljiveFigure == 0)
                    {
                        igraci.Igrac1.NaPotezu = false;
                        igraci.Igrac2.NaPotezu = false;
                        game.Winner = igraci.Igrac1.Ime;
                        this.Content = new GameOverPage();
                    }
                    trenutnoStanje = OdabranoStanje.Neutralno;
                    igraci.ZamjeniPotez();
                }
                else if (igraci.Igrac2.NaPotezu && krug.Status == KrugStatus.P1)
                {
                    krug.Status = KrugStatus.Prazno;
                    igraci.Igrac1.FiguricaOstalo--;
                    if (igraci.Igrac1.FiguricaOstalo == 3 && igraci.Igrac1.NevidljiveFigure == 0)
                        game.State = GameState.Skakanje;
                    if (igraci.Igrac1.FiguricaOstalo == 2 && igraci.Igrac1.NevidljiveFigure == 0)
                    {
                        igraci.Igrac1.NaPotezu = false;
                        igraci.Igrac2.NaPotezu = false;
                        game.Winner = igraci.Igrac2.Ime;
                        this.Content = new GameOverPage();
                    }
                    trenutnoStanje = OdabranoStanje.Neutralno;
                    igraci.ZamjeniPotez();
                }
            }
            else if (game.State == GameState.Postavljanje)
            {
                if (krug.Status != KrugStatus.Prazno)
                {

                }
                else if (igraci.Igrac1.NaPotezu)
                {
                    krug.Status = KrugStatus.P1;
                    igraci.Igrac1.NevidljiveFigure--;
                    igraci.Igrac1.FiguricaOstalo++;
                    if (igraci.Igrac1.NoviMlinovi(game.Krugovi, KrugStatus.P1, krug))
                    {
                        trenutnoStanje = OdabranoStanje.MicanjeFigure;
                    }
                    else igraci.ZamjeniPotez();
                }
                else if (igraci.Igrac2.NaPotezu)
                {
                    krug.Status = KrugStatus.P2;
                    igraci.Igrac2.NevidljiveFigure--;
                    igraci.Igrac2.FiguricaOstalo++;
                    if (igraci.Igrac2.NoviMlinovi(game.Krugovi, KrugStatus.P2, krug))
                    {
                        trenutnoStanje = OdabranoStanje.MicanjeFigure;
                    }
                    else igraci.ZamjeniPotez();
                }
                if (igraci.Igrac1.NevidljiveFigure == 0 &&
                    igraci.Igrac2.NevidljiveFigure == 0)
                {
                    game.State = GameState.Kretanje;
                }
            }
            else if (game.State == GameState.Kretanje)
            {
                if (game.CurrentlyMovingPiece == null)
                {
                    if (igraci.Igrac1.NaPotezu && krug.Status == KrugStatus.P1 ||
                        igraci.Igrac2.NaPotezu && krug.Status == KrugStatus.P2)
                    {
                        game.CurrentlyMovingPiece = krug;
                        krug.Highlight();
                    }
                }
                else
                {
                    if (krug.Status == KrugStatus.Prazno && game.CurrentlyMovingPiece.SusjedniKrugovi.Contains(krug))
                    {
                        krug.Status = game.CurrentlyMovingPiece.Status;
                        game.CurrentlyMovingPiece.Status = KrugStatus.Prazno;
                        if (igraci.Igrac1.NaPotezu)
                        {
                            if (igraci.Igrac1.NoviMlinovi(game.Krugovi, KrugStatus.P1, krug))
                            {
                                trenutnoStanje = OdabranoStanje.MicanjeFigure;
                            }
                            else igraci.ZamjeniPotez();
                        }
                        else if (igraci.Igrac2.NaPotezu)
                        {
                            if (igraci.Igrac2.NoviMlinovi(game.Krugovi, KrugStatus.P2, krug))
                            {
                                trenutnoStanje = OdabranoStanje.MicanjeFigure;
                            }
                            else igraci.ZamjeniPotez();
                        }
                    }
                    game.CurrentlyMovingPiece.UnHighlight();
                    game.CurrentlyMovingPiece = null;
                }
            }
            else if (game.State == GameState.Skakanje)
            {
                if (game.CurrentlyMovingPiece == null)
                {
                    if (igraci.Igrac1.NaPotezu && krug.Status == KrugStatus.P1 ||
                        igraci.Igrac2.NaPotezu && krug.Status == KrugStatus.P2)
                    {
                        game.CurrentlyMovingPiece = krug;
                        krug.Highlight();
                    }
                }
                else
                {
                    if (krug.Status == KrugStatus.Prazno)
                    {
                        krug.Status = game.CurrentlyMovingPiece.Status;
                        game.CurrentlyMovingPiece.Status = KrugStatus.Prazno;
                        if (igraci.Igrac1.NaPotezu)
                        {
                            if (igraci.Igrac1.NoviMlinovi(game.Krugovi, KrugStatus.P1, krug))
                            {
                                trenutnoStanje = OdabranoStanje.MicanjeFigure;
                            }
                            else igraci.ZamjeniPotez();
                        }
                        else if (igraci.Igrac2.NaPotezu)
                        {
                            if (igraci.Igrac2.NoviMlinovi(game.Krugovi, KrugStatus.P2, krug))
                            {
                                trenutnoStanje = OdabranoStanje.MicanjeFigure;
                            }
                            else igraci.ZamjeniPotez();
                        }
                    }
                    game.CurrentlyMovingPiece.UnHighlight();
                    game.CurrentlyMovingPiece = null;
                }
            }
            if (igraci.Igrac1.NaPotezu && igraci.Igrac1.IsComputer)
            {
                IgraKompjutor();
            }
            else if(igraci.Igrac2.NaPotezu && igraci.Igrac2.IsComputer)
            {
                IgraKompjutor();
            }
        }

        private void IgraKompjutor()
        {
            if (igraci.Igrac2.NaPotezu && igraci.Igrac2.IsComputer)
            {

                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
                if (trenutnoStanje == OdabranoStanje.MicanjeFigure)
                {
                    var moguciKrugovi = game.Krugovi.Where(x => x.Status == KrugStatus.P1);
                    Random r = new Random();
                    var index = r.Next(moguciKrugovi.Count());
                    var krugA = moguciKrugovi.ToArray()[index];
                    var krug = game.Krugovi.FirstOrDefault(x => x.KrugIme == krugA.KrugIme);
                    krug.Status = KrugStatus.Prazno;
                    igraci.Igrac1.FiguricaOstalo--;
                    if (igraci.Igrac1.FiguricaOstalo == 3 && igraci.Igrac1.NevidljiveFigure == 0)
                        game.State = GameState.Skakanje;
                    if (igraci.Igrac1.FiguricaOstalo == 2 && igraci.Igrac1.NevidljiveFigure == 0)
                    {
                        igraci.Igrac1.NaPotezu = false;
                        igraci.Igrac2.NaPotezu = false;
                        game.Winner = igraci.Igrac2.Ime;
                        this.Content = new GameOverPage();
                    }
                    trenutnoStanje = OdabranoStanje.Neutralno;
                    igraci.ZamjeniPotez();
                }
                else if (game.State == GameState.Postavljanje)
                {
                    var slobodniKrugovi = game.Krugovi.Where(x => x.Status == KrugStatus.Prazno);
                    Random r = new Random();
                    int krugIndeks = r.Next(slobodniKrugovi.Count());
                    var krug = slobodniKrugovi.ToArray()[krugIndeks];
                    game.Krugovi.FirstOrDefault(x => x.KrugIme == krug.KrugIme).Status = KrugStatus.P2;
                    igraci.Igrac2.NevidljiveFigure--;
                    igraci.Igrac2.FiguricaOstalo++;
                    if (igraci.Igrac2.NoviMlinovi(game.Krugovi, KrugStatus.P2, krug))
                    {
                        trenutnoStanje = OdabranoStanje.MicanjeFigure;
                    }
                    else
                    {
                        igraci.ZamjeniPotez();
                    }

                    if (igraci.Igrac1.NevidljiveFigure == 0 && igraci.Igrac2.NevidljiveFigure == 0)
                    {
                        trenutnoStanje = OdabranoStanje.Neutralno;
                        game.State = GameState.Kretanje;
                    }
                }
                else if (game.State == GameState.Kretanje || game.State == GameState.Skakanje)
                {
                    var mojiKrugovi = game.Krugovi.Where(x => x.Status == KrugStatus.P2);
                    var contprog = false;
                    Random r = new Random();
                    Krug noviKrug = null;
                    while (!contprog)
                    {
                        var indeks = r.Next(mojiKrugovi.Count());
                        var krug = mojiKrugovi.ToArray()[indeks];
                        var moguce = krug.SusjedniKrugovi.ToArray();
                        for (int i = 0; i < moguce.Count(); i++)
                        {
                            var current = game.Krugovi.FirstOrDefault(x => x.KrugIme == moguce[i].KrugIme);
                            if (current.Status == KrugStatus.Prazno)
                            {
                                current.Status = KrugStatus.P2;
                                game.Krugovi.FirstOrDefault(x => x.KrugIme == krug.KrugIme).Status = KrugStatus.Prazno;
                                noviKrug = current;
                                contprog = true;
                                break;
                            }
                        }
                    }
                    if (igraci.Igrac2.NoviMlinovi(game.Krugovi, KrugStatus.P2, noviKrug))
                    {
                        trenutnoStanje = OdabranoStanje.MicanjeFigure;
                    }
                    else
                    { 
                        igraci.ZamjeniPotez();
                    }
                }
            }
            else if (igraci.Igrac2.NaPotezu && igraci.Igrac2.IsComputer)
            {
                IgraKompjutor();
            }
        }

        private void MouseUpOnElipse(object sender, MouseButtonEventArgs e)
        {
            var ellipse = sender as Ellipse;
            if (ellipse == null) return;
            var tile = game.Krugovi.FirstOrDefault(t => ellipse.Tag as string == t.KrugIme);
            if (tile == null) return;
        }

        private void MouseMovesOut(object sender, MouseEventArgs e)
        {
        }

        private void ellipse_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void ellipse_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
        }

        private void ellipse_DragEnter(object sender, DragEventArgs e)
        {
        }

        private void ellipse_DragLeave(object sender, DragEventArgs e)
        {
        }

        private void ellipse_DragOver(object sender, DragEventArgs e)
        {
        }

        private void ellipse_Drop(object sender, DragEventArgs e)
        {
        }
    }
}