using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlatform : Interactible
{
    [SerializeField] private bool LaunchesCutscene = false;
    [SerializeField] private float CutsceneFreezeDuration = 2f;

    [SerializeField] private CinemachineVirtualCamera[] vms;

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
        IsActivated = true;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);

        if (LaunchesCutscene) cutscene ??= StartCoroutine(LaunchCutscene());
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
