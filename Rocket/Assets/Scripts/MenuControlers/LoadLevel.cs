using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] levelMap levelMap;
    [SerializeField] int indexInLevelMap;
    [SerializeField] int index;
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject lockPanel;

    void Start()
    {
        UnLockPanelOfLevel();
    }


    void Update()
    {
        
    }
    public void LoadThisLevel()
    {
        for (int i = 0; i < levelMap.ScenesIndex.Length; i++)
        {
            
        }
        print(levelMap.ScenesIndex[indexInLevelMap]);
        SceneManager.LoadScene(levelMap.ScenesIndex[indexInLevelMap]);
    }

    void UnLockPanelOfLevel ()
    {
        print(PlayerPrefs.GetInt("maxUnlockLevelIndex"));
        if (index <= PlayerPrefs.GetInt("maxUnlockLevelIndex"))
        {
            playButton.SetActive(true);
            lockPanel.SetActive(false);
        }
    }

}
