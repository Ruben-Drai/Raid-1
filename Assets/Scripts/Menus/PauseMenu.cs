using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject Menu, Settings, Controls, MainMenuButtons;

    [SerializeField]
    private List<GameObject> menuStack;

    void Update()
    {
        //selects a button if none are and current control scheme is set to gamepad
        if (EventSystem.current.currentSelectedGameObject == null
            && PlayerController.instance.Controller.currentControlScheme == "Gamepad" && menuStack.Count>0)
                EventSystem.current.SetSelectedGameObject(FindObjectOfType<Button>().gameObject);
    }
    public void ShowMenu()
    {
        DisableMenus();
        Menu.SetActive(true);
        menuStack.Add(Menu);
        SoundManager.instance.Click.PlayOneShot(SoundManager.instance.Click.clip);

    }
    public void ShowControls()
    {
        DisableMenus();
        Controls.SetActive(true);
        menuStack.Add(Controls);
        SoundManager.instance.Click.PlayOneShot(SoundManager.instance.Click.clip);
    }
    public void ShowSettings()
    {
        DisableMenus();
        Settings.SetActive(!Settings.activeSelf);
        menuStack.Add(Settings);
        SoundManager.instance.Click.PlayOneShot(SoundManager.instance.Click.clip);

    }
    //goes back to the previous menu if there is one, otherwise do nothing except if the player is not on the main menu
    //in which case the pause menu is shown
    public void Escape()
    {
        if ((SceneManager.GetActiveScene().name == "MainMenu" && menuStack.Count >= 2)
            || (SceneManager.GetActiveScene().name != "MainMenu" && menuStack.Count >= 1))
                Back();

        else if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "GameOver")
        {
            Time.timeScale = 0;
            ShowMenu();
        }
    }
    public void MainMenu()
    {
        SaveNLoad.instance.StartCoroutine(SaveNLoad.instance.SaveRoutine());
        SoundManager.instance.Click.PlayOneShot(SoundManager.instance.Click.clip);
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void SaveQuit()
    {
        SaveNLoad.instance.StartCoroutine(SaveNLoad.instance.SaveRoutine(true));
        SoundManager.instance.Click.PlayOneShot(SoundManager.instance.Click.clip);
    }
    public void Back()
    {
        if (PlayerController.instance.Controller.currentControlScheme != "Gamepad")
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        DisableMenus();
        menuStack.RemoveAt(menuStack.Count - 1);
        EventSystem.current.SetSelectedGameObject(null);

        SoundManager.instance.Back.PlayOneShot(SoundManager.instance.Back.clip);

        if (menuStack.Count > 0)
            menuStack[menuStack.Count - 1].SetActive(true);
        else
            Time.timeScale = 1;
    }
    private void DisableMenus()
    {
        if (Menu != null)
            Menu.SetActive(false);

        if (MainMenuButtons != null)
            MainMenuButtons.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);

        Settings.SetActive(false);
        Controls.SetActive(false);

    }
}
