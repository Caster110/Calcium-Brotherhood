using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConfirmationUI : MonoBehaviour, IInputHandler
{
    [SerializeField] private Button confirm;
    [SerializeField] private Button cancel;
    public bool IsActive => enabled;
    private Action action;
    public void OnEnable()
    {
        confirm.onClick.AddListener(() => { 
            action.Invoke();
            gameObject.SetActive(false);
        });
        cancel.onClick.AddListener(() => { gameObject.SetActive(false); });
    }
    public void AskConfirmation(Action action)
    {
        gameObject.SetActive(true);
        this.action = action;
    }
    public bool TryDisable()
    {
        gameObject.SetActive(false);
        return true;
    }
    public void OnDisable()
    {
        action = null;
        confirm.onClick.RemoveAllListeners();
        cancel.onClick.RemoveAllListeners();
    }
}
