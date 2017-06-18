using UnityEngine;
using System.Collections;
using System.IO;

public class LoadOnClick : MonoBehaviour {

	public GameObject loadingImage;
	string direktorij, provjera, linija;

	public void LoadScene(int level)
	{
		loadingImage.SetActive (true);

		if (level == 2) {
			direktorij = Directory.GetCurrentDirectory ();
			
			provjera = direktorij + @"\radioaktivne_mrkve.txt";
			
			if (!File.Exists (provjera)) {
				using (StreamWriter sw = File.CreateText(provjera)) {
					linija = "Klasa: 1";
					sw.WriteLine (linija);
					for (int i=1; i<6; i++) {
						sw.WriteLine ("-----");
						linija = i.ToString () + "Level: 1";
						sw.WriteLine (linija);
						linija = i.ToString () + "Experience: 0";
						sw.WriteLine (linija);
						linija = i.ToString () + "statPoints: 0";
						sw.WriteLine (linija);
						linija = i.ToString () + "abilityPoints: 0";
						sw.WriteLine (linija);
						linija = i.ToString () + "hpUp: 0";
						sw.WriteLine (linija);
						linija = i.ToString () + "enUp: 0";
						sw.WriteLine (linija);
						linija = i.ToString () + "regUp: 0";
						sw.WriteLine (linija);
						linija = i.ToString () + "movUp: 0";
						sw.WriteLine (linija);
						linija = i.ToString () + "pdUp: 0";
						sw.WriteLine (linija);
						linija = i.ToString () + "sdUp: 0";
						sw.WriteLine (linija);
					}
				}
			}
		}
		Application.LoadLevel (level);
	}
}
