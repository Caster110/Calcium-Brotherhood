using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CombatGameplayUI : MonoBehaviour, IInputHandler
{
    [SerializeField] private IInputHandler currentInputHandler;
    [SerializeField] private ConfirmationUI confirmationUI;
    [SerializeField] private InGameMenuUI inGameMenuUI;
    [SerializeField] private CombatInputHandler combatGridInputHandler;
    public Character character;
    public Ability ability;
    public bool IsActive => ability is not MoveAbility;
    public void Init(GridInputHandler gridInputHandler, Action exitAction)
    {
        currentInputHandler = combatGridInputHandler;
        inGameMenuUI.Init(exitAction);

        InputHandler.Instance.SystemKeyPressed += HandleSystemInput;
    }
    private void HandleSystemInput(object sender, KeyCode e)
    {
        if (e == KeyCode.Escape)
        {
            if (currentInputHandler.IsActive)
            {
                currentInputHandler.TryDisable();
            }
            else
            {
                //disable game input mb
                currentInputHandler = inGameMenuUI;
                inGameMenuUI.gameObject.SetActive(true);
            }
        }
    }
    public bool TryDisable()
    {
        currentInputHandler = inGameMenuUI;
        return false;
    }
    private void OnDestroy()
    {
        InputHandler.Instance.SystemKeyPressed -= HandleSystemInput;
    }
}
