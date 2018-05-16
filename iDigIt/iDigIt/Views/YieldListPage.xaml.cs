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
	public partial class YieldListPage : ContentPage
	{
		public YieldListPage ()
		{
			InitializeComponent ();
		}

        #region Page Events
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new YieldListViewModel(Navigation);
            YieldList.SelectedItem = null;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((YieldListViewModel)BindingContext).DisposeRealm();
        }
        #endregion
        #region Commands
        private async void OnImageButtonTapped(object sender, EventArgs e)
        {
            //var file = ((Image)sender).Source;
            await Navigation.PushAsync(new ViewImagePage((Image)sender));
        }
        #endregion
    }
}