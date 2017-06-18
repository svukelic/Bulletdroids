using UnityEngine;
using System.Collections;

public class ClassRemember : MonoBehaviour {

	GameObject batn, plusPanel;
	ClassSave klasaBR;

	public int unos;
	
	void Awake ()
	{
		batn = GameObject.FindGameObjectWithTag ("Sejvanje");
		klasaBR = batn.GetComponent<ClassSave> ();
	}

	public void rememberClassNumber()
	{
		klasaBR.Pamti (unos);
	}
}
