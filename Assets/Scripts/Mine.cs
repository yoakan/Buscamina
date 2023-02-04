using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mine : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private TypeMine typeMine = TypeMine.none;
    private BlockType blockType = BlockType.none;

    private bool showed = false;

    private int numero = -1;
    private int indexBlock = 0;

    private int posX=-1;

    private int posY=-1;
    public GameObject text;
    MinesManagers manager=null;
    public bool Showed { get => showed; set => showed = value; }
    public int Numero { get => numero; set => numero = value; }
    internal TypeMine TypeMine { get => typeMine; set => typeMine = value; }
    public int PosX { get => posX; set => posX = value; }
    public int PosY { get => posY; set => posY = value; }
    public BlockType BlockType { get => blockType;  }

    private void Start()
    {
        manager = FindObjectOfType<MinesManagers>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void showResult()
    {
        GameManager.Instance.isStartGame(posX,posY);
        if (indexBlock==0)
        {
            showed = true;
            if(typeMine != TypeMine.explosive)
            {
                _spriteRenderer.sprite = SpriteMineManager.Instance.Numbers[numero];

                if (numero == 0)
                {
                    manager.clickMines(posX, posY);
                }
                
            }
            else
            {
                _spriteRenderer.sprite = SpriteMineManager.Instance.MinesState[0];
            }
            GameManager.Instance.checkIfLose(typeMine);
        }
        
    }
    public void blockMine()
    {
        if (!showed)
        {
            indexBlock++;
            Sprite[] blockMine = SpriteMineManager.Instance.BlockEstate;
            if (indexBlock >= blockMine.Length)
            {
                indexBlock = 0;
            }
            _spriteRenderer.sprite = blockMine[indexBlock];
            updateInGame();
            

        }
        
    }

    private void updateInGame()
    {
        switch (indexBlock)
        {
            case (int)BlockType.flag:
                GameManager.Instance.potFlag(1,this);blockType = BlockType.flag;
                break;
            case (int)BlockType.question:
                GameManager.Instance.potFlag(-1,this); blockType = BlockType.none;
                break;
        }
    }

    private void Update()
    {
        if(typeMine == TypeMine.explosive)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (typeMine == TypeMine.cube)
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        if(numero!=-1 /*&&  numero !=0*/ && showed)
            text.GetComponent<Text>().text = "" + numero;
    }
    public void showStateFinal()
    {
        if(typeMine == TypeMine.explosive)
        {
            if (!showed && indexBlock != (int)BlockType.flag)
            {
                _spriteRenderer.sprite = SpriteMineManager.Instance.MinesState[1];
                print("HOLA ME ESTOY MOSTRANDO!!");
            }
        }
        else 
        {
            _spriteRenderer.sprite = SpriteMineManager.Instance.MinesState[2];
        }
    }
}
public enum TypeMine
{
    none,
    explosive,
    cube
}

public enum BlockType
{
    none=0,
    flag=1,
    question=2,

}

