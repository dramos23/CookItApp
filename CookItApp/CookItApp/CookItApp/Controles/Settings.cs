// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace CookItApp.Controles
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
{
    private static ISettings AppSettings
    {
        get
        {
            return CrossSettings.Current;
        }
    }

    #region Setting Constants

    private const string UltimoEmailSettingsKey = "ultimo_email_key"; 
    private const string UltimaPassSettingsKey  = "ultimo_pass_key";
    private static readonly string SettingsDefault = string.Empty;

        #endregion


        //public static string GeneralSettings
        //{
        //    get
        //    {
        //        return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
        //    }
        //    set
        //    {
        //        AppSettings.AddOrUpdateValue(SettingsKey, value);
        //    }
        //}

        public static string UltimoEmailUsado
        {
            get
            {
                return AppSettings.GetValueOrDefault(UltimoEmailSettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UltimoEmailSettingsKey, value);
            }
        }

        public static string UltimaPassUsada
        {
            get
            {
                return AppSettings.GetValueOrDefault(UltimaPassSettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UltimaPassSettingsKey, value);
            }
        }

    }
}