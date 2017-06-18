using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AoEstun : MonoBehaviour {

	GameObject[] enemies = new GameObject[50];

	float damage = 10f;
	float cooldown = 10f;
	int cost = 30;
	float areaRange = 4f;
	
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
		//healAudio = GetComponent <AudioSource> ();
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
	}
	
	void Activate ()
	{
		timer = 0f;
		//healAudio.Play ();

		center = GameObject.Find ("CenterBismarck(Clone)");
		particles = center.GetComponent<ParticleSystem> ();
		
		if(cHealth > 0)
		{
			if(playerEnergy.currentEnergy >= cost){
				Vector3 pozicija = center.transform.position;
				Collider[] hitColliders = Physics.OverlapSphere(pozicija, areaRange);
				particles.Play ();
				int i = 0;
				while (i < hitColliders.Length && i < 50) {
					enemies[i] = hitColliders[i].gameObject;
					if(enemies[i].tag == "Enemy"){
						enemyHealth = enemies[i].GetComponent<EnemyHealth> ();
						enemyHealth.TakeDamage (damage, enemies[i].transform.position, 2);
					}
					i++;
				}

				playerEnergy.DecreaseEnergy (cost);
				abilityImage.color = used;
			}
		}
	}
}
