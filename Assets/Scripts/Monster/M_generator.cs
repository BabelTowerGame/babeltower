using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_generator : MonoBehaviour {
	//testing part
	//change dynamic loading method later according to team leader decision
	//Using Resource.Load or AssetBundle
	//Using this part only for testing.
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
		Cudge,
		Scythe,
		Trident,
		Pike,
		Hammer,
		Bow,
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

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void GenerateMonster(BullHoundSkin BS,Vector3 position,Vector3 rotation){ 
		GameObject Monster;
		GameObject Hound = Resources.Load ("Monster/Bullhound/Prefab/bullhound_hi_Prefab",typeof(GameObject)) as GameObject;
		string skinpath = "Monster/Bullhound/Material/" + BS.ToString ();
		Monster = Instantiate (Hound, position, Quaternion.identity);
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
		Transform shader = Monster.transform.FindChild ("Demon");
		Material[] m;
		m = shader.GetComponent<SkinnedMeshRenderer> ().materials;
		Material test = Resources.Load (skinpath,typeof(Material))as Material;
		m [0] = test;
		shader.GetComponent<SkinnedMeshRenderer> ().materials = m;
		Monster.GetComponent<Transform> ().Rotate (rotation.x, rotation.y, rotation.z);
	}
}
