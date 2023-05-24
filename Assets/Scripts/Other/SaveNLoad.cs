using System;
using System.Collections;
using System.Collections.Generic;
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
        Interactibles = GameObject.Find("Interactibles");
    }
    private void OnApplicationQuit()
    {
        Save();
    }


    public void Save()
    {
        for (int i = 0; i < Interactibles.transform.childCount; i++)
        {
            PlayerPrefs.SetInt("Child"+i, Interactibles.transform.GetChild(i).GetComponent<Interactible>().IsActivated?1:0);
        }
        PlayerPrefs.SetFloat("PlayerPosX",PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY",PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ",PlayerController.instance.transform.position.z);

        foreach(var v in PlayerController.instance.UnlockedUpgrades)
        {
            PlayerPrefs.SetInt(v.Key, v.Value ? 1 : 0);
        }
    }

    public void ResetSave()
    {
        for (int i = 0; i < Interactibles.transform.childCount; i++)
        {
            PlayerPrefs.DeleteKey("Child" + i);
        }
        PlayerPrefs.DeleteKey("PlayerPosX");
        PlayerPrefs.DeleteKey("PlayerPosY");
        PlayerPrefs.DeleteKey("PlayerPosZ");

        foreach (var v in PlayerController.instance.UnlockedUpgrades)
        {
            PlayerPrefs.DeleteKey(v.Key);
        }
    }
    public IEnumerator SaveRoutine()
    {
        while (PlayerController.instance == null || Interactibles == null) yield return null;

        Save();
    }
    public IEnumerator LoadRoutine()
    {
        //waits for objects with saved values to be instantiated
        while (PlayerController.instance == null || Interactibles == null) yield return null;

        Load();
    }
    public IEnumerator ResetRoutine()
    {
        //waits for objects with saved values to be instantiated
        while (PlayerController.instance == null || Interactibles == null) yield return null;

        ResetSave();
    }
    public void Load()
    {
        for (int i = 0; i < Interactibles.transform.childCount; i++)
        {
            Interactibles.transform.GetChild(i).GetComponent<Interactible>().IsActivated = (PlayerPrefs.GetInt("Child" + i) == 1);
        }
        PlayerController.instance.transform.position = new Vector3(
            PlayerPrefs.GetFloat("PlayerPosX"), 
            PlayerPrefs.GetFloat("PlayerPosY"), 
            PlayerPrefs.GetFloat("PlayerPosZ"));

        foreach (var v in PlayerController.instance.UnlockedUpgrades)
        {
            PlayerController.instance.UnlockedUpgrades[v.Key] = PlayerPrefs.GetInt(v.Key)==1;
        }
    }
}
