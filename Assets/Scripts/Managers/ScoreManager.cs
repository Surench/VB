using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public int score;
    public int gems;
    public static int comboMultiplyer;

    [SerializeField] Text scoreText;
    [SerializeField] Text gemsText;


    [SerializeField] GameObject NewScorePrefab;
    [SerializeField] Transform NewScoreParent;

	void Start () {
		
	}
	
    public void InitScore()
    {
        score = 0;
        comboMultiplyer = 1;

        scoreText.text = score.ToString();

        gems = DataManager.GetGems();
        gemsText.text = gems.ToString();
    }

	public void AddScore (bool alreadyJumped)
    {
        if(alreadyJumped){
            comboMultiplyer = 1;
        }
        else{
            comboMultiplyer += 1;

            if(comboMultiplyer == 3){
                GameManager.self.ballController.enterFever();
            }
        }

        int newScore = (LevelManager.currentLevel + 1)*comboMultiplyer;

        score += newScore;

        ShowScore(newScore, !alreadyJumped);
    }


    void ShowScore(int NewScore, bool isPerfect){

        GameObject go = Instantiate(NewScorePrefab, NewScoreParent.transform.position, Quaternion.identity, NewScoreParent);
        go.GetComponent<NewScoreController>().InitNewScore(NewScore,isPerfect);

        scoreText.text = score.ToString();


    }

    public void resetCombo(){
        comboMultiplyer = 1;
        GameManager.self.ballController.exitFever();
    }

    public void AddDiamond(){
        gems += 1;
        DataManager.SetGems(gems);

        gemsText.text = gems.ToString();

    }
	
}
