using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveNLoad : MonoBehaviour
{
    private GameObject Interactibles;
    public static SaveNLoad instance;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    //All data is saved into playerprefs, I don't know if this is efficient since playerprefs are inherently Register Keys
    //It might be better to save as a file but I don't have the time for that
    //This basically saves player pos as well as all interactibles' activation status, and position if they can be pushed
    public void Save()
    {
        PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name);

        Interactibles = GameObject.Find("Props");
        var c = Interactibles.GetComponentsInChildren<Interactible>();
        for (int i = 0; i < c.Length; i++)
        {
            PlayerPrefs.SetInt("Child" + i, c[i].IsActivated ? 1 : 0);
            if (c[i].GetComponent<BigBox>() != null)
            {
                PlayerPrefs.SetFloat("ChildPosX" + i, c[i].transform.position.x);
                PlayerPrefs.SetFloat("ChildPosY" + i, c[i].transform.position.y);
            }
        }
        PlayerPrefs.SetFloat("PlayerPosX", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", PlayerController.instance.transform.position.y);

        foreach (var v in PlayerController.UnlockedUpgrades)
        {
            PlayerPrefs.SetInt(v.Key, v.Value ? 1 : 0);
        }
    }
    public void Load(bool FirstScene, bool NewScene)
    {
        Interactibles = GameObject.Find("Props");

        if (!NewScene)
        {
            var c = Interactibles.GetComponentsInChildren<Interactible>();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i].GetComponent<BigBox>() != null)
                {
                    c[i].transform.position = new(
                         PlayerPrefs.GetFloat("ChildPosX" + i),
                         PlayerPrefs.GetFloat("ChildPosY" + i),
                         0);
                }
                else if (PlayerPrefs.GetInt("Child" + i) == 1)
                {
                    c[i].Interact();
                }
            }
        }
            

        if (FirstScene)
            PlayerController.instance.transform.position = new Vector3(
                PlayerPrefs.GetFloat("PlayerPosX"),
                PlayerPrefs.GetFloat("PlayerPosY"),
                0);


        foreach (var v in PlayerController.UnlockedUpgrades.ToArray())
        {
            PlayerController.UnlockedUpgrades[v.Key] = PlayerPrefs.GetInt(v.Key) == 1;
        }
    }
    public void ResetSave()
    {
        Interactibles = GameObject.Find("Props");
        PlayerPrefs.DeleteKey("SceneName");
        var c = Interactibles.GetComponentsInChildren<Interactible>();
        for (int i = 0; i < c.Length; i++) 
        { 
            PlayerPrefs.DeleteKey("Child" + i);
            if (c[i].GetComponent<BigBox>() != null)
            {
                PlayerPrefs.DeleteKey("ChildPosX" + i);
                PlayerPrefs.DeleteKey("ChildPosY" + i);
            }
        }
        PlayerPrefs.DeleteKey("PlayerPosX");
        PlayerPrefs.DeleteKey("PlayerPosY");

        foreach (var v in PlayerController.UnlockedUpgrades.ToArray())
        {
            PlayerPrefs.DeleteKey(v.Key);
        }
    }
    public IEnumerator SaveRoutine(bool quit = false)
    {
        while (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "GameOver")
        {
            
            yield return null;
        }
        Save();
        if (quit) Application.Quit();
    }
    public IEnumerator LoadRoutine(bool firstScene, bool NewScene = false)
    {
        //waits for objects with saved values to be instantiated
        while (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "GameOver")
        {
            yield return null;
        }
        Load(firstScene,NewScene);
    }
    public IEnumerator ResetRoutine()
    {
        //waits for objects with saved values to be instantiated
        while (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "GameOver")
        {
            yield return null;
        }

        ResetSave();
    }
    
}