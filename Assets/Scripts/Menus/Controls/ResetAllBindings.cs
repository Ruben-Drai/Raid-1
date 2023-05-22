using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetAllBindings : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    public void ResetBindings()
    {
        foreach (var action in inputActions.actionMaps)
        {
            action.RemoveAllBindingOverrides();
        }
        PlayerPrefs.DeleteKey("Bindings");
    }
}
