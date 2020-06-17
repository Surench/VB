using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlatformSettings
{
    public List<int> DisabledBlocks = new List<int>();
    public List<int> EnabledRedBlocks = new List<int>();
    public List<int> RotatingRedBlocks = new List<int>();
    public List<int> RotatingNormalBlocks = new List<int>();
    public float rotatingRedSpeed = 10;
    public float rotatingRedAngle = 45;
}

public class LevelPartSettings
{
    public float percent;
    public List<PlatformSettings> availablePlatforms = new List<PlatformSettings>();
}

public class LevelSettings
{
    public int currentLevel;
}


public class LevelManager : MonoBehaviour {

    public PlatformSettings firstPlatformSettings = new PlatformSettings();

    public static int currentLevel;
    
    public int platformsAmount = 50;
    public int DiamondsStartIndex = 20;
    public int DiamondsAmount = 10;


    int PassedPlatformsAmount = 0;


    [SerializeField] private Slider levelSlider;
    [SerializeField] private Text currentLevelText;
    [SerializeField] private Text nextLevelText;

    [SerializeField] private Text LevelFailedText;
    [SerializeField] private Text LevelCompletedText;


    public void InitLevel ()
    {

        levelPart1.Clear();
        levelPart2.Clear();
        levelPart3.Clear();


        OrderPlatforms();

        currentLevel = DataManager.GetLevelSettings().currentLevel;

        CalculateHardness();

        PassedPlatformsAmount = 0;

        levelSlider.value = 0;
        currentLevelText.text = (currentLevel + 1).ToString();
        nextLevelText.text = (currentLevel + 2).ToString();



    }


    public List<LevelPartSettings> levelPart1 = new List<LevelPartSettings>();
    public List<LevelPartSettings> levelPart2 = new List<LevelPartSettings>();
    public List<LevelPartSettings> levelPart3 = new List<LevelPartSettings>();


