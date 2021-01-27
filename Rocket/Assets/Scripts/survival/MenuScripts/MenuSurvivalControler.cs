using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSurvivalControler : MonoBehaviour
{
    [SerializeField] int mainMenuIndex;
    [SerializeField] Text scoreText;
    [SerializeField] Text recordScoreText;
    [SerializeField] GameObject newRecordPanel;

    [SerializeField] GameObject DeathMenu;

    public void Restart()
    {
        SetRecord();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SurvivalControler.score = 0;
    }

    public void LoadMainMenu()
    {
        SetRecord();
        SceneManager.LoadScene(mainMenuIndex);
        SurvivalControler.score = 0;
    }

    public void ShowDeathMenu()
    {
        DeathMenu.SetActive(true);
    }


    public void SetTextAndRecord()
    {
        SetRecord();
        SetText();
    }

    public void SetRecord()
    {
        var currentRecordScore = PlayerPrefs.GetInt("currentRecordScore");
        var score = SurvivalControler.score;

        if (score > currentRecordScore && currentRecordScore != 0)
        {
            print(currentRecordScore);
            PlayerPrefs.SetInt("currentRecordScore", score);
        }
        recordScoreText.text = PlayerPrefs.GetInt("currentRecordScore").ToString();
    }

    public void SetText()
    {
        var score = SurvivalControler.score;
        scoreText.text = score.ToString();
    }

    public void OnNewRecord()
    {
        if (newRecordPanel)
        {
            try
            {
                StartCoroutine(NewRecordPanelSetColor());
            }
            catch (System.Exception)
            {
                Debug.LogError("please import the System.Collections library");
                throw;
            }
        }
        else
        {
            Debug.LogError("there is no record bar in this context");
        }
    }
    IEnumerator NewRecordPanelSetColor()
    {
        newRecordPanel.SetActive(true);
        var newRecordPanelRenderer = newRecordPanel.GetComponent<Image>();
        newRecordPanelRenderer.color = Color.Lerp(newRecordPanelRenderer.material.color, new Color(0, 0, 0, 0.5f), Time.time);
        yield return new WaitForSeconds(3);
        newRecordPanel.SetActive(false);
    }
}
