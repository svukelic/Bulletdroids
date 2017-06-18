using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class ClassSave : MonoBehaviour {

	string direktorij, provjera, linija, line, spremanje, provjera_load, provjera_tekst, tempText;
	int classNumber;

	ClassData classData = new ClassData();

	GameObject stat1, stat2, statClass, abClass;
	Text statPoint, abPoint, statText, abText;
	public GameObject Panel;

	int[] setText = new int[6];

	int[] level = new int[6];
	int[] experience = new int[6];
	int[] statPoints = new int[6];
	int[] abilityPoints = new int[6];
	int[] hpUp = new int[6];
	float[] pdUp = new float[6];
	float[] sdUp = new float[6];
	float[] enUp = new float[6];
	float[] regUp = new float[6];
	float[] movUp  = new float[6];

	void Awake()
	{
		stat1 = GameObject.Find ("StatPointsText");
		statPoint = stat1.GetComponent <Text> ();
		stat2 = GameObject.Find ("AbilityPointsText");
		abPoint = stat2.GetComponent <Text> ();

		for (int j=1; j<6; j++)
			setText [j] = 0;

		direktorij = Directory.GetCurrentDirectory ();
		
		provjera = direktorij + @"\radioaktivne_mrkve.txt";

		if (!File.Exists (provjera)) {
			using (StreamWriter sw = File.CreateText(provjera)) {
				linija = "Klasa: 1";
				sw.WriteLine (linija);
				for(int i=1; i<6; i++){
					sw.WriteLine ("-----");
					linija = i.ToString() + "Level: 1";
					sw.WriteLine (linija);
					linija = i.ToString() + "Experience: 0";
					sw.WriteLine (linija);
					linija = i.ToString() + "statPoints: 0";
					sw.WriteLine (linija);
					linija = i.ToString() + "abilityPoints: 0";
					sw.WriteLine (linija);
					linija = i.ToString() + "hpUp: 0";
					sw.WriteLine (linija);
					linija = i.ToString() + "enUp: 0";
					sw.WriteLine (linija);
					linija = i.ToString() + "regUp: 0";
					sw.WriteLine (linija);
					linija = i.ToString() + "movUp: 0";
					sw.WriteLine (linija);
					linija = i.ToString() + "pdUp: 0";
					sw.WriteLine (linija);
					linija = i.ToString() + "sdUp: 0";
					sw.WriteLine (linija);
				}
			}
		}// else {
			System.IO.StreamReader file = new System.IO.StreamReader (provjera);
			while ((line = file.ReadLine()) != null) {
				for(int i=1; i<6; i++){
					provjera_load = i.ToString() + "Level: ";
					if(line.Contains(provjera_load)){
						spremanje = line;
						spremanje = spremanje.Replace (provjera_load, "");
						level[i] = int.Parse (spremanje);
					}
					provjera_load = i.ToString() + "Experience: ";
					if(line.Contains(provjera_load)){
						spremanje = line;
						spremanje = spremanje.Replace (provjera_load, "");
						experience[i] = int.Parse (spremanje);
					}
					provjera_load = i.ToString() + "statPoints: ";
					if(line.Contains(provjera_load)){
						spremanje = line;
						spremanje = spremanje.Replace (provjera_load, "");
						statPoints[i] = int.Parse (spremanje);
					}
					provjera_load = i.ToString() + "abilityPoints: ";
					if(line.Contains(provjera_load)){
						spremanje = line;
						spremanje = spremanje.Replace (provjera_load, "");
						abilityPoints[i] = int.Parse (spremanje);
					}
					provjera_load = i.ToString() + "hpUp: ";
					if(line.Contains(provjera_load)){
						spremanje = line;
						spremanje = spremanje.Replace (provjera_load, "");
						hpUp[i] = int.Parse (spremanje);
					}
					provjera_load = i.ToString() + "pdUp: ";
					if(line.Contains(provjera_load)){
						spremanje = line;
						spremanje = spremanje.Replace (provjera_load, "");
						pdUp[i] = float.Parse (spremanje);
					}
					provjera_load = i.ToString() + "sdUp: ";
					if(line.Contains(provjera_load)){
						spremanje = line;
						spremanje = spremanje.Replace (provjera_load, "");
						sdUp[i] = float.Parse (spremanje);
					}
					provjera_load = i.ToString() + "enUp: ";
					if(line.Contains(provjera_load)){
						spremanje = line;
						spremanje = spremanje.Replace (provjera_load, "");
						enUp[i] = float.Parse (spremanje);
					}
					provjera_load = i.ToString() + "regUp: ";
					if(line.Contains(provjera_load)){
						spremanje = line;
						spremanje = spremanje.Replace (provjera_load, "");
						regUp[i] = float.Parse (spremanje);
					}
					provjera_load = i.ToString() + "movUp: ";
					if(line.Contains(provjera_load)){
						spremanje = line;
						spremanje = spremanje.Replace (provjera_load, "");
						movUp[i] = float.Parse (spremanje);
					}
				}
			}
		//}
	}

	public int RetrieveInt(int retrieveChoice, int retrieveClass)
	{
		if (retrieveChoice == 1)
			return statPoints [retrieveClass];
		if (retrieveChoice == 2)
			return abilityPoints [retrieveClass];
		else
			return 0;
	}

	public int RetrieveCN()
	{
		return classNumber;
	}

	public void Pamti(int temp)
	{
		classNumber = temp;

		Panel.SetActive (true);

		UpdateText ();
	}

	public void PointSpent (int type, int choice)
	{
		if (type == 1) {
			switch(choice){
			case 1: 
				hpUp[classNumber] += 1;
				break;
			case 2: 
				enUp[classNumber] += 1f;
				break;
			case 3: 
				regUp[classNumber] += 0.1f;
				break;
			case 4: 
				movUp[classNumber] += 0.1f;
				break;
			case 5:
				pdUp[classNumber] += 1f;
				break;
			case 6: 
				sdUp[classNumber] += 1f;
				break;
			}
			statPoints[classNumber] -= 1;
			UpdateText ();
		}
	}

	public void UpdateText()
	{
		statPoint.text = "Stat Points: " + statPoints [classNumber];
		
		statClass = GameObject.Find ("StatText");
		statText = statClass.GetComponent <Text> ();
		statText.enabled = true;
		
		if (classNumber == 1) {
			tempText = "Name: Aurelius";
		}
		if (classNumber == 2) {
			tempText = "Name: Bismarck";
		}
		if (classNumber == 3) {
			tempText = "Name: Themistocles";
		}
		if (classNumber == 4) {
			tempText = "Name: Tesla";
		}
		if (classNumber == 5) {
			tempText = "Name: Archimedes";
		}
		
		int tempHP;
		float tempEN, tempREG, tempMov, tempPROF, tempSROF, tempPDMG, tempSDMG;

		tempPDMG = classData.RetrieveFloat (6, classNumber);
		tempSDMG = classData.RetrieveFloat (7, classNumber);
		tempHP = classData.RetrieveInteger (1, classNumber);
		tempEN = classData.RetrieveFloat (1, classNumber);
		tempREG = classData.RetrieveFloat (2, classNumber);
		tempMov = classData.RetrieveFloat (3, classNumber);
		tempPROF = classData.RetrieveFloat (4, classNumber);
		tempSROF = classData.RetrieveFloat (5, classNumber);
		
		tempText = tempText + "NEWLINE Level: " + (level[classNumber]).ToString();
		tempText = tempText + "NEWLINE Experience: " + (experience[classNumber]).ToString();
		tempText = tempText + "NEWLINE Health: " + (tempHP+hpUp[classNumber]).ToString();
		tempText = tempText + "NEWLINE Energy: " + (tempEN+enUp[classNumber]).ToString();
		tempText = tempText + "NEWLINE Energy Regen: " + (tempREG+regUp[classNumber]).ToString();
		tempText = tempText + "NEWLINE Movement: " + (tempMov+movUp[classNumber]).ToString();
		tempText = tempText + "NEWLINE";
		tempText = tempText + "NEWLINE Primary DPS: " + ((tempPDMG+pdUp[classNumber])/tempPROF).ToString();
		tempText = tempText + "NEWLINE Primary Effect: Slow";
		tempText = tempText + "NEWLINE Secondary DPS: " + ((tempSDMG+sdUp[classNumber])/tempSROF).ToString();
		tempText = tempText + "NEWLINE Secondary Effect: Slow";
		tempText = tempText.Replace ("NEWLINE", "\n");
		statText.text = tempText;
	}

	public void Sejvaj(){

		direktorij = Directory.GetCurrentDirectory ();
		
		provjera = direktorij + @"\radioaktivne_mrkve.txt";

		using (StreamWriter sw = File.CreateText(provjera)) {
			linija = "Klasa: " + classNumber.ToString();
			sw.WriteLine (linija);
			for(int i=1; i<6; i++){
				sw.WriteLine ("-----");
				linija = i.ToString() + "Level: " + level[i].ToString();
				sw.WriteLine (linija);
				linija = i.ToString() + "Experience: " + experience[i].ToString();
				sw.WriteLine (linija);
				linija = i.ToString() + "statPoints: " + statPoints[i].ToString();
				sw.WriteLine (linija);
				linija = i.ToString() + "abilityPoints: " + abilityPoints[i].ToString();
				sw.WriteLine (linija);
				linija = i.ToString() + "hpUp: " + hpUp[i].ToString();
				sw.WriteLine (linija);
				linija = i.ToString() + "enUp: " + enUp[i].ToString();
				sw.WriteLine (linija);
				linija = i.ToString() + "regUp: " + regUp[i].ToString();
				sw.WriteLine (linija);
				linija = i.ToString() + "movUp: " + movUp[i].ToString();
				sw.WriteLine (linija);
				linija = i.ToString() + "pdUp: " + pdUp[i].ToString();
				sw.WriteLine (linija);
				linija = i.ToString() + "sdUp: " + sdUp[i].ToString();
				sw.WriteLine (linija);
			}
		}
	}
}
