using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    [SerializeField] GameObject tube;
    [SerializeField] GameObject platform;
    [SerializeField] GameObject DiamondPrefab;


    public float platfromYPosition;

    float platfromsYDistance = 7;
    
      
    void Start()
    {
        
    }

    public void InitSceneManager()
    {
        transform.localEulerAngles = Vector3.zero;


        InitPlatforms();
        InitTubes();
        screenK = Screen.width / swipeMultiplier;
    }

    int spawnedPlatformsAmount = 0;

    void InitPlatforms()
    {
        spawnedPlatformsAmount = 0;
        float platformYPosition = 0;

        SpawnPlatform(GameManager.self.levelManager.firstPlatformSettings, platformYPosition, 105);

        int totalPlatformsAmount = GameManager.self.levelManager.platformsAmount - 1;
        if(GameManager.self.levelManager.DiamondsAmount>0){
            int firstSectionPlatforms = GameManager.self.levelManager.DiamondsStartIndex;

            platformYPosition = loopThroughPlatforms(platformYPosition, firstSectionPlatforms);

            platformYPosition = spawnDiamonds(platformYPosition, GameManager.self.levelManager.DiamondsAmount);

            loopThroughPlatforms(platformYPosition, totalPlatformsAmount - firstSectionPlatforms);
        }else{

            loopThroughPlatforms(platformYPosition, totalPlatformsAmount);

        }


    }


    float loopThroughPlatforms(float platformYPosition,int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            platformYPosition -= platfromsYDistance;


            List<LevelPartSettings> currentPartSettings = new List<LevelPartSettings>();

            float percentageInThePart = 0;
            float percentage = (spawnedPlatformsAmount * 1f) / (GameManager.self.levelManager.platformsAmount * 1f);
            if(percentage <= 0.33){
                currentPartSettings = GameManager.self.levelManager.levelPart1;

                percentageInThePart = percentage/0.33f;
            }else
            if (percentage <= 0.66)
            {
                currentPartSettings = GameManager.self.levelManager.levelPart2;

                percentageInThePart = (percentage-0.33f) / 0.33f;
            }
            else
            {
                currentPartSettings = GameManager.self.levelManager.levelPart3;

                percentageInThePart = (percentage - 0.66f) / 0.33f;
            }

            List<PlatformSettings> availablePlatforms = new List<PlatformSettings>();
            float percentChecked = 0;
            for (int j = 0; j < currentPartSettings.Count; j++)
            {
                if(percentageInThePart <= percentChecked+ currentPartSettings[j].percent){

                    availablePlatforms = currentPartSettings[j].availablePlatforms;

                    break;
                }

                percentChecked += currentPartSettings[j].percent;
            }

            if (percentageInThePart >=1 ){
                availablePlatforms = currentPartSettings.Last().availablePlatforms;
            }



            PlatformSettings currentPlatformSettings = availablePlatforms[Random.Range(0, availablePlatforms.Count)];

            float offsetRotation = GetPlatformOffset(spawnedPlatformsAmount);

            SpawnPlatform(currentPlatformSettings, platformYPosition, offsetRotation);

        }

        return platformYPosition;
    }


    void SpawnPlatform(PlatformSettings currentPlatformSettings, float platformYPosition, float offsetRotation)
    {
        GameObject go = Instantiate(platform, new Vector3(0, platformYPosition, 0), Quaternion.identity, transform);

        PlatformController platformController = go.GetComponent<PlatformController>();
        platformController.InitPlatform(currentPlatformSettings, offsetRotation, spawnedPlatformsAmount);

        spawnedPlatformsAmount += 1;
    }

    float spawnDiamonds(float platformYPosition, int amount)
    {
        int pattern = Random.Range(0,2);

        platformYPosition -= 15;
        float offsetRotation = 0;//Random.Range(-60, 60);
        for (int i = 0; i < amount; i++)
        {
            platformYPosition -= platfromsYDistance/1.5f;

            offsetRotation = GetDiamondPatternOffset(pattern,i,amount);

            GameObject go = Instantiate(DiamondPrefab, new Vector3(0, platformYPosition, 0), Quaternion.identity, transform);
            go.transform.localEulerAngles = new Vector3(0, offsetRotation,0);

        }

        return platformYPosition;
    }


    void InitTubes()
    {
        float tubeYPosition = 10;

        for (int i = 0; i <(GameManager.self.levelManager.platformsAmount + GameManager.self.levelManager.DiamondsAmount) * 6f; i++)
        {            
            GameObject go = Instantiate(tube, new Vector3(0, tubeYPosition, 0), Quaternion.identity, transform);
            go.GetComponent<TubeController>().InitTube(i%2==0);

            tubeYPosition -= 2;
        }
    }




    // Rotation

    [SerializeField]
    private float swipeMultiplier;
    private Vector3 RotationBeforeDrag;
    private bool isDragging_buffer = false;

    public static bool isDragging;
    private Vector2 startTouch;
    private Vector2 lastTouch;
    public float currentMagnitude;
    public float currentDeltaMagnitude;
    public static Vector3 NewCurrentRot;
    private float screenK = 0;


    void Update()
    {
        ProcessSceneRotation();
    }

    void ProcessSceneRotation()
    {

        if (!GameManager.GameStarted) return; //Do not process rotation if the game is not started


        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            startTouch = Input.mousePosition;
            lastTouch = startTouch;


            isDragging_buffer = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        };

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDragging = true;
                startTouch = Input.touches[0].position;
                lastTouch = startTouch;

                isDragging_buffer = false;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragging = false;
            };
        }

        float fixedDeltaMagnitude=0;
        if (isDragging)
        {

            if (Input.touches.Length > 0)
            {
                currentMagnitude = Input.touches[0].position.x - startTouch.x;
                currentDeltaMagnitude = Input.touches[0].position.x - lastTouch.x;

                lastTouch = Input.touches[0].position;
            }

            else if (Input.GetMouseButton(0))
            {
                currentMagnitude = Input.mousePosition.x - startTouch.x;
                currentDeltaMagnitude = Input.mousePosition.x - lastTouch.x;
                lastTouch = Input.mousePosition;
            }

           /*if (Mathf.Abs(currentDeltaMagnitude) > 65)
            {
                fixedDeltaMagnitude = currentDeltaMagnitude / 7;
            }
            else
            {
                fixedDeltaMagnitude = currentDeltaMagnitude;
            }*/
            fixedDeltaMagnitude = currentDeltaMagnitude;
        }

        if (isDragging != isDragging_buffer && isDragging)
        {
            RotationBeforeDrag = transform.localEulerAngles;
        }
        else
        {
            NewCurrentRot = transform.localEulerAngles;
        }

        isDragging_buffer = isDragging;

        if (isDragging)
        {
            //NewCurrentRot = RotationBeforeDrag - new Vector3(0, currentMagnitude, 0) / screenK;
            //transform.localEulerAngles = NewCurrentRot;
            transform.localEulerAngles -= new Vector3(0, fixedDeltaMagnitude, 0) / screenK;
        }

    }



    float GetDiamondPatternOffset(int patternIndex, int DiamondIndex, int TotalDiamonds){

        float offset = 0;

        switch(patternIndex){
            case 0:
                offset = DiamondIndex * 20;
                break;
            case 1:

                if(DiamondIndex < TotalDiamonds/2){
                    offset = 0;
                }else{
                    offset = 30;
                }

                break;
           
            default: break;
        }


        return offset;

    }


    float GetPlatformOffset(int PlatformIndex){
        float offset = 0;


        if(LevelManager.currentLevel == 0){

            if (PlatformIndex == 0)
            {
                offset = 0;
            }
            else if (PlatformIndex == 1)
            {
                offset = 150;
            }
            else if (PlatformIndex == 2)
            {
                offset = 0;
            }
            else if (PlatformIndex < 9)
            {
                offset = 150;
            }else if(PlatformIndex == 9){
                offset = 0;
            }
            else if (PlatformIndex == 10)
            {
                offset = 150;
            }
            else
            {
                offset = 0;
            }

        }
            else{
            offset = Random.Range(-120, 120);
        }

        return offset;

       
    }

}// end
