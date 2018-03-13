using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Newtonsoft.Json;
using Windows.UI.Popups;

namespace App1.ViewModel
{
    class MainModel : Helper.MyPropertyChanged
    {
        private ObservableCollection<Model.Tasks> _ListTasks { get; set; }
        public ObservableCollection<Model.Tasks> ListTasks { get { return _ListTasks; } set { _ListTasks = value; NotifyPropertyChanged(); } }
        private Helper.Command _addCommand;
        private Helper.Command _modifItem;
        private Helper.Command _supprItem;
        private Model.Tasks _SelectedTask;
        //private static string JsonFile = "../Appdata/tasks.json";

        /**
         * Envoie a la fonction addSomething lorsque l'utilisateur clique sur le boutton valider la tâche
         * 
         **/
        public ICommand addCommand {
            get
            {
                if (_addCommand == null)
                    _addCommand = new Helper.Command((x) => addSomething(x), true);
                return _addCommand;
            }
        }

        public ICommand supprItem
        {
            get
            {
                if (_supprItem == null)
                    _supprItem = new Helper.Command((x) => supprSelected(x), true);
                return _supprItem;
            }
        }

        public ICommand modifItem
        {
            get
            {
                if (_modifItem == null)
                    _modifItem = new Helper.Command((x) => modifSelected(x), true);
                return _modifItem;
            }
        }

        public Model.Tasks SelectedTask
        {
            get { return (_SelectedTask); }
            set { _SelectedTask = value; }
        }


        /**
         * Fonctions qui permettent de récupérer les informations lors de la validation du formulaire
         * 
         **/
        public String NewTaskTitle { get { return NewTask.title; } set { NewTask.title = value; NotifyPropertyChanged(); } }
        public String NewTaskContent { get { return NewTask.content; } set { NewTask.content = value; NotifyPropertyChanged(); } }
        public DateTimeOffset NewTaskDate { get { return NewTask.date; } set { NewTask.date = value.DateTime; NotifyPropertyChanged(); } }
        public Boolean NewTaskUrgence { get { return NewTask.urgence; } set { NewTask.urgence = value; NotifyPropertyChanged(); } }
        private Model.Tasks NewTask { get; set; }
        public object TaskList { get; private set; }


        public MainModel()
        {

            //Création de tâche en dur dans l'application
            ListTasks = new ObservableCollection<Model.Tasks>()
            {
                new Model.Tasks { title = "Finir le Todolist", content = "Permettre la suppression des tasks, mieux gerer le datepicker", date = DateTime.Today, urgence = true },
                new Model.Tasks { title = "Epicture", content="Faire tout le projet epicture", date = DateTime.Today, urgence = false},
            };
            NewTask = new Model.Tasks() { date = DateTime.Today, statut = 1 };
            
        }

        public class RootObject
        {
            public string title { get; set; }
            public string content { get; set; }
            public string date { get; set; }
            public bool urgence { get; set; }
        }

        public bool canExecuteAdd(object args)
        {
            return true;
        }


        /**
         * Fonction qui crée une nouvelle tâche avec les paramètre rentré dans le formulaire
         * 
         **/
        public void addSomething(object parameter)
        {
            if (NewTask.title == null || NewTask.content == null)
                dialogErrorAdd();
            else
            {
                ListTasks.Add(NewTask);
                NotifyPropertyChanged("ListTasks");
                NewTask = new Model.Tasks();
            }
        }

        public void supprSelected(object parameter)
        {
            ValidateDelete(parameter);
        }

        public void modifSelected(object parameter)
        {
            if (SelectedTask != null)
            {
            }
        }

        private async void dialogErrorAdd()
        {
            MessageDialog showDialog = new MessageDialog("Impossible d'ajout une tâche incomplète, veuillez renseigner un titre et un contenu.");
            var result = await showDialog.ShowAsync();
        }


        private async void ValidateDelete(object sender)
        {
            MessageDialog showDialog = new MessageDialog("Etes vous sur de vouloir supprimer cette tache : " + SelectedTask.title + " ?");
            showDialog.Commands.Add(new UICommand("Oui") { Id = 0 });
            showDialog.Commands.Add(new UICommand("Non") { Id = 1 });
            showDialog.DefaultCommandIndex = 0;
            showDialog.CancelCommandIndex = 1;
            var result = await showDialog.ShowAsync();
            if ((int)result.Id == 0)
            {
                if (SelectedTask != null)
                {
                    ListTasks.Remove(SelectedTask);
                    NotifyPropertyChanged("ListTasks");
                }
            }
        }


    }
}
