using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;


public class GameManager : MonoBehaviour {

    public static GameManager self;

    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private GameObject GameCanvas;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject LvlPassedPanel;
    [SerializeField] private Text LevelNumberMenu;

    public LevelManager levelManager;
    public ColorManager colorManager;
    public DataManager dataManager;
    public SceneManager sceneManager;
    public ScoreManager scoreManager;
    public BallController ballController;
    public SoundManager soundManager;

    private bool isGameOver;
    public static bool GameStarted = false;

    public static bool TapticEnabled = true;
    public static bool SoundEnabled = true;

    private void Awake()
    {

        Application.targetFrameRate = 60;

        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }


    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    void Start ()
    {
        self = this;
        GameAnalytics.Initialize();

        OpenMenu();
    }



    public void OpenMenu ()
    {
        DestroyObjects();

        MenuCanvas.SetActive(true);
        GameCanvas.SetActive(false);
        GamePanel.SetActive(true);
        GameOverPanel.SetActive(false);
        LvlPassedPanel.SetActive(false);

        InitGame();

        LevelNumberMenu.text = "Level "+(LevelManager.currentLevel+1).ToString();
    }


    public void GameStartButton()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game");

        GameStarted = true;

        MenuCanvas.SetActive(false);
        GameCanvas.SetActive(true);
        GameOverPanel.SetActive(false);
    }

    private void InitGame ()
    {
        isGameOver = false;
        GameStarted = false;

        levelManager.InitLevel();
        colorManager.InitColor();
        scoreManager.InitScore();
        sceneManager.InitSceneManager();
        ballController.InitBall();
        soundManager.InitSoundManager();
    }

    public void LevelPassed(){
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level", (LevelManager.currentLevel + 1).ToString());

        GameStarted = false;


        LvlPassedPanel.SetActive(true);
        GamePanel.SetActive(false);
    }

    public void GameOver ()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level", (LevelManager.currentLevel + 1));

        if (isGameOver) return;

        isGameOver = true;
        GameStarted = false;

        levelManager.LevelFailed();

        GameOverPanel.SetActive(true);
        GamePanel.SetActive(false);
    }

   

    private void DestroyObjects()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Platform");
        for (int i = 0; i < obj.Length; i++)
        {
            Destroy(obj[i]);
        }
       
        obj = GameObject.FindGameObjectsWithTag("diamond");
        for (int i = 0; i < obj.Length; i++)
        {
            Destroy(obj[i]);
        }
        obj = GameObject.FindGameObjectsWithTag("tube");
        for (int i = 0; i < obj.Length; i++)
        {
            Destroy(obj[i]);
        }
    }


    public void ToggleTaptic()
    {
        TapticEnabled = !TapticEnabled;
    }

    [SerializeField] GameObject linesImg;

    public void ToggleSound()
    {
        SoundEnabled = !SoundEnabled;
        linesImg.SetActive(SoundEnabled);
    }

}//end
