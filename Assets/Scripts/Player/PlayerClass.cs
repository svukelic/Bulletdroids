using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class PlayerClass : MonoBehaviour {

	string direktorij, provjera, provjera2, spremanje="", line, provjera_load, tempStr, className;
	int prebroj=0;

	public int klasa;

	GameObject player, Center, Front, Back, klasaTekstObjekt, abilityImageHUD1, abilityImageHUD2, abilityImageHUD3, LeftWing, RightWing;
	Text klasaTekst;
	Image abilityImage1, abilityImage2, abilityImage3;
	PlayerShooting lW, rW;
	PlayerMovement kretanje;
	PlayerHealth pHP;
	PlayerEnergy pEN;
	LineRenderer lWLR, rWLR;

	ParticleSystem particles, lWParticles, rWParticles;

	int tempHP=0, level, experience, fireEffect1, fireEffect2;
	float tempEN=0, tempREG=0, tempMov=0, tempPROF=0, tempSROF=0, tempPDMG=0, tempSDMG=0;

	ClassData classData = new ClassData();

	int rHealth;
	float rEnergy, rRegen, rMovement, rPROF, rSROF, rPDMG, rSDMG;

	GameObject testText;
	Text test;

	public Slider expSlider;

	public void UpdateExp(int increase)
	{
		experience += increase;
		if (experience >= 1000) {
			experience -= 1000;
			level ++;
			tempStr = className + level.ToString ();
			klasaTekst.text = tempStr;
		}
		expSlider.value = experience;
	}

	public void SaveProgress()
	{
		string[] arrLine = File.ReadAllLines(provjera);
		int prebroj, prebroj2;

		prebroj = 2 + (klasa - 1) * 11;
		arrLine [prebroj] = klasa.ToString () + "Level: " + level.ToString ();
		prebroj2 = 3 + (klasa - 1) * 11;
		arrLine [prebroj2] = klasa.ToString () + "Experience: " + experience.ToString ();

		File.WriteAllLines(provjera, arrLine);
	}

	void Awake ()
	{
		testText = GameObject.Find ("TestText");
		test = testText.GetComponent<Text> ();

		player = GameObject.FindGameObjectWithTag ("Player");
		kretanje = player.GetComponent <PlayerMovement> ();
		pHP = player.GetComponent <PlayerHealth> ();
		pEN = player.GetComponent <PlayerEnergy> ();
//		Center = GameObject.Find ("Center");
//		Front = GameObject.Find ("Front");
//		Back = GameObject.Find ("Back");
		LeftWing = GameObject.Find ("LeftWing");
		RightWing = GameObject.Find ("RightWing");
		lW = LeftWing.GetComponent <PlayerShooting> ();
		rW = RightWing.GetComponent <PlayerShooting> ();
		lWLR = LeftWing.GetComponent <LineRenderer> ();
		rWLR = RightWing.GetComponent <LineRenderer> ();
		lWParticles = LeftWing.GetComponent <ParticleSystem> ();
		rWParticles = RightWing.GetComponent <ParticleSystem> ();
		klasaTekstObjekt = GameObject.Find ("ClassNameText");
		klasaTekst = klasaTekstObjekt.GetComponent <Text> ();
		abilityImageHUD1 = GameObject.FindGameObjectWithTag ("Ability1");
		abilityImage1 = abilityImageHUD1.GetComponent <Image> ();
		abilityImageHUD2 = GameObject.FindGameObjectWithTag ("Ability2");
		abilityImage2 = abilityImageHUD2.GetComponent <Image> ();
		abilityImageHUD3 = GameObject.FindGameObjectWithTag ("Ability3");
		abilityImage3 = abilityImageHUD3.GetComponent <Image> ();

		direktorij = Directory.GetCurrentDirectory ();
		
		provjera = direktorij + @"\radioaktivne_mrkve.txt";
		provjera2 = direktorij + @"\radioaktivne_mrkve2.txt";

		if (!File.Exists (provjera)) {
			klasa = 1;
		} else {
			System.IO.StreamReader file = new System.IO.StreamReader (provjera);
			while ((line = file.ReadLine()) != null) {
				if(line.Contains("Klasa: ")){
					spremanje = line;
					spremanje = spremanje.Replace ("Klasa: ", "");
				}
			}
			file.Close ();
			klasa = int.Parse (spremanje);
			if (klasa < 1 || klasa > 5)
				klasa = 1;
			file.Close ();
			System.IO.StreamReader file2 = new System.IO.StreamReader (provjera);
			while ((line = file2.ReadLine()) != null) {
				provjera_load = klasa.ToString() + "Level: ";
				if(line.Contains(provjera_load)){
					spremanje = line;
					spremanje = spremanje.Replace (provjera_load, "");
					level = int.Parse (spremanje);
				}
				provjera_load = klasa.ToString() + "Experience: ";
				if(line.Contains(provjera_load)){
					spremanje = line;
					spremanje = spremanje.Replace (provjera_load, "");
					experience = int.Parse (spremanje);
				}

				provjera_load = klasa.ToString() + "hpUp: ";
				if(line.Contains(provjera_load)){
					spremanje = line;
					spremanje = spremanje.Replace (provjera_load, "");
					tempHP = int.Parse (spremanje);
				}
				provjera_load = klasa.ToString() + "pdUp: ";
				if(line.Contains(provjera_load)){
					spremanje = line;
					spremanje = spremanje.Replace (provjera_load, "");
					tempPDMG = float.Parse (spremanje);
				}
				provjera_load = klasa.ToString() + "sdUp: ";
				if(line.Contains(provjera_load)){
					spremanje = line;
					spremanje = spremanje.Replace (provjera_load, "");
					tempSDMG = float.Parse (spremanje);
				}
				provjera_load = klasa.ToString() + "enUp: ";
				if(line.Contains(provjera_load)){
					spremanje = line;
					spremanje = spremanje.Replace (provjera_load, "");
					tempEN = float.Parse (spremanje);
				}
				provjera_load = klasa.ToString() + "regUp: ";
				if(line.Contains(provjera_load)){
					spremanje = line;
					spremanje = spremanje.Replace (provjera_load, "");
					tempREG = float.Parse (spremanje);
				}
				provjera_load = klasa.ToString() + "movUp: ";
				if(line.Contains(provjera_load)){
					spremanje = line;
					spremanje = spremanje.Replace (provjera_load, "");
					tempMov = float.Parse (spremanje);
				}
			}
			file2.Close ();
		}

		if (klasa == 1) {
			className = "Aurelius ";
			fireEffect1 = 1;
			fireEffect2 = 2;

			Sprite newSprite =  Resources.Load <Sprite>("Images/64 flat icons/png/128px/Elements_Nature");
			if(newSprite) abilityImage1.sprite = newSprite;
			//Heal healComp = Center.AddComponent<Heal>() as Heal;
		}
		if (klasa == 2){
			className = "Bismarck ";
			fireEffect1 = 0;
			fireEffect2 = 3;

			MeshRenderer mesh = player.GetComponent<MeshRenderer> ();
			mesh.material = Resources.Load <Material>("Meshes/SciFi_Fighter_AK5-diffuse-Yellow");

			Sprite newSprite =  Resources.Load <Sprite>("Images/64 flat icons/png/128px/Weapons_Bow");
			if(newSprite) abilityImage1.sprite = newSprite;
			Front = (GameObject)Instantiate(Resources.Load("Parts/FrontBismarck"));
			Front.transform.parent = player.transform;

			Sprite newSprite2 =  Resources.Load <Sprite>("Images/64 flat icons/png/128px/Weapons_Hammer");
			if(newSprite2) abilityImage2.sprite = newSprite2;
			Center = (GameObject)Instantiate(Resources.Load("Parts/CenterBismarck"));
			Center.transform.parent = player.transform;

			Sprite newSprite3 =  Resources.Load <Sprite>("Images/64 flat icons/png/128px/Equipment_Shield");
			if(newSprite3) abilityImage3.sprite = newSprite3;
			Back = (GameObject)Instantiate(Resources.Load("Parts/BackBismarck"));
			Back.transform.parent = player.transform;
		}
		if (klasa == 3){
			className = "Themistocles ";
			fireEffect1 = 0;
			fireEffect2 = 2;

			Sprite newSprite3 =  Resources.Load <Sprite>("Images/64 flat icons/png/128px/Weapons_Bomb");
			if(newSprite3) abilityImage3.sprite = newSprite3;
			Front = (GameObject)Instantiate(Resources.Load("Parts/FrontThemistocles"));
			Front.transform.parent = player.transform;

			Sprite newSprite2 =  Resources.Load <Sprite>("Images/64 flat icons/png/128px/Modifiers_Time");
			if(newSprite2) abilityImage2.sprite = newSprite2;
			Center = (GameObject)Instantiate(Resources.Load("Parts/CenterThemistocles"));
			Center.transform.parent = player.transform;

			Sprite newSprite1 =  Resources.Load <Sprite>("Images/64 flat icons/png/128px/Equipment_Boots");
			if(newSprite1) abilityImage1.sprite = newSprite1;
			Back = (GameObject)Instantiate(Resources.Load("Parts/BackThemistocles"));
			Back.transform.parent = player.transform;
		}
		if (klasa == 4){
			className = "Tesla ";
			fireEffect1 = 0;
			fireEffect2 = 5;

			MeshRenderer mesh = player.GetComponent<MeshRenderer> ();
			mesh.material = Resources.Load <Material>("Meshes/SciFi_Fighter_AK5-diffuse-Blue");

			rWParticles.startColor = Color.blue;
			lWParticles.startColor = Color.blue;

			Sprite newSprite1 =  Resources.Load <Sprite>("Images/64 flat icons/png/128px/Modifiers_Accuracy");
			if(newSprite1) abilityImage1.sprite = newSprite1;
			Center = (GameObject)Instantiate(Resources.Load("Parts/BackTesla"));
			Center.transform.parent = player.transform;

			Sprite newSprite2 =  Resources.Load <Sprite>("Images/64 flat icons/png/128px/Elements_Air");
			if(newSprite2) abilityImage2.sprite = newSprite2;
			Center = (GameObject)Instantiate(Resources.Load("Parts/CenterTesla"));
			Center.transform.parent = player.transform;

			Sprite newSprite3 =  Resources.Load <Sprite>("Images/64 flat icons/png/128px/Modifiers_LevelUp");
			if(newSprite3) abilityImage3.sprite = newSprite3;
			Center = (GameObject)Instantiate(Resources.Load("Parts/FrontTesla"));
			Center.transform.parent = player.transform;
		}
		if (klasa == 5){
			className = "Archimedes ";
			fireEffect1 = 0;
			fireEffect2 = 4;

			Sprite newSprite1 =  Resources.Load <Sprite>("Images/64 flat icons/png/128px/Elements_Fire");
			if(newSprite1) abilityImage1.sprite = newSprite1;
			Front = (GameObject)Instantiate(Resources.Load("Parts/FrontArchimedes"));
			Front.transform.parent = player.transform;

			Sprite newSprite2 =  Resources.Load <Sprite>("Images/64 flat icons/png/128px/World_RoseOfWinds");
			if(newSprite2) abilityImage2.sprite = newSprite2;
			Back = (GameObject)Instantiate(Resources.Load("Parts/BackArchimedes"));
			Back.transform.parent = player.transform;

			Sprite newSprite3 =  Resources.Load <Sprite>("Images/64 flat icons/png/128px/UI_Gamepad");
			if(newSprite3) abilityImage3.sprite = newSprite3;
			Center = (GameObject)Instantiate(Resources.Load("Parts/CenterArchimedes"));
			Center.transform.parent = player.transform;
			//Drones dronesComp = Center.AddComponent<Drones>();
		}
		tempStr = className + level.ToString ();
		klasaTekst.text = tempStr;

		expSlider.value = experience;

		rHealth = classData.RetrieveInteger (1, klasa);
		rHealth = rHealth + tempHP;
		rEnergy = classData.RetrieveFloat (1, klasa);
		rEnergy = rEnergy + tempEN;
		rRegen = classData.RetrieveFloat (2, klasa);
		rRegen = rRegen + tempREG;
		rMovement = classData.RetrieveFloat (3, klasa);
		rMovement = rMovement + tempMov;
		rPROF = classData.RetrieveFloat (4, klasa);

		rSROF = classData.RetrieveFloat (5, klasa);

		rPDMG = classData.RetrieveFloat (6, klasa);
		rPDMG = rPDMG + tempPDMG;
		rSDMG = classData.RetrieveFloat (7, klasa);
		rSDMG = rSDMG + tempSDMG;

		float tempDelay1, tempDelay2;
		tempDelay1 = 0.2f / (rPROF / 0.15f);
		tempDelay2 = 0.2f / (rSROF / 0.15f);

		pHP.SetHealth(rHealth);
		pEN.SetEnergy(rEnergy, rRegen);
		kretanje.SetSpeed(rMovement);
		lW.SetData(rPDMG, rPROF, 80f, "Fire1", tempDelay1, fireEffect1);
		rW.SetData(rSDMG, rSROF, 80f, "Fire2", tempDelay2, fireEffect2);

		string tempLineRender = "LineRenderMaterial" + klasa.ToString ();
		lWLR.material = Resources.Load <Material>(tempLineRender);
		rWLR.material = Resources.Load <Material>(tempLineRender);
	}
}
