using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Barrage : MonoBehaviour {

	float damage = 20f;
	float cooldown = 10f;
	int cost = 40;
	float areaRange = 3f;
	int count = 0, maxCount = 8;

	GameObject[] enemies = new GameObject[50];
	
	bool active;
	
	static Plane XZPlane = new Plane(Vector3.up, Vector3.zero);
	
	string klik = "Ability1";
	
	GameObject abilityImageHUD, barrageEffectObj;
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
	
	Vector3 point;
	Ray ray;
	
	GameObject testText;
	Text test;
	
	void Awake ()
	{
		//healAudio = GetComponent <AudioSource> ();
		abilityImageHUD = GameObject.FindGameObjectWithTag ("Ability1");
		abilityImage = abilityImageHUD.GetComponent <Image> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		playerEnergy = player.GetComponent <PlayerEnergy> ();
		timer = 100f;
		
		barrageEffectObj = GameObject.Find ("BarrageEffect");
		particles = barrageEffectObj.GetComponent<ParticleSystem> ();
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
			abilityImage.color = used;
		}

		if (active == true && timer >= 0.5f) {
			count += 1;
			float distance;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(XZPlane.Raycast (ray, out distance)) {
				point = ray.GetPoint(distance);
				point.y = 0f;
			}

			Collider[] hitColliders = Physics.OverlapSphere(point, areaRange);
			barrageEffectObj.transform.position = point;
			particles.Play ();
			int i = 0;
			while (i < hitColliders.Length && i < 50) {
				enemies[i] = hitColliders[i].gameObject;
				if(enemies[i].tag == "Enemy"){
					enemyHealth = enemies[i].GetComponent<EnemyHealth> ();
					enemyHealth.TakeDamage (damage, enemies[i].transform.position, 0);
				}
				i++;
			}
			timer = 0f;
		}
	}
	
	void Activate ()
	{
		timer = 0.9f;
		//healAudio.Play ();
		
		if(cHealth > 0)
		{
			if(playerEnergy.currentEnergy >= cost){
				abilityImage.color = Color.blue;
				
				active = true;
				
				playerEnergy.DecreaseEnergy (cost);
			}
		}
	}
}
