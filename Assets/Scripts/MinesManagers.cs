using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinesManagers : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tablero,layaoutPrefabs,minasPrefabs;

    public int canMinasEnX=9,canMinasEnY=9;
    private  int numMinas =10;
    private Mine[,] casillas;
    private List<Mine> mines= new List<Mine>();


    void Start()
    {
        GameManager.Instance.MinesManagers = this;
        numMinas = GameManager.Instance.CantMines;
        generateTablet();
        
    }

    public void generateAtributesMines(int x,int y)
    {
        generateMines(x,y);
        generateNumbers();
    }
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
    private void generateMines(int clickX,int clickY)
    {
        mines.Clear();
        System.Random random = new System.Random();
        int posX;
        int posY;
        for(int e = 0; e < numMinas; e++)
        {
            posX = random.Next(0,canMinasEnX);
            posY = random.Next(0, canMinasEnY);

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

    public  void generateTablet()
    {
        casillas = new Mine[canMinasEnX, canMinasEnY];
        tablero.GetComponent<GridLayoutGroup>().constraintCount = canMinasEnX;

        for (int x = 0; x < casillas.GetLength(0); x++)
        {
            for(int y =0; y < casillas.GetLength(1); y++)
            {
                GameObject mineObject = Instantiate(minasPrefabs);
                Mine mine = mineObject.GetComponent<Mine>();
                mine.transform.position = transform.position;
                mine.transform.SetParent( tablero.transform);
                mine.transform.localScale = Vector3.one ;
                casillas[x, y] = mine;
                casillas[x, y].PosX = x; casillas[x, y].PosY = y;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
    public void restartTablet()
    {
        for (int x = 0; x < casillas.GetLength(0); x++)
        {
            for (int y = 0; y < casillas.GetLength(1); y++)
            {


                Destroy(casillas[x, y].gameObject);
                 
            }
        }
        generateTablet();
    }

    public bool allMinesWitchFlag()
    {
        //print("Minas "+mines.Count+" , "+numMinas);
        if (mines.Count == numMinas)
        {
            print("MISMO NUMERO MINAS");
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
