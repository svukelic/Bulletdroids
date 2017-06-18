using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Drones : MonoBehaviour {
	
	GameObject droneObj;
	
	float cooldown = 5f;
	int cost = 20;
	int amount = 3;
	int currentAmount;

	string klik = "Ability3";
	
	GameObject abilityImageHUD;
	Image abilityImage;
	
	Color used = new Color(0.68f, 0.68f, 0.68f, 1f);
	Color ready = new Color (0.15f, 0.94f, 0.08f, 1f);
	
	float timer;
	
	GameObject player, center;
	PlayerHealth playerHealth;
	PlayerEnergy playerEnergy;
	EnemyHealth enemyHealth;
	int cHealth;
	ParticleSystem particles;
	
	GameObject testText;
	Text test;
	
	void Awake ()
	{
		abilityImageHUD = GameObject.FindGameObjectWithTag ("Ability3");
		abilityImage = abilityImageHUD.GetComponent <Image> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		playerEnergy = player.GetComponent <PlayerEnergy> ();
		timer = 100f;
		currentAmount = 0;
	}
	
	void Update ()
	{
		timer += Time.deltaTime;
		cHealth = playerHealth.RetrieveCurrentHP ();
		
		if (currentAmount > 0)
			abilityImage.color = ready;

		if (timer >= cooldown) {
			timer = 0f;
			if(currentAmount < amount) currentAmount += 1;
		}
		
		if(Input.GetButtonDown (klik) && currentAmount > 0)
		{
			Activate ();
		}
	}
	
	void Activate ()
	{
		if(cHealth > 0)
		{
			if(playerEnergy.currentEnergy >= cost){
				abilityImage.color = used;
				droneObj = (GameObject)Instantiate(Resources.Load("Parts/ArchimedesDrone"));
				currentAmount -= 1;
				
				playerEnergy.DecreaseEnergy (cost);
			}
		}
	}
}