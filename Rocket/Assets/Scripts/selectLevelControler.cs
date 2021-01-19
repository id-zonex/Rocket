using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectLevelControler : MonoBehaviour
{
    [SerializeField] GameObject scrollbar;

    public void ShowMenu()
    {
        if (scrollbar.activeSelf)
        {
            scrollbar.SetActive(false);
        }
        else
        {
            scrollbar.SetActive(true);
        }
    }

    public void LoadHome()
    {
        SceneManager.LoadScene(0);
    }
} 
