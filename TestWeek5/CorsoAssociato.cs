using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeek5
{
    public class CorsoAssociato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CFU { get; set; }                
        public string CodiceCorsoDiLaurea { get; set; } //FK
        
        public CorsoAssociato()
        {

        }
    }
}
