using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpendPoint : MonoBehaviour {

	GameObject save;
	ClassSave spend;
	public int type, choice;
	int classNumber;

	int[] sP = new int[6];
	int[] aP = new int[6];

	void Awake()
	{
		save = GameObject.Find ("SaveButton");
		spend = save.GetComponent <ClassSave> ();
	}

	public void SpendPointOnClick()
	{
		classNumber = spend.RetrieveCN ();
		
		sP [classNumber] = spend.RetrieveInt (1, classNumber);
		aP [classNumber] = spend.RetrieveInt (2, classNumber);

		if (sP [classNumber] > 0) {
			spend.PointSpent (type, choice);
		}
	}
}
