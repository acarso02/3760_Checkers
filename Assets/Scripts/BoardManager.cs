using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    void Awake()
    {
        Board.ClearBoard();
    }
}
