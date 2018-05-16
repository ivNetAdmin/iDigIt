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
    public class FrostListViewModel : BaseModel
    {
        #region Constructors
        public FrostListViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            Title = "Frost List";

            Frosts = GetFrosts();

            ItemSelectedCommand = new Command<Frost>(HandleItemSelected);
        }
        #endregion

        #region Properties
        private ObservableCollection<Frost> _listOfFrosts;
        public ObservableCollection<Frost> Frosts
        {
            get { return _listOfFrosts; }
            set
            {
                _listOfFrosts = value;
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
        private void HandleItemSelected(Frost frost)
        {
            if (frost == null) return;
            Navigation.PushAsync(new EditFrostPage(frost.FrostId));
        }
        #endregion       
    }
}
