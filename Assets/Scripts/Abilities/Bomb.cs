using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bomb : MonoBehaviour {
	
	GameObject mineObj, blinkEffectObj, blinkEffectChild1, blinkEffectChild2;

	static Plane XZPlane2 = new Plane(Vector3.up, Vector3.zero);
	Vector3 point;
	
	float cooldown = 15f;
	int cost = 50;
	
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
	ParticleSystem particles, blinkParticles, blinkParticles2;
	
	void Awake ()
	{
		abilityImageHUD = GameObject.FindGameObjectWithTag ("Ability3");
		abilityImage = abilityImageHUD.GetComponent <Image> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		playerEnergy = player.GetComponent <PlayerEnergy> ();
		timer = 100f;

		blinkEffectObj = GameObject.Find ("BlinkEffect");
		blinkParticles = blinkEffectObj.GetComponent<ParticleSystem> ();
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

				float distance;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if(XZPlane2.Raycast (ray, out distance)) {
					point = ray.GetPoint(distance);
					point.y = 1f;
				}

				mineObj = (GameObject)Instantiate(Resources.Load("Parts/NuclearBomb"));
				mineObj.transform.position = point;

				
				playerEnergy.DecreaseEnergy (cost);
			}
		}
	}
}