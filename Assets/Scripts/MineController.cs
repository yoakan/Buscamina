using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MineController : MonoBehaviour
{
    private Mine mine;

    public void clickMine(BaseEventData eventData)
    {
       
        PointerEventData ped = (PointerEventData)eventData;
        if (ped.button == PointerEventData.InputButton.Left)
        {
            
            mine.showResult();
        }
        else if(ped.button== PointerEventData.InputButton.Right)
        {
            mine.blockMine();
        }
        //print("TYPE CLICK: "+ ped.button);
    }

    private void Start()
    {
        mine = GetComponent<Mine>();
    }


    
}
