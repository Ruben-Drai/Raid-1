using System.Collections;
using UnityEngine;

public abstract class Interactible : MonoBehaviour
{
    public bool IsActivated = false;
    public abstract void Interact();

    
}
