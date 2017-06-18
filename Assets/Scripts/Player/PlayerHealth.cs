using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    int startingHealth;
    int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;

	int shielded;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
		shielded = 0;
        
		//currentHealth = startingHealth;
    }

	public void SetShield(int choice)
	{
		shielded = choice;
	}

	public int RetrieveStartingHP()
	{
		return startingHealth;
	}

	public int RetrieveCurrentHP()
	{
		return currentHealth;
	}

	public void SetHealth(int zdravlje)
	{
		startingHealth = zdravlje;
		currentHealth = zdravlje;
		healthSlider.maxValue = zdravlje;
		healthSlider.value = zdravlje;
	}

    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

		if (shielded == 1) {
			amount = (int)(amount/2);
		}

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }

	public void HealUp (int amount)
	{
		if (currentHealth + amount >= startingHealth) {
			amount = startingHealth - currentHealth;
		}
		currentHealth += amount;
		
		healthSlider.value = currentHealth;
	}


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        Application.LoadLevel (Application.loadedLevel);
    }
}
