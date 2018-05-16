
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using iDigIt.Helpers;
using iDigIt.Models;
using iDigIt.Views;
using Xamarin.Forms;

namespace iDigIt.ViewModels
{
    public class BaseModel : INotifyPropertyChanged
    {
        internal Realm _realmInstance;
        internal ObservableCollection<string> _typeList 
            = new ObservableCollection<string> { "Cultivate", "General", "Preparation" };

        #region application colours                
        internal string _mainColor = "#4caf50";
        internal string _mainColorAccent = "#795548";
        
        internal string _frameBackground = "#edf8ed";
        internal string _frameBorder = "#604439"; // "#388e3c";

        internal string _calendarBackground = "#f5f1ef";
        internal string _calendarNavigationBackground = "#b38d80"; //"#e6e6e6";
        internal string _calendarNavigationButton = "#d9c6bf"; //"#f2f2f2";
        internal string _calendarNavigationButtonText = "#00000";
        internal string _calendarNavigationLabelText = "#ffffff";
        internal string _calendarDayOfWeekText = "#4caf50";
        internal string _calendarFontColor = "#212121";
        internal string _calendarLowlight = "#d9d9d9";
        internal string _calendarToday = "#ffa000";

        internal string _cultivateColor = "#c8e9ca";
        internal string _preparationColor = "#c8e9ca";
        internal string _generalColor = "#ffe6cc";

        internal string _footerBackgroundr = "#388e3c";

        internal string _frostGraph = "#8dbff2";

        public string MAIN_COLOR { get { return _mainColor; } }
        public string MAIN_COLOR_ACCENT { get { return _mainColorAccent; } }

        public string FRAME_BACKGROUND { get { return _frameBackground; } }       
        public string FRAME_BORDER { get { return _frameBackground; } }
        public string FOOTER_BACKGROUND { get { return _footerBackgroundr; } }
        public string FOOTER_FONT_COLOR { get { return _calendarLowlight; } }

        public string CALENDAR_BACKGROUND { get { return _calendarBackground; } }
        public string CALENDAR_NAVIGATION_BACKGROUND { get { return _calendarNavigationBackground; } }
        public string CALENDAR_NAVIGATION_BUTTON { get { return _calendarNavigationButton; } }
        public string CALENDAR_NAVIGATION_BUTTON_TEXT { get { return _calendarNavigationButtonText; } }
        public string CALENDAR_NAVIGATION_LABEL_TEXT { get { return _calendarNavigationLabelText; } }
        public string CALENDAR_DAY_OF_WEEK_TEXT { get { return _calendarDayOfWeekText; } }

        public string CULTIVATE_COLOR { get { return _cultivateColor; } }
        public string PREPARATION_COLOR { get { return _preparationColor; } }
        public string GENERAL_COLOR { get { return _generalColor; } }

        public string FROST_GRAPH { get { return _frostGraph; } }

        public string JOB_LIST_PLANT_NAME_TEXT { get { return _calendarLowlight; } }
        #endregion

        #region Constructors
        public BaseModel()
        {
             _realmInstance = Realm.GetInstance();
        }

        #endregion

        #region Properties
        public INavigation Navigation { get; set; }
        public string Title { get; set; }
        #endregion

        #region Navigation Icons
        public ImageSource MainPageIcon { get { return ImageSource.FromFile("digit.png"); } }
        public ImageSource PlantListIcon { get { return ImageSource.FromFile("plant.png"); } }
        public ImageSource JobListIcon { get { return ImageSource.FromFile("job.png"); } }
        public ImageSource FrostListIcon { get { return ImageSource.FromFile("frost.png"); } }
        public ImageSource YieldListIcon { get { return ImageSource.FromFile("crop.png"); } }
        public ImageSource ReviewIcon { get { return ImageSource.FromFile("review.png"); } }
        public ImageSource AddIcon { get { return ImageSource.FromFile("add.png"); } }
        public ImageSource CancelIcon { get { return ImageSource.FromFile("cancel.png"); } }
        public ImageSource DeleteIcon { get { return ImageSource.FromFile("delete.png"); } }
        public ImageSource SaveIcon { get { return ImageSource.FromFile("save.png"); } }
        public ImageSource SearchIcon { get { return ImageSource.FromFile("search.png"); } }
        public ImageSource CameraIcon { get { return ImageSource.FromFile("camera.png"); } }
        public ImageSource LibraryIcon { get { return ImageSource.FromFile("library.png"); } }
        #endregion

        #region Navigation Commands
        public ICommand NavigationClickedCommand {

            get
            {
                return new Command<WTGPage>(async (pageId) =>
                {
                    switch (pageId)
                    {
                        case WTGPage.PlantList:
                            await Navigation.PushAsync(new PlantListPage());
                            break;
                        case WTGPage.JobList:
                            await Navigation.PushAsync(new JobListPage());
                            break;                      
                        case WTGPage.YieldList:
                            await Navigation.PushAsync(new YieldListPage());
                            break;
                        case WTGPage.FrostList:
                            await Navigation.PushAsync(new FrostListPage());
                            break;
                        case WTGPage.Review:
                            await Navigation.PushAsync(new ReviewPage());
                            break;
                        case WTGPage.AddPlant:
                            await Navigation.PushAsync(new AddPlantPage());
                            break;
                        case WTGPage.AddJob:
                            await Navigation.PushAsync(new AddJobPage());
                            break;
                        case WTGPage.AddYield:
                            await Navigation.PushAsync(new AddYieldPage());
                            break;
                        case WTGPage.AddFrost:
                            await Navigation.PushAsync(new AddFrostPage());
                            break;
                        case WTGPage.Cancel:
                            await Navigation.PopAsync();
                            break;
                        default:
                            //App.Current.MainPage = new NavigationPage(new MainPage());
                            await Navigation.PopToRootAsync();
                            break;
                    }
                });
            }
        }        

