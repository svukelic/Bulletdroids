using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
	GameObject[] enemies = new GameObject[50];

    float primaryDmg = 20, damage;
    float primaryTime = 0.15f;
	int effect = 0;
    
	float range = 100f;

	string klik; 

    float timer, chargeTimer;
	bool chargeActive;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime;

	GameObject testText;
	Text test;

	EnemyHealth enemyHealth;
	BombHealth bombHealth;

	GameObject Explosion;
	ParticleSystem explParticle;
	
	public void SetData(float temp1, float temp2, float temp3, string temp4, float temp5, int temp6)
	{
		primaryDmg = temp1;
		damage = primaryDmg;
		primaryTime = temp2;
		range = temp3;
		klik = temp4;
		effectsDisplayTime = temp5;
		effect = temp6;
	}

    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
		timer = 100f;

		testText = GameObject.Find ("TestText");
		test = testText.GetComponent<Text> ();

		Explosion = GameObject.Find("BismarckAltFire");
		explParticle = Explosion.GetComponent<ParticleSystem> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton (klik) && timer >= primaryTime && Time.timeScale != 0 && effect != 5)
        {
            Shoot ();
        }

		if(Input.GetButton (klik) && timer >= primaryTime && Time.timeScale != 0 && effect == 5)
		{
			if(chargeTimer < 2) gunParticles.startColor = Color.blue;
			if(chargeActive == false) ChargeUp ();
			chargeTimer += Time.deltaTime;
			gunParticles.Play ();
			if(chargeTimer > 2) gunParticles.startColor = Color.cyan;
		}

		if(Input.GetButtonUp (klik) && chargeActive == true && effect == 5){
			chargeActive = false;
			if(chargeTimer > 2) chargeTimer = 2;
			damage = primaryDmg + (2 * chargeTimer * primaryDmg);
			Shoot ();
		}

        if(timer >= primaryTime * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }

	void ChargeUp ()
	{
		chargeTimer = 0f;
		chargeActive = true;
	}


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
			if(shootHit.collider.tag == "Bomb"){
				bombHealth = shootHit.collider.GetComponent <BombHealth> ();
				bombHealth.TakeDamage (damage, shootHit.point, effect);
			}else{ 
				enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
	            if(enemyHealth != null)
	            {
	                enemyHealth.TakeDamage (damage, shootHit.point, effect);
					if(effect == 3){
						Explosion.transform.position = shootHit.point;
						explParticle.Play ();
						Collider[] hitColliders = Physics.OverlapSphere(shootHit.point, 3f);
						int i = 0;
						while (i < hitColliders.Length && i < 50) {
							enemies[i] = hitColliders[i].gameObject;
							if(enemies[i].tag == "Enemy"){
								enemyHealth = enemies[i].GetComponent<EnemyHealth> ();
								enemyHealth.TakeDamage (primaryDmg/2, enemies[i].transform.position, 0);
							}
							i++;
						}
					}
	            }
			}
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
