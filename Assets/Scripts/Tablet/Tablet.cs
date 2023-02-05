using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablet 
{
    private int squareX, squareY, numMines;
    public Tablet(int squareX, int squareY, int numMines)
    {
        this.squareX = squareX;
        this.squareY = squareY;
        this.numMines = numMines;
    }

    public int SquareX { get => squareX; set => squareX = value; }
    public int NumMines { get => numMines; set => numMines = value; }
    public int SquareY { get => squareY; set => squareY = value; }
}
