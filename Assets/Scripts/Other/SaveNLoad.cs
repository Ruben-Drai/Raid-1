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
    void Start()
    {
    }

    //All data is saved into playerprefs, I don't know if this is efficient since playerprefs are inherently Register Keys
    //It might be better to save as a file but I don't have the time for that
    //This basically saves player pos as well as all interactibles' activation status, and position if they can be pushed
    public void Save()
    {
        Interactibles = GameObject.Find("Interactibles");

        for (int i = 0; i < Interactibles.transform.childCount; i++)
        {
            PlayerPrefs.SetInt("Child" + i, Interactibles.transform.GetChild(i).GetComponent<Interactible>().IsActivated ? 1 : 0);
            if (Interactibles.transform.GetChild(i).GetComponent<BigBox>() != null)
            {
                PlayerPrefs.SetFloat("ChildPosX" + i, Interactibles.transform.GetChild(i).transform.position.x);
                PlayerPrefs.SetFloat("ChildPosY" + i, Interactibles.transform.GetChild(i).transform.position.y);
            }
        }
        PlayerPrefs.SetFloat("PlayerPosX", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", PlayerController.instance.transform.position.y);

        foreach (var v in PlayerController.instance.UnlockedUpgrades)
        {
            PlayerPrefs.SetInt(v.Key, v.Value ? 1 : 0);
        }
    }

    public void ResetSave()
    {
        Interactibles = GameObject.Find("Interactibles");

        for (int i = 0; i < Interactibles.transform.childCount; i++)
        {
            PlayerPrefs.DeleteKey("Child" + i);
            if (Interactibles.transform.GetChild(i).GetComponent<BigBox>() != null)
            {
                PlayerPrefs.DeleteKey("ChildPosX" + i);
                PlayerPrefs.DeleteKey("ChildPosY" + i);
            }
        }
        PlayerPrefs.DeleteKey("PlayerPosX");
        PlayerPrefs.DeleteKey("PlayerPosY");

        foreach (var v in PlayerController.instance.UnlockedUpgrades.ToArray())
        {
            PlayerPrefs.DeleteKey(v.Key);
        }
    }
    public IEnumerator SaveRoutine(bool quit = false)
    {
        while (SceneManager.GetActiveScene().name != "DevRoom")
        {
            yield return null;
        }
        Save();
        if(quit) Application.Quit();
    }
    public IEnumerator LoadRoutine()
    {
        //waits for objects with saved values to be instantiated
        while (SceneManager.GetActiveScene().name != "DevRoom")
        {
            yield return null;
        }
        Load();
    }
    public IEnumerator ResetRoutine()
    {
        //waits for objects with saved values to be instantiated
        while (SceneManager.GetActiveScene().name != "DevRoom")
        {
            yield return null;
        }

        ResetSave();
    }
    public void Load()
    {
        Interactibles = GameObject.Find("Interactibles");

        for (int i = 0; i < Interactibles.transform.childCount; i++)
        {
            Interactibles.transform.GetChild(i).GetComponent<Interactible>().IsActivated = (PlayerPrefs.GetInt("Child" + i) == 1);
            if (Interactibles.transform.GetChild(i).GetComponent<BigBox>() != null)
            {
                Interactibles.transform.GetChild(i).transform.position = new(
                     PlayerPrefs.GetFloat("ChildPosX" + i),
                     PlayerPrefs.GetFloat("ChildPosY" + i),
                     0);
            }
        }
        PlayerController.instance.transform.position = new Vector3(
            PlayerPrefs.GetFloat("PlayerPosX"),
            PlayerPrefs.GetFloat("PlayerPosY"),
            0);


        foreach (var v in PlayerController.instance.UnlockedUpgrades.ToArray())
        {
            PlayerController.instance.UnlockedUpgrades[v.Key] = PlayerPrefs.GetInt(v.Key) == 1;
        }
    }
}
