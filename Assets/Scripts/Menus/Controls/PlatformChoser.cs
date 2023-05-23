using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

public class PlatformChoser : MonoBehaviour
{
    [SerializeField]private GameObject KMtree;
    [SerializeField]private GameObject GamePadtree;
    [SerializeField]private GameObject BackButton;
    private PlayerInput controlScheme;
    // Start is called before the first frame update
    void Start()
    {
        controlScheme = PlayerController.instance.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controlScheme.currentControlScheme == "K&M" && !KMtree.activeSelf)
        {
            GamePadtree.SetActive(false); 
            KMtree.SetActive(true);
        }
            
        else if (controlScheme.currentControlScheme == "GamePad" && !GamePadtree.activeSelf)
        {
            GamePadtree.SetActive(true);
            KMtree.SetActive(false);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("Back"));
        }
    }
   
}
