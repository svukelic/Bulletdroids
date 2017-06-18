using UnityEngine;
using System.Collections;

public class ActiveEffects : MonoBehaviour {

	//Nothing, Slow, Stun, AoE, Detonation, ChargeUp, ElCharge
	int[] effects = new int[7];

	void Awake()
	{
		effects[0] = 0;
		effects[1] = 0;
		effects[2] = 0;
		effects[3] = 0;
		effects[4] = 0;
		effects[5] = 0;
		effects[6] = 0;
	}

	public void SetEffect(int active, int index)
	{
		effects[index] = active;
	}

	public int RetrieveEffect(int index)
	{
		if(effects[index] == 1) return 1;
		else return 0;
	}
}
