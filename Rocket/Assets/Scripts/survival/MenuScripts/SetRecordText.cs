using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetRecordText : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            var currentRecordScore = PlayerPrefs.GetInt("currentRecordScore");
            var score = SurvivalControler.score;
            if (score > currentRecordScore && currentRecordScore != 0)
            {
                print(currentRecordScore);
                PlayerPrefs.SetInt("currentRecordScore", score);
            }
            text.text = PlayerPrefs.GetInt("currentRecordScore").ToString();
        }
    }
}
