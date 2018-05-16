using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using iDigIt.Helpers;
using iDigIt.Models;
using Xamarin.Forms;

namespace iDigIt.ViewModels
{
    public class ReviewContentFrostViewModel : BaseModel
    {
        public ReviewContentFrostViewModel()
        {
            GetYearList();
            Title = "Frost Review";
        }
     
        #region Properties

        private DateTimeOffset _earliestFrost;
        public DateTimeOffset EarliestFrost
        {
            get { return _earliestFrost; }
            set
            {
                _earliestFrost = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
            }
        }

        private DateTimeOffset _latestFrost;
        public DateTimeOffset LatestFrost
        {
            get { return _latestFrost; }
            set
            {
                _latestFrost = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
            }
        }

        private ObservableCollection<FrostCount> _listOfMonths;
        public ObservableCollection<FrostCount> Months
        {
            get { return _listOfMonths; }
            set
            {
                _listOfMonths = value;
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
		
		private ObservableCollection<Frost> _listOfNotes;
        public ObservableCollection<Frost> Notes
        {
            get { return _listOfNotes; }
            set
            {
                _listOfNotes = value;
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
                ProcessFrostData();
            }
        }

        private string _image;
        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
            }
        }
        #endregion

        private void GetYearList()
        {
            var years = new List<string> { "All" };
            var frosts = new List<Frost>(GetFrosts());
            foreach (var frost in frosts)
            {
                var year = string.Format("{0}/{1}", frost.Year, frost.Year + 1);
                if (!years.Contains(year))
                {
                    years.Add(year);
                }
            }

            Years = new ObservableCollection<string>(years);
        }

        private void ProcessFrostData()
        {           
            var frosts = new List<Frost>(GetFrosts(_year));

            var monthNames = new string[12] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            var months = new int[12];
            var firstYear = 0;
            var lastYear = 0;
            var earliestFrost = new DateTimeOffset();
            var latestFrost = new DateTimeOffset();            
            var maxFrostCount = 0;
            var frostCounts = new List<FrostCount>();
            var frostNotes = new List<Frost>();
            var duplicateFrosts = new List<string>();

            foreach (var frost in frosts)
            {
                var yearSplitDate = new DateTimeOffset(new DateTime(frost.Date.Year, 10, 1));
                var frostDate = frost.Date;

                #region set variables first time around
                if (frosts.IndexOf(frost) == 0)
                {                    
                    firstYear = lastYear = frost.Date.Year;
                }
                #endregion

                #region  determine first and last frost dates
                if (frostDate > yearSplitDate)
                {
                    // earliest frost
                    if (earliestFrost == DateTime.MinValue)
                    {
                        earliestFrost = frost.Date;
                    }
                    if (frostDate.Date
                        <= new DateTimeOffset(new DateTime(frost.Date.Year, earliestFrost.Month, earliestFrost.Day)))
                    {
                        earliestFrost = frost.Date;
                    }                   
                }
                else
                {
                    // latest frost
                    if (latestFrost == DateTime.MinValue)
                    {
                        latestFrost = frost.Date;
                    }
                    if (frostDate.Date
                        >= new DateTimeOffset(new DateTime(frost.Date.Year, latestFrost.Month, latestFrost.Day)))
                    {
                        latestFrost = frost.Date;
                    }
                }
                #endregion

                #region determine date range
                if (frostDate.Year > lastYear) lastYear = frostDate.Year;
                if (frostDate.Year < firstYear) firstYear = frostDate.Year;
                #endregion

                #region collect any notes
                if (!string.IsNullOrEmpty(frost.Notes))
                {
                    frostNotes.Add(frost);
                }                
                #endregion

                months[frost.Date.Month - 1] = months[frost.Date.Month - 1] + 1;
            }

            #region check for missing early or late dates
            if (latestFrost == DateTime.MinValue)
            {
                foreach (var frost in frosts)
                {
                    var frostDate = frost.Date;
                    if (frosts.IndexOf(frost) == 0)
                    {
                        latestFrost = frost.Date;
                    }

                    if (frostDate.Date
                       >= new DateTimeOffset(new DateTime(frost.Date.Year, latestFrost.Month, latestFrost.Day)))
                    {
                        latestFrost = frost.Date;
                    }
                }
            }
            if (earliestFrost == DateTime.MinValue)
            {
                foreach (var frost in frosts)
                {
                    var frostDate = frost.Date;
                    if (frosts.IndexOf(frost) == 0)
                    {
                        earliestFrost = frost.Date;
                    }

                    if (frostDate.Date
                        <= new DateTimeOffset(new DateTime(frost.Date.Year, earliestFrost.Month, earliestFrost.Day)))
                    {
                        earliestFrost = frost.Date;
                    }
                }
            }
            #endregion

            if (firstYear * lastYear > 0)
            {                               
                for (int i = earliestFrost.Date.Month - 1; i < 12; i++)
                {
                    if (months[i] > 0)
                    {                        
                        if (!duplicateFrosts.Contains(string.Format("{0}{1}",monthNames[i],months[i])))
                        {
                            duplicateFrosts.Add(string.Format("{0}{1}", monthNames[i], months[i]));

                            frostCounts.Add(new FrostCount { Month = monthNames[i], Count = months[i] });
                            if (maxFrostCount < months[i]) maxFrostCount = months[i];
                        }
                    }
                }
                for (int i = 0; i < latestFrost.Date.Month; i++)
                {
                    if (months[i] > 0)
                    {
                        if (!duplicateFrosts.Contains(string.Format("{0}{1}", monthNames[i], months[i])))
                        {
                            duplicateFrosts.Add(string.Format("{0}{1}", monthNames[i], months[i]));

                            frostCounts.Add(new FrostCount { Month = monthNames[i], Count = months[i] });
                            if (maxFrostCount < months[i]) maxFrostCount = months[i];
                        }
                    }
                }

                foreach (var frostCount in frostCounts)
                {
                    frostCount.MaxFrostCount = maxFrostCount;
                }
            }

            #region initialise view
            Months = new ObservableCollection<FrostCount>(frostCounts);
            Notes = new ObservableCollection<Frost>(frostNotes);                  
            EarliestFrost = earliestFrost;
            LatestFrost = latestFrost;            
  
            #endregion
        }

    }

}
