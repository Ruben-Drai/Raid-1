using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
        Time.timeScale = 1;

    }

    public void ContinueGame()
    {
        //TODO: Check for save, if no save, don't make button available to click.
        SceneManager.LoadSceneAsync("DevRoom");
        SaveNLoad.instance.StartCoroutine(SaveNLoad.instance.LoadRoutine());
        Time.timeScale = 1;


    }

    public void QuitGame()
    {
        Debug.Log("Quit Game button clicked");
        Application.Quit();
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
