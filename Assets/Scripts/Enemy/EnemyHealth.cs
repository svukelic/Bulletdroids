using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float startingHealth = 100;
    public float currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
	public int exp;

	float timer;
    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
	ActiveEffects actEff;
	float effectTimer;
	NavMeshAgent nav;
	int provjera, provjeraDetonation;
	float pocBrz;
	PlayerClass pClass;
	GameObject player, enemyManager;
	WaveManager waveManager;

	GameObject[] enemies = new GameObject[50];
	EnemyHealth enemyHealth;

	ParticleSystem particles;
	GameObject back;

    void Awake ()
    {
		player = GameObject.FindGameObjectWithTag ("Player");
		pClass = player.GetComponent <PlayerClass> ();
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
		actEff = GetComponent <ActiveEffects> ();
		nav = GetComponent <NavMeshAgent> ();
		pocBrz = nav.speed;

		enemyManager = GameObject.Find ("EnemyManager");
		waveManager = enemyManager.GetComponent<WaveManager> ();

		timer = 0f;
        currentHealth = startingHealth;

		back = GameObject.Find ("MineEffect");
		particles = back.GetComponent<ParticleSystem> ();
    }


    void Update ()
    {
        if (isSinking) {
			timer += Time.deltaTime;
			if(timer > 1) transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		} else {
			provjera = actEff.RetrieveEffect (1);
			if (provjera == 1) {
				effectTimer += Time.deltaTime;
				if (effectTimer <= 2)
					nav.speed = (float)(pocBrz * 0.3);
				if (effectTimer > 2) { 
					nav.speed = pocBrz;
					effectTimer = 0f;
					actEff.SetEffect (0, 1);
				}
			}
		}
    }


    public void TakeDamage (float amount, Vector3 hitPoint, int effect)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
		actEff.SetEffect (1, effect);
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

		provjeraDetonation = actEff.RetrieveEffect (4);
		if (provjeraDetonation == 1) {
			Vector3 pozicija = transform.position;
			Collider[] hitColliders = Physics.OverlapSphere(pozicija, 3f);
			back.transform.position = pozicija;
			particles.Play ();
			int i = 0;
			while (i < hitColliders.Length && i < 50) {
				enemies[i] = hitColliders[i].gameObject;
				if(enemies[i].tag == "Enemy"){
					enemyHealth = enemies[i].GetComponent<EnemyHealth> ();
					enemyHealth.TakeDamage (30f, enemies[i].transform.position, 0);
				}
				i++;
			}
		}

		pClass.UpdateExp (exp);

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();

		waveManager.IncreaseEnemy ();

		StartSinking ();
    }


    public void StartSinking ()
    {
        GetComponent <NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
