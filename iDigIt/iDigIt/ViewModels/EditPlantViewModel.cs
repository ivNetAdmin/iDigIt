using Realms;
using System;
using System.Collections.Generic;
using System.Text;
using iDigIt.Models;
using Xamarin.Forms;

namespace iDigIt.ViewModels
{
    public class EditPlantViewModel : BaseModel
    {       
        private Plant _plant;

        #region Constructors
        public EditPlantViewModel(INavigation navigation, string plantId)
        {
            this.Navigation = navigation;
            Title = "Update Plant";

            _plant = _realmInstance.Find<Plant>(plantId);
        }
        #endregion

        #region Properties
        public Plant Plant
        {
            get { return _plant; }
            set
            {
                _plant = value;
                OnPropertyChanged(); // Add the OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public Command UpdatePlantCommand // for ADD
        {
            get
            {
                return new Command(async () => {
                    if (!string.IsNullOrEmpty(_plant.Name))
                    {
                        _realmInstance.Write(() =>
                        {
                            _realmInstance.Add(_plant, update: true); // Add the whole set of details
                    });

                        await Navigation.PopAsync();
                    }
                });
            }
        }

        public Command DeletePlantCommand // for DELETE
        {
            get
            {
                return new Command(async () => {                   
                    _realmInstance.Write(() =>
                    {
                        _realmInstance.Remove(_plant);
                    });

                    await Navigation.PopAsync();
                });
            }
        }
        #endregion
    }
}
