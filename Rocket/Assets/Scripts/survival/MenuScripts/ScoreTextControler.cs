using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextControler : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text recordScoreText;
    
    public void SetText()
    {
        var currentRecordScore = PlayerPrefs.GetInt("currentRecordScore");
        var score = SurvivalControler.score;

        if (score > currentRecordScore && currentRecordScore != 0)
        {
            print(currentRecordScore);
            PlayerPrefs.SetInt("currentRecordScore", score);
        }
        recordScoreText.text = PlayerPrefs.GetInt("currentRecordScore").ToString();

        scoreText.text = score.ToString();
    }

}
