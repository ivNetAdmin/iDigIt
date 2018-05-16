using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iDigIt.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iDigIt.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReviewPage : CarouselPage
    {
        public ReviewPage()
        {

            InitializeComponent();
            this.Children.Add(new ReviewContentYieldPage(new ReviewContentYieldViewModel(Navigation)));            
            this.Children.Add(new ReviewContentJobPage(new ReviewContentJobViewModel()));
            this.Children.Add(new ReviewContentFrostPage(new ReviewContentFrostViewModel()));
            this.Children.Add(new ReviewContentSearchPage(new ReviewContentSearchViewModel()));

            BindingContext = new ReviewViewModel(Navigation);

            //var contentPages = new List<ContentPage>
            //{
            //    new ReviewContentYieldPage(),
            //    new ReviewContentFrostPage(),
            //    new ReviewContentJobPage(),
            //    new ReviewContentPlantPage()
            //};

            //this.Children = contentPages;
        }

        #region Page Events
        protected override void OnAppearing()
        {
            base.OnAppearing();
           // BindingContext = new ReviewViewModel(Navigation);
           // this.SelectedItem = ((ObservableCollection<ContentPage>)ItemsSource)[0];

            //this.SelectedItem = ((NamedColor[])ItemsSource)[4];  // navigates to Blue when displayed
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((ReviewViewModel)BindingContext).DisposeRealm();
        }
        #endregion
    }
}