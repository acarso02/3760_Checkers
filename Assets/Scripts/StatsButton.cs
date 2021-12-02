using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsButton : MonoBehaviour
{
    public void StatsMenu(){
        SaveNames.TestStartup();
        SceneManager.LoadScene("Scenes/PlayerStats");
    }
}
