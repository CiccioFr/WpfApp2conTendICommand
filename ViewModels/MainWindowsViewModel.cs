using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp2conTendICommand.Models;

namespace WpfApp2conTendICommand.ViewModels
{
    /// <summary>
    /// Dopo la NotifyPropertyChanged();
    /// occorre isolare la View dalla gestione dagli eventi (es. evento Click veri e propri, non affini come mouseUp/Down)
    /// Dato che gli oggetti cliccabili possono essere tanti, conviene usare una classe dedicata
    /// </summary>
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        // i metodi da implementare saranno forniti dall'esterno al Costruttore della classe...
        //public bool CanExecute(object? parameter)
        //public void Execute(object? parameter)
        // e saranno memorizzati in:
        // (sono dei riferimenti a dei metodi che potranno essere invocati tramite essi)
        private Action<object> executeMethod;
        // Action corrisponde alla definizione di un metodo che accetta
        // esattamente un parametro di tipo Object e non restituisce nulla
        // qui sarà specificato cosa dovrò essere fatto quando si fa click sul bottone
        private Predicate<object> canExecuteMethod;
        // Predicate definisce un delegato, cioè un riferimento ad un metodo,
        // che contiene al suo interno dei criteri (delle verifiche)
        // acettando in ingresso un unico parametro di tipo Object
        // e restituendo true/false a seconda che i criteri siano rispettati
        // *** inciso sintattico: in internet vari esempi riportano:
        // *** in alternativa al Syntactic Sugar:  private Predicate<object> canExecuteMethod;
        // ***  che rende più semplice la definizione di delegato, 
        // *** IL PIU' GENERICO  "Func<object,bool>" (bool in questo caso per avere l'equivalenza col Predicate)
        // *** che però a differenza di quest'ultimo può ricevere sino a 16 parametri
        // *** e può definire un tipo restituito anche diverso da bool
        // ** in sostanza il "Predicate" è una comodità

        // i Costruttori:
        /// <summary>
        /// Costruttore generico
        /// </summary>
        /// <param name="Execute"> delegato per il click </param>
        /// <param name="CanExecute"> controllo per lo stato di attivo del pulsante
        /// (non fa altro che memorizzare internamente nelle due variabili private
        /// i riferimenti ai metodi ricevuti come parametro)</param>
        public RelayCommand(Action<object> Execute, Predicate<object> CanExecute)
        {
            this.executeMethod = Execute;
            this.canExecuteMethod = CanExecute;
        }

        /// <summary>
        /// per i casi in cui andremo a specificare soltanto il da farsi sul click
        /// (sosstinteso cle l'elemento sarà sempre cliccabile (attivo)
        /// e che sfrutta la delega tra costruttori,
        /// richiama il precedente (quello piu completo) (this) 
        /// passandogli lo stesso riferimento al metodo Execute
        /// e null come riferimetno all'altro metodo che non c'è
        /// </summary>
        /// <param name="Execute"></param>
        public RelayCommand(Action<object> Execute) : this(Execute, null)
        {
        }

        // essendo i delegati privati, abbiamo bisogno di 2 metodi pubblici per invocarli:
        public bool CanExecute(object parameter)
        {
            // il parametro sarà passato al metodo agganciato al delegato
            // essendoci lapossibilià che, richiamando il costruttore sopra
            // (public RelayCommand(Action<object> Execute) : this(Execute, null))
            // manchi, si fa un conrollo:
            // se non c'è, l'elemento è sempre clò.to verificato dal metodo delegato, qua invocato
            return (canExecuteMethod == null) ? true : canExecuteMethod.Invoke(parameter);
        }

