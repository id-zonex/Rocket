using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SurvivalRocketControler : MonoBehaviour
{
	[SerializeField] float rotSpeed = 100f;
	[SerializeField] float flySpeed = 100f;
	[SerializeField] AudioClip flySound;
	[SerializeField] AudioClip boomSound;
	[SerializeField] ParticleSystem flyPartiles;
	[SerializeField] ParticleSystem boomPartiles;
	[SerializeField] UnityEvent deathEvent;

	bool collisionOff = false;
	Rigidbody rigidBody;
	AudioSource audioSource;

	bool isLeftButtonDown = false;
	bool isRightButtonDown = false;
	bool isLaunchButtonDown = false;


	public enum State { Playing, Dead, NextLevel };
	public State state = State.Playing;

	//Main
	//--------------------------------------------------------------------------------------------------

	void Start()
	{
		Time.timeScale = 1;

		state = State.Playing;
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}


	void Update()
	{
		if (state == State.Playing)
		{
			Launch();
			Rotation();
		}

		if (Debug.isDebugBuild)
		{
			DebugKeys();
		}

	}

	void DebugKeys()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			collisionOff = !collisionOff;
		}
	}


	void OnCollisionEnter(Collision collision)
	{
		if (state == State.Dead || state == State.NextLevel || collisionOff)
		{
			return;
		}

		switch (collision.gameObject.tag)
		{
			case "Friendly":
				print("OK");
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


	void Death()
	{
		audioSource.Stop();
		deathEvent.Invoke();
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
		if (Input.GetKey(KeyCode.Space) || isLaunchButtonDown)
		{
			rigidBody.AddRelativeForce(Vector3.up * flySpeed * Time.deltaTime);
			if (audioSource.isPlaying == false)
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
	[System.Serializable]
	public class UnityEventScoreText : UnityEvent<string> { }
}
