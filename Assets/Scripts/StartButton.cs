using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void StartGame() {
        //Set teams, probably by assigning a static variable?
        //Or should this happen when the game scene loads in? Bring it up next meeting
        SceneManager.LoadScene("Scenes/GameBoard");
    }
}
