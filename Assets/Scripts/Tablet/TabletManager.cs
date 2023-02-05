using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabletManager : MonoBehaviour
{
    public GameObject tablero, layaoutPrefabs, minasPrefabs;
    private Tablet tablet;
    private int numMinas = 10;
    private Mine[,] casillas;
    private List<Mine> mines = new List<Mine>();

    private void Awake()
    {
        tablet = TabletDefautls.Begginer();
        numMinas = tablet.NumMines;
    }

    void Start()
    {

        generateTablet();

    }
    public Mine[,] generateTablet()
    {
        casillas = new Mine[tablet.SquareX, tablet.SquareX];
        tablero.GetComponent<GridLayoutGroup>().constraintCount = tablet.SquareX;

        for (int x = 0; x < casillas.GetLength(0); x++)
        {
            for (int y = 0; y < casillas.GetLength(1); y++)
            {
                GameObject mineObject = Instantiate(minasPrefabs);
                Mine mine = mineObject.GetComponent<Mine>();
                mine.transform.position = transform.position;
                mine.transform.SetParent(tablero.transform);
                mine.transform.localScale = Vector3.one;
                casillas[x, y] = mine;
                casillas[x, y].PosX = x; casillas[x, y].PosY = y;
            }
        }
        GameManager.Instance.MinesManagers.Casillas = casillas;
        return casillas;
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
    // Start is called before the first frame update

}
