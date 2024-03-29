﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2conTendICommand.ViewModels;

namespace WpfApp2conTendICommand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // togliamo:
        // private IPersoneService? personeService = null;
        // perchè useremo il DataContext
        // DataContext è l'oggetto dal quale recuperare i dati che dovranno essere gestiti tramite il controllo stesso
        // Se un oggetto non ha il DataContext di suo, erediterà quello del suo contenitore

        //public MainWindow(IPersoneService personeService)
        public MainWindow(MainWindowsViewModel vm)
        {
            InitializeComponent();

            DataContext = vm;
            // sarà ereditato da tutto il suo contenuto
        }

        // RIMOSSO dopo la modifica del codice XAML relativo (tolto evento click dall'elemento Button)
        // il tutto ora è gestito direttamente dalla ViewModel
        //private void ButtonName_Click(object sender, RoutedEventArgs e)
        //{
        //    Trascrivi();
        //}

        //private void Trascrivi()
        //{
        //    //if (cmdBoxPersone.SelectedItem != null)
        //    //{
        //    //    Persona persona = (Persona)cmdBoxPersone.SelectedItem;
        //    //    // stringa interpolata
        //    //    xtxSaluto.Text = $"ciao {persona.Nome} {persona.Cognome}";
        //    //}
        //    (DataContext as MainWindowsViewModel).Trascrivi();
        //}
    }
}
