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
    public partial class ReviewContentSearchPage : ContentPage
    {
        public ReviewContentSearchPage(ReviewContentSearchViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
           
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((ReviewContentSearchViewModel)BindingContext).DisposeRealm();
        }
    }
}