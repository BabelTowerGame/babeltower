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
		GameObject monster1;
		GameObject monster2;
		GameObject monster3;
		GameObject monster4;
		monster1 = GenerateMonster(DemonType.Demon1,DemonSkin.Demons1,WeaponType.Sword,new Vector3(-10,0,0),new Vector3(0,180,0));
		monster2 = GenerateMonster(DemonType.Demon2,DemonSkin.Demons4,WeaponType.Pike,new Vector3(-15,0,3),new Vector3(0,180,0));
		monster3 = GenerateMonster(DemonType.Demon3,DemonSkin.Demons2,WeaponType.Hammer,new Vector3(20,0,5),new Vector3(0,180,0));
		monster4 = GenerateMonster(DemonType.Demon4,DemonSkin.Demons3,WeaponType.Trident,new Vector3(-25,0,7),new Vector3(0,180,0));
        //monster2 = GenerateMonster(BullHoundSkin.bullhound10, new Vector3(1, 4, 1), new Vector3(0, 180, 0));
		monster1.GetComponent<Monster> ().Current_health = 20;
		monster2.GetComponent<Monster> ().Current_health = 20;
		monster3.GetComponent<Monster> ().Current_health = 20;
		monster4.GetComponent<Monster> ().Current_health = 20;
		//monster2.GetComponent<Monster> ().Current_health = 20;
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	public GameObject GenerateMonster(BullHoundSkin BS,Vector3 position,Vector3 rotation){ 
		GameObject Monster;
		GameObject Hound = Resources.Load ("Monster/Bullhound/Prefab/bullhound_hi_Prefab",typeof(GameObject)) as GameObject;
		string skinpath = "Monster/Bullhound/Material/" + BS.ToString ();
		Monster = Instantiate (Hound, position, Quaternion.identity);
		Monster.tag = "Monster";
		Transform shader = Monster.transform.Find ("bullhound_hi");
		Material[] m;
		m = shader.GetComponent<SkinnedMeshRenderer> ().materials;
		Material test = Resources.Load (skinpath,typeof(Material))as Material;
		m [0] = test;
		shader.GetComponent<SkinnedMeshRenderer> ().materials = m;

		Monster.GetComponent<Transform> ().Rotate (rotation.x, rotation.y, rotation.z);
        Monster.GetComponent<AutoAttack>().OriPosition = position;
		//Physics.IgnoreCollision(Monster.GetComponent<Collider>(), GetComponent<Collider>());
        return Monster;
		//generate BullHound
		
	}
	public GameObject GenerateMonster(DemonType DT,DemonSkin DS,WeaponType WT,Vector3 position,Vector3 rotation){
		//overload generate 
		//generate generate Demon
		GameObject Monster;
		string Demonpath = "Monster/Demon/Prefab/" + DT.ToString ();
		string skinpath = "Monster/Demon/Material/" + DS.ToString ();
		GameObject Demon = Resources.Load (Demonpath,typeof(GameObject)) as GameObject;
		Monster = Instantiate (Demon, position, Quaternion.identity);
		Monster.tag = "Monster";
		Transform shader = Monster.transform.Find ("Demon");
		Material[] m;
		m = shader.GetComponent<SkinnedMeshRenderer> ().materials;
		Material test = Resources.Load (skinpath,typeof(Material))as Material;
		m [0] = test;
		shader.GetComponent<SkinnedMeshRenderer> ().materials = m;
		Monster.GetComponent<Transform> ().Rotate (rotation.x, rotation.y, rotation.z);
        Monster.GetComponent<AutoAttack>().OriPosition = position;
        initWeapon (Monster, WT);
        return Monster;
	}
	void assignMotion(WeaponType WT){
		//for demon
		//TODO different weapon different attack motion
	}

	void assignMotion(){
		//overload 
		//for bullhound
        //TODO different bullhound different attack motion
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
			weapon = Demon.transform.Find (item_name).gameObject;
			weapon.SetActive (true);
		} else if (WT == WeaponType.Sword) {
			if (sword_num <= 6) {
				item_name = "item_sword_0" + sword_num.ToString ();
				sword_num++;
			} else {
				sword_num = 1;
				item_name = "item_sword_0" + sword_num.ToString ();
			}
			//Debug.Log (item_name);
			weapon = Demon.transform.Find (item_name).gameObject;
			weapon.SetActive (true);
		} else if (WT == WeaponType.Trident) {
			if (trident_num <= 2) {
				item_name = "item_trident_0" + trident_num.ToString ();
				trident_num++;
			} else {
				trident_num = 1;
				item_name = "item_trident_0" + trident_num.ToString ();
			}
			weapon = Demon.transform.Find (item_name).gameObject;
			weapon.SetActive (true);

		}
	}
}
