using System;
using System.Collections.Generic;
using System.Text;
using iDigIt.Models;
using Xamarin.Forms;

namespace iDigIt.ViewModels
{
    class EditFrostViewModel : BaseModel
    {
        private Frost _frost;

        #region Constructors
        public EditFrostViewModel(INavigation navigation, string frostId)
        {
            this.Navigation = navigation;
            Title = "Update Frost";

            _frost = _realmInstance.Find<Frost>(frostId);
        }
        #endregion

        #region Properties
        public Frost Frost
        {
            get { return _frost; }
            set
            {
                _frost = value;
                OnPropertyChanged(); // Add the OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public Command UpdateFrostCommand // for ADD
        {
            get
            {
                return new Command(async () =>
                {
                    _realmInstance.Write(() =>
                    {
                        _frost.Year = _frost.Date.Year;
                        _frost.Month = _frost.Date.Month;
                        _frost.Day = _frost.Date.Day;
                        _realmInstance.Add(_frost, true); // Add the whole set of details
                        });
                    await Navigation.PopAsync();
                });
            }
        }

        public Command DeleteFrostCommand // for DELETE
        {
            get
            {
                return new Command(async () => {
                    _realmInstance.Write(() =>
                    {
                        _realmInstance.Remove(_frost);
                    });

                    await Navigation.PopAsync();
                });
            }
        }
        #endregion
    }
}
