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
    }

    public void ContinueGame()
    {
        SceneManager.LoadSceneAsync("DevRoom");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game button clicked");
        Application.Quit();
    }

    public void GoToSettings()
    {
        Debug.Log("Settings button clicked");
        //SceneManager.LoadScene("settingscene");
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
