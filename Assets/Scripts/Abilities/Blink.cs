using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Blink : MonoBehaviour {

	float cooldown = 15f;
	int cost = 50;
	
	bool active;

	static Plane XZPlane = new Plane(Vector3.up, Vector3.zero);
	
	string klik = "Ability2";
	
	GameObject abilityImageHUD, blinkEffectObj;
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
		abilityImageHUD = GameObject.FindGameObjectWithTag ("Ability2");
		abilityImage = abilityImageHUD.GetComponent <Image> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		playerEnergy = player.GetComponent <PlayerEnergy> ();
		timer = 100f;

		blinkEffectObj = GameObject.Find ("BlinkEffect");
		particles = blinkEffectObj.GetComponent<ParticleSystem> ();
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
					point.y = 0.5f;
				}

				player.transform.position = point;

				Vector3 pozicija = transform.position;
				blinkEffectObj.transform.position = pozicija;
				particles.Play ();

				playerHealth.HealUp(20);
				
				playerEnergy.DecreaseEnergy (cost);
			}
		}
	}
}
