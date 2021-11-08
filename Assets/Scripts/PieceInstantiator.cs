using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceInstantiator : MonoBehaviour
{
    public bool isRed;
    public bool isBlack;

    private Piece myPiece;
    public Piece MyPiece
    {
        get { return myPiece; }
    }

    private Colour myColour;
    public Colour MyColour
    { 
        get { return myColour; }
    }

    void Start() {
        int atRow, atCol;
        atRow = (int) (transform.position.x + 0.5f);
        atCol = (int) transform.position.z;

        if (isRed) {
            myColour = Colour.red;
            myPiece = Board.AddPiece(atCol, atRow, myColour, gameObject);
        } else if (isBlack) {
            myColour = Colour.black;
            myPiece = Board.AddPiece(atCol, atRow, myColour, gameObject);
        } else {
            Debug.Log("isRed or isBlack must be checked on Piece:" + gameObject.name);
        }
    }
}
