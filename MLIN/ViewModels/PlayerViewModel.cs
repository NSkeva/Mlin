using MLIN.Models;
using ReactiveUI;

namespace MLIN.ViewModels
{
    public class PlayerViewModel : ReactiveObject
    {
        public PlayerViewModel()
        {
            Igrac1 = new Igrac { NaPotezu = true, Ime = "Igrac 1" };
            Igrac2 = new Igrac { NaPotezu = false, Ime = "Igrac 2" };
        }

        private Igrac playerOne;
        public Igrac Igrac1
        {
            get { return playerOne; }
            set { this.RaiseAndSetIfChanged(ref playerOne, value); }
        }

        private Igrac playerTwo;
        public Igrac Igrac2
        {
            get { return playerTwo; }
            set { this.RaiseAndSetIfChanged(ref playerTwo, value); }
        }

        public void ZamjeniPotez()
        {
            if (Igrac1.NaPotezu)
            {
                Igrac1.Potez++;
            }
            if (Igrac2.NaPotezu)
            {
                Igrac2.Potez++;
            }
            Igrac1.NaPotezu = !Igrac1.NaPotezu;
            Igrac2.NaPotezu = !Igrac2.NaPotezu;
        }
    }
}
