using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/MineManager")]
public class SpriteMineManager : ScripteableObjectSingleton<SpriteMineManager>
{
    [SerializeField]
    private Sprite[] numbers;

    [SerializeField]
    private Sprite[] blockEstate;

    [SerializeField]
    private Sprite[] mines;

    public Sprite[] BlockEstate { get => Instance.blockEstate;  }
    public Sprite[] Numbers { get => Instance.numbers;  }
    public Sprite[] MinesState { get => mines;  }
}
