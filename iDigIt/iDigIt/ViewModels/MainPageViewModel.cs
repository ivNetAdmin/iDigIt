
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using iDigIt.Models;
using iDigIt.Views;
using Xamarin.Forms;

namespace iDigIt.ViewModels
{
    public class MainPageViewModel : BaseModel
    {
        private DateTimeOffset _currentDate;
        private Frost _frost;
        private string _frostId;
        private string _frostImage = "quickFrostOff.png";

        #region Constructors
        public MainPageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            Title = "dIgIt";

            SetPropertiesCollections();

            _currentDate = DateTimeOffset.Now;
            DisplayCalendarDate = _currentDate.ToString("MMM yyyy");

            _frostId = string.Format("{0}{1}{2}", _currentDate.Date.Year, _currentDate.Date.Month, _currentDate.Date.Day);

            Frost = _realmInstance.Find<Frost>(_frostId);

            if (_frost != null) _frostImage = "quickFrostOn.png";

            QuickFrostIcon = _frostImage;

            ItemSelectedCommand = new Command<Job>(HandleItemSelected);
        }        
        #endregion

        #region Properties
        private string _displayCalendarDate;
        public string DisplayCalendarDate
        {
            get { return _displayCalendarDate; }
            set
            {
                if (_displayCalendarDate != value)
                {
                    _displayCalendarDate = value;                   
                    OnPropertyChanged();
                }
            }
        }
        public Frost Frost
        {
            get { return _frost; }
            set
            {
                _frost = value;
                OnPropertyChanged(); // Add the OnPropertyChanged();
            }
        }

        public String QuickFrostIcon
        {
            get { return _frostImage; }
            set
            {
                _frostImage = value;
                OnPropertyChanged(); // Add the OnPropertyChanged();
            }
        }

