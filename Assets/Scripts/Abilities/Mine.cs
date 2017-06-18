using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Mine : MonoBehaviour {
	
	GameObject mineObj;

	float cooldown = 5f;
	int cost = 30;
	
	string klik = "Ability1";
	
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
		abilityImageHUD = GameObject.FindGameObjectWithTag ("Ability1");
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
		
		if(cHealth > 0)
		{
			if(playerEnergy.currentEnergy >= cost){
				abilityImage.color = used;

				mineObj = (GameObject)Instantiate(Resources.Load("Parts/ThemistoclesMine"));

				playerEnergy.DecreaseEnergy (cost);
			}
		}
	}
}