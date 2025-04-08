using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


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
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(Horizontal) > 0.1f || Mathf.Abs(Vertical) > 0.1f)
        {
            GameObject nextSelection = eventSystem.currentSelectedGameObject;

            if (Vertical > 0)
            {
                nextSelection = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp()?.gameObject;
            }
            else if (Vertical < 0)
            {
                nextSelection = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown()?.gameObject;
            }
            else if (Horizontal > 0)
            {
                nextSelection = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnRight()?.gameObject;
            }
            else if (Horizontal < 0)
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
