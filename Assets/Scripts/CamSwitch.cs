using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public GameObject redCam;
    public GameObject blackCam;

    public GameObject winPopup;
    public GameObject redText;
    public GameObject blackText;

    private bool flag = true;

    void Start() {
        redCam.SetActive(true);
        blackCam.SetActive(false);
        //Debug.Log(Camera.main);
    }


    /*public void switchCam() {
        if(Input.GetKeyUp(KeyCode.Space)) {
            if(flag == false) {
                redCam.SetActive(true);
                blackCam.SetActive(false);
            } else {
                redCam.SetActive(false);
                blackCam.SetActive(true);
            }
            flag = !flag;
        }

    }*/

    void Update() {
        if(Input.GetKeyUp(KeyCode.Space)) {
            if(flag == false) {
                redCam.SetActive(true);
                blackCam.SetActive(false);
            } else {
                redCam.SetActive(false);
                blackCam.SetActive(true);
            }
            flag = !flag;

            string winner = Board.hasWon();
            if (winner != "None") {
                Debug.Log(winner + " wins!");
                if (winPopup != null)
                    if (winner == "Red") {
                        redText.SetActive(true);
                    }
                    else if (winner == "Black") {
                        blackText.SetActive(true);
                    }
                winPopup.SetActive(true);
            }
        }
    }
}
