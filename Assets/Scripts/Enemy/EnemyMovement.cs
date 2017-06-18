using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;
	int cHealth;
	ActiveEffects actEff;
	int provjeraStun;
	float timer;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <NavMeshAgent> ();
		actEff = GetComponent <ActiveEffects> ();
    }


    void Update ()
    {
		timer += Time.deltaTime;
		cHealth = playerHealth.RetrieveCurrentHP ();
		provjeraStun = actEff.RetrieveEffect (2);
        if(enemyHealth.currentHealth > 0 && cHealth > 0)
        {
            if(provjeraStun==0) nav.SetDestination (player.position);
			else{
				nav.enabled = false;
				timer = 0f;
			}

			if(provjeraStun==0 && nav.enabled==false && timer>2){
				nav.SetDestination (player.position);
				actEff.SetEffect(0, 2);
			}
        }
        else
        {
            nav.enabled = false;
        }
    }
}
