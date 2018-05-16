using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;

namespace iDigIt.Droid
{
    [Activity(Label = "iDigIt", Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;

                RequestedOrientation = ScreenOrientation.Portrait;

                base.Window.RequestFeature(WindowFeatures.ActionBar);
                base.SetTheme(Resource.Style.MainTheme);

                base.OnCreate(bundle);

                global::Xamarin.Forms.Forms.Init(this, bundle);
                LoadApplication(new App());
            }
            catch (Exception ex)
            {
                var cakes = ex;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

