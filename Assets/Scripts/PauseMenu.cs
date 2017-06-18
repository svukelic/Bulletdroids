using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public GameObject pauseMenu, player;
	PlayerClass playerClass;
	bool paused=false;

	void Awake()
	{
		playerClass = player.GetComponent <PlayerClass> ();
	}

	void Update()
	{
		if (Input.GetButtonDown ("p")) {
			if(paused) ResumeOnClick();
			else PauseOnEsc ();
		}
		if (Input.GetButtonDown ("Escape")) {
			ExitMenuOnClick ();
		}
	}

	void PauseOnEsc()
	{
		paused = true;
		Time.timeScale = 0;
		pauseMenu.SetActive (true);
	}

	public void ResumeOnClick()
	{
		paused = false;
		Time.timeScale = 1;
		pauseMenu.SetActive (false);
	}

	public void ExitMenuOnClick()
	{
		playerClass.SaveProgress ();
		Application.LoadLevel (0);
	}
}
