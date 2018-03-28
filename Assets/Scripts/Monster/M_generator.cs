using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_generator : MonoBehaviour {
	//testing part
	//change dynamic loading method later according to team leader decision
	//Using Resource.Load or AssetBundle
	//Using this part only for testing.
	private int sword_num = 1;
	private int trident_num = 1;
	public enum DemonType {
		Demon1,
		Demon2,
		Demon3,
		Demon4
	}
	public enum DemonSkin
	{
		Demons1,
		Demons2,
		Demons3,
		Demons4,
		Demons5,
		Demons6,
		Demons7,
		Demons8,
		Demons9,
		Demons10,
		Demons11,
		Demons12,
		Demons13,
		Demons14,
		Demons15,
		Demons16,
		Demons17,
		Demons18,
		Demons19,
		Demons20,
		Demons21

	}
	public enum WeaponType{
		Sword,
		Scythe,
		Trident,
		Pike,
		Hammer,
		Axe

	}
	public enum BullHoundSkin
	{
		bullhound1,
		bullhound2,
		bullhound3,
		bullhound4,
		bullhound5,
		bullhound6,
		bullhound7,
		bullhound8,
		bullhound9,
		bullhound10
	}
	// Use this for initialization
	void Start () {
		GenerateMonster(BullHoundSkin.bullhound1,new Vector3(1,2,3),new Vector3(0,180,0));
		GenerateMonster(BullHoundSkin.bullhound2,new Vector3(2,2,3),new Vector3(0,180,0));
		GenerateMonster(BullHoundSkin.bullhound3,new Vector3(3,2,3),new Vector3(0,180,0));
		GenerateMonster(BullHoundSkin.bullhound4,new Vector3(4,2,3),new Vector3(0,180,0));
		GenerateMonster(BullHoundSkin.bullhound5,new Vector3(5,2,3),new Vector3(0,180,0));
		GenerateMonster(BullHoundSkin.bullhound6,new Vector3(0,2,3),new Vector3(0,180,0));
		GenerateMonster(BullHoundSkin.bullhound7,new Vector3(-1,2,3),new Vector3(0,180,0));
		GenerateMonster(BullHoundSkin.bullhound8,new Vector3(-2,2,3),new Vector3(0,180,0));
		GenerateMonster(BullHoundSkin.bullhound9,new Vector3(-3,2,3),new Vector3(0,180,0));
		GenerateMonster(BullHoundSkin.bullhound10,new Vector3(-4,2,3),new Vector3(0,180,0));
		GenerateMonster(DemonType.Demon1,DemonSkin.Demons1,WeaponType.Sword,new Vector3(3,4,4),new Vector3(0,180,0));
		GenerateMonster(DemonType.Demon2,DemonSkin.Demons2,WeaponType.Sword,new Vector3(2,4,4),new Vector3(0,180,0));
		GenerateMonster(DemonType.Demon3,DemonSkin.Demons3,WeaponType.Sword,new Vector3(1,4,4),new Vector3(0,180,0));
		GenerateMonster(DemonType.Demon4,DemonSkin.Demons4,WeaponType.Sword,new Vector3(0,4,4),new Vector3(0,180,0));
		GenerateMonster(DemonType.Demon2,DemonSkin.Demons5,WeaponType.Pike,new Vector3(-1,4,4),new Vector3(0,180,0));
		GenerateMonster(DemonType.Demon3,DemonSkin.Demons6,WeaponType.Hammer,new Vector3(-2,4,4),new Vector3(0,180,0));

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void GenerateMonster(BullHoundSkin BS,Vector3 position,Vector3 rotation){ 
		GameObject Monster;
		GameObject Hound = Resources.Load ("Monster/Bullhound/Prefab/bullhound_hi_Prefab",typeof(GameObject)) as GameObject;
		string skinpath = "Monster/Bullhound/Material/" + BS.ToString ();
		Monster = Instantiate (Hound, position, Quaternion.identity);
		Monster.tag = "Monster";
		Transform shader = Monster.transform.FindChild ("bullhound_hi");
		Material[] m;
		m = shader.GetComponent<SkinnedMeshRenderer> ().materials;
		Material test = Resources.Load (skinpath,typeof(Material))as Material;
		m [0] = test;
		shader.GetComponent<SkinnedMeshRenderer> ().materials = m;

		Monster.GetComponent<Transform> ().Rotate (rotation.x, rotation.y, rotation.z);

		//generate BullHound
		
	}
	void GenerateMonster(DemonType DT,DemonSkin DS,WeaponType WT,Vector3 position,Vector3 rotation){
		//overload generate 
		//generate generate Demon
		GameObject Monster;
		string Demonpath = "Monster/Demon/Prefab/" + DT.ToString ();
		string skinpath = "Monster/Demon/Material/" + DS.ToString ();
		GameObject Demon = Resources.Load (Demonpath,typeof(GameObject)) as GameObject;
		Monster = Instantiate (Demon, position, Quaternion.identity);
		Monster.tag = "Monster";
		Transform shader = Monster.transform.FindChild ("Demon");
		Material[] m;
		m = shader.GetComponent<SkinnedMeshRenderer> ().materials;
		Material test = Resources.Load (skinpath,typeof(Material))as Material;
		m [0] = test;
		shader.GetComponent<SkinnedMeshRenderer> ().materials = m;
		Monster.GetComponent<Transform> ().Rotate (rotation.x, rotation.y, rotation.z);
		initWeapon (Monster, WT);
	}
	void assignMotion(WeaponType WT){
		//for demon
		
	}

	void assignMotion(){
		//overload 
		//for bullhound
	}

	void initWeapon(GameObject Demon,WeaponType WT){
		//disable other weapon in model
		string item_name;
		GameObject weapon;
		for (int i = 0; i < Demon.transform.childCount; i++) {
			var child = Demon.transform.GetChild (i).gameObject;
			if (child != null) {
				if (child.name.Contains ("item")) {
					child.SetActive (false);
				}
			}
		}
		if (WT != WeaponType.Sword && WT != WeaponType.Trident) {
			item_name = "item_" + WT.ToString ().ToLower();
			Debug.Log (item_name);
			weapon = Demon.transform.FindChild (item_name).gameObject;
			weapon.SetActive (true);
		} else if (WT == WeaponType.Sword) {
			if (sword_num <= 6) {
				item_name = "item_sword_0" + sword_num.ToString ();
				sword_num++;
			} else {
				sword_num = 1;
				item_name = "item_sword_0" + sword_num.ToString ();
			}
			Debug.Log (item_name);
			weapon = Demon.transform.FindChild (item_name).gameObject;
			weapon.SetActive (true);
		} else if (WT == WeaponType.Trident) {
			if (trident_num <= 2) {
				item_name = "item_trident_0" + trident_num.ToString ();
				trident_num++;
			} else {
				trident_num = 1;
				item_name = "item_trident_0" + trident_num.ToString ();
			}
			weapon = Demon.transform.FindChild (item_name).gameObject;
			weapon.SetActive (true);

		}
	}
}
