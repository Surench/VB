using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewScoreController : MonoBehaviour {

    [SerializeField] Text text;

    [SerializeField] string[] PerfectTexts;
    [SerializeField] RectTransform rectTransform;

    public void InitNewScore(int newScore,bool isPerfect){

        string t = "";

        t = "+" + newScore.ToString();

        if(isPerfect){
            t += " " + PerfectTexts[Random.Range(0,PerfectTexts.Length)];
        }

        text.text = t;


        rectTransform.anchoredPosition3D = Vector3.zero;
        rectTransform.localEulerAngles = Vector3.zero;


        Invoke("destroyObj",3);
    }

    void destroyObj(){
        Destroy(gameObject);
    }
}
