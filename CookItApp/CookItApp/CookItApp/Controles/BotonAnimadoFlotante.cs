using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.Controles
{
    public class BotonAnimadoFlotante : Button
    {
        public static BindableProperty ButtonColorProperty =
            BindableProperty.Create(nameof(ButtonColor), typeof(Color), typeof(BotonAnimadoFlotante), Color.Accent);

        public static BindableProperty RippleColorProperty =
            BindableProperty.Create(nameof(RippleColor), typeof(Color), typeof(BotonAnimadoFlotante), Color.White);

        public Color ButtonColor
        {
            get => (Color)GetValue(ButtonColorProperty);
            set => SetValue(ButtonColorProperty, value);
        }

        public Color RippleColor
        {
            get => (Color)GetValue(RippleColorProperty);
            set => SetValue(RippleColorProperty, value);
        }
    }
}
