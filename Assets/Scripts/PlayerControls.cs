using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public bool playerIsRed, playerIsBlack;
    public Vector3 pieceBoardDifference;

    private Camera cam;
    private string relevantTag;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        if (playerIsRed) {
            relevantTag = "Red Piece";
        } else if (playerIsBlack) {
            relevantTag = "Black Piece";
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If the mouse is clicked, cast a ray from the camera, in the direction of the mouse pointer
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
            //If it hits something, check the tag on the GameObject it hit,
            if (Physics.Raycast(ray.origin, ray.direction, out hit)) {
                //If that tag is the same as the one belonging to the pieces that this player controls, start waiting for the next click
                if (hit.collider.CompareTag(relevantTag)) {
                    StartCoroutine(WaitForBoardClick(hit.collider.transform.root.gameObject));
                }
            }
        }
    }

    IEnumerator WaitForBoardClick(GameObject piece) {
        yield return null; //In a coroutine yield return null means "wait for one frame"
        while(!Input.GetMouseButtonDown(0)) {
            yield return null;
        }
        //check if the player clicked on an empty square to move to (in checkers its not possible to move to red squares)
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray.origin, ray.direction, out hit)) {
            if (hit.collider.CompareTag("Black Square")) {
                GameObject square = hit.collider.gameObject;

                //Convert the piece and squares coordinates in world space to coordinates on the board (This should eventually be done by a function in Board)
                int atRow, atCol, toRow, toCol;
                atCol = (int) (piece.transform.position.x + 0.5f);
                atRow = (int) piece.transform.position.z;
                toCol = (int) (square.transform.position.x + 0.5f);
                toRow = (int) square.transform.parent.position.z;
                //attempt to move the piece to the new position
                bool canMove = Board.MovePiece(atRow, atCol, toRow, toCol);
                Debug.Log("atRow: " + atRow + " atCol: " + atCol + " toRow: " + toRow + " toCol: " + toCol);
                Debug.Log(canMove);
                //if the attempt was successful, move the model to the squares position, but move it up by the positional difference between pieces and squares
                if (canMove) {
                    //Move the model to the new location
                    piece.transform.position = hit.collider.gameObject.transform.position + pieceBoardDifference;
                }
            }
        }
    }
}
