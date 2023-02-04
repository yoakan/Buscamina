using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    [SerializeField]
    private Image emoji;
    [SerializeField]
    private Image[] numberMines;
    [SerializeField]
    private Image[] numberTime;
    private float timePass = -1;
    [SerializeField]
    private float timeEmoji;
    // Start is called before the first frame update
    


    // Update is called once per frame
    void Update()
    {
        if (timePass >= 0)
        {
            timePass += Time.deltaTime;
            setImageLayaout(numberTime,(int)timePass);
        }
    }

    private void setImageLayaout(Image[] numberSprite,int value)
    {
        string numberInString = reverseString(value);
        if (numberInString.Length <= numberSprite.Length)
        {
            for (int i = 0; i < numberInString.Length; i++)
            {
                numberSprite[i].sprite = SpriteUIManager.Instance.Numbers[int.Parse(numberInString[i]+"")];
            }
        }
        
    }
    private string reverseString(int value)
    {
        string valueString =value+"";
        string valueReverse="";
        for (int i = valueString.Length - 1; i >= 0; i--)
        {
            valueReverse += valueString[i];
        }
        return valueReverse;
    }
    public void startTime()
    {
        timePass = 0;
        setImageLayaout(numberTime, 0);
    }
    public void resetTime()
    {
        timePass = -1;
        setImageLayaout(numberTime, 0);
    }
    public void setInfoMines(int mines)
    {
        setImageLayaout(numberMines, mines);
    }
    public void setStateEmoji(EmojiState emoji)
    {
        switch (emoji)
        {
            case EmojiState.surprise:
                StartCoroutine(emojiTimeInSprite(emoji));
                break;
            case EmojiState.pressed:
                StartCoroutine(emojiTimeInSprite(emoji));
                break;
            default:
                this.emoji.sprite = SpriteUIManager.Instance.EmojiEstate[(int)emoji];
                break;
        }
    }

    private IEnumerator emojiTimeInSprite(EmojiState emoji)
    {

        this.emoji.sprite = SpriteUIManager.Instance.EmojiEstate[(int)emoji];
        yield return new WaitForSeconds(timeEmoji);
        this.emoji.sprite = SpriteUIManager.Instance.EmojiEstate[(int)EmojiState.defect];
    }
}
public enum EmojiState
{
    pressed =0,
    defect=1,
    surprise =2,
    winner =3,
    losser = 4
}