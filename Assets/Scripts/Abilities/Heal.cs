using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Heal : MonoBehaviour {

	int heal = 30;
	float cooldown = 20f;
	int cost = 50;

	string klik = "Ability1";

	GameObject abilityImageHUD;
	Image abilityImage;

	Color used = new Color(0.68f, 0.68f, 0.68f, 1f);
	Color ready = new Color(0.08f, 0.94f, 016f, 1f);

	float timer;

	GameObject player;
	PlayerHealth playerHealth;
	PlayerEnergy playerEnergy;
	AudioSource healAudio;
	int sHealth, cHealth;

	void Awake ()
	{
		healAudio = GetComponent <AudioSource> ();
		abilityImageHUD = GameObject.FindGameObjectWithTag ("Ability1");
		abilityImage = abilityImageHUD.GetComponent <Image> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		sHealth = playerHealth.RetrieveStartingHP ();
		playerEnergy = player.GetComponent <PlayerEnergy> ();
		timer = 100f;
	}

	void Update ()
	{
		timer += Time.deltaTime;
		cHealth = playerHealth.RetrieveCurrentHP ();

		if (timer >= cooldown)
			abilityImage.color = ready;
		
		if(Input.GetButton (klik) && timer >= cooldown)
		{
			Activate ();
		}
	}

	void Activate ()
	{
		timer = 0f;
		healAudio.Play ();

		if(cHealth > 0)
		{
			if(playerEnergy.currentEnergy >= cost){
				//if(cHealth + heal > sHealth) heal = (sHealth - cHealth);
				playerHealth.HealUp (heal);
				playerEnergy.DecreaseEnergy (cost);
				abilityImage.color = used;
			}
		}
	}
}
