using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomTableSetting : MonoBehaviour
{
    // Start is called before the first frame update
    private int defaultPixelX = 240,defaultPixelY=220;
    private int idScene = 1;
    [SerializeField] private InputField heightInput,wightInput,minesInput;
    void Start()
    {
        Screen.SetResolution(defaultPixelX, defaultPixelY, false);
    }
    public void exit()
    {
        SceneManager.UnloadScene(idScene);
    }
    public void modifyTablet()
    {
        int squareX = int.Parse(wightInput.text);
        int squareY = int.Parse(heightInput.text);
        int mines = int.Parse(minesInput.text);
        if(squareX>=TabletConstants.MIN_VALUES_SQUARE && squareY > TabletConstants.MIN_VALUES_SQUARE &&
            mines >= TabletConstants.MIN_VALUES_MINES)
        {
            PlayerPrefs.SetInt(TabletConstants.KEY_DEFUALT + TabletConstants.KEY_SQUAREX, squareX);
            PlayerPrefs.SetInt(TabletConstants.KEY_DEFUALT + TabletConstants.KEY_SQUAREY, squareY);
            PlayerPrefs.SetInt(TabletConstants.KEY_DEFUALT + TabletConstants.KEY_MINES, mines);
        }
        exit();
    }
}
