using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2conTendICommand.Models
{
    public interface IPersoneService
    {
        IList<Persona> Persone { get; }
    }
    /// <summary>
    /// Classe che simula la sorgente dati
    /// Incapsula al suo interno un elenco di oggetti Persona
    /// memorizzandoli in una lista privata
    /// assegnamo null perchè sarà il costruttore a crearla
    /// 
    /// Costruttore a cui daremo anche il compito di riempire di dati la lista
    /// </summary>
    class PersoneService : IPersoneService
    {
        private List<Persona>? _persone = null;

        /// <summary>
        /// il costruttore avrà anche il compito di riempire di dati la lista
        /// </summary>
        public PersoneService()
        {
            _persone = new List<Persona>(); // tipo concreto
            // lista di inizializzazione
            _persone.Add(new Persona() { Nome = "Mario", Cognome = "Terra" });
            _persone.Add(new Persona() { Nome = "Andrea", Cognome = "Acqua" });
            _persone.Add(new Persona() { Nome = "Sara", Cognome = "Cielo" });
            _persone.Add(new Persona() { Nome = "Akane", Cognome = "Fuoco" });
        }

        /// <summary>
        /// GET per restituire, essento List Private!
        /// Il tipo di ritorno non è il tipo concreto (List<Persona>)
        /// ma una interfaccia IList
        /// il tipo generico è iterabile ed ha i suoi metodi
        /// </summary>
        public IList<Persona> Persone => _persone;
        /*
         * Abbreviazione di:
         * public IList<Tendina> Tendine {
         *   get { return _tendine; }
         * } 
        */
    }
}
