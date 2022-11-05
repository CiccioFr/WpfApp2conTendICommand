using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp2conTendICommand.Models;
using WpfApp2conTendICommand.ViewModels;

namespace WpfApp2conTendICommand
{
    /// <summary>
    /// Logica di interazione per App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Personalizziamo il momento dell'avvio dell'app
        /// facendo overrieder del metodo OnStartup() eseguito in automatico all'esecuzione dell'app
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            /// richiamo al rispettivo metodo della classe base
            base.OnStartup(e);

            PersoneService personeService = new PersoneService();
            MainWindowsViewModel mainWindowsViewModel = new MainWindowsViewModel(personeService);

            /// aggiungiamo a mano:
            //MainWindow mainWindows = new MainWindow(new PersoneService());
            MainWindow mainWindows = new MainWindow(mainWindowsViewModel);
            mainWindows.Show();
        }
    }
}
