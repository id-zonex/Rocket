using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    [SerializeField] levelMap levelMap;
    void Start()
    { 
    }
    
    public void LoadMenu()
    {
        PlayerPrefs.SetInt("currentMenu", levelMap.menuIndex);
        PlayerPrefs.Save();
        SceneManager.LoadSceneAsync(levelMap.menuIndex);
    }
}
