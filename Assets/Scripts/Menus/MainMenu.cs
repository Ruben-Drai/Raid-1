using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_newGameButton;
    [SerializeField] private string m_newGameScene;


    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null
           && PlayerController.instance.Controller.currentControlScheme == "Gamepad")
            EventSystem.current.SetSelectedGameObject(FindObjectOfType<Button>().gameObject);

    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(m_newGameScene);
        SaveNLoad.instance.StartCoroutine(SaveNLoad.instance.ResetRoutine());
        Time.timeScale = 1;
        SoundManager.instance.Click.PlayOneShot(SoundManager.instance.Click.clip);
    }

    public void ContinueGame()
    {
        //TODO: Check for save, if no save, don't make button available to click.
        SceneManager.LoadSceneAsync(PlayerPrefs.GetString("SceneName"));
        SaveNLoad.instance.StartCoroutine(SaveNLoad.instance.LoadRoutine(true));
        Time.timeScale = 1;
        SoundManager.instance.Click.PlayOneShot(SoundManager.instance.Click.clip);

    }

    public void QuitGame()
    {
        SoundManager.instance.Back.PlayOneShot(SoundManager.instance.Back.clip);

        Application.Quit();
    }

    /* Set the first button to be selected when a gamepad is used */
}
