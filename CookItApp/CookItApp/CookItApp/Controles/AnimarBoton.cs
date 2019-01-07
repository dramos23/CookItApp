using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.Controles
{
    public class AnimarBoton : FlatButton
    {
        public AnimarBoton()
        {
            const int animationTime = 75;
            Clicked += async (sender, e) =>
            {
                try
                {
                    var btn = (AnimarBoton)sender;
                    await btn.ScaleTo(1.2, animationTime);
                    await btn.ScaleTo(1, animationTime);
                    //await Task.Delay(400);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }
            };
        }
    }
}
