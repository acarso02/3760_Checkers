using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class ReturnToMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            SceneManager.LoadScene("Menu");
        }
    }

    public void loadScene(string scene) {
        SceneManager.LoadScene(scene);
    }
}
