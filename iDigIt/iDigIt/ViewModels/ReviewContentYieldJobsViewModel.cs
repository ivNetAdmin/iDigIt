using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using iDigIt.Helpers;
using iDigIt.Models;
using iDigIt.Views;
using Xamarin.Forms;

namespace iDigIt.ViewModels
{
    public class ReviewContentYieldJobsViewModel : BaseModel
    {
        #region Constructors
        public ReviewContentYieldJobsViewModel(INavigation navigation, string yieldId)
        {
            this.Navigation = navigation;
            Title = "Yield Review";

            Yield = _realmInstance.Find<Yield>(yieldId);

            RelatedJobs = new ObservableCollection<Job>(GetRelatedJobs(_yield.Plant, _yield.Year));
           // UnrelatedJobs = new ObservableCollection<Job>(GetUnRelatedJobs(_yield.Year));
            MoreYields = new ObservableCollection<Yield>(GetMoreYields(_yield.Plant, _yield.Year));

            ItemSelectedCommand = new Command<Yield>(HandleItemSelected);
        }
        #endregion

        #region Properties
        private Yield _yield;
        public Yield Yield
        {
            get { return _yield; }
            set
            {
                _yield = value;
                OnPropertyChanged(); // Add the OnPropertyChanged();
            }
        }
        private ObservableCollection<Job> _listOfRelatedJobs;
        public ObservableCollection<Job> RelatedJobs
        {
            get { return _listOfRelatedJobs; }
            set
            {
                _listOfRelatedJobs = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
            }
        }
        private ObservableCollection<Job> _listOfUnrelatedJobs;
        public ObservableCollection<Job> UnrelatedJobs
        {
            get { return _listOfUnrelatedJobs; }
            set
            {
                _listOfUnrelatedJobs = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
            }
        }
        private ObservableCollection<Yield> _listOfMoreYields;
        public ObservableCollection<Yield> MoreYields
        {
            get { return _listOfMoreYields; }
            set
            {
                _listOfMoreYields = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
            }
        }
        #endregion

        #region Events
        private void HandleItemSelected(Yield yield)
        {
            if (yield == null) return;            
            Navigation.PushAsync(new ReviewContentYieldJobsPage(yield.YieldId));
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
        }
        #endregion    

        #region Commands#
        public ICommand ItemSelectedCommand { get; private set; }
        public Command AddYieldJobsCommand // for ADD
        {
            get
            {
                return new Command(async (param) =>
                {

                    switch ((string)param)
                    {
                        case "related":
                            AddRelatedJobs();                           
                            break;
                        case "unrelated":
                            break;

                    }                    
                    var firstYear = Convert.ToInt16(_yield.Year);
                    var secondYear = Convert.ToInt16(_yield.Year + 1);

                    await Application.Current.MainPage.DisplayAlert("", 
                        string.Format("All related jobs added sucessfully to {0}/{1} Season", firstYear, secondYear), "Ok");
                });
            }
        }       
        #endregion

        #region Private methods
        private List<Job> GetRelatedJobs(string plant,int year)
        {
            var startDate = new DateTimeOffset(new DateTime(year - 1, 9, 30));
            var endDate = new DateTimeOffset(new DateTime(year, 10, 01));
            return new List<Job>(
                _realmInstance.All<Job>()
                .Where(x => x.Plant == plant && x.Date > startDate && x.Date < endDate)
                .OrderBy(x => x.Type)
                .ThenBy(x => x.Date).ToList());

        }
        private List<Job> GetUnRelatedJobs(int year)
        {
            var startDate = new DateTimeOffset(new DateTime(year - 1, 9, 30));
            var endDate = new DateTimeOffset(new DateTime(year, 10, 01));
            return new List<Job>(
                _realmInstance.All<Job>()
                .Where(x => x.Type == "General" && x.Date > startDate && x.Date < endDate)
                .OrderBy(x => x.Type)
                .ThenBy(x => x.Date).ToList());

        }
        private List<Yield> GetMoreYields(string plant, int year)
        {
            return new List<Yield>(
               _realmInstance.All<Yield>()
               .Where(x => x.Plant == plant && x.Year != year)
               .OrderByDescending(x => x.Crop)
               .ThenBy(x => x.Year).ToList());

        }

        private void AddRelatedJobs()
        {
            foreach (Job job in _listOfRelatedJobs)
            {
                AddJobToNextSeason(job);                
            }
        }
        #endregion
    }
}
