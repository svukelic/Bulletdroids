using UnityEngine;
using System.Collections;

public class ClassData : MonoBehaviour {
	
	int[] Health = new int[6] {0, 100, 150, 75, 100, 125};
	float[] Energy = new float[6] {0, 100f, 100f, 75f, 125f, 125f};
	float[] enRegen = new float[6] {0, 0.2f, 0.15f, 0.25f, 0.25f, 0.15f};
	float[] Movement = new float[6] {0, 5f, 4f, 7f, 4f, 5f};
	float[] primDmg = new float[6] {0, 15f, 30f, 13f, 20f, 20f};
	float[] primRoF = new float[6] {0, 0.15f, 0.3f, 0.1f, 0.2f, 0.2f};
	float[] secDmg = new float[6] {0, 15f, 50f, 30f, 48f, 48f};
	float[] secRoF = new float[6] {0, 0.15f, 1.4f, 2f, 0.8f, 1.6f};
	
	public int RetrieveInteger(int choice, int klasa)
	{
		if (choice == 1)
			return Health[klasa];
		else
			return 0;
	}
	
	public float RetrieveFloat(int choice, int klasa)
	{
		if (choice == 1)
			return Energy[klasa];
		if (choice == 2)
			return enRegen[klasa];
		if (choice == 3)
			return Movement[klasa];
		if (choice == 4)
			return primRoF[klasa];
		if (choice == 5)
			return secRoF [klasa];
		if (choice == 6)
			return primDmg[klasa];
		if (choice == 7)
			return secDmg [klasa];
		else
			return 0;
	}
}
