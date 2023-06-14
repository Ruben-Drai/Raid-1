using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlatform : Interactible
{
    [SerializeField] private bool LaunchesCutscene = false;
    [SerializeField] private float CutsceneFreezeDuration = 2f;

    [SerializeField] private CinemachineVirtualCamera[] vms;

    [Header("Audio")]
    [SerializeField] private AudioClip destroyedSound;

    private Coroutine cutscene;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsActivated && collision.gameObject.CompareTag("Crate"))
        {
            Interact();
        }
    }
    public override void Interact()
    {
        IsActivated = !IsActivated;
        transform.GetChild(0).gameObject.SetActive(!IsActivated); //changes the platforms appearance
        transform.GetChild(1).gameObject.SetActive(IsActivated);

        GetComponent<AudioSource>().volume = SoundManager.instance == null ? 0.7f : SoundManager.instance.volumeSoundSlider.value;
        GetComponent<AudioSource>().PlayOneShot(destroyedSound); //play sound

        if (LaunchesCutscene) cutscene ??= StartCoroutine(LaunchCutscene()); //launch cutscene (if there is one)
    }

    public IEnumerator LaunchCutscene()
    {
        foreach (var pos in vms)
        {
            pos.Priority = 11;

            yield return new WaitForSeconds(CutsceneFreezeDuration);
            pos.Priority = 9;
        }
        yield return null;
        FindFirstObjectByType<CinemachineVirtualCamera>().Follow = PlayerController.instance.transform;
    }
}
