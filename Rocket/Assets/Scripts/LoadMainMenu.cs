using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    [SerializeField] levelMap levelMap;
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject lockPanel;

    void Start()
    {
        UnLockBiomPanel();
        //PlayerPrefs.DeleteAll();
    }
    
    public void LoadMenu()
    {
        var currentMenu = PlayerPrefs.GetInt("currentMenu");

        PlayerPrefs.SetInt("currentMenu", levelMap.menuIndex);
        PlayerPrefs.Save();
        if (currentMenu == levelMap.menuIndex)
        {
            SceneManager.LoadSceneAsync(levelMap.firstLevelIndex);
        }
        else
        {
            SceneManager.LoadSceneAsync(levelMap.menuIndex);
        }
    }

    private void UnLockBiomPanel()
    {
        var maxUnlockBiomIndex = PlayerPrefs.GetInt("maxBiomIndex");
        print(maxUnlockBiomIndex);
        if (levelMap.biomIndex <= maxUnlockBiomIndex)
        {
            playButton.SetActive(true);
            lockPanel.SetActive(false);
        }
    }
}
