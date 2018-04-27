using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tob;

public class NetworkMonsterManager : MonoBehaviour {

	M_generator mgen;
	[SerializeField]private BagManager bg;
	// Use this for initialization
	void Awake () {
		mgen = GetComponent<M_generator> ();
		
	}


	void OnMonsterSpawn(MonsterEvent e) {
		//generate monster based on the data sent from server
		//TODO
		int id = int.Parse(e.Spawn.Id);

		M_generator.DemonType  dt = (M_generator.DemonType)e.Spawn.DemonType;
		M_generator.DemonSkin ds = (M_generator.DemonSkin)e.Spawn.DemonSkin;
		M_generator.WeaponType wt = (M_generator.WeaponType)e.Spawn.WeaponType;
		Vector3 pos = new Vector3 (e.Spawn.Position.X,e.Spawn.Position.Y,e.Spawn.Position.Z);


		mgen.GenerateMonster (id, dt, ds, wt,pos);


	}

	void OnMonsterDestory(MonsterEvent e) {
		//TODO:
		//PARAMETER NEEDED MONSTER ID;
		int id =  int.Parse(e.Id);
		mgen.destroyMonster(id);
		
	}
	void OnMonsterBack(MonsterEvent e){
		int id =  int.Parse(e.Id);
		mgen.monsterList [id].GetComponent<AutoAttack> ().manual_patrol ();
		mgen.monsterList[id].GetComponent<Monster>().InBattle = false;
		mgen.monsterList [id].GetComponent<Monster> ().InMovement = false;
		
	}

	void OnMonsterDie(MonsterEvent e) {
		//TODO:
		//PARAMETER NEEDED MONSTER ID;
		int id =  int.Parse(e.Id);
		mgen.monsterList[id].GetComponent<AutoAttack>().manual_die();
		mgen.monsterList[id].GetComponent<AutoAttack>().LootReady = true;
		mgen.monsterList [id].GetComponent<Monster> ().InBattle = false;
		mgen.monsterList [id].GetComponent<Monster> ().InMovement = false;
		mgen.monsterList [id].GetComponent<Monster> ().Current_health = 0.0f;
		for (int i = 0; i < e.Die.Items.Count; i++) {
			mgen.monsterList [id].GetComponent<Monster> ().LootList [i] = e.Die.Items [i];
		}
		//mgen.monsterList [id].GetComponent<Monster> ().LootList = e.Die.Items;

		
	}

	void OnMonsterMove(MonsterEvent e) {
		//TODO:
		//PARAMETER NEEDED MONSTER ID,MONSTER POSITION, TARGET POSITION;


		int id =  int.Parse(e.Id);
		mgen.monsterList[id].GetComponent<Monster>().X = e.Move.Position.X;
		mgen.monsterList[id].GetComponent<Monster>().Y = e.Move.Position.Y;
		mgen.monsterList[id].GetComponent<Monster>().Z = e.Move.Position.Z;
		mgen.monsterList[id].GetComponent<Monster>().PX = e.Move.Target.X;
		mgen.monsterList[id].GetComponent<Monster>().PY = e.Move.Target.Y;
		mgen.monsterList[id].GetComponent<Monster>().PZ = e.Move.Target.Z;
		mgen.monsterList [id].GetComponent<Monster> ().Updated = true;
		
		
	}
	void OnMonsterPatrol(MonsterEvent e){
		//TODO:
		//PARAMETER NEEDED,ID
	}

	void OnMonsterAttack(MonsterEvent e) {
		//TODO: 
		//PARAMETER NEEDED MONSTER ID,TARGETPOSITION;
		int id =  int.Parse(e.Id);
		mgen.monsterList [id].GetComponent<AutoAttack> ().manual_attack ();		
	}

	void OnMonsterLoot(MonsterEvent e) {
		//TODO:
		//PARAMETER NEEDED ID, item index;
		//server 
		int id =  int.Parse(e.Loot.MonsterId);
		int itemID = int.Parse (e.Loot.ItemId);
		int[] templist = mgen.monsterList [id].GetComponent<Monster> ().LootList;
		int pos = -1;
		for (int i = 0; i < templist.Length; i++) {
			if (templist [i] == itemID) {
				pos = i;
			}
		}
		if (pos > 0) {
			mgen.monsterList [id].GetComponent<Monster> ().LootList[pos] = -1;
			//TODO;
		}


	}

	void OnMonsterLootResult(MonsterEvent e) {
		//TODO:
		//PARAMETER NEEDED ID, item index;
		int itemID = int.Parse (e.Loot.ItemId);
		if ((e.Loot.PlayerId == NetworkID.Local_ID)) {
			bg.addItembyID(itemID);
		}
		
	}	

	// Update is called once per frame
	void Update () {
		
	}
}
