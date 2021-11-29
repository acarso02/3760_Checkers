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

    private bool camFlag = true;

    private static List<Piece> Pieces;

    void Start() {
        redCam.SetActive(true);
        blackCam.SetActive(false);
        //Debug.Log(Camera.main);
    }

    void Update() {
        if(Input.GetKeyUp(KeyCode.Space)) {

            Pieces = Board.GetPieceList();

            if(camFlag == false) {
                redCam.SetActive(true);
                blackCam.SetActive(false);
            } else {
                redCam.SetActive(false);
                blackCam.SetActive(true);
            }
            camFlag = !camFlag;

            // Add functionality for setting all pieces back to false flag
            foreach (Piece piece in Pieces) {
                piece.flag = false;
            }

            string winner = Board.HasWon();
            if (winner != "None") {
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
            }
        }
    }
}