        private ObservableCollection<Job> _jobList = new ObservableCollection<Job>();
        public ObservableCollection<Job> JobList
        {
            get { return _jobList; }
            set
            {
                _jobList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Color> _dateRangeTextColour = new ObservableCollection<Color>();
        public ObservableCollection<Color> DateRangeTextColour
        {
            get { return _dateRangeTextColour; }
            set
            {
                _dateRangeTextColour = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DateTime> _calendarDates = new ObservableCollection<DateTime>();
        public ObservableCollection<DateTime> CalendarDates
        {
            get { return _calendarDates; }
            set
            {
                if (_calendarDates != value)
                {
                    _calendarDates = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<string> _dateRangeDate = new ObservableCollection<string>();
        public ObservableCollection<string> DateRangeDate
        {
            get { return _dateRangeDate; }
            set
            {
                _dateRangeDate = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Boolean> _dateRangeVisible = new ObservableCollection<Boolean>();
        public ObservableCollection<Boolean> DateRangeVisible
        {
            get { return _dateRangeVisible; }
            set
            {
                _dateRangeVisible = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _dateRangeJobType = new ObservableCollection<string>();
        public ObservableCollection<String> DateRangeJobType
        {
            get { return _dateRangeJobType; }
            set
            {
                _dateRangeJobType = value;
                OnPropertyChanged();
            }
        }

        public ImageSource QuickFrostOffIcon { get { return ImageSource.FromFile(_frostImage); } }
        #endregion

        #region Commands
        public ICommand ItemSelectedCommand { get; private set; }
        public Command ChangeCalendarCommand // for ADD
        {
            get
            {
                return new Command((param) =>
                {

                    switch ((string)param)
                    {
                        case "NextMonth":
                            _currentDate = _currentDate.AddMonths(1);
                            break;
                        case "NextYear":
                            _currentDate = _currentDate.AddYears(1);
                            break;
                        case "LastMonth":
                            _currentDate = _currentDate.AddMonths(-1);
                            break;
                        case "LastYear":
                            _currentDate = _currentDate.AddYears(-1);
                            break;
                    }
                    DisplayCalendarDate = _currentDate.ToString("MMM yyyy");
                    SetDateRange();
                });
            }
        }
        public Command CalendarDatePickedCommand
        {
            get
            {
                return new Command(async (param) =>
                {
                    await Navigation.PushAsync(new AddJobPage(_calendarDates[Convert.ToInt32(param)]));
                });
            }
        }
        public Command OnQuickFrostButtonTapped
        {
            get
            {
                return new Command(() =>
                {
                    if (Frost == null)
                    {
                        Frost = new Frost { Date = DateTimeOffset.Now };
                        _frost.FrostId = _frostId.ToLower().Replace(" ", "");
                        _frost.Year = _frost.Date.Year;
                        _frost.Month = _frost.Date.Month;
                        _frost.Day = _frost.Date.Day;

                        _realmInstance.Write(() =>
                        {
                            _realmInstance.Add(_frost, true); // Add the whole set of details
                        });

                        QuickFrostIcon = "quickFrostOn.png";
                    }

                });
            }
        }
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
        #region Public methods
        public void PageAppearingSetDateRange()
        {
            SetDateRange();
        }
        #endregion

        #region Private methods
        private void SetDateRange()
        {
            #region set calendar variables
            var fistOfTheMonth = new DateTime(_currentDate.Year, _currentDate.Month, 1);
            var firstDayofMonth = fistOfTheMonth.DayOfWeek;
            var startDate = (int)firstDayofMonth == 0 ? fistOfTheMonth.AddDays(-6) : fistOfTheMonth.AddDays(-1 * ((int)firstDayofMonth - 1));
            var endDate = startDate.AddDays(42);
            var visible = true;
            _dateRangeDate.Clear();
            _dateRangeJobType.Clear();
            _dateRangeTextColour.Clear();
            _calendarDates.Clear();
            _dateRangeVisible.Clear();
            _jobList.Clear();
            #endregion

            #region set calendar cell values
            for (int i = 0; i < 42; i++)
            {
                var date = startDate.Date.AddDays(i);
                var day = date.Day;
                var fontColour = Color.FromHex(_calendarFontColor);

                if (date.Day == DateTime.Now.Day
                    && date.Month == DateTime.Now.Month
                    && date.Year == DateTime.Now.Year) fontColour = Color.FromHex(_calendarToday);
                if (i < 7 && day > 10) fontColour = Color.FromHex(_calendarLowlight);
                if (i > 20 && day < 10) fontColour = Color.FromHex(_calendarLowlight);

                _dateRangeTextColour.Add(fontColour);
                _dateRangeDate.Add(day.ToString("D2"));
                _calendarDates.Add(date);

                if (i == 35 && day < 10) visible = false;
                _dateRangeVisible.Add(visible);
                
                SetCellBackgroundImage(date);

            }
            #endregion

            #region get jobs for date range
            var visibleDate = GetVisibleDate();
            var jobs = GetJobsForDateRange(startDate, visibleDate);

            foreach (var job in jobs)
            {
             //   if(job.Date<visibleDate)
                var fontColour = Color.DarkSlateGray;
                if (job.Date.Month < _currentDate.Month || job.Date.Month > _currentDate.Month) fontColour = Color.Gray;

                job.TextColor = fontColour;
                _jobList.Add(job);
            }
            #endregion

            CalendarDates = _calendarDates;
            DateRangeDate = _dateRangeDate;
            DateRangeJobType = _dateRangeJobType;
            DateRangeTextColour = _dateRangeTextColour;
            DateRangeVisible = _dateRangeVisible;
            JobList = _jobList;
        }

        private DateTime GetVisibleDate()
        {
            for (int i = 41; i >= 0; i--)
            {
                if (_dateRangeVisible[i])
                {
                    return _calendarDates[i];
                }
            }
            return _calendarDates.Last() ;
        }

        private void SetCellBackgroundImage(DateTime date)
        {
            var startDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            var endDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

            var jobs = GetJobsForDateRange(startDate, endDate);

            var types = new string[3];

            foreach (var job in jobs)
            {
                switch (job.Type)
                {
                    case "Cultivate":
                        types[0] = "c";
                        break;
                    case "General":
                        types[1] = "g";
                        break;
                    case "Preparation":
                        types[2] = "p";
                        break;
                }
            }
            var image = string.Format("tt{0}{1}{2}.png", types[0], types[1], types[2]);
            if (image.Length == 6)
            {
                _dateRangeJobType.Add("ttblank.png");
            }
            else
            {
                _dateRangeJobType.Add(image);
            }

        }
        private List<Job> GetJobsForDateRange(DateTime startDate, DateTime endDate)
        {
            return new List<Job>(
                _realmInstance.All<Job>()
                .Where(j => j.Date >= startDate && j.Date <= endDate)
                .OrderBy(j => j.Date)
                .ThenBy(j => j.Type).ToList());

        }

        private void SetPropertiesCollections()
        {
            for (int i = 0; i < 42; i++)
            {
                _dateRangeDate.Add(string.Empty);
                _dateRangeJobType.Add(string.Empty);
                _dateRangeTextColour.Add(Color.White);
                _calendarDates.Add(new DateTime());
                _dateRangeVisible.Add(false);
            }
        }
        #endregion
    }
}
