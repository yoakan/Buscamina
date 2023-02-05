using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinesManagers : MonoBehaviour
{
    // Start is called before the first frame update



    private  int numMinas =10;
    private Mine[,] casillas;
    private List<Mine> mines= new List<Mine>();

    public Mine[,] Casillas {  set => casillas = value; }




    public void generateAtributesMines(int x,int y)
    {
        generateMines(x,y);
        generateNumbers();
    }
    /**
     * Genera un numero dependiendo de la cantidad de mians al rededor-
     * Timi: O un placer conocerte Lucas Joseabtonio  de las Praderas del Monte Conminavo!!!
     */
    private void generateNumbers()
    {
        for (int x = 0; x < casillas.GetLength(0); x++)
        {
            for (int y = 0; y < casillas.GetLength(1); y++)
            {
                if (casillas[x, y].TypeMine!= TypeMine.explosive)
                {
                    casillas[x, y].Numero = canMinas(x, y);
                }
            }
        }
    }
    /*
     * Indica cuantas minas hay alrededor de  una casilla
     * TIMI:  El de al lado se ha fumado un peta... No me fio de el :(
     */
    private int canMinas(int xCasilla,int yCasilla)
    {
        int minas = 0;
        for (int x = xCasilla-1; x <= xCasilla+1; x++)
        {
            
            for (int y = yCasilla-1; y <= yCasilla+1; y++)
            {
                
                try {
                    if (casillas[x, y].TypeMine == TypeMine.explosive) { 
                        minas++;
                        //print("UNA MINA MAS");
                    };
                }
                catch(Exception e) { }
                //print(" X " + x+ " Y" + y+"");
               
            }
            
        }
        //print("CANTIDAD DE MINAS: " + minas);
        return minas;
    }
    /*Genera aleatoria mente las minas y las añade a una lista-
     * Espias: EL SEÑOR VA HA CAER EN NUESTRA TRAMPA JAJAJAJAJAJAJAJAJA
     */
    private void generateMines(int clickX,int clickY)
    {
        mines.Clear();
        System.Random random = new System.Random();
        numMinas = GameManager.Instance.TabletManager.getMines();
        int posX;
        int posY;
        for(int e = 0; e < numMinas; e++)
        {
            posX = random.Next(0,casillas.GetLength(0));
            posY = random.Next(0, casillas.GetLength(1));

            if (casillas[posX, posY].TypeMine==TypeMine.none && clickX != posX && posY != clickY)
            {
                casillas[posX, posY].TypeMine = TypeMine.explosive;
                mines.Add(casillas[posX, posY]);
            }
            else
            {
                e--;
            }
        }
    }



    /*
        Comprueba si las casilla de alrededor se pueden mostrar
        TIMI: el señor dice que mi grupito le gusta mucho :)
     */
    public void clickMines(int xCasilla, int yCasilla)
    {
        

        for (int x = xCasilla - 1; x <= xCasilla + 1; x++)
        {

            for (int y = yCasilla - 1; y <= yCasilla + 1; y++)
            {

                try
                {
                        if (!casillas[x, y].Showed)
                        {
                            casillas[x, y].showResult();

                        };
                   
                   
                }catch (Exception e) { }
                
            }

        }

    }

    public void showAllMines()
    {
        foreach(Mine mine in mines)
        {
            mine.showStateFinal();
        }
        
    }
    /*Modifica el array de minas pero las que ya estaba la dejas igual
     * SEÑOR HAY UN ESPIA ENTRE NOSOTROS!!!
     */
    public void modifyArrayMines(Mine mine, int flag)
    {
        if (flag > 0)
        {
            if (!mines.Contains(mine))
            {
                mines.Add(mine);
            }
           
        }
        else
        {
            if (mine.TypeMine != TypeMine.explosive)
            {
                mines.Remove(mine);
            }
            
        }
    }

    /*
     * Comprueba si hay mas minas de la cuenta y si no dime si todas las minas tiene su bandera
     * SEÑOR TENEMOS QUE HECHAR AL ESPIA NOS ESTA SABOTEANDO!!!
     */
    public bool allMinesWitchFlag()
    {
        //print("Minas "+mines.Count+" , "+numMinas);
        if (mines.Count == numMinas)
        {
            //print("MISMO NUMERO MINAS");
            foreach (Mine mine in mines)
            {
                if (mine.BlockType != BlockType.flag)
                    return false;
            }
        }
        else
        {
            return false;
        }
        
        return true;
    }


}
