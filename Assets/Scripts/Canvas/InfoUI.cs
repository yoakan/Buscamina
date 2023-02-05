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

    public void resetClick()
    {
        setStateEmoji(EmojiState.pressed);
        GameManager.Instance.TabletManager.restartTablet();
        GameManager.Instance.resetGame();
    }
    /*
     * Añade el numero en formato que pueda ser representado por la pantalla, al ser 3 imagenes hay que mostrarlo en el orden correspondiente.
     * SEÑOR NUESTRO INDICADORES ESTAN FALLANDO.
     * SEÓR: MIERDAAA LA PROXIMA VEZ NO LO COMPRO POR ALIEXPRESS
     */
    private void setImageLayaout(Image[] numberSprite,int value)
    {
        string numberInString = reverseString(value);
        if (numberInString.Length <= numberSprite.Length)
        {
            for (int i = 0; i < numberInString.Length; i++)
            {
                string chart = numberInString[i] + "";


                numberSprite[i].sprite = (chart!="-")?SpriteUIManager.Instance.Numbers[int.Parse(chart)]: SpriteUIManager.Instance.Numbers[10];
            }
        }
        
    }

    /*Da la vuelta a la cadena y añade sero si lo necesita las imagenes
     * SEÑOR COMO RECOMENDACION LA PROXIMA VEZ TORNILLOS DE PAPEL QUE NO DURAN!!
     */
    private string reverseString(int value)
    {
        string valueString =value+"";
        for (int i = 0; i < numberMines.Length; i++)
        {
            valueString = "0"+value;
        }
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
    /*
     Cambia el icono del emoji si es surprise y pressed los deja por unos segundos.
    TIMY: Muchas gracias por la pastilla sonriente señor!! Seguro que me ayudara con la tost! 
     */
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
                timePass = -1;
                break;
        }
    }
    /*
     * Espera unos segundo y vuelve al emoji por defecto
     * TIMY: Joder que ha pasado, esa pastilla me ha sentado mal...
     */
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