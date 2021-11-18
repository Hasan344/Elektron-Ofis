using System;

namespace Entity
{
    public class Gelir
    {
        public int Id { get; set; }
        public int GelirTeyinatiId { get; set; }
        public GelirTeyinati GelirTeyinati { get; set; }
        public int Miqdar { get; set; }
        public decimal Mebleg { get; set; }
        public DateTime Tarix { get; set; }
    }
}