using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    private MinesManagers minesManagers;
    private InfoUI infoUI;

    [SerializeField]
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
        infoUI.setInfoMines(CantMines);
    }
    public MinesManagers MinesManagers { get => minesManagers; set => minesManagers = value; }
    public InfoUI InfoUI { get => infoUI; set => infoUI = value; }
    public int CantMines { get => cantMines; set => cantMines = value; }

    private void Awake()
    {
        infoUI = FindObjectOfType<InfoUI>();

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
            minesManagers.generateAtributesMines(posClickX, postClickY);
            InfoUI.startTime();
        }
    }
    public void potFlag(int value,Mine mine)
    {
        minesManagers.modifyArrayMines(mine, value);
        flagPush += value;
        infoUI.setInfoMines(cantMines-flagPush);
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
}
