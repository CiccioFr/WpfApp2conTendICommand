# WpfApp2conTendICommand

Terzo Passaggio
partendpo dal video - [6: ICommand (RelayCommand Class)](https://www.youtube.com/watch?v=WZJNSG60GjM&list=PL0qAPtx8YtJe3WjjoRaB28ZGlX9heBqn3&index=11)

# Continuiamo col progressivo miglioramento architetturale

vito che ogni classe ViewModel (MainWindowsViewModel), di collegamento tra un model e view, deve implementre un'interfaccia INotifyPropertyChanged
� meglio raccogliere a fattor comune alcuni elenti in una classe base astratta,
che abbia gi� al suo interno incorporata questa caratteristica e implementi qunto necessario:
- inseriamo in MainWindowsViewModel.cs la classe **public abstract class BaseViewModel : INotifyPropertyChanged** (prima di MainWindowsViewModel)
	- questa implementer� l'interfaccia INotifyPropertyChanged




---
si potevano usare due librerie:
- https://github.com/Fody/PropertyChanged
	- inietta (cio� genera) in fase di compilazione del codice, in modo automatico, per la notifica del cambio valore all'interno della nostra propriet�
- MvvmLight
	- prevede classi per la gesione delle ViewModel e della Dependency Injection quando vengono considerate pi� piattaforme