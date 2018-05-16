using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using iDigIt.Models;
using iDigIt.Views;
using Xamarin.Forms;

namespace iDigIt.ViewModels
{
    public class YieldListViewModel : BaseModel
    {
        #region Constructors
        public YieldListViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            Title = "Yield List";
            Yields = GetYields("All");

            ItemSelectedCommand = new Command<Yield>(HandleItemSelected);

        }     
        #endregion

        #region Properties        
        private ObservableCollection<Yield> _listOfYields;
        public ObservableCollection<Yield> Yields
        {
            get { return _listOfYields; }
            set
            {
                _listOfYields = value;
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
        private void HandleItemSelected(Yield yield)
        {
            if (yield == null) return;
            Navigation.PushAsync(new EditYieldPage(yield.YieldId));
            // selectedItemText = plant.Name;
        }
        #endregion       
    }
}
