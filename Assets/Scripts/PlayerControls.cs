using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public bool playerIsRed, playerIsBlack;
    public Vector3 pieceBoardDifference;

    private Camera cam;
    private string relevantTag;

    private CamSwitch camSwitch;
    private GameObject otherGameObject;

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

    public void DeletePiece(GameObject piece)
    {
        Destroy(piece);
    }

    // Update is called once per frame
    void Update()
    {
        //If the mouse is clicked, cast a ray from the camera, in the direction of the mouse pointer
        if (Input.GetMouseButtonDown(0)) {
            GameObject piece = GetClickedOnPiece();
            if (piece != null) {
                PieceGameFeel pGF = piece.GetComponent<PieceGameFeel>();
                pGF.DoPieceJiggle();
                StartCoroutine(WaitForBoardClick(piece));
            }
        }
    }

    private GameObject GetClickedOnPiece() {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        //If it hits something, check the tag on the GameObject it hit,
        if (Physics.Raycast(ray.origin, ray.direction, out hit)) {
            //If that tag is the same as the one belonging to the pieces that this player controls, start waiting for the next click
            if (hit.collider.CompareTag(relevantTag)) {
                return hit.collider.transform.root.gameObject;
            }
        }

        return null;
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

                PieceInstantiator pI = piece.GetComponent<PieceInstantiator>();
                Piece mP = pI.MyPiece;

                atCol = mP.col;
                atRow = mP.row;
                //(eventually there should be a cleaner way to get the Board coordinates from the squares, but this is fine for now)
                toCol = (int) (square.transform.position.x + 0.5f);
                toRow = (int) square.transform.parent.position.z;
                //attempt to move the piece to the new position, in the Board representation
                bool canMove = Board.MovePiece(atRow, atCol, toRow, toCol);
                //if the attempt was successful, move the model to the squares position, but move it up by the positional difference between pieces and squares
                if (canMove) {
                    //Move the model to the new location
                    piece.transform.position = hit.collider.gameObject.transform.position + pieceBoardDifference;
                    PieceGameFeel pGF = piece.GetComponent<PieceGameFeel>();
                    pGF.DoPieceDrop();

                    //check for king move
                    if(mP.isKingMove(atRow, atCol, toRow, toCol) == true)
                    {
                        mP.isKing = true;
                    }

                    // Camera Switch functionality upon legal move
                    /*otherGameObject = GameObject.Find("CamSwitch");
                    camSwitch = otherGameObject.GetComponent<CamSwitch>();

                    // Function call switches camera
                    camSwitch.switchCam();*/
                }
                else {
                    Debug.Log("The requested piece move is not a legal move!");
                }
            }
        }
    }


}
