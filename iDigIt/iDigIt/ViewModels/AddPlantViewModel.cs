using Plugin.Media;
using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using iDigIt.Models;
using Xamarin.Forms;

namespace iDigIt.ViewModels
{
    public class AddPlantViewModel : BaseModel
    {
        #region Constructors
        public AddPlantViewModel(INavigation navigation)
        {          
            this.Navigation = navigation;
            Title = "Add Plant";
        }
        #endregion

        #region Properties
        private Plant _plant = new Plant();
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
        public Command AddPlantCommand // for ADD
        {
            get
            {                
                return new Command(async () => {
                    if (!string.IsNullOrEmpty(_plant.Name))
                    {                        
                        if (string.IsNullOrEmpty(_plant.Variety)) _plant.Variety = "Common";
                        _plant.PlantId = string.Format("{0}{1}", _plant.Name, _plant.Variety).ToLower().Replace(" ", "");
                        _realmInstance.Write(() =>
                        {
                            _realmInstance.Add(_plant,true); // Add the whole set of details
                    });

                        await Navigation.PopAsync();
                    }
                });
            }
        }       
        #endregion
    }
}
