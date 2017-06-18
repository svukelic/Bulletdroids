using UnityEngine;
using System.Collections;

public class MineTrigger : MonoBehaviour {

	bool explode = false;
	GameObject[] enemies = new GameObject[50];

	float damage = 50;
	int effect;
	float areaRange = 3f;

	GameObject player;
	EnemyHealth enemyHealth;
	BombHealth bombHealth;
	AudioSource audioSrc;
	ParticleSystem particles;
	GameObject back;

	float timer;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		transform.position = player.transform.position;

		back = GameObject.Find ("MineEffect");
		particles = back.GetComponent<ParticleSystem> ();
		explode = false;
		timer = 0f;
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			if(timer>1) explode = true;
		}
	}

	void Update () {
		timer += Time.deltaTime;

		if (explode == true) {
			Vector3 pozicija = transform.position;
			back.transform.position = pozicija;
			Collider[] hitColliders = Physics.OverlapSphere(pozicija, areaRange);
			particles.Play ();
			int i = 0;
			while (i < hitColliders.Length && i < 50) {
				enemies[i] = hitColliders[i].gameObject;
				if(enemies[i].tag == "Enemy"){
					enemyHealth = enemies[i].GetComponent<EnemyHealth> ();
					enemyHealth.TakeDamage (damage, enemies[i].transform.position, 1);
				}
				if(enemies[i].tag == "Bomb"){
					bombHealth = enemies[i].GetComponent<BombHealth> ();
					bombHealth.TakeDamage (damage, enemies[i].transform.position, 0);
				}
				i++;
			}
			Destroy (gameObject);
		}
	}
}
