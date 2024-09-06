using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject controllerMenu;
    public GameObject mainMenu;

    public void DisableMenu()
    {
        controllerMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void EnableMenu()
    {
        controllerMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

