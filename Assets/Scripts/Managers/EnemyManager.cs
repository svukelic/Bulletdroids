using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy, player;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
	public int amount; // waveIncrease;
	int cHealth;
	int wave; // finalAmount;

	//GameObject enemyManager;
	WaveManager waveManager;
	bool waveActive;

    void Start ()
    {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();

		//enemyManager = GameObject.Find ("EnemyManager");
		waveManager = GetComponent<WaveManager> ();
	}

	void Update ()
	{
		cHealth = playerHealth.RetrieveCurrentHP ();
		waveActive = waveManager.ReturnWaveActive ();
		wave = waveManager.RetrieveWave (); 
	}


    void Spawn ()
    {
        if(cHealth <= 0)
        {
            return;
        }
		if (waveActive == true) {
			for (int i=0; i<amount; i++){
        		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
				waveManager.EnemySpawned();
				Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
			}
		}
    }
}
