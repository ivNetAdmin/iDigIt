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
    public class ReviewContentYieldViewModel : BaseModel
    {
        public ReviewContentYieldViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            Title = "Yield Review";

            GetYearList();

            ItemSelectedCommand = new Command<Yield>(HandleItemSelected);
        }

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

        private ObservableCollection<string> _listOfYears;
        public ObservableCollection<string> Years
        {
            get { return _listOfYears; }
            set
            {
                _listOfYears = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
            }
        }

        private string _year;
        public string Year
        {
            get { return _year; }
            set
            {
                _year = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
                ProcessYieldData();
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
        private void HandleItemSelected(Yield yield)
        {
            if (yield == null) return;
            Navigation.PushAsync(new ReviewContentYieldJobsPage(yield.YieldId));
        }
        #endregion       

        #region Private
        private void GetYearList()
        {
            var years = new List<string> { "All" };
            var yields = new List<Yield>(GetYields("All"));
            foreach (var yeild in yields)
            {
                var year = string.Format("{0}", yeild.Year);
                if (!years.Contains(year))
                {
                    years.Add(year);
                }
            }

            Years = new ObservableCollection<string>(years);
        }

        private void ProcessYieldData()
        {
            Yields = GetYields(_year, "review");
        }
        #endregion
    }
}


