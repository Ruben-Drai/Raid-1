using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HologramTrigger : MonoBehaviour
{
    public GameObject HologramBox;
    public void Open()
    {
        HologramBox.SetActive(true);
        FindObjectOfType<HologramManager>().Hologram();
    }
}