using System;
using UnityEngine;
public class InputHandler : MonoBehaviour, IInputHandler
{
    public static InputHandler Instance {  get; private set; }

    public bool IsActive => Instance;

    public event EventHandler<KeyCode> SystemKeyPressed;
    public void Init()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SystemKeyPressed?.Invoke(this, KeyCode.Escape);
        }
        else if (Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            SystemKeyPressed?.Invoke(this, KeyCode.KeypadEnter);
        }
    }
    public void Toggle()
    {

    }

    public bool TryDisable()
    {
        throw new NotImplementedException();
    }
}