    private void CalculateHardness()
    {
        firstPlatformSettings = GetPlatformSettings(0);

        LevelPartSettings levelPartSettings = new LevelPartSettings();


        if (currentLevel < 5)
        {
            //Part 1
            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 1f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart1.Add(levelPartSettings);

            //Part 2
            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.5f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart2.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.5f;
            levelPartSettings.availablePlatforms = mediumPlatforms;

            levelPart2.Add(levelPartSettings);


            //Part 3
            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.5f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart3.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.5f;
            levelPartSettings.availablePlatforms = mediumPlatforms;

            levelPart3.Add(levelPartSettings);
        }
        else if (currentLevel < 10)
        {
            //Part 1
            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.5f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart1.Add(levelPartSettings);


            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.5f;
            levelPartSettings.availablePlatforms = mediumAllPlatforms;

            levelPart1.Add(levelPartSettings);

            //Part 2
            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart2.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = mediumPlatforms;

            levelPart2.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.4f;
            levelPartSettings.availablePlatforms = mediumAllPlatforms;

            levelPart2.Add(levelPartSettings);


            //Part 3
            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart3.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.7f;
            levelPartSettings.availablePlatforms = mediumAllPlatforms;

            levelPart3.Add(levelPartSettings);
        }
        else if (currentLevel < 20)
        {
            //Part 1
            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart1.Add(levelPartSettings);


            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = mediumAllPlatforms;

            levelPart1.Add(levelPartSettings);


            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.4f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart1.Add(levelPartSettings);

            //Part 2
            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = mediumAllPlatforms;

            levelPart2.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.2f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart2.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.5f;
            levelPartSettings.availablePlatforms = mediumPlatforms;

            levelPart2.Add(levelPartSettings);


            //Part 3

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = mediumAllPlatforms;

            levelPart3.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart3.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.4f;
            levelPartSettings.availablePlatforms = mediumPlatforms;

            levelPart3.Add(levelPartSettings);
        }
        else if (currentLevel < 30)
        {
            //Part 1
            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.5f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart1.Add(levelPartSettings);


            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.5f;
            levelPartSettings.availablePlatforms = mediumAllPlatforms;

            levelPart1.Add(levelPartSettings);


            //Part 2
            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.5f;
            levelPartSettings.availablePlatforms = mediumAllPlatforms;

            levelPart2.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.2f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart2.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = mediumPlatforms;

            levelPart2.Add(levelPartSettings);


            //Part 3

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.1f;
            levelPartSettings.availablePlatforms = hardPlatforms;

            levelPart3.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.2f;
            levelPartSettings.availablePlatforms = mediumPlatforms;

            levelPart3.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.7f;
            levelPartSettings.availablePlatforms = mediumAllPlatforms;

            levelPart3.Add(levelPartSettings);
        }
        else if (currentLevel < 50)
        {
            //Part 1
            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.2f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart1.Add(levelPartSettings);


            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = mediumAllPlatforms;

            levelPart1.Add(levelPartSettings);


            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.1f;
            levelPartSettings.availablePlatforms = hardPlatforms;

            levelPart1.Add(levelPartSettings);


            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.4f;
            levelPartSettings.availablePlatforms = mediumPlatforms;

            levelPart1.Add(levelPartSettings);

            //Part 2
            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.1f;
            levelPartSettings.availablePlatforms = hardRotatingPlatforms;

            levelPart2.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = mediumAllPlatforms;

            levelPart2.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.6f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart2.Add(levelPartSettings);


            //Part 3

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = hardAllPlatforms;

            levelPart3.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = mediumAllPlatforms;

            levelPart3.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.4f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart3.Add(levelPartSettings);
        }
        else if (currentLevel < 80)
        {
            //Part 1
            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart1.Add(levelPartSettings);


            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = mediumAllPlatforms;

            levelPart1.Add(levelPartSettings);


            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.4f;
            levelPartSettings.availablePlatforms = hardPlatforms;

            levelPart1.Add(levelPartSettings);

            //Part 2
            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart2.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = hardAllPlatforms;

            levelPart2.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.4f;
            levelPartSettings.availablePlatforms = mediumPlatforms;

            levelPart2.Add(levelPartSettings);


            //Part 3

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = hardAllPlatforms;

            levelPart3.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = mediumAllPlatforms;

            levelPart3.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.4f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart3.Add(levelPartSettings);
        }
        else
        {
            //Part 1
            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart1.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.7f;
            levelPartSettings.availablePlatforms = mediumAllPlatforms;

            levelPart1.Add(levelPartSettings);

            //Part 2
            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.5f;
            levelPartSettings.availablePlatforms = mediumAllPlatforms;

            levelPart2.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart2.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.2f;
            levelPartSettings.availablePlatforms = hardAllPlatforms;

            levelPart2.Add(levelPartSettings);

            //Part 3
            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.5f;
            levelPartSettings.availablePlatforms = mediumAllPlatforms;

            levelPart3.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.2f;
            levelPartSettings.availablePlatforms = easyPlatforms;

            levelPart3.Add(levelPartSettings);

            levelPartSettings = new LevelPartSettings();
            levelPartSettings.percent = 0.3f;
            levelPartSettings.availablePlatforms = hardAllPlatforms;

            levelPart3.Add(levelPartSettings);
        }


        if(currentLevel < 5){
            platformsAmount = 16;
        }
        else if(currentLevel < 20){
            platformsAmount = 20;
        }
        else if (currentLevel < 20)
        {
            platformsAmount = 24;
        }else{
            platformsAmount = 30;
        }

        bool showDiamonds = Random.Range(0, 10) < 4;
        if(showDiamonds){
            DiamondsAmount = 20;

            DiamondsStartIndex = Random.Range(10, platformsAmount-4);
        }else{
            DiamondsAmount = 0;
        }


    }

    public bool PlatformPassed(){
        PassedPlatformsAmount += 1;

        bool levelPassed = PassedPlatformsAmount == platformsAmount;

        if(levelPassed)
        {
            LevelPassed();
        }


        updateLevelSlider();

        return levelPassed;
    }

    void LevelPassed(){

        GameManager.self.LevelPassed();

        LevelCompletedText.text = "Level " + (currentLevel + 1).ToString() + " Passed";


        LevelSettings levelSettings = DataManager.GetLevelSettings();
        levelSettings.currentLevel += 1;
        DataManager.SetLevelSettings(levelSettings);

        GameManager.self.ballController.LevelPassed();

    }

