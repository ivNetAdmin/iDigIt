using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using iDigIt.Models;
using Xamarin.Forms;

namespace iDigIt.ViewModels
{
    public class AddJobViewModel : BaseModel
    {

        #region Constructors
        public AddJobViewModel(INavigation navigation, DateTimeOffset jobDate)
        {
            this.Navigation = navigation;
            Title = "Add Job";            

            PlantList = GetPlantNameVarieties();

            if (jobDate.Date > DateTime.MinValue)
            {
                Job.Date = jobDate;
            }
            else
            {
                Job.Date = DateTimeOffset.Now;
            }
        }        
        #endregion

        #region Properties
        private Job _job = new Job();
        public Job Job
        {
            get { return _job; }
            set
            {
                _job = value;
                OnPropertyChanged(); // Add the OnPropertyChanged();
            }
        }
       
        public ObservableCollection<string> TypeList
        {
            get { return _typeList; }
            set
            {
                _typeList = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
            }
        }

        private ObservableCollection<string> _plantList;
        public ObservableCollection<string> PlantList
        {
            get { return _plantList; }
            set
            {
                _plantList = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
            }
        }
        #endregion

        #region Commands
        public Command AddJobCommand // for ADD
        {
            get
            {
                return new Command(async () =>
                {
                    if (!string.IsNullOrEmpty(_job.Name))
                    {
                        if (string.IsNullOrEmpty(_job.Type)) _job.Type = "General";
                        _job.JobId = string.Format("{0}{1}{2}{3}",
                            _job.Name,
                            _job.Plant,
                            _job.Type,
                            _job.Date.ToString("yyyyMMdd")).ToLower().Replace(" ", "");

                        _realmInstance.Write(() =>
                        {
                            _realmInstance.Add(_job, true); // Add the whole set of details
                        });

                        await Navigation.PopAsync();
                    }
                });
            }
        }
        #endregion
    }
}
