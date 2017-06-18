using UnityEngine;
using System.Collections;

public class DroneMovement : MonoBehaviour
{
	GameObject[] gos;
	Transform player;
	PlayerHealth playerHealth;
	NavMeshAgent nav;
	int cHealth;
	ActiveEffects actEff;
	int provjeraStun;
	float timer;
	
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		nav = GetComponent <NavMeshAgent> ();
	}
	
	
	void Update ()
	{
		timer += Time.deltaTime;
		cHealth = playerHealth.RetrieveCurrentHP ();

		if(cHealth > 0)
		{
			gos = GameObject.FindGameObjectsWithTag("Enemy");
			GameObject closest = null;
			float distance = Mathf.Infinity;
			Vector3 position = transform.position;
			foreach (GameObject go in gos) {
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) {
					closest = go;
					distance = curDistance;
				}
			}
			nav.SetDestination (closest.transform.position);
		}
		else
		{
			nav.enabled = false;
		}
	}
}
