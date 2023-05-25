using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject Menu, Settings, Controls, MainMenuButtons;

    [SerializeField]
    private List<GameObject> menuStack;
    
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null
            && PlayerController.instance.Controller.currentControlScheme == "Gamepad")
        {
            EventSystem.current.SetSelectedGameObject(FindFirstObjectByType<Button>().gameObject);
        }
    }
    public void ShowMenu()
    {
        DisableMenus();
        Menu.SetActive(true);
        menuStack.Add(Menu);
    }
    public void ShowControls()
    {
        DisableMenus();
        Controls.SetActive(true);
        menuStack.Add(Controls);
    }
    public void ShowSettings()
    {
        DisableMenus();
        Settings.SetActive(!Settings.activeSelf);
        menuStack.Add(Settings);
    }
    public void Escape()
    {
        if ((SceneManager.GetActiveScene().name == "MainMenu" &&menuStack.Count>=2)
            || (SceneManager.GetActiveScene().name != "MainMenu" && menuStack.Count >= 1))
        {
            Back();
        }
        else if(SceneManager.GetActiveScene().name == "DevRoom")
        {
            Time.timeScale = 0;
            ShowMenu();
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    
    public void Back()
    {
        DisableMenus();
        menuStack.RemoveAt(menuStack.Count - 1);
        if (menuStack.Count > 0)
        {
            menuStack[menuStack.Count - 1].SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    private void DisableMenus()
    {
        if(Menu!=null)
            Menu.SetActive(false);
        
        if(MainMenuButtons!=null)
            MainMenuButtons.SetActive(false);

        Settings.SetActive(false);
        Controls.SetActive(false);

    }
}
