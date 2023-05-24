using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactible : MonoBehaviour
{
    public static Interactible CurrentInteractibleObject = null;
    public abstract void Interact();
}
