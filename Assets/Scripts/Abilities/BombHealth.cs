using UnityEngine;

public class BombHealth : MonoBehaviour
{
	public float startingHealth = 100f;
	public float currentHealth = 100f;
	public float sinkSpeed = 2.5f;
	public int scoreValue = 10;
	public AudioClip deathClip;

	float damage = 100f;
	
	float timer;
	AudioSource enemyAudio;
	ParticleSystem hitParticles;
	CapsuleCollider capsuleCollider;
	bool isDead;
	bool isSinking;
	int provjera;
	PlayerClass pClass;
	GameObject player;
	
	GameObject[] enemies = new GameObject[50];
	EnemyHealth enemyHealth;
	
	ParticleSystem particles, blinkParticles;
	GameObject NukeEffect, blinkEffectObj;

	Vector3 pozicija;
	
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		pClass = player.GetComponent <PlayerClass> ();
		enemyAudio = GetComponent <AudioSource> ();
		hitParticles = GetComponentInChildren <ParticleSystem> ();
		capsuleCollider = GetComponent <CapsuleCollider> ();
		
		timer = 0f;
		currentHealth = startingHealth;
		
		NukeEffect = GameObject.Find ("NukeEffect");
		particles = NukeEffect.GetComponent<ParticleSystem> ();

		blinkEffectObj = GameObject.Find ("BlinkEffect");
		blinkParticles = blinkEffectObj.GetComponent<ParticleSystem> ();
	}

	void Start ()
	{
		pozicija = transform.position;
		
		blinkEffectObj.transform.position = pozicija;
		blinkParticles.Play ();
	}
	
	void Update ()
	{
	}
	
	
	public void TakeDamage (float amount, Vector3 hitPoint, int effect)
	{
		if(isDead)
			return;
		
		enemyAudio.Play ();
		
		currentHealth -= amount;
		
		//hitParticles.transform.position = hitPoint;
		//hitParticles.Play();
		
		if(currentHealth <= 0)
		{
			Death ();
		}
	}
	
	
	void Death ()
	{
		isDead = true;
		
		capsuleCollider.isTrigger = true;
		NukeEffect.transform.position = transform.position;
		particles.Play ();

		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f);
		int i = 0;
		while (i < hitColliders.Length && i < 50) {
			enemies[i] = hitColliders[i].gameObject;
			if(enemies[i].tag == "Enemy"){
				enemyHealth = enemies[i].GetComponent<EnemyHealth> ();
				enemyHealth.TakeDamage (damage, enemies[i].transform.position, 0);
			}
			i++;
		}
		
		enemyAudio.clip = deathClip;
		enemyAudio.Play ();
		
		Destroy (gameObject, 0.05f);
	}
}
