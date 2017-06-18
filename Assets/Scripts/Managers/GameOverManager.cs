using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
	PlayerClass playerClass;
	GameObject player;
	int cHealth;
	public GameObject screen;

    //Animator anim;
	float timer = 0;


    void Awake()
    {
        //anim = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		playerClass = player.GetComponent <PlayerClass> ();
    }


    void Update()
    {
		cHealth = playerHealth.RetrieveCurrentHP ();
        if (cHealth <= 0)
        {
			timer += Time.deltaTime;
            //anim.SetTrigger("GameOver");
			playerClass.SaveProgress();
			if(timer>2f) screen.SetActive (true);
			if(timer>5f) Application.LoadLevel (0);
        }
    }
}
