using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace CookItApp
{
    public class Ubicaciones : Map
    {
        public List<Position> RouteCoordinates { get; set; }

        public Ubicaciones()
        {
            RouteCoordinates = new List<Position>();
        }
    }
}
