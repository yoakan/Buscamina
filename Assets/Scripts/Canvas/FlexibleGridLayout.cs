using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    public int rows;
    public int columns;
    public Vector2 cellSize;
    public override void CalculateLayoutInputVertical()
    {
        
    }
    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        float sqrRT = Mathf.Sqrt(transform.childCount);
        rows = Mathf.CeilToInt(sqrRT);
        columns = Mathf.CeilToInt(sqrRT);
        //float 
    }
    public override void SetLayoutHorizontal()
    {
        throw new System.NotImplementedException();
    }

    public override void SetLayoutVertical()
    {
        throw new System.NotImplementedException();
    }
}
