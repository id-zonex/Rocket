using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuControl : MonoBehaviour {
	[Header("індекс першого левела")]
	[SerializeField] levelMap levelMap;
	[SerializeField] AudioClip flySound;
	[SerializeField] float speed;

	Rigidbody _rigidbody;
	AudioSource audioSource;
	bool isScreenDown = false;


	void Start ()
	{

		if (!(PlayerPrefs.GetInt("currentMenu") == SceneManager.GetActiveScene().buildIndex))
        {
			SceneManager.LoadScene(PlayerPrefs.GetInt("currentMenu"));
		}
		if (PlayerPrefs.GetInt("maxUnlockLevelIndex") == 0)
        {
			PlayerPrefs.SetInt("maxUnlockLevelIndex", 1);
        }


		Time.timeScale = 1;
		audioSource = GetComponent<AudioSource>();
		_rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Space) || isScreenDown)
		{
			_rigidbody.AddRelativeForce(Vector3.up * speed * Time.deltaTime);
			Invoke("Launch", 2f);
			audioSource.PlayOneShot(flySound);
		}
		else
        {
			audioSource.Stop();
        }
	}

	void Launch()
    {
		SceneManager.LoadScene(levelMap.firstLevelIndex);
	}

	public void OnScreenDown()
    {
		isScreenDown = true;
    }

	public void OnScreenUp()
	{
		isScreenDown = false;
	}
}
