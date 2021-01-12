using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Paused : MonoBehaviour
{
    [SerializeField] GameObject Panel;

    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject deathMenu;


    private void Start()
    {
    }

    bool isPause = true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Pause(isPause);
            isPause = !isPause;
        }
    }

    public void Pause (bool isPause)
    {
        if (!(winMenu.activeSelf || deathMenu.activeSelf))
        {
            if (isPause)
            {
                print("lol");
                Time.timeScale = 0;
                Panel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                Panel.SetActive(false);
            }
        }
    }

    public void LoadScene(int sceneIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
    }
}
