using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using iDigIt.Models;
using Xamarin.Forms;

namespace iDigIt.ViewModels
{
    public class EditJobViewModel : BaseModel
    {
        private Job _job;

        #region Constructors
        public EditJobViewModel(INavigation navigation, string jobId)
        {
            this.Navigation = navigation;
            Title = "Update Job";

            _job = _realmInstance.Find<Job>(jobId);

            PlantList = GetPlantNameVarieties();
        }
        #endregion

        #region Properties
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
        public Command UpdateJobCommand // for ADD
        {
            get
            {
                return new Command(async () => {
                    if (!string.IsNullOrEmpty(_job.Name))
                    {
                        _realmInstance.Write(() =>
                        {
                            _realmInstance.Add(_job, update: true); // Add the whole set of details
                    });

                        await Navigation.PopAsync();
                    }
                });
            }
        }

        public Command DeleteJobCommand // for DELETE
        {
            get
            {
                return new Command(async () => {
                    _realmInstance.Write(() =>
                    {
                        _realmInstance.Remove(_job);
                    });

                    await Navigation.PopAsync();
                });
            }
        }
        #endregion
    }
}
