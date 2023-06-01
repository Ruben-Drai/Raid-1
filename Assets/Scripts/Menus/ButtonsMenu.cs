using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Manage buttons' sprites appearances
public class ButtonsMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Sprite spriteNotSelected, spriteSelected, spritePushed;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = spriteNotSelected;

        if (name == "Continue Button")
            GetComponent<Button>().interactable = PlayerPrefs.HasKey("PlayerPosX");

    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null && PlayerController.instance.Controller.currentControlScheme == "Gamepad")
            ChangeButtonSpriteWithGamePad();
    }

    void ChangeButtonSpriteWithGamePad()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
            GetComponent<Image>().sprite = spriteSelected;

        else
            GetComponent<Image>().sprite = spriteNotSelected;
    }

    /* Detect if the cursor is inside the button, and if it's clicked */
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = spriteSelected;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = spriteNotSelected;
    }

    public void OnClick()
    {
        GetComponent<Image>().sprite = spritePushed;
    }
}
