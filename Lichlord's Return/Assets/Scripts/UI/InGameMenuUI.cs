using System;
using UnityEngine;
using UnityEngine.UI;
public class InGameMenuUI : MonoBehaviour, IInputHandler
{
    [SerializeField] private IInputHandler currentInputHandler;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private SettingsUI settingsUI;
    public bool IsActive => enabled;
    public void Init(Action exitAction)
    {
        gameObject.SetActive(false);
        settingsUI.Init();
        continueGameButton.onClick.AddListener(() => { gameObject.SetActive(false); });
        settingsButton.onClick.AddListener(() => {
            currentInputHandler = settingsUI;
            settingsUI.gameObject.SetActive(true); });
        exitButton.onClick.AddListener(() => { exitAction(); }); //+ confirmation
    }
    public bool TryDisable()
    {
        if (currentInputHandler == this)
        {
            gameObject.SetActive(false);
            return true;
        }
        else if (currentInputHandler.IsActive)
        {
            currentInputHandler.TryDisable();
            return false;
        }
        else
        {
            currentInputHandler = this;
            return TryDisable();
        }
    }
}
