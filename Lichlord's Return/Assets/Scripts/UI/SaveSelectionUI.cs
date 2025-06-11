using System;
using UnityEngine;
using UnityEngine.UI;
public class SaveSelectionUI : MonoBehaviour, IInputHandler
{
    [SerializeField] private Button closeButton;

    public bool IsActive => throw new NotImplementedException();

    public void Init()
    {
        //gamesAvailableInfo
    }
    private void OnEnable()
    {
        InputHandler.Instance.SystemKeyPressed += InputHandler_SystemKeyPressed;
        closeButton.onClick.AddListener(() => { gameObject.SetActive(false); });
    }

    private void InputHandler_SystemKeyPressed(object sender, KeyCode e)
    {
        if (e == KeyCode.Escape)
        {
            gameObject.SetActive(false);
        }
        else if (e == KeyCode.KeypadEnter)
        {

        }    
    }
    public void OnDisable()
    {
        InputHandler.Instance.SystemKeyPressed -= InputHandler_SystemKeyPressed;
        closeButton.onClick.RemoveAllListeners();
    }

    public void Toggle()
    {
        throw new NotImplementedException();
    }

    public bool TryDisable()
    {
        throw new NotImplementedException();
    }
}
