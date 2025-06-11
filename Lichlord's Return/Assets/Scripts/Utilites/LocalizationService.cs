using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;
public class LocalizationService
{
    public static async Task Init(int localeID)
    {
        await LocalizationSettings.InitializationOperation.Task;

        if (localeID >= 0 && localeID < LocalizationSettings.AvailableLocales.Locales.Count)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
            Debug.Log("Locale set successfully");
        }
        else
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
            Debug.Log("Locale set by default");
        }
    }
}
