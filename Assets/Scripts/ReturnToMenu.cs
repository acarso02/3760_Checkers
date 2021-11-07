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

    //generic scene loader used to reload scene when a player wins
    public void loadScene(string scene) {
        SceneManager.LoadScene(scene);
    }
}
