using System;

namespace MLIN.Models
{
    public class Mlin : IEquatable<Mlin>
    {
        public Krug First { get; set; }
        public Krug Second { get; set; }
        public Krug Third { get; set; }
        public int Turn { get; set; }

        public Mlin(Krug first, Krug second, Krug third)
        {
            First = first;
            Second = second;
            Third = third;
        }

        public bool Equals(Mlin other)
        {
            return First == other.First &&
                   Second == other.Second &&
                   Third == other.Third;
        }
    }
}