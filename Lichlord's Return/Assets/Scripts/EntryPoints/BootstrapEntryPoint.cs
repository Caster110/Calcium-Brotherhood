using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class BootstrapEntryPoint : MonoBehaviour
{
    private async void Awake()
    {
        SettingsConfig settingsConfig = SaveSystem.LoadObject<SettingsConfig>("SettingsConfig");
        if (settingsConfig == null)
        {
            settingsConfig = new SettingsConfig();
        }

        Task localizationTask = LocalizationService.Init(settingsConfig.languageID);

        InputHandler.Instance.Init();

        await Task.WhenAll(localizationTask);


        Debug.Log("All services initialized. Granting player control.");
    }
}
