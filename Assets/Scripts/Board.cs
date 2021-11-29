using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
This class models an 8x8 checkers gameboard

Idea: Refactor Piece[,] into a list of new objects representing pieces. on Start(), each piece gameobject sends a reference of itself to this class.
This will make it easier to get pieces locations and will solve the on load bug where the boardModel state doesn't reset on scene load.
*/

public enum Colour {
    red,
    black
}

public class Piece{    
    public int row;
    public int col;
    public Colour myColour;
    public GameObject myPieceGameObject;
    public bool isKing;

    public Piece(int r, int c, Colour mC, GameObject mPGO) {
        row = r;
        col = c;
        myColour = mC;
        myPieceGameObject = mPGO;
        isKing = false;
    }

    public Boolean isKingMove(int atRow, int atCol, int toRow, int toCol)
    {
        if ((myColour == Colour.red && toRow == 7) || (myColour == Colour.black && toRow == 0))
        {
        	/*
			myPieceGameObject.transform.GetChild(1)
        		Get the second child of the Piece game object related to this, which will always be the crown
			.gameObject.SetActive(true)
				Set the crown to be active
        	*/
        	myPieceGameObject.transform.GetChild(1).gameObject.SetActive(true);
            return true;
        }
        else
        {
            return false;
        }
    }
    
    // Flag = false if a piece is able to move freely
    // Flag = true if a piece has done a capture in its turn and would like to continue to capture other pieces.
    public bool flag = false;
}


public class PieceDestroyer : MonoBehaviour
{
    public static void DestroyPiece(Piece piece)
    {
        PieceGameFeel pGF = piece.myPieceGameObject.GetComponent<PieceGameFeel>();
        pGF.DoPieceExplosion();
        Destroy(piece.myPieceGameObject);
    }
}


public static class Board {
    private static List<Piece> boardModel = new List<Piece>();

    //Called when GameBoard is loaded in, by every piece GameObject
    public static Piece AddPiece(int r, int c, Colour mC, GameObject mPGO) {
        Piece newPiece = new Piece(r, c, mC, mPGO);
        boardModel.Add(newPiece);
        
        return newPiece;
    }

    public static void RemovePiece(int r, int c) {
        if(boardModel.Exists(p => p.row == r && p.col == c)){
            Piece toRemove = boardModel.Find(p => p.row == r && p.col == c);
            boardModel.Remove(toRemove);
            PieceDestroyer.DestroyPiece(toRemove);
        }
    }

    public static Piece GetPiece(int r, int c) {
        return boardModel.Find(p => p.row == r && p.col == c);
    }

    public static String GetPieceColour(int r, int c)
    {
        try
        {
            Piece piece = boardModel.Find(p => p.row == r && p.col == c);
            return piece.myColour.ToString();
        }
        catch(Exception)
        {
            return "none";
        }
    }

    public static List<Piece> GetPieceList() {
        return boardModel;
    }

    //Deletes every Piece in boardModel
    public static void ClearBoard() {
        boardModel.Clear();
    }

    /*
    Desc:       Moves a piece from boardModel[atRow,atCol] to gameBoard[toRow,toCol], if such a move can be made without intersecting pieces
    Returns:    True if the piece was able to legally move, otherwise False
    */
    public static bool MovePiece(int atRow, int atCol, int toRow, int toCol) {

        // Verify move is legal
        bool moveIsLegal = IsLegal(atRow,atCol,toRow,toCol);
        Piece toRemove = IsCapture(atRow, atCol, toRow, toCol);

        if (moveIsLegal) {
            Piece toMove = GetPiece(atRow, atCol);

            // Checks to see if there is currently a hotpiece selected to prevent other pieces from moving in the same turn
            foreach (Piece pieces in GetPieceList()) {
                if(toMove.flag == false && pieces.flag == true) {
                    return false;
                }
            }

            toMove.row = toRow;
            toMove.col = toCol;
            if(toRemove != null){
                toMove.flag = true;
                boardModel.Remove(toRemove);
                PieceDestroyer.DestroyPiece(toRemove);
                return true;
            }
            return true;
        }
        return false;
    }

