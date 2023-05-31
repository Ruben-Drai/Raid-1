using UnityEngine;
using UnityEngine.InputSystem;

public class ResetAllBindings : MonoBehaviour
{
    private InputActionAsset inputActions;
    private void Start()
    {
        inputActions = PlayerController.instance.GetComponent<PlayerInput>().actions;
    }
    public void ResetBindings()
    {

        foreach (var action in inputActions.actionMaps)
            action.RemoveAllBindingOverrides();

        PlayerPrefs.DeleteKey("rebinds");
    }
}
