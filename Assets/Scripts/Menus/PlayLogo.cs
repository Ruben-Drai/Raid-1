using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayLogo : MonoBehaviour
{
    public Animator logo;
    public GameObject imglogo;
    public static bool HasWon = false;
    [SerializeField] private GameObject dialogueWin;
    [SerializeField] private GameObject dialogueLose;

    private void Start()
    {
        if (HasWon)
        {
            dialogueWin.SetActive(true);
        }
        else
        {
            dialogueLose.SetActive(true);
        }
    }
    public void PlayAnimation()
    {
        imglogo.SetActive(true);
        logo.SetBool("isPlaying", true);
        StartCoroutine(ReturnToMainMenu());
    }

    IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
        SaveNLoad.instance.ResetSave();
    }

}
