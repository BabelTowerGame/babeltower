using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour {

    Hashtable lootTable;
	// Use this for initialization
	void Start () {
        lootTable = new Hashtable();
        for (int i = 0; i < 84; i++) {
            //lootTable.Add(i)
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
