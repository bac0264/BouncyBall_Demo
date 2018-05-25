using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField]
    private Text scoreText = null, scoreTextPanel = null, bestScoreTextPanel = null, yourScore = null, bestScoreTextWin = null;
    [SerializeField]
    public List<GameObject> lifeImage = new List<GameObject>();
    // life,score,lv
    public int lifeCount = 3;
    public int lv = 1;
    public int _score = 0;
    // public GameObject UIPrefabs;
    public GameObject gameController;
    //public GameObject preLifeImage;
    public GameObject image;
    public GameObject LifePanel;
    public GameObject gameOver, win;
    public GameObject repo, guidingText, UI, Ball;
    public const string HighScore = "High Score";
    //set HighScore

    //public Vector3 defaultofpos = new Vector3(140, -83, 0);
    private void Awake()
    {
        IsGameStartedForTheFirstTime();
        _createLifeImage(lifeCount);
        _makeInstance();
    }
    void IsGameStartedForTheFirstTime() {
        if (!PlayerPrefs.HasKey("IsGameStartedForTheFirstTime")) {
            PlayerPrefs.SetInt(HighScore, 0);
            PlayerPrefs.SetInt("IsGameStartedForTheFirstTime", 0);
        }
    }
    public void _setHighScore(int score) {
        if (score > _getHighScore())
        {

            PlayerPrefs.SetInt(HighScore, score);
        }
    }
    public int _getHighScore() {
        return PlayerPrefs.GetInt(HighScore);
    }
    private void _makeInstance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameController);
        }
     //   else Destroy(gameController);
    }
    public void _setScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
    public void _destroyLifeImage(bool isholding)
    {
        if (lifeImage.Count > 0 && isholding == false)
        {
            Destroy(lifeImage[lifeImage.Count - 1]);
            lifeImage.RemoveAt(lifeImage.Count - 1);
        }
    }
    public void _createLifeImage(int n)
    {
        for (int i = 0; i < n; i++)
        {
            lifeImage.Add(Instantiate(image, LifePanel.transform));
        }
    }
    public void _clearLifeImage()
    { 
        for (int i = 0; i < lifeImage.Count; i++)
        {
            Destroy(lifeImage[i]);
        }
        lifeImage.Clear();
    }
    public bool _stillAlive()
    {
        if (lifeImage.Count < 1)
            return false;
        return true;
    }
    public void _ReloadLife()
    {
        _clearLifeImage();
        _createLifeImage(lifeCount);
    }
    public void _setGameOver(int score) {
        gameOver.SetActive(true);
        scoreTextPanel.text = score.ToString();
        //if (score > _getHighScore()) {
        //    _setHighScore(score);
        //}
        bestScoreTextPanel.text = _getHighScore().ToString();
    }
    public void _setWin(int score) {
        win.SetActive(true);
        yourScore.text = score.ToString();
        //if (score > _getHighScore())
        //{
        //    _setHighScore(score);
        //}
        bestScoreTextWin.text = _getHighScore().ToString();
    }
    public void _resetGameController() {
        scoreText.text = "0";
        yourScore.text = "0";
    }
    public void _resetScoreLv() {
        _score = 0;
        lv = 1;
    }
}
