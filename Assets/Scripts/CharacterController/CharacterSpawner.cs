using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour {
    GameObject Male_Prefab;

    public void SpawnCharacter(Vector3 loc) {
        Debug.Log("Spawning Other Player at" + loc.ToString());
        Instantiate(Male_Prefab, loc, Quaternion.identity);
    }
    
}
