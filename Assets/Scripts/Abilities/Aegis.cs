using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Aegis : MonoBehaviour {
	
	float cooldown = 18f;
	int cost = 50, count=0, maxCount=6;
	
	bool active;
	
	string klik = "Ability3";
	
	GameObject abilityImageHUD;
	Image abilityImage;
	
	Color used = new Color(0.68f, 0.68f, 0.68f, 1f);
	Color ready = new Color (0.15f, 0.94f, 0.08f, 1f);
	
	float timer;
	
	GameObject player, center;
	PlayerHealth playerHealth;
	PlayerEnergy playerEnergy;
	AudioSource healAudio;
	int cHealth;
	ParticleSystem particles;
	
	Vector3 point;
	Ray ray;
	
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

		if (count >= maxCount) {
			active = false;
			count = 0;
			timer = 0f;
			playerHealth.SetShield(0);
			abilityImage.color = used;
		}

		if (active == true && timer >= 1f) {
			playerHealth.HealUp(5);
			count += 1;
			timer = 0f;
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
				playerHealth.SetShield(1);

				playerHealth.HealUp(5);

				playerEnergy.DecreaseEnergy (cost);
			}
		}
	}
}
