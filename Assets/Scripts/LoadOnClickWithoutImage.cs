using UnityEngine;
using System.Collections;

public class LoadOnClickWithoutImage : MonoBehaviour {

	public AudioSource clickSound;

	public void LoadScene(int level)
	{
		clickSound.Play ();
		Application.LoadLevel (level);
	}
}
