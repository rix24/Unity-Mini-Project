using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MenuManager menuManager;
    public GameObject menu;
    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>(true);
        if (menuManager == null)
        {
            Instantiate(menu);
        }
    }
}