    public void LevelFailed(){

        float percentPassed = Mathf.Floor((PassedPlatformsAmount * 1f / platformsAmount * 1f)*100f);

        LevelFailedText.text = "Level " + (currentLevel + 1).ToString() + " Failed\nCompleted " + percentPassed.ToString() + "%";
    }


    void updateLevelSlider(){
        levelSlider.value = (PassedPlatformsAmount * 1f / platformsAmount * 1f);
    }










    public List<PlatformSettings> easyPlatforms = new List<PlatformSettings>();
    public List<PlatformSettings> mediumPlatforms = new List<PlatformSettings>();
    public List<PlatformSettings> mediumRotatingPlatforms = new List<PlatformSettings>();
    public List<PlatformSettings> mediumAllPlatforms = new List<PlatformSettings>();
    public List<PlatformSettings> hardPlatforms = new List<PlatformSettings>();
    public List<PlatformSettings> hardRotatingPlatforms = new List<PlatformSettings>();
    public List<PlatformSettings> hardAllPlatforms = new List<PlatformSettings>();



    void OrderPlatforms(){


        //easy
        easyPlatforms.Add(GetPlatformSettings(0));
        easyPlatforms.Add(GetPlatformSettings(1));
        easyPlatforms.Add(GetPlatformSettings(2));
        easyPlatforms.Add(GetPlatformSettings(3));
        easyPlatforms.Add(GetPlatformSettings(5));
        easyPlatforms.Add(GetPlatformSettings(9));
        easyPlatforms.Add(GetPlatformSettings(18));
        easyPlatforms.Add(GetPlatformSettings(20));
        easyPlatforms.Add(GetPlatformSettings(24));
        easyPlatforms.Add(GetPlatformSettings(25));


        //mediums
        mediumPlatforms.Add(GetPlatformSettings(4));
        mediumPlatforms.Add(GetPlatformSettings(5));
        mediumPlatforms.Add(GetPlatformSettings(21));
        mediumPlatforms.Add(GetPlatformSettings(23));
        mediumPlatforms.Add(GetPlatformSettings(27));

        mediumRotatingPlatforms.Add(GetPlatformSettings(28));
        mediumRotatingPlatforms.Add(GetPlatformSettings(29));
        mediumRotatingPlatforms.Add(GetPlatformSettings(35));
        mediumRotatingPlatforms.Add(GetPlatformSettings(36));

        mediumAllPlatforms = mediumPlatforms.Concat(mediumRotatingPlatforms).ToList();



        //hards
        hardPlatforms.Add(GetPlatformSettings(6));
        hardPlatforms.Add(GetPlatformSettings(7));
        hardPlatforms.Add(GetPlatformSettings(8));
        hardPlatforms.Add(GetPlatformSettings(19));
        hardPlatforms.Add(GetPlatformSettings(22));
        hardPlatforms.Add(GetPlatformSettings(26));

        hardRotatingPlatforms.Add(GetPlatformSettings(30));
        hardRotatingPlatforms.Add(GetPlatformSettings(31));
        hardRotatingPlatforms.Add(GetPlatformSettings(32));
        hardRotatingPlatforms.Add(GetPlatformSettings(33));
        hardRotatingPlatforms.Add(GetPlatformSettings(34));

        hardAllPlatforms = hardPlatforms.Concat(hardRotatingPlatforms).ToList();


    }



