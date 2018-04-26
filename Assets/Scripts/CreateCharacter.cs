using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacter : MonoBehaviour
{
    public GameObject MalePrefab;
    public GameObject FemalePrefab;


    void Awake(){
		Character c = CharacterManager.character;

		Debug.Log (transform.position);

		Vector3 point = new Vector3 (Random.Range (1000, 1300), 1000, Random.Range (1000, 1300));

		RaycastHit hit;
		// note that the ray starts at 100 units

		if (Physics.Raycast(point, -Vector3.up, out hit)) { 
			Debug.Log ("hit");
			Debug.Log (hit.point);
		}

		Vector3 spawn = hit.point;

		spawn.y += 10;
        GameObject go;

        if (c.appearance.gender == Appearance.Gender.Male) {
			go = GameObject.Instantiate (MalePrefab, spawn, Quaternion.identity) as GameObject;
		}
        else {
            go = GameObject.Instantiate(FemalePrefab, spawn, Quaternion.identity) as GameObject;
        }

        go.GetComponent<EasyEquipmentSystem.EquipmentSystem>().onHairColorChanged(c.appearance.hairColor);

    }
}
