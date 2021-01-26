using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuSurvivalControler : MonoBehaviour
{
    [SerializeField] int mainMenuIndex;
    private GameObject DeathMenu;

    void Start()
    {
        DeathMenu = transform.Find("DeathMenu").gameObject;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SurvivalControler.score = 0;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuIndex);
        SurvivalControler.score = 0;
    }

    public void ShowDeathMenu()
    {
        DeathMenu.SetActive(true);
    }
}
