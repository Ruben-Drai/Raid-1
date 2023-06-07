using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlatformChoser : MonoBehaviour
{
    [SerializeField] private GameObject KMtree;
    [SerializeField] private GameObject GamePadtree;
    [SerializeField] private GameObject BackButton;
    private PlayerInput controlScheme;
    // Start is called before the first frame update
    void Start()
    {
        controlScheme = PlayerController.instance.GetComponent<PlayerInput>();
    }

    //switches what rebind screen is shown depending on the currently active control scheme
    void Update()
    {
        if (controlScheme.currentControlScheme == "K&M" && !KMtree.activeSelf)
        {
            GamePadtree.SetActive(false);
            KMtree.SetActive(true);
        }

        else if (controlScheme.currentControlScheme == "Gamepad" && !GamePadtree.activeSelf)
        {
            GamePadtree.SetActive(true);
            KMtree.SetActive(false);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("Back"));
        }
    }

}
