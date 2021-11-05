using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeek5
{
    public static class Menu
    {
        internal static void Start()
        {
            UniversitaManager.AggiungiDatiProva();
            bool continua = true;

            do
            {
                Console.WriteLine("******Menù******");
                Console.WriteLine("[1] Per prenotarsi ad un esame");
                Console.WriteLine("[2] Per verbalizzare un esame precedentemente prenotato");
                Console.WriteLine("[0] Per uscire");
                char scelta = Console.ReadKey().KeyChar;                
                switch (scelta)
                {
                    case '1':
                        PrenotazioneEsame();
                        break;
                    case '2':
                        VerbalizzazioneEsame();
                        break;
                    default:
                        break;
                    case'0':
                        Console.WriteLine("Arrivederci");
                        continua = false;
                        break;
                }

            }
            while (continua == true);
        }
      
        private static void PrenotazioneEsame()
        {
            string codiceCorsoDiLaurea = GetString("il codice del corso di laurea");
            CorsoDiLaurea corsoselezionato = UniversitaManager.GetByCode(codiceCorsoDiLaurea);
            if (corsoselezionato == null)
            {
                Console.WriteLine("Non esiste alcun corso di laurea corrispondente a questo codice.");
            }
            else
            {
                Console.WriteLine($"Il corso selezionato è {corsoselezionato.Nome}.");
                int matricola = GetNumber("la propria matricola");
                Studente utente = UniversitaManager.GetByInt(matricola, corsoselezionato);
                if(utente== null)
                {
                    Console.WriteLine($"Non è stato trovato alcuno studente con questa matricola in {corsoselezionato.Nome}");
                }
                else 
                {
                    Console.WriteLine($"Benvenuto {utente.Nome} {utente.Cognome}");
                    
                    bool checkLaurea = UniversitaManager.CheckRichiestaLaurea(utente);
                    if (checkLaurea == false)
                    {
                        Console.WriteLine("Esami non ancora sostenuti:");
                        UniversitaManager.StampaDatiDaLista(corsoselezionato.ListaCorsiDiUnCorsoDiLaurea);
                        CorsoAssociato esamedasostenere = UniversitaManager.SelezionaEsame("sostenere");
                        Console.WriteLine($"L'esame selezionato è {esamedasostenere.Nome}");
                        bool IsPresent = UniversitaManager.CheckPresenzaEsame(utente,esamedasostenere.Id);
                        if(IsPresent == false)
                        {
                            int prospettoCrediti =utente.CFUAccumulati + esamedasostenere.CFU;
                            if (prospettoCrediti > corsoselezionato.CFUTotali)
                            {
                                Console.WriteLine("Non puoi sostenere questo esame:i CFU sono superiori ai CFU richiesti dal tuo corso di laurea");
                            }
                            else
                            {
                                UniversitaManager.AggiungiEsame(utente, esamedasostenere);
                                if (utente.DictionaryEsami.ContainsKey(esamedasostenere.Id))
                                {
                                    Console.WriteLine($"{utente.Nome} {utente.Cognome} hai prenotato {esamedasostenere.Nome}");

                                }
                                else
                                {
                                    Console.WriteLine("Qualcosa è andato storto...");
                                }
                            }
                        }                      
                    }
                }
            }        
        }

        private static void VerbalizzazioneEsame()
        {
            string codiceCorsoDiLaurea = GetString("il codice del corso di laurea");
            CorsoDiLaurea corsoselezionato = UniversitaManager.GetByCode(codiceCorsoDiLaurea);
            if (corsoselezionato == null)
            {
                Console.WriteLine("Non esiste alcun corso di laurea corrispondente a questo codice.");
            }
            else
            {
                Console.WriteLine($"Il corso selezionato è {corsoselezionato.Nome}.");
                int matricola = GetNumber("la propria matricola");
                Studente utente = UniversitaManager.GetByInt(matricola, corsoselezionato);
                if (utente == null)
                {
                    Console.WriteLine($"Non è stato trovato alcuno studente con questa matricola in {corsoselezionato.Nome}");
                }
                else
                {
                    Console.WriteLine($"Benvenuto {utente.Nome} {utente.Cognome}");
                    Console.WriteLine("Gli esami del tuo corso di laurea sono:");
                    UniversitaManager.StampaDatiDaLista(corsoselezionato.ListaCorsiDiUnCorsoDiLaurea);
                    CorsoAssociato esameDaRegistrare = UniversitaManager.SelezionaEsame("verbalizzare");
                    Console.WriteLine($"L'esame selezionato è {esameDaRegistrare.Nome}");
                    
                    bool IsPresent = UniversitaManager.CheckPresenzaEsame(utente, esameDaRegistrare.Id);
                    bool IsVerbalize =UniversitaManager.CheckVerbalizzazione(utente.DictionaryEsami, esameDaRegistrare.Id);
                    if (IsPresent == true && IsVerbalize ==false)
                    { 
                        utente.DictionaryEsami[esameDaRegistrare.Id] = true; 
                        utente.CFUAccumulati += esameDaRegistrare.CFU;
                        if(utente.CFUAccumulati == corsoselezionato.CFUTotali)
                        {
                            utente.RichiestaLaurea = true;
                            Console.WriteLine("Hai i crediti necessari per laurearti!");
                        }
                        else
                        {
                            Console.WriteLine($"L'esame è stato verbalizzato, i tuoi crediti sono {utente.CFUAccumulati}");
                        }
                    }
                    else
                    {
                        if(IsPresent == false)
                        {
                            Console.WriteLine("Non hai sostenuto questo esame, non puoi verbalizzarlo");
                        }
                    }                                        
                }
            }
        }      
        public static int GetNumber(string value)
        {
            int numero;
            do
            {
                Console.WriteLine($"Inserire {value}");
            }
            while (!(int.TryParse(Console.ReadLine(), out numero) && numero >0));

            return numero;

        }

        private static string GetString(string value) 
        {
            string codice;
            do
            {
                Console.WriteLine($"Inserisci {value}");
                codice = Console.ReadLine();
            } while (string.IsNullOrEmpty(codice));

            return codice;
        }
    }
}
