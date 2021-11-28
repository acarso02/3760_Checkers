using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public float startGameDelayInSeconds;

    public void StartGame() {
        StartCoroutine(StartGameAfterDelay());
    }

    private IEnumerator StartGameAfterDelay() {
        yield return new WaitForSeconds(startGameDelayInSeconds);
        //Set teams, probably by assigning a static variable?
        //Or should this happen when the game scene loads in? Bring it up next meeting
        SceneManager.LoadScene("Scenes/GameBoard");
    }
}