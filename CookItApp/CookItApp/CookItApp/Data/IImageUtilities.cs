using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookItApp
{
    public interface IImageUtilities
    {
        Task<Stream> GetImageStreamAsync();
    }
}
