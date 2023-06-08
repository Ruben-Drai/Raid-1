using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    private int hp = 3;

    public void DamageBoss()
    {
        hp -= 1;

        if (hp <= 0)
        {
            Debug.Log("Boss is DEAD");
        }
    }
}
