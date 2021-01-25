using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScoreText : MonoBehaviour
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
            text.text = SurvivalControler.score.ToString();
        }
    }
}
