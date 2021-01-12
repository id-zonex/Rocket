using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rocket : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		state = State.Playing;
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
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


	public void LoadNextLevel() // Finish
	{
		int currentLevelIndex = Array.IndexOf(levelMap.ScenesIndex, SceneManager.GetActiveScene().buildIndex);
		int nextLevelIndex = currentLevelIndex + 1;
		//print(levelMap.ScenesIndex[0]);
		//print(levelMap.ScenesIndex[1]);
		//print(SceneManager.GetActiveScene().buildIndex);
		//print(currentLevelIndex);
		//print(nextLevelIndex);
		if (nextLevelIndex == levelMap.sceneCount)
		{
			SceneManager.LoadSceneAsync(levelMap.menuIndex); // цикл 
			print("load main menu");
		}
        else
        {
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



	void Launch()
	{
		if(Input.GetKey(KeyCode.Space) || isLaunchButtonDown)
		{
			rigidBody.AddRelativeForce(Vector3.up * flySpeed * Time.deltaTime);
			if(audioSource.isPlaying == false)
			audioSource.PlayOneShot(flySound);
		}
		else
		{
			audioSource.Pause();
			flyPartiles.Play();
		}
		
	}

	void Rotation()
	{

		float rotationSpeed = rotSpeed * Time.deltaTime;

		rigidBody.freezeRotation = true;
		if(Input.GetKey(KeyCode.A) || isLeftButtonDown)
		{
			transform.Rotate(Vector3.forward * rotationSpeed);
		}
		else if(Input.GetKey(KeyCode.D) || isRightButtonDown)
		{
			transform.Rotate(-Vector3.forward * rotationSpeed);
		}
		rigidBody.freezeRotation = false;
	}
}
