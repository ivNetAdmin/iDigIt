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
	public partial class ReviewContentYieldJobsPage : ContentPage
	{
        string _yieldId;
        public ReviewContentYieldJobsPage(string yieldId)
        {
            InitializeComponent();
            _yieldId = yieldId;
        }

        #region Page Events
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new ReviewContentYieldJobsViewModel(Navigation, _yieldId);
            RelatedJobList.SelectedItem = null;
            MoreYieldsList.SelectedItem = null;            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((ReviewContentYieldJobsViewModel)BindingContext).DisposeRealm();
        }
        #endregion
    }
}