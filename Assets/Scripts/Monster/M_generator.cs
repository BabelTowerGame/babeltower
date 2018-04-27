using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tob;

public class M_generator : MonoBehaviour {
	//testing part
	//change dynamic loading method later according to team leader decision
	//Using Resource.Load or AssetBundle
	//Using this part only for testing.
	private int sword_num = 1;
	private int trident_num = 1;
	private float[] regenCounter = new float[50];
	public GameObject[] monsterList = new GameObject[50];
	NetworkService NS = NetworkService.Instance;
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
		/* constant gen*/
		Vector3 hard = new Vector3 (1168.5f, 136.4f, 611.0f);
		Vector3 mobLocation1 = hard;
		Vector3 mobLocation2 = hard;
		Vector3 mobLocation3 = hard;
		Vector3 mobLocation4 = hard;
		mobLocation1.x += 50.0f;
		mobLocation1.z += 50.0f;
		mobLocation1.y = 150.0f;
		mobLocation2.x += 40.0f;
		mobLocation2.z += 40.0f;
		mobLocation2.y = 150.0f;
		mobLocation3.x += 40.0f;
		mobLocation3.z += 50.0f;
		mobLocation3.y = 150.0f;
		mobLocation4.x += 20.0f;
		mobLocation4.z += 50.0f;
		mobLocation4.y = 150.0f;
		GameObject monster1;
		GameObject monster2;
		GameObject monster3;
		GameObject monster4;
		monster1 = GenerateMonster(50,M_generator.DemonType.Demon1, M_generator.DemonSkin.Demons1, 
			M_generator.WeaponType.Sword, mobLocation1);
		monster2 = GenerateMonster(51,M_generator.DemonType.Demon2, M_generator.DemonSkin.Demons2,
			M_generator.WeaponType.Trident, mobLocation2);
		monster3 = GenerateMonster(52,M_generator.DemonType.Demon3, M_generator.DemonSkin.Demons3,
			M_generator.WeaponType.Hammer, mobLocation3);
		monster4 = GenerateMonster(53,M_generator.DemonType.Demon4, M_generator.DemonSkin.Demons4,
			M_generator.WeaponType.Pike, mobLocation4);
		

		NS.SendMessage("OnMonsterEvent",)
		//random gen
		for (int i = 0; i < 50; i++) {
			regenCounter [i] = 60.0f;
			int iUp=2400; 
			int iDown=1;
			float resultX = Random.Range (1000, 1200);
			float resultZ = Random.Range (500, 700);
			DemonType dt = RdType ();
			DemonSkin ds = RdSkin ();
			WeaponType wt = RdWeapon ();
			Vector3 gencoord  = new Vector3(resultX,300,resultZ);
			monsterList[i] = GenerateMonster(i, dt, ds, wt, gencoord);
			MonsterEvent e = new MonsterSpawnEvent();
			e.Spawn.Id = i.ToString();
			e.Spawn.Position = gencoord;
			e.Spawn.DemonSkin = ds;
			e.Spawn.DemonType = dt;
			e.Spawn.WeaponType = wt;
			NS.SendMessage("OnMonsterEvent",e);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (NetworkService.isServer) {
			//server;
			for (int i = 0; i < regenCounter.Length; i++) {
				if (regenCounter [i] < 60.0f) {
					regenCounter [i] -= Time.deltaTime;
				}
				if (regenCounter [i] <= 0.0f) {
					int iUp=2400; 
					int iDown=1;
					float resultX = Random.Range (1000, 1200);
					float resultZ = Random.Range (500, 700);
					DemonType dt = RdType ();
					DemonSkin ds = RdSkin ();
					WeaponType wt = RdWeapon ();
					Vector3 gencoord  = new Vector3(resultX,300,resultZ);
					monsterList[i] = GenerateMonster(i, dt, ds, wt, gencoord);
					MonsterEvent e = new MonsterSpawnEvent();
					e.Spawn.Id = i.ToString();
					e.Spawn.Position = gencoord;
					e.Spawn.DemonSkin = ds;
					e.Spawn.DemonType = dt;
					e.Spawn.WeaponType = wt;
					NS.SendMessage("OnMonsterEvent",e);
					regenCounter [i] = 60.0f;
				}
			}
			
			//server
		} else {
			//client
		}
		
		
	}

	/*public GameObject GenerateMonster(BullHoundSkin BS,Vector3 position,Vector3 rotation){ 
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
		
	}*/



	public GameObject GenerateMonster(int uid,DemonType DT,DemonSkin DS,WeaponType WT,Vector3 position){
		//overload generate 
		//generate generate Demon

		GameObject Monster;
		Vector3 rotation = new Vector3 (0, Random.Range (0, 360), 0);
		string Demonpath = "Monster/Demon/Prefab/" + DT.ToString ();
		string skinpath = "Monster/Demon/Material/" + DS.ToString ();
		GameObject Demon = Resources.Load (Demonpath,typeof(GameObject)) as GameObject;
		Monster = Instantiate (Demon, position, Quaternion.identity);
		float temp1 = position.x / 240.0f;
		float temp2 = position.y / 240.0f;
		int levelZoneX = (int)temp1;
		int levelZoneY = (int)temp2;
		if (levelZoneX == 0) {
			levelZoneX = 1;
		}
		if (levelZoneY == 0) {
			levelZoneY = 1;
		}
		int finalLevel = levelZoneX + levelZoneY;
		Monster.GetComponent<Monster> ().Level = finalLevel;
		Monster.GetComponent<Monster> ().Health = finalLevel * 10.0f;
		Monster.GetComponent<Monster>().Current_health = finalLevel * 10.0f;
		Monster.GetComponent<Monster> ().Damage = finalLevel * 2;
		Monster.GetComponent<Monster> ().Defense = finalLevel * 2;
		Monster.GetComponent<Monster> ().Name = DT.ToString ();
		Monster.GetComponent<Monster> ().InBattle = false;
		Monster.GetComponent<Monster> ().InMovement = false;
		Monster.tag = "Monster";
		Monster.GetComponent<Monster> ().ID = uid;
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
	DemonType RdType(){
		int result = Random.Range (0, 3);
		return (DemonType)result;
	}
	DemonSkin RdSkin(){
		int result = Random.Range (0, 20);
		return (DemonSkin)result;
	}
	WeaponType RdWeapon(){
		int result = Random.Range (0, 5);
		return (WeaponType)result;
		
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
			//Debug.Log (item_name);
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
	public void destroyMonster(int id){
		Destroy (monsterList [id]);
		regenCounter [id] -= 1.0f;
	}
}
