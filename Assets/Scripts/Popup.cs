using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Popup : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
