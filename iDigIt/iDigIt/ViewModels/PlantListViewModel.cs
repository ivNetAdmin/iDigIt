using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using iDigIt.Models;
using iDigIt.Views;
using Xamarin.Forms;

namespace iDigIt.ViewModels
{
    public class PlantListViewModel : BaseModel
    {
        #region Constructors
        public PlantListViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            Title = "Plant List";

            Plants = GetPlants(); 

            ItemSelectedCommand = new Command<Plant>(HandleItemSelected);          
        }        
        #endregion

        #region Properties
        private ObservableCollection<Plant> _listOfPlants;
        public ObservableCollection<Plant> Plants
        {
            get { return _listOfPlants; }
            set
            {
                _listOfPlants = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
            }
        }      
        #endregion

        #region Commands
        public ICommand ItemSelectedCommand { get; private set; }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(
    [CallerMemberName] string caller = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }
        private void HandleItemSelected(Plant plant)
        {
            if (plant == null) return;
            Navigation.PushAsync(new EditPlantPage(plant.PlantId));
            // selectedItemText = plant.Name;
        }
        #endregion       
    }
}
