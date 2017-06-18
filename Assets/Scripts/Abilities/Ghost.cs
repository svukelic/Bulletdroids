using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ghost : MonoBehaviour {
	
	GameObject[] enemies = new GameObject[50];
	
	float duration = 3f;
	float cooldown = 15f;
	int cost = 50;

	bool active;
	
	string klik = "Ability2";
	
	GameObject abilityImageHUD;
	Image abilityImage;
	
	Color used = new Color(0.68f, 0.68f, 0.68f, 1f);
	Color ready = new Color (0.15f, 0.94f, 0.08f, 1f);
	
	float timer;
	
	GameObject player, center;
	PlayerHealth playerHealth;
	PlayerEnergy playerEnergy;
	EnemyHealth enemyHealth;
	AudioSource healAudio;
	int cHealth;
	ParticleSystem particles;
	
	void Awake ()
	{
		healAudio = GetComponent <AudioSource> ();
		abilityImageHUD = GameObject.FindGameObjectWithTag ("Ability2");
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

		if (timer > duration && active == true) {
			active = false;
			player.tag = "Player";
		}
	}
	
	void Activate ()
	{
		timer = 0f;
		healAudio.Play ();
		
		center = GameObject.Find ("CenterThemistocles(Clone)");
		particles = center.GetComponent<ParticleSystem> ();
		
		if(cHealth > 0)
		{
			if(playerEnergy.currentEnergy >= cost){
				active = true;
				particles.Play();
				player.tag = "Untagged";
				timer = 0f;
				
				playerEnergy.DecreaseEnergy (cost);
				abilityImage.color = used;
			}
		}
	}
}
