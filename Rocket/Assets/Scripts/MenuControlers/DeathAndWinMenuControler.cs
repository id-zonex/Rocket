using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathAndWinMenuControler : MonoBehaviour
{
    [SerializeField] levelMap levelMap;
	[SerializeField] GameObject DeathMenu;
	[SerializeField] GameObject WinMenu;

	public int levelIndex;

	public void ShowWinMenu()
    {
		WinMenu.SetActive(true);
    }

	public void ShowDeathMenu()
    {
		DeathMenu.SetActive(true);
    }

    public void Restart()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

    public void Home()
    {
		SceneManager.LoadScene(levelMap.menuIndex);
    }

	public void LoadNextLevel() // Finish
	{
		int currentLevelIndex = Array.IndexOf(levelMap.ScenesIndex, SceneManager.GetActiveScene().buildIndex);
		int nextLevelIndex = currentLevelIndex + 1;

		//Load MainMenu
		if (nextLevelIndex == levelMap.sceneCount)
		{
			var maxBiomIndex = PlayerPrefs.GetInt("maxBiomIndex");
			if (maxBiomIndex == levelMap.biomIndex)
			{
				PlayerPrefs.SetInt("maxBiomIndex", maxBiomIndex + 1);
				PlayerPrefs.SetInt("maxUnlockLevelIndex", PlayerPrefs.GetInt("maxUnlockLevelIndex") + 1);
			}
			SceneManager.LoadSceneAsync(levelMap.menuIndex);
			print("load main menu");
		}
		//Load Next Level
		else
		{
			var maxUnlockLevelIndex = PlayerPrefs.GetInt("maxUnlockLevelIndex");
			print(maxUnlockLevelIndex);
			if (maxUnlockLevelIndex == levelIndex)
			{
				print(maxUnlockLevelIndex);
				PlayerPrefs.SetInt("maxUnlockLevelIndex", maxUnlockLevelIndex + 1);
			}
			SceneManager.LoadSceneAsync(levelMap.ScenesIndex[nextLevelIndex]);
			print("load first level");

		}
	}
}
