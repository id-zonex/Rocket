using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rocket : MonoBehaviour {

	[SerializeField] int levelIndex;
	[SerializeField] levelMap levelMap;
	[SerializeField] float rotSpeed = 100f;
	[SerializeField] float flySpeed = 100f;
	[SerializeField] AudioClip flySound;
	[SerializeField] AudioClip boomSound;
	[SerializeField] AudioClip finishSound;
	[SerializeField] ParticleSystem flyPartiles;
	[SerializeField] ParticleSystem boomPartiles;
	[SerializeField] ParticleSystem finishPartiles;
	[SerializeField] GameObject WinMenu;
	[SerializeField] GameObject DeathMenu;

	bool collisionOff = false;
	Rigidbody rigidBody;
	AudioSource audioSource;

	bool isLeftButtonDown = false;
	bool isRightButtonDown = false;
	bool isLaunchButtonDown = false;


	public enum State {Playing,Dead, NextLevel};
	public State state = State.Playing;

	//Main
	//--------------------------------------------------------------------------------------------------

	void Start () {
		Time.timeScale = 1;
		if (PlayerPrefs.GetInt("currentMenu") != levelMap.menuIndex)
        {
			PlayerPrefs.SetInt("currentMenu", levelMap.menuIndex);
        }
		state = State.Playing;
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	

	void Update () {
		if(state == State.Playing){
		Launch();
		Rotation();
		}
		if(Debug.isDebugBuild){
		DebugKeys();
		}

	}


	void DebugKeys()
	{
		if(Input.GetKeyDown(KeyCode.L))
		{
			LoadNextLevel();
		}
		else if(Input.GetKeyDown(KeyCode.C))
		{
			collisionOff = !collisionOff;
		}
	}


	void OnCollisionEnter(Collision collision)
	{
		if(state == State.Dead || state == State.NextLevel || collisionOff)
		{
			return;
		}

		switch(collision.gameObject.tag)
		{
			case "Friendly":
				print("OK");
				break;
			case "Finish":
				Finish();
				break;
			case "Battery":
				//PlusEnergy(1000, collision.gameObject);
				break;	
			default:
				Lose();
				break;

		}
	}

	//--------------------------------------------------------------------------------------------------

	void Lose()
	{
				state = State.Dead;
				print("RocketBoom!");
				audioSource.Stop();
				audioSource.PlayOneShot(boomSound);
				boomPartiles.Play();
				Invoke("Death", 1);
	}


	void Finish()
	{
				state = State.NextLevel;
				audioSource.Stop();
				audioSource.PlayOneShot(finishSound);
				finishPartiles.Play();
				Invoke("Win", 1);
	}
	 void Win()
     {
		audioSource.Stop();
		finishPartiles.Stop();
		WinMenu.SetActive(true);
     }

	void Death()
    {
		audioSource.Stop();
		finishPartiles.Stop();
		DeathMenu.SetActive(true);
	}

	// Scene loader
	//--------------------------------------------------------------------------------------------------

	public void LoadNextLevel() // Finish
	{
		int currentLevelIndex = Array.IndexOf(levelMap.ScenesIndex, SceneManager.GetActiveScene().buildIndex);
		int nextLevelIndex = currentLevelIndex + 1;
		//print(levelMap.ScenesIndex[0]);
		//print(levelMap.ScenesIndex[1]);
		//print(SceneManager.GetActiveScene().buildIndex);
		//print(currentLevelIndex);
		//print(nextLevelIndex);

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

	public void LoadFirstLevel() // Lose
	{
		SceneManager.LoadSceneAsync(levelMap.firstLevelIndex);
	}

	public void LoadMainMenu()
    {
		SceneManager.LoadSceneAsync(levelMap.menuIndex);
	}

	public void Restart()
    {
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
	}

	// Mobile Controler
	//--------------------------------------------------------------------------------------------------

    public void OnLaunchButtonSpatus(bool isLaunchButtonStatus)
    {
		isLaunchButtonDown = isLaunchButtonStatus;

	}

    public void OnLeftButtonDown()
    {
		isLeftButtonDown = true;
		isRightButtonDown = false;
	}
	public void OnRightButtonDown()
	{
		isLeftButtonDown = false;
		isRightButtonDown = true;
	}
	public void OnButtonUp()
    {
		isLeftButtonDown = false;
		isRightButtonDown = false;
	}

	//Mover
	//---------------------------------------------------------------------------------------------

	void Launch()
	{    
		//          PC			       |         Mobile
		if(Input.GetKey(KeyCode.Space) || isLaunchButtonDown)
		{
			rigidBody.AddRelativeForce(Vector3.up * flySpeed * Time.deltaTime);
			if(audioSource.isPlaying == false)
			audioSource.PlayOneShot(flySound);//Play Sound And Particles
		}
		else
		{
			//Stop Sound And Particles
			audioSource.Pause();
			flyPartiles.Play();// Костылььььььь!!!!!
		}
		
	}

	void Rotation()
	{

		float rotationSpeed = rotSpeed * Time.deltaTime;
        
		// отменяет возможность поворота от других обектов
		rigidBody.freezeRotation = true;

		//          PC			    |         Mobile
		if (Input.GetKey(KeyCode.A) || isLeftButtonDown)
		{
			transform.Rotate(Vector3.forward * rotationSpeed);
		}
		//          PC			          |         Mobile
		else if (Input.GetKey(KeyCode.D) || isRightButtonDown)
		{
			transform.Rotate(-Vector3.forward * rotationSpeed);
		}
		// позволяет впащение
		rigidBody.freezeRotation = false;
	}
}
