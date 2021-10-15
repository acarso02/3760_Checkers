using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Popup : MonoBehaviour
{
    public float quitGameDelayInSeconds;

    public void QuitGame()
    {
        StartCoroutine(QuitGameAfterDelay());
    }

    private IEnumerator QuitGameAfterDelay() {
        yield return new WaitForSeconds(quitGameDelayInSeconds);
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
