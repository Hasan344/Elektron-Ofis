using System.Collections.Generic;

namespace Entity
{
    public class GelirTeyinati
    {
        public int Id { get; set; }
        public string Teyinat { get; set; }
        public List<Gelir> Gelirler { get; set; }
    }
}