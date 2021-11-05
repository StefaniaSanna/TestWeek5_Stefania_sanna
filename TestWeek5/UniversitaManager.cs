using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeek5
{
    public static class UniversitaManager
    {
        //mettiamo la lista di studenti, lista di corsi, lista di facoltà
        static List<Studente> studenti = new List<Studente>();
        static List<CorsoAssociato> corsiAssociati = new List<CorsoAssociato>();
        static List<CorsoDiLaurea> corsiDiLaurea = new List<CorsoDiLaurea>();

        public static void AggiungiDatiProva()
        {
            CorsoDiLaurea Matematica = new CorsoDiLaurea()
            {
                Codice = "LM-21",
                Nome = (Materia)3,
                Anni = 2,
                CFUTotali = 63,
                ListaCorsiDiUnCorsoDiLaurea = new List<CorsoAssociato>(), 
                ListaStudentiCorsoDiLaurea = new List<Studente>() 
                                                                                             
            };
            corsiDiLaurea.Add(Matematica);

            CorsoDiLaurea Fisica = new CorsoDiLaurea()
            {
                Codice = "LM-16",
                Nome = (Materia)1,
                Anni = 3,
                CFUTotali = 59,
                ListaCorsiDiUnCorsoDiLaurea = new List<CorsoAssociato>(), 
                ListaStudentiCorsoDiLaurea = new List<Studente>() 
                                                                                          
            };
            corsiDiLaurea.Add(Fisica);

            
            CorsoAssociato Analisi1 = new CorsoAssociato()
            {
                Id = 1,
                Nome = "Analisi1",
                CFU = 12,  
                CodiceCorsoDiLaurea = "LM-21"
            };
            
            corsiAssociati.Add(Analisi1);
            Matematica.ListaCorsiDiUnCorsoDiLaurea.Add(Analisi1);

            CorsoAssociato Fisica1 = new CorsoAssociato()
            {
                Id = 2,
                Nome = "Fisica1",
                CFU = 12,
                CodiceCorsoDiLaurea = "LM-16"
            };
            corsiAssociati.Add(Fisica1);
            Fisica.ListaCorsiDiUnCorsoDiLaurea.Add(Fisica1);

            CorsoAssociato Geometria = new CorsoAssociato()
            {
                Id = 3,
                Nome = "Geometria",
                CFU = 9, 
                CodiceCorsoDiLaurea = "LM-21"
            };
            corsiAssociati.Add(Geometria);
            Matematica.ListaCorsiDiUnCorsoDiLaurea.Add(Geometria);

            
            Studente studente1 = new Studente()
            {
                Matricola = 2345,
                Nome = "Mario",
                Cognome = "Rossi",
                AnnoNascita = 2000,
                RichiestaLaurea = false,
                CFUAccumulati = 0,
                DictionaryEsami = new Dictionary<int, bool>(),
                CodiceCorsoDiLaurea = "LM-21"
            };
            studente1.DictionaryEsami.Add(1, false);
            studenti.Add(studente1);
            Matematica.ListaStudentiCorsoDiLaurea.Add(studente1);
        }

        internal static void AggiungiEsame(Studente utente, CorsoAssociato esame)
        {
            int Id = esame.Id;
            utente.DictionaryEsami.Add(Id, false); 

        }

        internal static bool CheckPresenzaEsame(Studente studente, int i)
        {
            bool IsPresent = false;
            if (studente.DictionaryEsami.ContainsKey(i) == true)
            {              
                Console.WriteLine("Hai sostenuto questo esame");
                IsPresent = true;                            
            }
            return IsPresent;

        }    
        internal static CorsoAssociato SelezionaEsame(string value)
        {
            int Id = Menu.GetNumber($"L'Id dell'esame che si desidera {value}");
            foreach (CorsoAssociato c in corsiAssociati)
            {
                if (c.Id == Id)
                {
                    return c;
                }
            }
            return null;
        }
        internal static bool CheckVerbalizzazione(Dictionary<int,bool> listaEsami, int Id)
        {
            bool Verbalizzato = true;
            foreach (var item in listaEsami) 
            {
                if (item.Key== Id)
                {
                    if (item.Value == true)
                    {
                        Console.WriteLine("Questo esame è già stato verbalizzato");
                        Verbalizzato = true;
                    }
                    else
                    {
                        Verbalizzato = false;
                    }
                }              
            }
            return Verbalizzato;
        }

     
        internal static void StampaDatiDaLista(List<CorsoAssociato> corsi)
        {
            foreach (CorsoAssociato c in corsi)
            {

                Console.WriteLine($"{c.Id} - {c.Nome} CFU = {c.CFU} ");
            }
        }


        internal static bool CheckRichiestaLaurea(Studente s)
        {
            if (s.RichiestaLaurea == false)
            {
                return false;
            }
            else
            {
                Console.WriteLine("Hai già termitato gli esami, sei iscritto alla sessione di laurea!");

            }
            return true;
        }

        internal static Studente GetByInt(int matricola, CorsoDiLaurea laurea)
        {
            foreach (Studente s in laurea.ListaStudentiCorsoDiLaurea)
            {
                if (s.Matricola == matricola)
                {
                    return s;
                }
            }
            return null;

        }

        internal static CorsoDiLaurea GetByCode(string codice)
        {
            foreach (CorsoDiLaurea c in corsiDiLaurea)
            {
                if (c.Codice == codice)
                {
                    return c;
                }
            }
            return null;
        }
    }
}