    private PlatformSettings GetPlatformSettings(int PlatformType)
    {

        PlatformSettings platformSettings = new PlatformSettings();

        platformSettings.DisabledBlocks = new List<int>();
        platformSettings.EnabledRedBlocks = new List<int>();
        platformSettings.RotatingRedBlocks = new List<int>(); 
        platformSettings.RotatingNormalBlocks = new List<int>(); 

        switch (PlatformType)
        {

            case 0:
                platformSettings.DisabledBlocks = new List<int>() { 0 };
                break;
            case 1:
                platformSettings.DisabledBlocks = new List<int>() { 0 };
                platformSettings.EnabledRedBlocks = new List<int>() { 1 };
                break;
            case 2:
                platformSettings.DisabledBlocks = new List<int>() { 0 };
                platformSettings.EnabledRedBlocks = new List<int>() { 5 };
                break;
            case 3:
                platformSettings.DisabledBlocks = new List<int>() { 0 };
                platformSettings.EnabledRedBlocks = new List<int>() { 11 };
                break;
            case 4:
                platformSettings.DisabledBlocks = new List<int>() { 0 };
                platformSettings.EnabledRedBlocks = new List<int>() { 1,2 };
                break;
            case 5:
                platformSettings.DisabledBlocks = new List<int>() { 0 };
                platformSettings.EnabledRedBlocks = new List<int>() { 5,6 };
                break;
            case 6:
                platformSettings.DisabledBlocks = new List<int>() { 0 };
                platformSettings.EnabledRedBlocks = new List<int>() { 1,11 };
                break;
            case 7:
                platformSettings.DisabledBlocks = new List<int>() { 0 };
                platformSettings.EnabledRedBlocks = new List<int>() { 1, 3,5,7,9,11 };
                break;
            case 8:
                platformSettings.DisabledBlocks = new List<int>() { 0 };
                platformSettings.EnabledRedBlocks = new List<int>() { 1, 2, 10, 11 };
                break;
            case 9:
                platformSettings.DisabledBlocks = new List<int>() { 0,1 };
                break;
            case 10:
                platformSettings.DisabledBlocks = new List<int>() { 0, 1 };
                platformSettings.EnabledRedBlocks = new List<int>() { 2 };
                break;
            case 11:
                platformSettings.DisabledBlocks = new List<int>() { 0, 1 };
                platformSettings.EnabledRedBlocks = new List<int>() { 5 };
                break;
            case 12:
                platformSettings.DisabledBlocks = new List<int>() { 0, 1 };
                platformSettings.EnabledRedBlocks = new List<int>() { 11 };
                break;
            case 13:
                platformSettings.DisabledBlocks = new List<int>() { 0, 1 };
                platformSettings.EnabledRedBlocks = new List<int>() { 2, 3 };
                break;
            case 14:
                platformSettings.DisabledBlocks = new List<int>() { 0, 1 };
                platformSettings.EnabledRedBlocks = new List<int>() { 5, 6 };
                break;
            case 15:
                platformSettings.DisabledBlocks = new List<int>() { 0, 1 };
                platformSettings.EnabledRedBlocks = new List<int>() { 2, 11 };
                break;
            case 16:
                platformSettings.DisabledBlocks = new List<int>() { 0, 1 };
                platformSettings.EnabledRedBlocks = new List<int>() { 2, 4, 6, 9, 10 };
                break;
            case 17:
                platformSettings.DisabledBlocks = new List<int>() { 0, 1 };
                platformSettings.EnabledRedBlocks = new List<int>() { 2, 3, 11 };
                break;
            case 18:
                platformSettings.DisabledBlocks = new List<int>() { 0, 1, 2 };
                break;
            case 19:
                platformSettings.DisabledBlocks = new List<int>() { 0, 1, 2 };
                platformSettings.EnabledRedBlocks = new List<int>() { 3,7, 11 };
                break;
            case 20:
                platformSettings.DisabledBlocks = new List<int>() { 0, 2 };
                break;
            case 21:
                platformSettings.DisabledBlocks = new List<int>() { 0, 2 };
                platformSettings.EnabledRedBlocks = new List<int>() { 3, 11 };
                break;
            case 22:
                platformSettings.DisabledBlocks = new List<int>() { 0, 2 };
                platformSettings.EnabledRedBlocks = new List<int>() { 1,3, 11 };
                break;
            case 23:
                platformSettings.DisabledBlocks = new List<int>() { 0, 2,4,6 };
                platformSettings.EnabledRedBlocks = new List<int>() { 7, 11 };
                break;

            case 24:
                platformSettings.DisabledBlocks = new List<int>() { 0, 2, 7, 8 };
                break;
            case 25:
                platformSettings.DisabledBlocks = new List<int>() { 0, 2, 7, 8 };
                platformSettings.EnabledRedBlocks = new List<int>() { 6};
                break;
            case 26:
                platformSettings.DisabledBlocks = new List<int>() { 0, 2, 7, 8 };
                platformSettings.EnabledRedBlocks = new List<int>() { 3,6,9,11 };
                break;

            case 27:
                platformSettings.DisabledBlocks = new List<int>() { 0, 2, 4, 6 };
                platformSettings.EnabledRedBlocks = new List<int>() { 5, 11 };
                break;
            case 28:
                platformSettings.DisabledBlocks = new List<int>() { 0};
                platformSettings.EnabledRedBlocks = new List<int>() { 5 };
                platformSettings.RotatingRedBlocks = new List<int>() { 5 };
                platformSettings.rotatingRedSpeed = 1;
                platformSettings.rotatingRedAngle = 45;
                break;
            case 29:
                platformSettings.DisabledBlocks = new List<int>() { 0 };
                platformSettings.EnabledRedBlocks = new List<int>() { 5 };
                platformSettings.RotatingRedBlocks = new List<int>() { 5 };
                platformSettings.rotatingRedSpeed = 0.3f;
                platformSettings.rotatingRedAngle = 180;
                break;
            case 30:
                platformSettings.DisabledBlocks = new List<int>() { 0,2,3,6,7 };
                platformSettings.EnabledRedBlocks = new List<int>() { 5 };
                platformSettings.RotatingRedBlocks = new List<int>() { 5 };
                platformSettings.RotatingNormalBlocks = new List<int>() { 4 };
                platformSettings.rotatingRedSpeed = 0.3f;
                platformSettings.rotatingRedAngle = 180;
                break;
            case 31:
                platformSettings.DisabledBlocks = new List<int>() { 0, 3, 6, 9, 2, 5, 8, 11 };
                platformSettings.EnabledRedBlocks = new List<int>() { 2,5,8,11 };
                platformSettings.RotatingRedBlocks = new List<int>() { 2, 5, 8, 11 };
                platformSettings.RotatingNormalBlocks = new List<int>() { 1,4,7,10 };
                platformSettings.rotatingRedSpeed = 0.3f;
                platformSettings.rotatingRedAngle = 180;
                break;
            case 32:
                platformSettings.DisabledBlocks = new List<int>() { 0, 1, 2, 3 };
                platformSettings.EnabledRedBlocks = new List<int>() { 1,8 };
                platformSettings.RotatingRedBlocks = new List<int>() { 1 };
                platformSettings.rotatingRedSpeed = 0.3f;
                platformSettings.rotatingRedAngle = 120;
                break;
            case 33:
                platformSettings.DisabledBlocks = new List<int>() { 0, 1, 2, 3,7,8,9 };
                platformSettings.EnabledRedBlocks = new List<int>() { 1, 7 };
                platformSettings.RotatingRedBlocks = new List<int>() { 1,7 };
                platformSettings.rotatingRedSpeed = 0.3f;
                platformSettings.rotatingRedAngle = 120;
                break;
            case 34:
                platformSettings.DisabledBlocks = new List<int>() { 0, 1, 4,5 };
                platformSettings.EnabledRedBlocks = new List<int>() { 1 };
                platformSettings.RotatingRedBlocks = new List<int>() { 1 };
                platformSettings.RotatingNormalBlocks = new List<int>() { 2,3 };
                platformSettings.rotatingRedSpeed = 0.3f;
                platformSettings.rotatingRedAngle = 120;
                break;
            case 35:
                platformSettings.DisabledBlocks = new List<int>() { 0, 1,2 };
                platformSettings.EnabledRedBlocks = new List<int>() { 4 };
                platformSettings.RotatingRedBlocks = new List<int>() { 4 };
                platformSettings.rotatingRedSpeed = 0.3f;
                platformSettings.rotatingRedAngle = 80;
                break;
            case 36:
                platformSettings.DisabledBlocks = new List<int>() { 0, 1, 2 };
                platformSettings.EnabledRedBlocks = new List<int>() { 4,7 };
                platformSettings.RotatingRedBlocks = new List<int>() { 4,7 };
                platformSettings.rotatingRedSpeed = 0.3f;
                platformSettings.rotatingRedAngle = 80;
                break;


            default: break;
        }

        return platformSettings;
    }



}//end
