using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using iDigIt.Helpers;
using Xamarin.Forms;

namespace iDigIt.ViewModels
{
    public class ViewImageViewModel : BaseModel
    {
        //private string _source;

        public ViewImageViewModel(INavigation navigation, string path)
        {
            Navigation = navigation;
            Title = "Image Viewer";

            var absPath = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;

            var streamReader = new StreamReader(path);

            var bytes = default(byte[]);

            var memstream = new MemoryStream();
            streamReader.BaseStream.CopyTo(memstream);
            bytes = memstream.ToArray();

            var resizeBytes = ImageResizer.Resize(bytes, 0.75);

            Source = ImageSource.FromStream(() => new MemoryStream(bytes));
            Path = path;

            //_source = image.Source.ToString().Substring(6);
            //"/storage/emulated/0/Android/data/co.uk.ivNet.DiGiT/files/Pictures/temp/IMG-20180128-WA0000.jpg";
        }

        #region Properties      
        private ImageSource _source;
        public ImageSource Source { get; }
       
        public string Path { get; }
       
        public int Size { get; }

        #endregion
    }
}
