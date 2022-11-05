using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2conTendICommand.Models;

namespace WpfApp2conTendICommand.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class MainWindowsViewModel : INotifyPropertyChanged
    {
        // controllo per la sorgente dei dati, da passare al costruttore
        private IPersoneService _personeService = null;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindowsViewModel(IPersoneService personeService)
        {
            _personeService = personeService;
        }

        // esporre sotto forma di Property il PersoneService
        // Persone (come Property) accede alla lista degli oggetti Persona del service
        public IList<Persona> Persone => _personeService.Persone;

        // le 2 Property su cui si baserà il Bilding
        // devono essere proprio Property, non basta mettere delle variabili standard pubbliche
        public Persona PersonaSelezionata { get; set; }
        private string _textSaluto;
        public string TextSaluto
        {
            get { return _textSaluto; }
            set
            {
                _textSaluto = value;
                /// <summary>
                /// operatore Elvis ?.  verifica che il riferimento all'oggetto non sia null
                /// parametro this - riferimento all'oggetto stesso che ha sollevato l'evento
                /// parametro 2 - i dati aggiuntivi (nel caso il riferimento alla proprietà il cui valore è cambiato) 
                /// </summary>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextSaluto)));

            }
        }


        // spostiamo anche la parte della logica di gestione
        public void Trascrivi()
        {
            if (PersonaSelezionata != null)
            {
                // stringa interpolata
                TextSaluto = $"ciao {PersonaSelezionata.Nome} {PersonaSelezionata.Cognome}!";
            }
        }
    }
}
