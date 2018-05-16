using System;
using System.Collections.Generic;
using System.Text;
using iDigIt.Models;
using Xamarin.Forms;

namespace iDigIt.ViewModels
{
    public class AddFrostViewModel : BaseModel
    {
        #region Constructors
        public AddFrostViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            Title = "Add Frost";

            Frost.Date = DateTimeOffset.Now;
        }
        #endregion

        #region Properties
        private Frost _frost = new Frost();
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
        public Command AddFrostCommand // for ADD
        {
            get
            {
                return new Command(async () => {
                    var frostId = string.Format("{0}{1}{2}", _frost.Date.Year, _frost.Date.Month, _frost.Date.Day);
                    if (!string.IsNullOrEmpty(frostId))
                    {
                        _frost.FrostId = frostId.ToLower().Replace(" ", "");
                        _frost.Year = _frost.Date.Year;
                        _frost.Month = _frost.Date.Month;
                        _frost.Day = _frost.Date.Day;

                        _realmInstance.Write(() =>
                        {
                            _realmInstance.Add(_frost, true); // Add the whole set of details
                        });
                        await Navigation.PopAsync();
                    }
                });
            }
        }
        #endregion
    }
}
