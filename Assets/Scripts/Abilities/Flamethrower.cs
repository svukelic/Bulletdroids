using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Flamethrower : MonoBehaviour {

	float damage=5f;
	string klik = "Ability1";

	bool flameActive;

	GameObject player;
	EnemyHealth enemyHealth;

	ParticleSystem particles;

	GameObject abilityImageHUD, flamethrowerEffect;
	Image abilityImage;

	Color used = new Color(0.68f, 0.68f, 0.68f, 1f);
	Color ready = new Color (0.15f, 0.94f, 0.08f, 1f);

	GameObject testText;
	Text test;

	void Awake()
	{
		testText = GameObject.Find ("TestText");
		test = testText.GetComponent<Text> ();

		flamethrowerEffect = GameObject.Find ("FlamethrowerEffect");
		particles = flamethrowerEffect.GetComponent<ParticleSystem> ();
		abilityImageHUD = GameObject.FindGameObjectWithTag ("Ability1");
		abilityImage = abilityImageHUD.GetComponent <Image> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			if(flameActive == true){
				enemyHealth = other.gameObject.GetComponent<EnemyHealth> ();
				enemyHealth.TakeDamage(damage, other.gameObject.transform.position, 0);
			}
		}
	}

	void Update()
	{
		if(Input.GetButton (klik)){
			flameActive = true;
			flamethrowerEffect.transform.position = gameObject.transform.position;
			test.text = flamethrowerEffect.transform.position.ToString();
			particles = flamethrowerEffect.GetComponent<ParticleSystem> ();
			particles.Play ();
		}
		if(Input.GetButtonUp (klik)){
			flameActive = false;
			particles.Stop ();
		}

		if(flameActive == true) abilityImage.color = used;
		if(flameActive == false) abilityImage.color = ready;
	}
}
