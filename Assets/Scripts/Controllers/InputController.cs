using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAsset;
    [SerializeField] private InputActionReference holdAction;
    [SerializeField] private List<InputTypeMap> inputMaps;

    public event Action HoldStarted;
    public event Action HoldEnded;
    
    public void Initialize()
    {
        Debug.Log("[InputController] Initialize");
        
        holdAction.action.started += HoldStart;
        holdAction.action.canceled += HoldEnd;
    }
    
    public void Deinitialize()
    {
        Debug.Log("[InputController] Deinitialize");
        
        holdAction.action.started -= HoldStart;
        holdAction.action.canceled -= HoldEnd;
    }
    
    // Turned out to be not needed
    public void SwitchMap(InputMapType type)
    {
        var mapName = inputMaps.FirstOrDefault(m => m.type == type).name;
        inputAsset.FindActionMap(mapName).Enable();
    }

    private void HoldStart(InputAction.CallbackContext _)
    {
        HoldStarted?.Invoke();
    }

    private void HoldEnd(InputAction.CallbackContext _)
    {
        HoldEnded?.Invoke();
    }
}

[Serializable]
public struct InputTypeMap
{
    public InputMapType type;
    public string name;
}

public enum InputMapType
{
    Gameplay,
    UI,
}