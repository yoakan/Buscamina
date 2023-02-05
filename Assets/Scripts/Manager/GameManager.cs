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
        cantMines = minesManagers.getMines();
        infoUI.setInfoMines(cantMines);
    }
    public MinesManagers MinesManagers { get => minesManagers; set => minesManagers = value; }
    public InfoUI InfoUI { get => infoUI; set => infoUI = value; }

    public bool GameOver { get => gameOver;  }

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
    public void potFlag(int value,Mine mine)
    {
        minesManagers.modifyArrayMines(mine, value);
        flagPush += value;
        
        infoUI.setInfoMines(cantMines-flagPush);
        StartCoroutine(checkWin());
    }
    IEnumerator checkWin()
    {
        yield return null;
        if (minesManagers.allMinesWitchFlag())
        {
            InfoUI.setStateEmoji(EmojiState.winner);
        }
    }
    public void checkIfLose(TypeMine mineType)
    {
        if(mineType == TypeMine.explosive)
        {
            InfoUI.setStateEmoji(EmojiState.losser);
            minesManagers.showAllMines();

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
    }
}
