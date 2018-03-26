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
	public enum BullHoundType
	{
		BullHound1,
		BullHound2
	}
	public enum BullHoundSkin
	{
		BullHounds1,
		Bullhounds2
	}
	[SerializeField] private GameObject DemonPrefab_1;
	[SerializeField] private GameObject DemonPrefab_2;
	[SerializeField] private GameObject DemonPrefab_3;
	[SerializeField] private GameObject DemonPrefab_4;
	[SerializeField] private GameObject BullHoundPrefab_1;
	[SerializeField] private GameObject BullHoundPrefab_2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void GenerateMonster(BullHoundType BT,BullHoundSkin BS,Vector3 position,Vector3 rotation){ 
		GameObject Monster;
		//iterate through enum to decide which tyep of BullHound
		Monster = Instantiate (BullHoundPrefab_1, position, Quaternion.identity);
		Monster.GetComponent<Transform> ().Rotate (rotation.x, rotation.y, rotation.z);
		//generate BullHound
		
	}
	void GenerateMonster(DemonType DT,DemonSkin DS,Vector3 position,Vector3 rotation){
		//overload generate 
		//generate generate Demon
		GameObject Monster;
		Monster = Instantiate (DemonPrefab_1, position, Quaternion.identity);
		Monster.GetComponent<Transform> ().Rotate (rotation.x, rotation.y, rotation.z);
	}
	void setDemonSkin(){
	}
	void setBullhoundSkin(){
	}
}
