using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Singletons/SpriteUIManager")]
public class SpriteUIManager : ScriptableObject
{
    [SerializeField]
    private Sprite[] numbers;
    [SerializeField]
    private Sprite[] emojiEstate;

    public Sprite[] Numbers { get => numbers;  }
    public Sprite[] EmojiEstate { get => emojiEstate; }
}
