using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveManager : MonoBehaviour {

	float timer;
	int wave, enemyNumber, enemiesSpawned, startNew=0;
	public int enemyMax = 50;
	GameObject waveObj;
	Text waveText;

	bool waveActive;

	public int RetrieveWave()
	{
		return wave;
	}

	public void IncreaseEnemy()
	{
		enemyNumber += 1;
	}

	public void EnemySpawned()
	{
		enemiesSpawned += 1;
	}

	public bool ReturnWaveActive()
	{
		return waveActive;
	}

	void Awake ()
	{
		timer = 0f;
		enemyNumber = 0;
		enemiesSpawned = 0;
		wave = 0;
		startNew = 1;
		waveObj = GameObject.Find ("WaveText");
		waveText = waveObj.GetComponent <Text> ();
		waveActive = false;
	}

	void Update () 
	{
		timer += Time.deltaTime;
		if (enemyNumber >= enemiesSpawned && enemyNumber != 0) {
			timer = 0f;
			enemyNumber = 0;
			enemiesSpawned = 0;
			enemyMax += (int)((float)enemyMax/2);
			startNew = 1;
		}
		if (timer > 2 && waveActive == false && startNew == 1) {
			waveActive = true;
			wave += 1;
			waveText.text = "Wave: " + wave;
		}
		if (enemiesSpawned >= enemyMax) {
			waveActive = false;
			startNew = 0;
		}
	}
}
