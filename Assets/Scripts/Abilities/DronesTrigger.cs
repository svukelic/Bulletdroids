using UnityEngine;
using System.Collections;

public class DronesTrigger : MonoBehaviour {
	
	bool explode = false;
	GameObject[] enemies = new GameObject[50];
	
	float damage = 50;
	int effect;
	float areaRange = 4f;
	
	GameObject player;
	EnemyHealth enemyHealth;
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
			Vector3 pozicija = transform.position;
			Collider[] hitColliders = Physics.OverlapSphere(pozicija, areaRange);
			back.transform.position = pozicija;
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
			Destroy (gameObject);
		}
	}
	
	void Update () {
		//timer += Time.deltaTime;
	}
}
