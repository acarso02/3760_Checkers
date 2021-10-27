/*
This class models an 8x8 checkers gameboard
*/
public static class Board {
    private enum Piece {
        none,
        red,
        black
    }

    private static Piece[,] boardModel = new Piece[8,8]
        {
            {Piece.none, Piece.red,   Piece.none, Piece.red,   Piece.none, Piece.red,   Piece.none, Piece.red},
            {Piece.red,   Piece.none, Piece.red,   Piece.none, Piece.red,   Piece.none, Piece.red,   Piece.none},
            {Piece.none, Piece.red,   Piece.none, Piece.red,   Piece.none, Piece.red,   Piece.none, Piece.red},
            
            {Piece.none, Piece.none, Piece.none, Piece.none, Piece.none, Piece.none, Piece.none, Piece.none},
            {Piece.none, Piece.none, Piece.none, Piece.none, Piece.none, Piece.none, Piece.none, Piece.none},

            {Piece.black, Piece.none, Piece.black, Piece.none, Piece.black, Piece.none, Piece.black, Piece.none},
            {Piece.none, Piece.black, Piece.none, Piece.black, Piece.none, Piece.black, Piece.none, Piece.black},
            {Piece.black, Piece.none, Piece.black, Piece.none, Piece.black, Piece.none, Piece.black, Piece.none},
        };

    public static string BoardToString() {
        string bMS = "BoardModel : \n\t1\t2\t3\t4\t5\t6\t7\t8\n";

        for (int row = 0; row < 8; row++) {
            bMS += (row+1) + "\t";
            for (int col = 0; col < 8; col++) {
                bMS += boardModel[row, col] + "\t";
            }
            bMS += "\n";
        }

        return bMS;
    }

    public static string GetPieceAt(int row, int col) {
        if (row < 8 && col < 8)
            return "" + boardModel[row,col];
        else
            return null;
    }

    /*
    Desc:       Moves a piece from boardModel[atRow,atCol] to gameBoard[toRow,toCol], if such a move can be made without intersecting pieces
    Returns:    True if the piece was able to legally move, otherwise False
    */
    public static bool MovePiece(int atRow, int atCol, int toRow, int toCol) {
        if ((boardModel[atRow, atCol] != Piece.none) && (boardModel[toRow, toCol] == Piece.none)) {
            boardModel[toRow, toCol] = boardModel[atRow, atCol];
            boardModel[atRow, atCol] = Piece.none;
            return true;
        }
        return false;
    }
}
