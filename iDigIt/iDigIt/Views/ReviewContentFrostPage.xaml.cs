using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iDigIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iDigIt.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReviewContentFrostPage : ContentPage
	{
        public ReviewContentFrostPage(ReviewContentFrostViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        #region Page Events
        protected override void OnAppearing()
        {
            base.OnAppearing();            
            YearList.SelectedItem = "All";
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((ReviewContentFrostViewModel)BindingContext).DisposeRealm();
        }
        #endregion
    }
}