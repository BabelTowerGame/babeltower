using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMonsterManager : MonoBehaviour {

	M_generator mgen ;
	AutoAttack auto;
	// Use this for initialization
	void Awake () {
		mgen = GetComponent<M_generator> ();
		
	}


	void OnMonsterSpawn() {
		//generate monster based on the data sent from server
		//TODO



		/*
		M_generator.DemonType  dt = (M_generator.DemonType)MonsterSpawnEvent.demonType;
		M_generator.DemonSkin ds = (M_generator.DemonSkin)MonsterSpawnEvent.demonSkin;
		M_generator.WeaponType wt = (M_generator.WeaponType)MonsterSpawnEvent.weaponType;

		mgen.GenerateMonster (MonsterSpawnEvent.id, dt, ds, wt, MonsterSpawnEvent.weaponType);
		*/

	}

	void OnMonsterDestory() {
		//TODO:
		//PARAMETER NEEDED MONSTER ID;
		mgen.destroyMonster(id);
		
	}

	void OnMonsterDie() {
		//TODO:
		//PARAMETER NEEDED MONSTER ID;
		mgen.monsterList[id].GetComponent<AutoAttack>().manual_die();
		mgen.monsterList[id].GetComponent<AutoAttack>().LootReady = true;
		
	}

	void OnMonsterMove() {
		//TODO:
		//PARAMETER NEEDED MONSTER ID,MONSTER POSITION, TARGET POSITION;



		mgen.monsterList[id].GetComponent<Monster>().X = MOSTERPOSITION.X;
		mgen.monsterList[id].GetComponent<Monster>().Y = MOSTERPOSITION.Y;
		mgen.monsterList[id].GetComponent<Monster>().Z = MOSTERPOSITION.Z;
		mgen.monsterList[id].GetComponent<Monster>().PX = TARGETPOSITION.X;
		mgen.monsterList[id].GetComponent<Monster>().PY = TARGETPOSITION.Y;
		mgen.monsterList[id].GetComponent<Monster>().PZ = TARGETPOSITION.Z;
		mgen.monsterList [id].GetComponent<Monster> ().Updated = true;
		
		
	}
	void OnMonsterPatrol(){
		//TODO:
		//PARAMETER NEEDED,ID
		mgen.monsterList[id].GetComponent<Monster>().InBattle = false;
		mgen.monsterList [id].GetComponent<Monster> ().InMovement = false;
	}

	void OnMonsterAttack() {
		//TODO: 
		//PARAMETER NEEDED MONSTER ID,TARGETPOSITION;
		mgen.monsterList [id].GetComponent<AutoAttack> ().manual_attack (TARGETPOSTION);		
	}

	int OnMonsterLoot() {
		//TODO:
		//PARAMETER NEEDED ID, item index;
		return index;
		
	}

	int OnMonsterLootResult() {
		//TODO:
		//PARAMETER NEEDED ID, item index;
		return mgen.monsterList[id].GetComponent<AutoAttack>().lootItem();
		
	}	

	// Update is called once per frame
	void Update () {
		
	}
}
