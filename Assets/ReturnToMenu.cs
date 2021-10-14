using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            SceneManager.LoadScene("Menu");
        }
    }
}
