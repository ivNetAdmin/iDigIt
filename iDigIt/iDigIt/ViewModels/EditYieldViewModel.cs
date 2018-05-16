using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using iDigIt.Models;
using Xamarin.Forms;

namespace iDigIt.ViewModels
{
    public class EditYieldViewModel : BaseModel
    {
        private Yield _yield;

        #region Constructors
        public EditYieldViewModel(INavigation navigation, string yieldId)
        {
            this.Navigation = navigation;
            Title = "Update Yield";

            Plants = GetPlantNameVarieties();
            Years = GetYears();

            _yield = _realmInstance.Find<Yield>(yieldId);
        }
        #endregion

        #region Properties
        public Yield Yield
        {
            get { return _yield; }
            set
            {
                _yield = value;
                OnPropertyChanged(); // Add the OnPropertyChanged();
            }
        }
        private ObservableCollection<string> _listOfPlants;
        public ObservableCollection<string> Plants
        {
            get { return _listOfPlants; }
            set
            {
                _listOfPlants = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
            }
        }
        private ObservableCollection<int> _listOfYears;
        public ObservableCollection<int> Years
        {
            get { return _listOfYears; }
            set
            {
                _listOfYears = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
            }
        }
        #endregion

        #region Commands
        public Command UpdateYieldCommand // for ADD
        {
            get
            {
                return new Command(async () => {
                    if (!string.IsNullOrEmpty(_yield.Crop))
                    {
                        _realmInstance.Write(() =>
                        {
                            _realmInstance.Add(_yield, update: true); // Add the whole set of details
                        });

                        await Navigation.PopAsync();
                    }
                });
            }
        }

        public Command DeleteYieldCommand // for DELETE
        {
            get
            {
                return new Command(async () => {
                    _realmInstance.Write(() =>
                    {
                        _realmInstance.Remove(_yield);
                    });

                    await Navigation.PopAsync();
                });
            }
        }
        #endregion
    }
}
