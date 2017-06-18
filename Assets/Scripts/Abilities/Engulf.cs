using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Engulf : MonoBehaviour {
	
	GameObject[] enemies = new GameObject[50];
	
	float damage = 5f;
	float cooldown = 12f;
	int cost = 40;
	float areaRange = 3f;
	float rate = 0.2f;
	int heal=1, count=0, countMax=10;
	
	string klik = "Ability1";

	bool active;
	
	GameObject abilityImageHUD;
	Image abilityImage;
	
	Color used = new Color(0.68f, 0.68f, 0.68f, 1f);
	Color ready = new Color (0.15f, 0.94f, 0.08f, 1f);
	
	float timer;
	
	GameObject player, front;
	PlayerHealth playerHealth;
	PlayerEnergy playerEnergy;
	EnemyHealth enemyHealth;
	AudioSource healAudio;
	int cHealth;
	ParticleSystem particles;
	
	void Awake ()
	{
		//healAudio = GetComponent <AudioSource> ();
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

		if (count >= countMax) {
			active = false;
			abilityImage.color = used;
			timer = 0f;
			count = 0;
			//particles.Stop();
		}

		if (active == true && timer >= rate) {
			Vector3 pozicija = front.transform.position;
			Collider[] hitColliders = Physics.OverlapSphere(pozicija, areaRange);
			particles.Play ();
			int i = 0;
			while (i < hitColliders.Length && i < 50) {
				enemies[i] = hitColliders[i].gameObject;
				if(enemies[i].tag == "Enemy"){
					enemyHealth = enemies[i].GetComponent<EnemyHealth> ();
					enemyHealth.TakeDamage (damage, enemies[i].transform.position, 2);
					playerHealth.HealUp(heal);
				}
				i++;
			}
			timer = 0f;
			count += 1;
		}
	}
	
	void Activate ()
	{
		timer = 0f;
		//healAudio.Play ();
		
		front = GameObject.Find ("FrontArchimedes(Clone)");
		particles = front.GetComponent<ParticleSystem> ();
		
		if(cHealth > 0)
		{
			if(playerEnergy.currentEnergy >= cost){
				active = true;
				
				playerEnergy.DecreaseEnergy (cost);
				abilityImage.color = Color.blue;
			}
		}
	}
}
