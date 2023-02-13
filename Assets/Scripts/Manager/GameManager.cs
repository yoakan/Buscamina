using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    private MinesManagers minesManagers;
    private TabletManager tabletManager;
    private InfoUI infoUI;
    private bool gameOver = false;


    private int cantMines;
    private int minesChecked =0;
    private int flagPush = 0;
    private bool firstClick = false;
    

    // Start is called before the first frame update
    // Game Instance Singleton
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Start()
    {
        cantMines = tabletManager.getMines();
        infoUI.setInfoMines(cantMines);
    }
    public MinesManagers MinesManagers { get => minesManagers; set => minesManagers = value; }
    public InfoUI InfoUI { get => infoUI; set => infoUI = value; }

    public bool GameOver { get => gameOver;  }
    public TabletManager TabletManager { get => tabletManager; set => tabletManager = value; }

    private void Awake()
    {
        infoUI = FindObjectOfType<InfoUI>();
        minesManagers = FindObjectOfType<MinesManagers>();
        tabletManager = FindObjectOfType<TabletManager>();

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }

    public static void CreateInstance()
    {
        if (instance == null)
        {
            new GameObject("GameManager", typeof(GameManager));
        }
    }
    /*Comprueba si ha empezado la guerra
     * MI SEÑOR POR EL CAMINO HAY MINAS CUIDADO POR DONDE PISA
     */
    public void isStartGame(int posClickX,int postClickY)
    {
        if (!firstClick)
        {
            firstClick = true;
            print("FIRST TOKEEEE");
            gameOver = false;
            minesManagers.generateAtributesMines(posClickX, postClickY);
            InfoUI.startTime();
        }
    }
    /*
     Añade una bandera.
    TIMI: SEÑOR por que los demas se rien de mi?
    SEÑOR: NO LO VES QUE TE HAN PUESTO LA BANDERA TURCA INVECIL???
     */
    public void potFlag(int value,Mine mine)
    {
        minesManagers.modifyArrayMines(mine, value);
        flagPush += value;
        
        infoUI.setInfoMines(cantMines-flagPush);
        StartCoroutine(checkWin());
    }
    /*
     Comprube si no queda mina sin bandera.
    SEÑOR:  HEMOS GANADO!!! Subirle el sueldo a esos niños con anginas.
     */
    IEnumerator checkWin()
    {
        yield return null;
        if (minesManagers.allMinesWitchFlag())
        {
            InfoUI.setStateEmoji(EmojiState.winner);
            gameOver = true;
        }
    }
    /*
     * Comprueba si la mina es un explisivo si es hací pierde sino muestra el emoji de sorpresa
     * Espias: Sabia que ese Señor caera en la TARTA JAJAAJHAHHAHAHAHAH
     */
    public void checkIfLose(TypeMine mineType)
    {
        if(mineType == TypeMine.explosive)
        {
            InfoUI.setStateEmoji(EmojiState.losser);
            minesManagers.showAllMines();
            gameOver = true;
        }
        else
        {
            
            InfoUI.setStateEmoji(EmojiState.surprise);
        }
    }
    
    public void resetGame()
    {
        firstClick = false;
        gameOver = false;
        infoUI.resetTime();
    }
}
