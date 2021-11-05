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

    public Piece(int r, int c, Colour mC, GameObject mPGO) {
        row = r;
        col = c;
        myColour = mC;
        myPieceGameObject = mPGO;
    }
}


public class PieceDestroyer : MonoBehaviour
{
    public static void DestroyPiece(Piece piece)
    {
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
        bool moveIsLegal = isLegal(atRow,atCol,toRow,toCol);
        Piece toRemove = isCapture(atRow, atCol, toRow, toCol);

        // Debug.Log("piece to move exists? " + boardModel.Exists(atPiece => atPiece.row == atRow && atPiece.col == atCol));
        // Debug.Log("space to move to is empty? " + !boardModel.Exists(toPiece => toPiece.row == toRow && toPiece.col == toCol));

        if (moveIsLegal) {
            Piece toMove = GetPiece(atRow, atCol);
            toMove.row = toRow;
            toMove.col = toCol;
            return true;
        }
        else if (toRemove != null)
        {
            Piece toMove = GetPiece(atRow, atCol);
            toMove.row = toRow;
            toMove.col = toCol;
            boardModel.Remove(toRemove);
            PieceDestroyer.DestroyPiece(toRemove);
            return true;
        }
        return false;
    }

    /*
    Desc:       Checks to see if the given coordinates for a piece to move to is legal or not
    Returns:    True if the piece is legally able to move to the requested square, false if not
    */
    public static bool isLegal(int atRow, int atCol, int toRow, int toCol) {
        Piece p = GetPiece(atRow,atCol);

        //Debug.Log("AtRow: " + atRow + "toRow " + toRow + "atCol " + atCol + "toCol " + toCol);


        //Debug.Log(p.myColour);
        // Checks if the requested tile to move to is out of bounds of the board array
        if((toRow > 7 || toRow < 0) || (toCol > 7 || toCol < 0)) {
            return false;
        } 
        // Checks if there exist a piece to move at the given column and row and if there already exists a piece at the requested row and column to move to
        else if((!boardModel.Exists(atPiece => atPiece.row == atRow && atPiece.col == atCol)) && (boardModel.Exists(toPiece => toPiece.row == toRow && toPiece.col == toCol))) {
            return false;

        // Checks to see if red piece wants to move backwords *Note: Will need to add additionally functionality to this once king piece is a thing
        } else if(((p.myColour).ToString() == "red" && atRow > toRow) || ((p.myColour).ToString() == "black" && atRow < toRow)) {
            return false;
      
        }

        //Check the red move more than 1 space for a capture
        else if ((toRow - atRow > 1 && (p.myColour).ToString() == "red") && (toRow - atRow < 3))
        {

            if ((atCol < toCol) && (GetPieceColour(atRow + 1, atCol - 1) == "black"))
            {
                RemovePiece(atRow + 1, atCol - 1);
                return true;
            }
            else if ((atCol < toCol) && (GetPieceColour(atRow + 1, atCol + 1) == "black"))
            {
                RemovePiece(atRow + 1, atCol + 1);
                return true;
            }
            else
                return false;
        }
        //Check the black move more than 1 space for a capture
        else if ((toRow - atRow < -1 && (p.myColour).ToString() == "black") && (toRow - atRow > -3))
        {

            if ((atCol < toCol) && (GetPieceColour(atRow - 1, atCol + 1) == "red"))
            {
                RemovePiece(atRow - 1, atCol + 1);
                return true;
            }
            else if ((atCol > toCol) && (GetPieceColour(atRow - 1, atCol - 1) == "red"))
            {
                RemovePiece(atRow - 1, atCol - 1);
                return true;
            }
            else
                return false;
        }

        // Checks to see if red moves more than one tile for a normal move or if black moves more than one tile for a normal move
        else if ((toRow - atRow > 1 && (p.myColour).ToString() == "red") || (toRow - atRow < -1 && (p.myColour).ToString() == "black")) {
            return false;
        }else {
            return true;
        }
    }
}
