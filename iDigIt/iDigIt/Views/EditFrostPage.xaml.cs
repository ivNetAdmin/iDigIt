using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using iDigIt.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using iDigIt.Helpers;
using System;

namespace iDigIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditFrostPage : ContentPage
    {
        string _frostId;
        public EditFrostPage(string frostId)
        {
            InitializeComponent();
            _frostId = frostId;

        }

        #region Page Events
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new EditFrostViewModel(Navigation, _frostId);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((EditFrostViewModel)BindingContext).DisposeRealm();
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
