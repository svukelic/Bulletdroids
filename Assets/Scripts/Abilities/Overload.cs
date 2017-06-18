using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Overload : MonoBehaviour {
	
	float cooldown = 20f;
	int cost = 50;
	float duration = 7f;
	
	bool active;
	
	string klik = "Ability3";
	
	GameObject abilityImageHUD;
	Image abilityImage;
	
	Color used = new Color(0.68f, 0.68f, 0.68f, 1f);
	Color ready = new Color (0.15f, 0.94f, 0.08f, 1f);
	
	float timer;
	
	GameObject player, front;
	PlayerHealth playerHealth;
	PlayerEnergy playerEnergy;
	AudioSource healAudio;
	int cHealth;
	ParticleSystem particles;
	
	GameObject testText;
	Text test;
	
	void Awake ()
	{
		//healAudio = GetComponent <AudioSource> ();
		abilityImageHUD = GameObject.FindGameObjectWithTag ("Ability3");
		abilityImage = abilityImageHUD.GetComponent <Image> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		playerEnergy = player.GetComponent <PlayerEnergy> ();
		timer = 100f;

		active = false;
	}
	
	void Update ()
	{
		timer += Time.deltaTime;
		cHealth = playerHealth.RetrieveCurrentHP ();
		
		if (timer >= cooldown) {
			abilityImage.color = ready;
		}
		
		if(Input.GetButton (klik) && timer >= cooldown)
		{
			Activate ();
		}
		
		if (active == true && timer >= duration) {
			active = false;
			timer = 0f;
			abilityImage.color = used;
			playerEnergy.SetOverload(0);
		}
	}
	
	void Activate ()
	{
		timer = 0f;
		//healAudio.Play ();
		
		if(cHealth > 0)
		{
			if(playerEnergy.currentEnergy >= cost){
				abilityImage.color = Color.blue;
				active = true;

				playerEnergy.SetOverload(1);
				
				playerEnergy.DecreaseEnergy (cost);
			}
		}
	}
}
