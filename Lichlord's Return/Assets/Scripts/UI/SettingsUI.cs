using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsUI : MonoBehaviour, IInputHandler
{
    [SerializeField] private IInputHandler currentInputHandler;
    [SerializeField] private Button setDefaultButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button saveButton;
    private ConfirmationUI confirmationUI;
    private bool settingsChanged;
    public bool IsActive => enabled;

    public void Init()
    {
        //settings data
    }
    public void OnEnable()
    {
        closeButton.onClick.AddListener(() => TryDisable());
        saveButton.onClick.AddListener(() => SaveChanges()); //+show confirmation
        setDefaultButton.onClick.AddListener(() => SetDefaultSettings());
    }
    private void SetDefaultSettings()
    {
        confirmationUI.AskConfirmation(SaveChanges);
    }

    private void SaveChanges()
    {
        //saving work
        settingsChanged = false;
    }
    public bool TryDisable()
    {
        if (!settingsChanged)
        {
            gameObject.SetActive(false);
            return true;
        }
        else
        {
            if (currentInputHandler == confirmationUI)
            {
                currentInputHandler.TryDisable();
            }
            else
            {
                confirmationUI.AskConfirmation(SaveChanges);
                currentInputHandler = confirmationUI;
            }
            return false;
        }
    }
    public void OnDisable()
    {
        closeButton.onClick.RemoveAllListeners();
        saveButton.onClick.RemoveAllListeners();
        setDefaultButton.onClick.RemoveAllListeners();
    }
}
