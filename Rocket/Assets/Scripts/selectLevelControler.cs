using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
} 
