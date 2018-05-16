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
using Xamarin.Forms;

namespace iDigIt.ViewModels
{
    public class ReviewContentJobViewModel : BaseModel
    {
        public ReviewContentJobViewModel()
        {
            GetYearList();
            Title = "Job Review";

            ItemSelectedCommand = new Command<object>(HandleItemSelected);
        }

        #region Properties   
        private ObservableCollection<Job> _jobs;
        public ObservableCollection<Job> Jobs
        {
            get { return _jobs; }
            set
            {
                _jobs = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
            }
        }
        private int _jobTotal;
        public int JobTotal
        {
            get { return _jobTotal; }
            set
            {
                _jobTotal = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
            }
        }
        private int _timeTotal;
        public int TimeTotal
        {
            get { return _timeTotal; }
            set
            {
                _timeTotal = value;
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

        private ObservableCollection<JobTypeCount> _jobTypeCounts;
        public ObservableCollection<JobTypeCount> JobTypeCounts
        {
            get { return _jobTypeCounts; }
            set
            {
                _jobTypeCounts = value;
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
                ProcessJobData();
            }
        }
        #endregion

        #region Commands
        public ICommand ItemSelectedCommand { get; private set; }
        #endregion

        #region Events
        private void HandleItemSelected(object itemSelected)
        {
            if (itemSelected == null) return;

            if (itemSelected.GetType() == typeof(JobTypeCount))
            {
                _timeTotal = 0;
                Jobs = GetJobsByTypeYear(((JobTypeCount)itemSelected).Name);
                JobTotal = Jobs.Count;
                foreach (var job in Jobs)
                {
                    _timeTotal = _timeTotal + job.Time;   
                }

                TimeTotal = _timeTotal;
            }
            else
            {
                var job = (Job)itemSelected;                

                AddJobToNextSeason(job);

                Application.Current.MainPage.DisplayAlert("",
                    string.Format("Job added sucessfully to {0}/{1} Season", job.Date.Year, job.Date.Year + 1), "Ok");
            }
        }
       
        #endregion

        #region Private Methods
        private ObservableCollection<Job> GetJobsByTypeYear(string type)
        {
            
            if (_year == "All")
            {
                return new ObservableCollection<Job>(
                _realmInstance.All<Job>()
                .Where(x=>x.Type == type)
                .OrderBy(x => x.Date)
                .ThenBy(x => x.Plant)
                .ThenBy(x => x.Type).ToList());
            }
            else
            {
                var yearPair = _year.Split('/');
                var firstYear = Convert.ToInt16(yearPair[0]);
                var secondYear = Convert.ToInt16(yearPair[1]);
                var startDate = new DateTimeOffset(new DateTime(firstYear, 8, 31));
                var endDate = new DateTimeOffset(new DateTime(secondYear, 8, 31));

                return new ObservableCollection<Job>(
                     _realmInstance.All<Job>()
                     .Where(x => x.Type == type && (x.Date > startDate && x.Date <= endDate))
                     .OrderBy(x => x.Date).ToList());
            }

        }
        private void GetYearList()
        {
            var years = new List<string> { "All" };
            var jobs = new List<Job>(GetJobs("All"));
            foreach (var job in jobs)
            {
                if(jobs.IndexOf(job)==0)
                {
                    var startYear = string.Format("{0}/{1}", job.Date.Year - 1, job.Date.Year);
                    if (!years.Contains(startYear))
                    {
                        years.Add(startYear);
                    }
                }
                var year = string.Format("{0}/{1}", job.Date.Year, job.Date.Year + 1);
                if (!years.Contains(year))
                {
                    years.Add(year);
                }
            }

            Years = new ObservableCollection<string>(years);
        }

        private void ProcessJobData()
        {
            var jobs = new List<Job>(GetJobs(_year));
            var cultivate = 0;
            var preparation = 0;
            var general = 0;

            var cultivateTime = 0;
            var preparationTime = 0;
            var generalTime = 0;

            foreach (var job in jobs)
            {
                switch (job.Type)
                {
                    case "Cultivate":
                        cultivate++;
                        cultivateTime = cultivateTime + job.Time;
                        break;
                    case "Preparation":
                        preparation++;
                        preparationTime = preparationTime + job.Time;
                        break;
                    case "General":
                        general++;
                        generalTime = generalTime + job.Time;
                        break;
                }
            }
            JobTotal = jobs.Count;
            TimeTotal = cultivateTime + preparationTime + generalTime;

            JobTypeCounts = new ObservableCollection<JobTypeCount>(new List<JobTypeCount> {
                new JobTypeCount { Name = "Cultivate", Count = cultivate, Total = jobs.Count, Time = cultivateTime, TotalTime=TimeTotal},
                new JobTypeCount { Name = "Preparation", Count = preparation,Total = jobs.Count, Time = preparationTime, TotalTime=TimeTotal },
                new JobTypeCount { Name = "General", Count = general,Total = jobs.Count, Time = generalTime, TotalTime=TimeTotal}
            });
        }

        #endregion
    }
}