using System;
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

    private static List<Piece> Pieces;

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

            Pieces = Board.GetPieceList();

            if(flag == false) {
                redCam.SetActive(true);
                blackCam.SetActive(false);
            } else {
                redCam.SetActive(false);
                blackCam.SetActive(true);
            }
            flag = !flag;

            string winner = Board.HasWon(); //checks for winner
            if (winner != "None") { //if there is a winner
                Debug.Log(winner + " wins!");
                if (winPopup != null) {
                    //sets active which player won; red or black
                    if (winner == "Red") {
                        redText.SetActive(true);
                    }
                    else if (winner == "Black") {
                        blackText.SetActive(true);
                    }
                    winPopup.SetActive(true); //displays winning popup
                }
                // Add functionality for setting all pieces back to false flag
                foreach (Piece piece in Pieces) {
                    piece.flag = false;
                }
            }
        }
    }
}
