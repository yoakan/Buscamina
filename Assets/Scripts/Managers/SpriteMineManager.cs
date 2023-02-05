using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/MineManager")]
public class SpriteMineManager : ScriptableObject
{
    [SerializeField]
    private Sprite[] numbers;

    [SerializeField]
    private Sprite[] blockEstate;

    [SerializeField]
    private Sprite[] mines;

    public Sprite[] BlockEstate { get => blockEstate;  }
    public Sprite[] Numbers { get => numbers;  }
    public Sprite[] MinesState { get => mines;  }


}
