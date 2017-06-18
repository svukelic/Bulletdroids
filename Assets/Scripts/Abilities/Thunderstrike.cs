using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Thunderstrike : MonoBehaviour {
	
	float damage = 20f, baseDamage = 20f;
	float cooldown = 4f;
	int cost = 40;
	float areaRange = 2f;
	
	static Plane XZPlane = new Plane(Vector3.up, Vector3.zero);

	GameObject[] enemies = new GameObject[50];
	
	string klik = "Ability1";
	
	GameObject abilityImageHUD, thunderEffectObj;
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

	int overloadCheck;
	
	Vector3 point;
	Ray ray;
	
	GameObject testText;
	Text test;
	
	void Awake ()
	{
		testText = GameObject.Find ("TestText");
		test = testText.GetComponent<Text> ();
		
		//healAudio = GetComponent <AudioSource> ();
		abilityImageHUD = GameObject.FindGameObjectWithTag ("Ability1");
		abilityImage = abilityImageHUD.GetComponent <Image> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		playerEnergy = player.GetComponent <PlayerEnergy> ();
		timer = 100f;
		
		thunderEffectObj = GameObject.Find ("ThunderstrikeEffect");
		particles = thunderEffectObj.GetComponent<ParticleSystem> ();
	}
	
	void Update ()
	{
		timer += Time.deltaTime;
		cHealth = playerHealth.RetrieveCurrentHP ();
		
		if (timer >= cooldown) {
			abilityImage.color = ready;
		}

		overloadCheck = playerEnergy.RetrieveOverload ();
		if (overloadCheck == 1)
			damage = 3 * baseDamage;

		if(Input.GetButtonDown (klik) && timer >= cooldown)
		{
			Activate ();
		}
	}
	
	void Activate ()
	{
		timer = 0f;
		//healAudio.Play ();
		
		if(cHealth > 0)
		{
			if(playerEnergy.currentEnergy >= cost){
				abilityImage.color = used;
				
				float distance;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if(XZPlane.Raycast (ray, out distance)) {
					point = ray.GetPoint(distance);
					point.y = 0f;
				}
				thunderEffectObj.transform.position = point;
				particles.Play ();

				Collider[] hitColliders = Physics.OverlapSphere(point, areaRange);
				int i = 0;
				while (i < hitColliders.Length && i < 50) {
					enemies[i] = hitColliders[i].gameObject;
					if(enemies[i].tag == "Enemy"){
						enemyHealth = enemies[i].GetComponent<EnemyHealth> ();
						enemyHealth.TakeDamage (damage, enemies[i].transform.position, 1);
					}
					i++;
				}
				
				playerEnergy.DecreaseEnergy (cost);
				int overloadHeal = (int)(cost/2);
				if(overloadCheck == 1)
					playerHealth.HealUp(overloadHeal);
			}
		}
	}
}
