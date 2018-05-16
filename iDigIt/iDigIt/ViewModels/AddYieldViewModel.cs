using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using iDigIt.Models;
using Xamarin.Forms;

namespace iDigIt.ViewModels
{
    public class AddYieldViewModel : BaseModel
    {
        public AddYieldViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            Title = "Add Yield";

            Plants = GetPlantNameVarieties();
            Years = GetYears();
        }

        #region Properties
        private Yield _yield = new Yield();
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
        public Command AddYieldCommand // for ADD
        {
            get
            {
                return new Command(async () =>
                {
                    if (!string.IsNullOrEmpty(_yield.Crop))
                    {
                        _yield.YieldId = string.Format("{0}{1}",
                        _yield.Plant,
                        _yield.Year).ToLower().Replace(" ", "");

                        _realmInstance.Write(() =>
                        {
                            _realmInstance.Add(_yield, true); // Add the whole set of details
                        });

                        await Navigation.PopAsync();
                    }
                });
            }
        }
        #endregion
    }
}
