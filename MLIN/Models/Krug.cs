using System.Collections.Generic;
using System.Windows.Media;
using ReactiveUI;

namespace MLIN.Models
{
    public class Krug : ReactiveObject
    {
        public static readonly Brush UnoccupiedColor = Brushes.White;
        public static Brush P1Color
        {
            get { return Brushes.CadetBlue; }
        }
        public static Brush P2Color
        {
            get { return Brushes.Coral; }
        }
        private Brush fillColor;
        private int strokeThickness;
        private KrugStatus krugStatus;

        public Krug()
        {
            Status = KrugStatus.Prazno;
            StrokeThickness = 0;
        }

        public KrugStatus Status
        {
            get { return krugStatus; }
            set { OnTileStatusChanged(value); }
        }
        public Brush FillColor
        {
            get { return fillColor; }
            set { this.RaiseAndSetIfChanged(ref fillColor, value); }
        }
        public int StrokeThickness
        {
            get { return strokeThickness; }
            set { this.RaiseAndSetIfChanged(ref strokeThickness, value); }
        }
        public string KrugIme { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public IEnumerable<Krug> SusjedniKrugovi { get; set; }

        public void Highlight()
        {
            StrokeThickness = 5;
        }

        public void UnHighlight()
        {
            StrokeThickness = 0;
        }

        private void OnTileStatusChanged(KrugStatus value)
        {
            if (value == KrugStatus.Prazno)
                FillColor = UnoccupiedColor;
            if (value == KrugStatus.P1)
                FillColor = P1Color;
            if (value == KrugStatus.P2)
                FillColor = P2Color;
            this.RaiseAndSetIfChanged(ref krugStatus, value);
        }
    }
}