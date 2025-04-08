using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class XboxControllerNavigation : MonoBehaviour
{
    private EventSystem eventSystem;
    private GameObject selectedButton;

    void Start()
    {
        eventSystem = EventSystem.current;
        selectedButton = eventSystem.currentSelectedGameObject;
    }

    void Update()
    {
        // Navegar entre los botones con el D-pad o Joystick
        NavigateMenu();
    }

    void NavigateMenu()
    {
        // Obtener la dirección del joystick del control (Horizontal y Vertical)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            GameObject nextSelection = eventSystem.currentSelectedGameObject;

            if (vertical > 0)
            {
                nextSelection = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp()?.gameObject;
            }
            else if (vertical < 0)
            {
                nextSelection = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown()?.gameObject;
            }
            else if (horizontal > 0)
            {
                nextSelection = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnRight()?.gameObject;
            }
            else if (horizontal < 0)
            {
                nextSelection = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnLeft()?.gameObject;
            }

            if (nextSelection != null)
            {
                eventSystem.SetSelectedGameObject(nextSelection);
            }
        }
    }
}
