using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeek5
{
    public class Studente
    {
        public int Matricola { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public int AnnoNascita { get; set; }
        public bool RichiestaLaurea { get; set; }
        public int CFUAccumulati { get; set; }
        
        public Dictionary<int, bool> DictionaryEsami { get; set; } = new Dictionary<int, bool>();

        public string CodiceCorsoDiLaurea { get; set; }
        

        public Studente()
        {

        }
    }
}
