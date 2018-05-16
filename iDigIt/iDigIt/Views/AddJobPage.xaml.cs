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
	public partial class AddJobPage : ContentPage
	{
        private DateTimeOffset _jobDate;

        public AddJobPage ()
		{
			InitializeComponent ();
		}

        public AddJobPage(DateTimeOffset jobDate)
        {
            InitializeComponent();
            this._jobDate = jobDate;
        }

        #region Page Events
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(BindingContext==null) BindingContext = new AddJobViewModel(Navigation, _jobDate);
            TypeList.SelectedItem = "General";
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((AddJobViewModel)BindingContext).DisposeRealm();
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