using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_newGameButton;


    private void Update()
    {
        SelectButtons();
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("DevRoom");
        SaveNLoad.instance.StartCoroutine(SaveNLoad.instance.ResetRoutine());

    }

    public void ContinueGame()
    {
        //TODO: Check for save, if no save, don't make button available to click.
        SceneManager.LoadSceneAsync("DevRoom");
        SaveNLoad.instance.StartCoroutine(SaveNLoad.instance.LoadRoutine());

    }

    public void QuitGame()
    {
        Debug.Log("Quit Game button clicked");
        Application.Quit();
    }

    public void GoToSettings()
    {
        SceneManager.LoadSceneAsync("Settings");
    }

    /* Set the first button to be selected when a gamepad is used */
    void SelectButtons()
    {
        if (EventSystem.current.currentSelectedGameObject== null 
            && PlayerController.instance.Controller.currentControlScheme == "Gamepad")
        {
            EventSystem.current.SetSelectedGameObject(m_newGameButton);
        }
    }
}
