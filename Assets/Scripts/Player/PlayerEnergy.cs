using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerEnergy : MonoBehaviour
{
	float startingEnergy = 100f;
	public float currentEnergy;
	float energyRegen;
	public Slider energySlider;

	int overloadActive;

	float timer;
	
	void Awake ()
	{
		overloadActive = 0;
	}
	
	
	void Update ()
	{
		timer += Time.deltaTime;

		if(currentEnergy < startingEnergy) IncreaseEnergy (energyRegen);
	}

	public void SetOverload(int choice)
	{
		overloadActive = choice;
	}

	public int RetrieveOverload ()
	{
		return overloadActive;
	}

	public void SetEnergy(float pocEn, float enReg)
	{
		startingEnergy = pocEn;
		energyRegen = enReg;
		currentEnergy = startingEnergy;
		energySlider.maxValue = startingEnergy;
		energySlider.value = startingEnergy;
	}
	
	public void DecreaseEnergy (float amount)
	{
		currentEnergy -= amount;
		
		energySlider.value = currentEnergy;

	}
	
	public void IncreaseEnergy (float amount)
	{
		currentEnergy += amount;
		
		energySlider.value = currentEnergy;
		
	}
}