        #endregion

        #region Page Events
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Shared Methods
        internal ObservableCollection<Job> GetJobs(string year = "All")
        {

            if (year == "All")
            {
                return new ObservableCollection<Job>(
                _realmInstance.All<Job>()
                .OrderBy(x => x.Date)
                .ThenBy(x => x.Plant)
                .ThenBy(x => x.Type).ToList());
            }
            else
            {
                var yearPair = year.Split('/');
                var firstYear = Convert.ToInt16(yearPair[0]);
                var secondYear = Convert.ToInt16(yearPair[1]);
                var startDate = new DateTimeOffset(new DateTime(firstYear, 8, 31));
                var endDate = new DateTimeOffset(new DateTime(secondYear, 8, 31));

                return new ObservableCollection<Job>(
                     _realmInstance.All<Job>()
                     .Where(x => (x.Date > startDate && x.Date <= endDate))
                     .OrderBy(x => x.Date).ToList());
            }

        }
        internal ObservableCollection<Plant> GetPlants()
        {
            return new ObservableCollection<Plant>(
                 _realmInstance.All<Plant>()
                 .OrderBy(x => x.Name)
                 .ThenBy(x => x.Variety).ToList());
        }
        internal ObservableCollection<string> GetPlantNames()
        {
            var plants = new List<Plant>(
               _realmInstance.All<Plant>()
               .OrderBy(x => x.Name).ThenBy(x => x.Variety).ToList());

            var plantnames = new List<string> { "All" };
            foreach (var plant in plants)
            {
                if (!plantnames.Contains(plant.Name))
                {
                    plantnames.Add(plant.Name);
                }
            }

            return new ObservableCollection<string>(plantnames);
        }
        internal ObservableCollection<string> GetPlantNameVarieties()
        {
            var plants = new List<Plant>(
               _realmInstance.All<Plant>()
               .OrderBy(x => x.Name).ThenBy(x => x.Variety).ToList());

            var plantnames = new List<string>();
            foreach (var plant in plants)
            {
                var plantVariety = string.Format("{0} * {1}", plant.Name, plant.Variety);
                if (!plantnames.Contains(plantVariety))
                {
                    plantnames.Add(plantVariety);
                }
            }

            return new ObservableCollection<string>(plantnames);
        }
        internal ObservableCollection<Yield> GetYields(string year, string orderType = "list")
        {
            if (year == "All")
            {
                if (orderType == "review")
                {
                    return new ObservableCollection<Yield>(
                     _realmInstance.All<Yield>()
                     .OrderByDescending(x => x.Crop)
                         .ThenBy(x => x.Plant)
                         .ThenBy(x => x.Year).ToList());
                }
                else {
                    return new ObservableCollection<Yield>(
                         _realmInstance.All<Yield>()
                         .OrderByDescending(x => x.Year)
                             .ThenBy(x => x.Plant).ToList());
                }
            }
            else
            {
                var intYear = Convert.ToInt16(year);
                return new ObservableCollection<Yield>(

                   _realmInstance.All<Yield>()
                   .Where(x => (x.Year == intYear))
                   .OrderByDescending(x => x.Crop)
                   .ThenBy(x => x.Plant)
                   .ThenBy(x => x.Year).ToList());
            }
        }
        internal ObservableCollection<int> GetYears()
        {
            var rtnList = new ObservableCollection<int>();
            var earliestJob = GetJobs().FirstOrDefault<Job>();

            if (earliestJob == null) return rtnList;

            for (var i = earliestJob.Date.Year; i < DateTime.Now.AddYears(1).Year; i++)
            {
                rtnList.Add(i);
            }

            return rtnList;
        }
       
        internal ObservableCollection<Frost> GetFrosts(string year = "All")
        {
            if (year == "All")
            {
                return new ObservableCollection<Frost>(
                     _realmInstance.All<Frost>()
                     .OrderBy(x => x.Year)
                     .ThenBy(x => x.Month)
                     .ThenBy(x => x.Day).ToList());
            }
            else
            {
                var yearPair = year.Split('/');
                var firstYear = Convert.ToInt16(yearPair[0]);
                var secondYear = Convert.ToInt16(yearPair[1]);
                return new ObservableCollection<Frost>(
                     _realmInstance.All<Frost>()
                     .Where(x => (x.Year == firstYear && x.Month > 8) || x.Year == secondYear && x.Month <= 8)
                     .OrderBy(x => x.Year)
                     .ThenBy(x => x.Month)
                     .ThenBy(x => x.Day).ToList());
            }
        }

        internal void AddJobToNextSeason(Job job)
        {
            var firstYear = Convert.ToInt16(job.Date.Year + 1);
            var secondYear = Convert.ToInt16(job.Date.Year + 2);

            _realmInstance.Write(() =>
            {
                var newJob = new Job
                {
                    Name = job.Name,
                    Plant = job.Plant,
                    Type = job.Type,
                    Date = NextSeasonDate.Date(job.Date)
                };

                newJob.JobId = string.Format("{0}{1}{2}{3}",
                newJob.Name,
                newJob.Plant,
                newJob.Type,
                newJob.Date.ToString("yyyyMMdd")).ToLower().Replace(" ", "");

                _realmInstance.Add(newJob, true); // Add the whole set of details
            });
        }

        #endregion
        internal void DisposeRealm()
        {
            //if (_realmInstance != null)
            //    _realmInstance.Dispose();
        }
    }
}