    /*
    Desc:       Checks to see if the given coordinates for a piece to move to is legal or not
    Returns:    True if the piece is legally able to move to the requested square, false if not
    */
    public static bool IsLegal(int atRow, int atCol, int toRow, int toCol) {
        Piece p = GetPiece(atRow,atCol);


        // Checks if the requested tile to move to is out of bounds of the board array
        if((toRow > 7 || toRow < 0) || (toCol > 7 || toCol < 0)) {
            return false;
        } 
        // Checks if there exist a piece to move at the given column and row and if there already exists a piece at the requested row and column to move to
        else if((!boardModel.Exists(atPiece => atPiece.row == atRow && atPiece.col == atCol)) && (boardModel.Exists(toPiece => toPiece.row == toRow && toPiece.col == toCol))) {
            return false;

        // Checks to see if piece wants to move backwords *Note: Will allow king piece to move backward
        } else if((((p.myColour).ToString() == "red" && atRow > toRow) || ((p.myColour).ToString() == "black" && atRow < toRow)) && p.isKing == false) {
            Debug.Log("backwards move detected");
            return false;
        }
        // Check for sideways movement
        else if(toRow == atRow)
        {
            return false;
        }
        // Checks to see if piece moves more than one tile for a normal move 
        else if (Math.Abs(toRow - atRow) > 1) {
            //Checks if abnormal move is a capture
            if (IsCapture(atRow, atCol, toRow, toCol) != null){
                return true;
            }
            else { 
                return false;
            }
        }         
        // Checks to see if a piece has already done a capture, prevents piece that has already done a capture in the same turn to do anything other than another capture
        else if(p.flag == true) {
            return false;
        }
        else {
            return true;
        }
    }

    /*
    Desc:       Checks to see if the given coordinates for a piece to move will result in a capture
    Returns:    The piece to remove if a capture happens, null otherwise
    */
    public static Piece IsCapture(int atRow, int atCol, int toRow, int toCol){
        Piece p = GetPiece(atRow, atCol);

        //Check the red move more than 1 space for a capture
        if (Math.Abs(toRow - atRow) > 1 && (p.myColour).ToString() == "red" && (Math.Abs(toRow - atRow) < 3))
        {
            //forward left
            if ((atCol > toCol) && (toRow > atRow) && (GetPieceColour(atRow + 1, atCol - 1) == "black"))
            {
                return GetPiece(atRow + 1, atCol - 1);
            }
            //forward right
            else if ((atCol < toCol) && (toRow > atRow) && (GetPieceColour(atRow + 1, atCol + 1) == "black"))
            {
                return GetPiece(atRow + 1, atCol + 1);
            }
            //back right
            else if (p.isKing) 
            {
                if ((atCol < toCol) && (toRow < atRow) && (GetPieceColour(atRow - 1, atCol + 1) == "black"))
                {
                    return GetPiece(atRow - 1, atCol + 1);
                }
                //back left
                else if ((atCol > toCol) && (toRow < atRow) && (GetPieceColour(atRow - 1, atCol - 1) == "black"))
                {
                    return GetPiece(atRow - 1, atCol - 1);
                }
                else
                    return null;
            }

            else
                return null;
        }
        //Check the black move more than 1 space for a capture
        else if (Math.Abs(toRow - atRow) > 1 && (p.myColour).ToString() == "black" && (Math.Abs(toRow - atRow) < 3))
        {
            //forward left
            if ((atCol < toCol) && (toRow < atRow) && (GetPieceColour(atRow - 1, atCol + 1) == "red"))
            {
                return GetPiece(atRow - 1, atCol + 1);
            }
            //forward right
            else if ((atCol > toCol) && (toRow < atRow) && (GetPieceColour(atRow - 1, atCol - 1) == "red"))
            {
                return GetPiece(atRow - 1, atCol - 1);
            }
            
            else if (p.isKing)
            {
                //back right
                if ((atCol > toCol) && (toRow > atRow) && (GetPieceColour(atRow + 1, atCol - 1) == "red"))
                {
                    return GetPiece(atRow + 1, atCol - 1);
                }
                //back left
                else if ((atCol < toCol) && (toRow > atRow) && (GetPieceColour(atRow + 1, atCol + 1) == "red"))
                {
                    return GetPiece(atRow + 1, atCol + 1);
                }
                else
                    return null;
            }
            else
                return null;
        }
        else
        {
            return null;
        }
    }

