using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iDigIt.Helpers
{
    public static class Camera
    {
        public static async Task<MediaFile> TappedAsync()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No camera", ":( No camera avaialble.", "OK");
                return null;
            }

           var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
               PhotoSize = PhotoSize.MaxWidthHeight,
               MaxWidthHeight = 1200,
               RotateImage = true,
               SaveToAlbum = true,
               //Directory = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath,
            Name = string.Format("digit_{0}.jpg", DateTime.Now.ToString("yyyyMMddmmss"))               
            });

            return file;           
        }

        public static async Task<MediaFile> LibraryTappedAsync()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No upload", ":( Picking a photo is not supported.", "OK");
                return null;
            }

            return await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 1200,
                RotateImage = true
            });       
        }
    }
}