        /// <summary>
        /// similmente, quando dovrà essere eseguito il metodo per il click verà richiamato questo
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            executeMethod?.Invoke(parameter);
        }
        // **** per l'invocazione del metodo agganciato ad un delegato si può trovare anche senza l'.invoke
        // **** ->  executeMethod(parameter);  <= oppure =>  executeMethod?.Invoke(parameter);
        // ****  sono sotanzialmente equivalenti

        /// <summary>
        /// metodo che sarà richiamato ogni volta che si dovrà rivalutare
        /// la condizione di cliccabile/non (attivo/non) dell'elemento
        /// es. se un Button non è mai stato usato ed è in uno stato di non cliccabile, dovrà diventarlo
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Classe che implementerà l'interfaccia INotifyPropertyChanged
    /// Fara da Base per le ViewModel
    /// conterrà l'evento PropertyChanged
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Metodo che solleverà l'evento
        /// 
        /// </summary>
        /// <param name="propertyName"></param> nome della Proprietà di cui si vuole notificare la modifica (come valore)
        /// l'atributo [CallerMemberName] permetterà di passare quale parametro attuale il nome della property corrente da cui è partita la chiamata al metodo
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// Classe di collegamento tra model e view
    /// </summary>
    public class MainWindowsViewModel : BaseViewModel
    {
        // controllo per la sorgente dei dati, da passare al costruttore
        private IPersoneService _personeService = null;

        // dopo la "class RelayCommand" aggiungiamo:
        /// <summary>
        /// Properti a cui si aggancia una istanza della classe RelayCommand
        /// il private del setter rende di fatto SalutaCommand -> readonly
        /// </summary>
        public RelayCommand SalutaCommand { get; private set; }

        public MainWindowsViewModel(IPersoneService personeService)
        {
            _personeService = personeService;
            // qui è dove si deve agganciare alla Property una istanza della classe RelayCommand
            // indicando i metodi da agganciare ai delegati
            SalutaCommand = new RelayCommand(SalutaMethod, SalutaCanExec);
            // si potrebbe anche usare (essendo semplice) una lamda:
            // SalutaCommand = new RelayCommand(param => Saluta(), param => PersonaSelezionata != null);
            // param => Saluta() -> va a richiamare il metodo Saluta()
            // param => PersonaSelezionata != null -> (i famosi chiteri / verifiche) restituisce se abbiamo selezionato qualcosa o no nella ComboBox

        }

        private void SalutaMethod(object param) // lui scrive iniziale minuscola
        {
            Trascrivi();
        }

        private bool SalutaCanExec(object param) // lui scrive iniziale minuscola
        {
            return PersonaSelezionata != null;
        }

        // esporre sotto forma di Property il PersoneService
        // Persone (come Property) accede alla lista degli oggetti Persona del service
        public IList<Persona> Persone => _personeService.Persone;

        /// <summary>
        /// le 2 Property su cui si baserà il Bilding
        /// devono essere proprio Property, non basta mettere delle variabili standard pubbliche
        /// </summary>
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
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextSaluto)));
                /// <summary>
                /// il compilatore a runtime, coglierà la variazione del valore della Property
                /// il Metodo (originario della classe astratta) grazie al suo partilare parametro con [attributo]
                /// prenderà quale parametro attuale il nome della property "TextSaluto"
                /// Se inserissimo come parametro "" (stringa nulla) o null
                /// la View farà un refresh di tutti i controlli
                /// Nota: in caso di lista (elenco valori) il puntatore (riferimento) resterebbe identico anche se cambiasse uno degli elementi contenuti
                /// e questo non farebbe scattare il refresh (alla modifica della lista, il puntatore sarebbe lo stesso,
                /// e NotifyPropertyChanged() non si accorgerebbe che è avvenuta una modifica, modifica che scatenerebbe il refresh).
                /// Ecco perchè per le liste spesso viene usata la "ObservableCollection<>" in vece del field "_textSaluto"
                /// che si contraddistingue proprio per essere in grado di notificare i cambiamenti al suo contenuto (l'aggiunta o la rimozione)
                /// </summary>
                NotifyPropertyChanged();
                /// Per completare a livello base una ViewModel, occorre isolare la View dalla gestione dagli eventi (es. evento Click veri e propri, non affini come mouseUp/Down)
                /// Bisogna accedere ad un altro tipo di interfaccia: ICommand - Direttiva "using System.Windows.Input"
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
