using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayButton : MonoBehaviour
{

    // Use this for initialization

    public void _playButton()
    {
        SceneManager.LoadScene("lv1");
        if (BouncyBall.instance != null)
        {
            Debug.Log("bouncyball");
            BouncyBall.instance.bouncyBall.SetActive(false);
        }
        if (GameController.instance != null)
        {
            GameController.instance._resetGameController();
            GameController.instance.guidingText.SetActive(true);
            GameController.instance.gameOver.SetActive(false);
            GameController.instance.UI.SetActive(false);
            GameController.instance.repo.SetActive(false);
            GameController.instance._ReloadLife();
            Drawer.instance._ReloadLine();
            Drawer.instance.mode = Drawer.Mode.Draw;
            Drawer.instance.drawline = true;
            TimeScale.instance.Scale = 3;
        }
        if (TypingStyle.instance != null)
        {
            TypingStyle.instance._reset();
        }
        if (BouncyBall.instance != null)
        {
            BouncyBall.instance.bouncyBall.SetActive(true);
        }
        SceneManager.LoadScene("lv1");
    }
    public void _startButton()
    { 
        SceneManager.LoadScene("lv1");
        if (Drawer.instance != null)
        {
            Drawer.instance._ReloadLine();
            Drawer.instance.mode = Drawer.Mode.Draw;
            Drawer.instance.drawline = true;
        }
        if (BouncyBall.instance != null)
        {
            Debug.Log("bouncyball");
            BouncyBall.instance.bouncyBall.SetActive(false);
        }
        else
        {
            Debug.Log("null");
        }
        if (GameController.instance != null)
        {
            GameController.instance._resetGameController();
            GameController.instance.gameOver.SetActive(false);
            GameController.instance.guidingText.SetActive(true);
            GameController.instance.repo.SetActive(false);
            GameController.instance.UI.SetActive(false);
            GameController.instance._ReloadLife();
            GameController.instance.gameController.SetActive(true);
        }
        if (TypingStyle.instance != null)
        {
            TypingStyle.instance._reset();
        }
        if (BouncyBall.instance != null)
        {
            BouncyBall.instance.bouncyBall.SetActive(true);
        }
    }
    public void _removeButton()
    {
        if (Drawer.instance != null)
        {
            Drawer.instance.RemoveModeEventButton();
            Debug.Log(Drawer.instance.mode.ToString());
        }
    }
    public void _drawButon()
    {
        if (Drawer.instance != null)
        {
            Drawer.instance.DrawModeEventButton();
            Debug.Log(Drawer.instance.mode.ToString());
        }
    }
    public void _menuButton()
    {
        if (GameController.instance != null)
        {
            Debug.Log("run menu1");
            GameController.instance.gameOver.SetActive(false);
            GameController.instance.win.SetActive(false);
            GameController.instance.UI.SetActive(false);
            GameController.instance.repo.SetActive(false);
            GameController.instance._resetGameController();
            GameController.instance._ReloadLife();
            //GameController.instance.mainCamera.setA
            Drawer.instance._ReloadLine();
            Drawer.instance.mode = Drawer.Mode.Draw;
            TimeScale.instance.Scale = 3;
            //SceneManager.LoadScene("Menu");
            // GameController.instance.gameController.SetActive(false);
        }
        SceneManager.LoadScene("Menu");
    }
    public void _stopButton()
    {
        if (TimeScale.instance != null)
        {
            TimeScale.instance.Scale = 0;
        }
    }
    public void _continueButton()
    {
        if (TimeScale.instance != null)
        {
            TimeScale.instance.Scale = 3;
        }
    }
    public void _guideButton()
    {
        if (GameController.instance != null)
        {
            GameController.instance.guidingText.SetActive(false);
            GameController.instance.repo.SetActive(true);
            GameController.instance.UI.SetActive(true);
        }
    }
}
