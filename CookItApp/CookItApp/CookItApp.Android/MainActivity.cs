using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.AppCenter.Push;
using System.Linq;
using Xamarin.Forms;

namespace CookItApp.Droid
{
    [Activity(Label = "CookItApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            UserDialogs.Init(this);            

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
           

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Android.Content.Intent intent)
        {
            base.OnNewIntent(intent);
            Push.CheckLaunchedFromNotification(this, intent);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            string aver = "";
            // check if the current item id 
            // is equals to the back button id
            if (item.ItemId == 16908332)
            {
                try
                {
                    var currentpage = (ContentPage)Xamarin.Forms.Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
                    return base.OnOptionsItemSelected(item);
                }
                catch
                {
                    return base.OnOptionsItemSelected(item);
                }
            }
            else
            {
                // since its not the back button 
                //click, pass the event to the base
                return base.OnOptionsItemSelected(item);
            }
        }
    }
}