using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mine : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private TypeMine typeMine = TypeMine.none;
    private BlockType blockype = BlockType.none;

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

    private void Start()
    {
        manager = FindObjectOfType<MinesManagers>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void showResult()
    {
        if (indexBlock==0)
        {
            showed = true;
            _spriteRenderer.sprite = SpriteMineManager.Instance.Numbers[numero];
            if (numero == 0)
            {
                manager.clickMines(posX, posY);
            }
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
}
enum TypeMine
{
    none,
    explosive,
    cube
}

enum BlockType
{
    none=0,
    flag=1,
    question=2,

}