using System;
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

public class Piece {    
    public int row;
    public int col;
    public Colour myColour;

    public Piece(int r, int c, Colour mC) {
        row = r;
        col = c;
        myColour = mC;
    }
}

public static class Board {
    private static List<Piece> boardModel = new List<Piece>();

    //Called when GameBoard is loaded in, by every piece GameObject
    public static Piece AddPiece(int r, int c, Colour mC) {
        Piece newPiece = new Piece(r, c, mC);
        boardModel.Add(newPiece);

        return newPiece;
    }

    public static void RemovePiece(int r, int c) {
        Piece toRemove = boardModel.Find(p => p.row == r && p.col == c);
        boardModel.Remove(toRemove);
    }

    public static Piece GetPiece(int r, int c) {
        return boardModel.Find(p => p.row == r && p.col == c);
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
        //When code merge goes down, this should be swapped for an "IsLegal() function"
        bool moveIsLegal = (boardModel.Exists(atPiece => atPiece.row == atRow && atPiece.col == atCol))
                        && (!boardModel.Exists(toPiece => toPiece.row == toRow && toPiece.col == toCol));

        // Debug.Log("piece to move exists? " + boardModel.Exists(atPiece => atPiece.row == atRow && atPiece.col == atCol));
        // Debug.Log("space to move to is empty? " + !boardModel.Exists(toPiece => toPiece.row == toRow && toPiece.col == toCol));

        if (moveIsLegal) {
            Piece toMove = GetPiece(atRow, atCol);
            toMove.row = toRow;
            toMove.col = toCol;
            return true;
        }
        return false;
    }
}
