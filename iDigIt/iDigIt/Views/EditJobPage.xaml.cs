using System;
using iDigIt.Helpers;
using iDigIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iDigIt.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditJobPage : ContentPage
	{
        string _jobId;
        public EditJobPage (string jobId)
		{
			InitializeComponent ();
            _jobId = jobId;

        }

        #region Page Events
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(BindingContext==null) BindingContext = new EditJobViewModel(Navigation, _jobId);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((EditJobViewModel)BindingContext).DisposeRealm();
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