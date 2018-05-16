using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iDigIt.Helpers;
using iDigIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iDigIt.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddFrostPage : ContentPage
	{
		public AddFrostPage ()
		{
			InitializeComponent ();
		}

        #region Page Events
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new AddFrostViewModel(Navigation);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((AddFrostViewModel)BindingContext).DisposeRealm();
        }
        #endregion
        #region Commands
        private async void OnCameraButtonTapped(object sender, EventArgs e)
        {
            ImagePath.Text = string.Empty;
            Image.Source = string.Empty;

            var file = await Camera.TappedAsync();
            if (file != null)
            {
                ImagePath.Text = file.Path;
                Image = new Image { Source = ImageSource.FromStream(() => file.GetStream()) };
            }
        }
        private async void OnLibraryButtonTapped(object sender, EventArgs e)
        {
            ImagePath.Text = string.Empty;
            Image.Source = string.Empty;

            var file = await Camera.LibraryTappedAsync();
            if (file != null)
            {
                ImagePath.Text = file.Path;
                Image = new Image { Source = ImageSource.FromStream(() => file.GetStream()) };
            }
        }
        private void OnRemoveImageButtonTapped(object sender, EventArgs e)
        {
            ImagePath.Text = string.Empty;
            Image.Source = string.Empty;
        }
        #endregion
    }
}