    /*
    Desc:       Counts all the pieces on the game board at any given time
    Returns:    Integer of how many pieces of a certain colour are on the board
    */
    private static int getPieceAmount(String pieceColour) {
        //finds all game objects with a "Red Piece" or "Black Piece" tag
        int count = GameObject.FindGameObjectsWithTag(pieceColour + " Piece").Length;
        return count/2; //*note* since pieces consist of both game models and game objects count must be divided by 2
    }

    /*
    Desc:       Counts the amount of legal moves a player has by going through every piece they have and summing up their moves
    Returns:    Integer of how many moves the player has
    */
    private static int getLegalMoveAmount(string colour) { //does not consider captures when counting legal moves atm
        int count = 0;

        foreach (Piece p in boardModel) {
            if((p.myColour).ToString() == "red" && colour == "red") { //check if red has legal moves
                //below variables are used to check if there are pieces on the left or right of current piece
                Piece leftPiece = GetPiece(p.row + 1, p.col - 1);
                Piece rightPiece = GetPiece(p.row + 1, p.col + 1);
                Piece twoLeft = GetPiece(p.row + 2, p.col - 2);
                Piece twoRight = GetPiece(p.row + 2, p.col + 2);

                //check if move left is legal                
                if (IsLegal(p.row, p.col, p.row + 1, p.col - 1) == true && leftPiece == null) { 
                    count++;
                }
                //check if a capture left is legal
                else if (IsCapture(p.row, p.col, p.row + 2, p.col - 2) != null && twoLeft == null && ((p.row + 2 < 8 && p.row + 2 >= 0) && (p.col - 2 < 8 && p.col - 2 >= 0))) { 
                    count++;
                }

                //check if move right is legal
                if (IsLegal(p.row, p.col, p.row + 1, p.col + 1) && rightPiece == null) { 
                    count++;
                }
                //check if a capture right is legal
                else if (IsCapture(p.row, p.col, p.row + 2, p.col + 2) != null && twoRight == null && ((p.row + 2 < 8 && p.row + 2 >= 0) && (p.col + 2 < 8 && p.col + 2 >= 0))) {
                    count++;
                }
            }
            else if ((p.myColour).ToString() == "black" && colour == "black") { //check if black has legal moves
                //below variables are used to check if there are pieces on the left or right of current piece
                Piece leftPiece = GetPiece(p.row - 1, p.col - 1);
                Piece rightPiece = GetPiece(p.row - 1, p.col + 1);
                Piece twoLeft = GetPiece(p.row - 2, p.col - 2);
                Piece twoRight = GetPiece(p.row - 2, p.col + 2);

                //check if move left is legal
                if (IsLegal(p.row, p.col, p.row - 1, p.col - 1) == true && leftPiece == null) { 
                    count++;
                }
                //check if capture left is legal
                else if (IsCapture(p.row, p.col, p.row - 2, p.col - 2) != null && twoLeft == null && ((p.row - 2 < 8 && p.row - 2 >= 0) && (p.col - 2 < 8 && p.col - 2 >= 0))) {
                    count++;
                }

                //check if move right is legal
                if (IsLegal(p.row, p.col, p.row - 1, p.col + 1) == true && rightPiece == null) {
                    count++;
                }
                //check if capture right is legal
                else if (IsCapture(p.row, p.col, p.row - 2, p.col + 2) != null && twoRight == null && ((p.row - 2 < 8 && p.row - 2 >= 0) && (p.col + 2 < 8 && p.col + 2 >= 0))) {
                    count++;
                }
            }
        }

        return count;
    }


    /*
    Desc:       Checks if one of the players has won the game and returns the winner
    Returns:    String of which player won the game; 'Red' or 'Black'
    */
    public static string HasWon() {
        int redMoveAmount = getLegalMoveAmount("red");
        int blackMoveAmount = getLegalMoveAmount("black");

        if (getPieceAmount("Red") == 0 || redMoveAmount == 0){
            return "Black";
        }
        else if (getPieceAmount("Black") == 0 || blackMoveAmount == 0) {
            return "Red";
        }
        return "None";
    }
}
