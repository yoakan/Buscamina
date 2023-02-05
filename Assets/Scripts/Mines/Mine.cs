using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mine : MonoBehaviour
{
    private Image _spriteRenderer;
    private TypeMine typeMine = TypeMine.none;
    private BlockType blockType = BlockType.none;
    [SerializeField]private  SpriteMineManager SpriteMineManager;

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
        _spriteRenderer = GetComponent<Image>();
    }
    
    /*
     * Muestra el valor de la mina si tiene valor 0 llama al gameManager para que mire los de alrededor.  
     * SEÑOR LOS SOLDADOS SE ESTAN DESNUDANDO, QUIEREN PROTESTAR POR LOS RECORTES EN LA COMIDA.
     */
    public void showResult()
    {
        GameManager.Instance.isStartGame(posX,posY);
        if (indexBlock==0)
        {
            showed = true;
            if(typeMine != TypeMine.explosive)
            {
                _spriteRenderer.sprite = SpriteMineManager.Numbers[numero];

                if (numero == 0)
                {
                    manager.clickMines(posX, posY);
                }
                
            }
            else
            {
                _spriteRenderer.sprite = SpriteMineManager.MinesState[0];
            }
            GameManager.Instance.checkIfLose(typeMine);
        }
        
    }
    /*
     Añade un tipo de block si la mina no ha sido mostrada.
    TIMI: Joder me han metido al calaboso, eso pasa por hechar cuenta al tonto de Mike
     */
    public void blockMine()
    {
        if (!showed)
        {
            indexBlock++;
            Sprite[] blockMine = SpriteMineManager.BlockEstate;
            if (indexBlock >= blockMine.Length)
            {
                indexBlock = 0;
            }
            _spriteRenderer.sprite = blockMine[indexBlock];
            updateBlock();
            

        }
        
    }
    /*
     Actualiza el estado y añade o quita una bandera del gameManager
    MI SEÑOR PORQUE HAS COMPRADO TANTAS BANDERAS DE TURQUIA?? ACASO VAMOS A IR AL TURCO?
     */
    private void updateBlock()
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
        /*
        if(typeMine == TypeMine.explosive)
        {
            GetComponent<Image>().color = Color.red;
        }*/


    }
    /*
     Muestra el estado de las minas. Si es un inpostor o no
    Espias: por fin me puedo quitar estos zapatos estrechos y esta camisa rosa que asco joder.
    TIMI: Joder todavía sigo en el calabozo...
     */
    public void showStateFinal()
    {
        if(typeMine == TypeMine.explosive)
        {
            if (!showed && indexBlock != (int)BlockType.flag)
            {
                _spriteRenderer.sprite = SpriteMineManager.MinesState[1];
                print("HOLA ME ESTOY MOSTRANDO!!");
            }
        }
        else 
        {
            _spriteRenderer.sprite = SpriteMineManager.MinesState[2];
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

