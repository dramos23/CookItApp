using System;

using Android.App;
using Android.Content;
using CookItApp.Models;
using CookItApp.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(FBLoginPage), typeof(CookItApp.Droid.FBLoginPageRenderer))]
namespace CookItApp.Droid
{
    public class FBLoginPageRenderer : PageRenderer
    {
        
        private FBLoginPage fBLoginPage;

        public FBLoginPageRenderer(Context context) : base(context)
        {
            Activity activity = Context as Activity;
            var auth = new OAuth2Authenticator(
                clientId: "276895199617406",
                scope: "",
                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));

            auth.Completed += async (sender, eventArgs) => {
                if (eventArgs.IsAuthenticated)
                {
                    var accessToken = eventArgs.Account.Properties["access_token"].ToString();
                    var expiresIn = Convert.ToDouble(eventArgs.Account.Properties["expires_in"]);
                    var expiryDate = DateTime.Now + TimeSpan.FromSeconds(expiresIn);

                    var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=email,first_name,last_name,picture"), null, eventArgs.Account);

                    var response = await request.GetResponseAsync();

                    var fbUser = await response.GetResponseTextAsync();

                    UsuarioFacebook fB = JsonConvert.DeserializeObject<UsuarioFacebook>(fbUser);
                    
                    fBLoginPage.Procesar(fB);
                    
                }
                else
                {
                    fBLoginPage.Procesar(null);
                }
            };
            activity.StartActivity(auth.GetUI(activity));
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            fBLoginPage = (FBLoginPage)e.NewElement;
        }
    }
}