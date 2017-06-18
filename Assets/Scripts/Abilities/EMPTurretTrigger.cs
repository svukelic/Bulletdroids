using UnityEngine;
using System.Collections;

public class EMPTurretTrigger : MonoBehaviour {

	float repeatTime=4f;
	float timer;
	float areaRange = 8f;
	float damage = 10f;
	int heal = 20;

	GameObject[] enemies = new GameObject[50];
	EnemyHealth enemyHealth;
	PlayerHealth playerHealth;

	GameObject TurretEffect;
	ParticleSystem particles;

	void Start () {
		InvokeRepeating ("Activate", 1f, repeatTime);
	}

	void Awake(){
		TurretEffect = GameObject.Find ("TurretEffect");
		particles = TurretEffect.GetComponent<ParticleSystem> ();
	}

	void Update () {
		timer += Time.deltaTime;
		if (timer >= 18f) {
			Destroy(gameObject);
		}
	}

	void Activate()
	{
		Vector3 pozicija = transform.position;
		Collider[] hitColliders = Physics.OverlapSphere(pozicija, areaRange);
		TurretEffect.transform.position = pozicija;
		particles.Play ();
		int i = 0;
		while (i < hitColliders.Length && i < 50) {
			enemies[i] = hitColliders[i].gameObject;
			if(enemies[i].tag == "Enemy"){
				enemyHealth = enemies[i].GetComponent<EnemyHealth> ();
				enemyHealth.TakeDamage (damage, enemies[i].transform.position, 1);
			}
			if(enemies[i].tag == "Player"){
				playerHealth = enemies[i].GetComponent<PlayerHealth> ();
				playerHealth.HealUp(heal);
			}
			i++;
		}
	}
}
