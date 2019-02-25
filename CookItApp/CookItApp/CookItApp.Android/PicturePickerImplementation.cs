using System;
using System.IO;
using System.Threading.Tasks;

using Android.Content;

using Xamarin.Forms;

using DependencyServiceSample.Droid;
using CookItApp;
using CookItApp.Droid;
using Android.Graphics;
using Java.IO;
using Android.Provider;

[assembly: Dependency(typeof(PicturePickerImplementation))]

namespace DependencyServiceSample.Droid
{
    public class PicturePickerImplementation : IImageUtilities
    {
        public Task<Stream> GetImageStreamAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            // Get the MainActivity instance
            MainActivity activity = Android.App.Application.Context as MainActivity;
            

            // Start the picture-picker activity (resumes in MainActivity.cs)
            activity.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"), 
                MainActivity.PickImageId);

            // Save the TaskCompletionSource object as a MainActivity property
            activity.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();

            // Return Task object
            return activity.PickImageTaskCompletionSource.Task;
        }

        public Size GetSize(string fileName)
        {
            var options = new BitmapFactory.Options
            {
                InJustDecodeBounds = true
            };

            fileName = fileName.Replace('-', '_').Replace(".png", "").Replace(".jpg", "");
            var resId = Android.App.Application.Context.Resources.GetIdentifier(fileName, "drawable", Android.App.Application.Context.PackageName);
            BitmapFactory.DecodeResource(Android.App.Application.Context.Resources, resId, options);

            return new Size((double)options.OutWidth, (double)options.OutHeight);
        }

    }
}