using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iDigIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iDigIt.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewImagePage : ContentPage
	{
        private Image _image;
        public ViewImagePage (Image image)
		{
			InitializeComponent ();
            _image = image;
        }

        #region Page Events
        protected override void OnAppearing()
        {
            BindingContext = new ViewImageViewModel(Navigation, _image.Source.ToString().Substring(6));

   
            // var streamReader = new StreamReader(_image.Source.ToString().Substring(6));

            // var bytes = default(byte[]);

            //var memstream = new MemoryStream();
            //streamReader.BaseStream.CopyTo(memstream);
            //bytes = memstream.ToArray();

            //ViewImage.Source = ImageSource.FromStream(() => new MemoryStream(bytes));

            // ViewImage = new Image { Source = ImageSource.FromStream(() => new MemoryStream(bytes)) };


            //    ViewImage.Source = ImageSource.FromFile(_image.Source.ToString().Substring(6));
            //ViewImage = new Image
            //{
            //    Source = ImageSource.FromFile(_image.Source.ToString().Substring(6))
            //};
            //_image.Source.ToString().Substring(6)

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();            
        }
        #endregion
    }
}