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
    public class JobListViewModel : BaseModel
    {        
        #region Constructors
        public JobListViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            Title = "Job List";

            Jobs = GetJobs();
            ItemSelectedCommand = new Command<Job>(HandleItemSelected);
            
        }        
        #endregion

        #region Properties
        private ObservableCollection<Job> _listOfJobs;
        public ObservableCollection<Job> Jobs
        {
            get { return _listOfJobs; }
            set
            {
                _listOfJobs = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
            }
        }
        #endregion

        #region Commands
        public ICommand ItemSelectedCommand { get; private set; }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }

        private void HandleItemSelected(Job job)
        {
            if (job == null) return;
            Navigation.PushAsync(new EditJobPage(job.JobId));
        }
        #endregion
    }
}
