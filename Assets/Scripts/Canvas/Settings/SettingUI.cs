using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingUI : MonoBehaviour
{
    [SerializeField]
    private GameObject gameSettings;
    private bool mouseInSetting = false;
    private int resolutionXDefault = 210, resolutionYDefault = 295;
    private int constantPixelPerSquare = 21;

    private void Awake()
    {
        Screen.SetResolution(resolutionXDefault, resolutionYDefault, false);
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(!mouseInSetting && Input.GetMouseButton(0))
        {
            gameSettings.SetActive(false);
        }

        if(PlayerPrefs.HasKey(TabletConstants.KEY_DEFUALT + TabletConstants.KEY_SQUAREX))
        {
            changenCustomTablet();
        }
    }

    private void changenCustomTablet()
    {
        Tablet tablet = new Tablet(
            PlayerPrefs.GetInt(TabletConstants.KEY_DEFUALT + TabletConstants.KEY_SQUAREX),
            PlayerPrefs.GetInt(TabletConstants.KEY_DEFUALT + TabletConstants.KEY_SQUAREY),
            PlayerPrefs.GetInt(TabletConstants.KEY_DEFUALT + TabletConstants.KEY_MINES)
            );
        PlayerPrefs.DeleteKey(TabletConstants.KEY_DEFUALT + TabletConstants.KEY_SQUAREX);
        PlayerPrefs.DeleteKey(TabletConstants.KEY_DEFUALT + TabletConstants.KEY_SQUAREY);
        PlayerPrefs.DeleteKey(TabletConstants.KEY_DEFUALT + TabletConstants.KEY_MINES);
        instanceTablet(tablet);
    }

    public void changeDificult(int number)
    {
        Tablet tablet = TabletDefautls.tablets()[number];
        instanceTablet(tablet);

    }
    private void instanceTablet(Tablet tablet)
    {
        escaleResolution(tablet);
        GameManager.Instance.TabletManager.changeTablet(tablet);
        GameManager.Instance.resetGame();

        gameSettings.SetActive(false);
    }
    public void escaleResolution(Tablet tablet)
    {
        int valueAddX = (tablet.SquareX - TabletConstants.MIN_VALUES_SQUARE) * constantPixelPerSquare;
        int valueAddY = (tablet.SquareY - TabletConstants.MIN_VALUES_SQUARE) * constantPixelPerSquare;
        //print("WIGHT: "+ (resolutionXDefault + valueAddX)+" HEIGHT: "+(resolutionYDefault + valueAddY));
        Screen.SetResolution(resolutionXDefault+valueAddX, resolutionYDefault+valueAddY, false);
    }
    public void showGameSetting()
    {
        gameSettings.SetActive(true);
    }

    public void mouseStaySetting(bool setting)
    {
        mouseInSetting = setting;
    }
    public void customiceDificult()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(currentScene);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        
    }
}
