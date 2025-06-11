using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuUI : MonoBehaviour, IInputHandler
{
    [SerializeField] private IInputHandler currentInpuHandler;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private SettingsUI settingsUI;
    [SerializeField] private ConfirmationUI confirmationUI;
    [SerializeField] private SaveSelectionUI saveSelectionUI;
    private Action newGameAction;
    private Action continueGameAction;
    private Action exitAction;
    public bool IsActive => enabled;
    public void Init(Action newGameAction, Action continueGameAction, Action exitAction)
    {
        currentInpuHandler = this;
        this.newGameAction = newGameAction;
        this.continueGameAction = continueGameAction;
        this.exitAction = exitAction;

        settingsUI.Init();
        saveSelectionUI.Init();

        newGameButton.onClick.AddListener(() => { 
            Debug.LogWarning("there is no new game menu yet");
            newGameAction();
        });
        continueGameButton.onClick.AddListener(() => { continueGameAction(); });
        loadGameButton.onClick.AddListener(() => {
            currentInpuHandler = saveSelectionUI;
            saveSelectionUI.gameObject.SetActive(true);
        });
        settingsButton.onClick.AddListener(() => {
            currentInpuHandler = settingsUI;
            settingsUI.gameObject.SetActive(true);
        });
        exitButton.onClick.AddListener(() => {
            currentInpuHandler = confirmationUI;
            TryDisable(); }); //+ confirmation

        InputHandler.Instance.SystemKeyPressed += Input_SystemKeyPressed;
    }

    public bool TryDisable()
    {
        confirmationUI.AskConfirmation(exitAction);
        return false;
    }

    private void Input_SystemKeyPressed(object sender, KeyCode e)
    {
        if (e == KeyCode.Escape)
        {
            if(currentInpuHandler.TryDisable())
                currentInpuHandler = this;
        }
    }

    private void OnDestroy()
    {
        InputHandler.Instance.SystemKeyPressed -= Input_SystemKeyPressed;
    }
}
