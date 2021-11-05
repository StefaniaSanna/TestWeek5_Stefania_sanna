using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeek5
{
    public class CorsoDiLaurea
    {
        public string Codice { get; set; }
        public Materia Nome { get; set; }
        public int Anni { get; set; }
        public int CFUTotali { get; set; }
        public List<Studente> ListaStudentiCorsoDiLaurea { get; set; } = new List<Studente>();
        public List<CorsoAssociato> ListaCorsiDiUnCorsoDiLaurea { get; set; } = new List<CorsoAssociato>();

        public CorsoDiLaurea()
        {

        }
    }

     public enum Materia
    {
        Fisica =1,
        Filosofia=2,
        Matematica=3,
        Ingegneria=4
    }
}

