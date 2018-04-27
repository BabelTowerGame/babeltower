using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class abilityCast : MonoBehaviour {

		public GameObject plasma;
		public GameObject flameShoot;
		public GameObject flame;
		public GameObject explosion;

		// Cast ability on specific position
		public void cast(string abilityName, Vector3 position, Quaternion rotation, float destroyTime){
			GameObject effect = Instantiate (getAbility(abilityName), position, rotation);
			Destroy (effect, destroyTime);
		}

		// Cast ability on monster position
		public void cast(string abilityName, Monster ms, float destroyTime){
			GameObject effect = Instantiate (getAbility(abilityName), ms.transform.position, ms.transform.rotation);

			effect.transform.parent = ms.transform;

			Destroy (effect, destroyTime);
		}

		GameObject getAbility(string name){
			switch (name) {
			case "plasma":
				return plasma;
			case "flame":
				return flame;
			case "flameShoot":
				return flameShoot;
			case "explosion":
				return explosion;
			}
			return null;
		}

		void sendMessage(Vector3 pos, string name, float destroyTime){
			float x = pos.x;
			float y = pos.y;
			float z = pos.z;

			// TODO: Send message to server with ability information
			// send(x,y,z,name, destroyTime);
		}

		void sendMessage(Monster ms, string name, float destroyTime){
			float x = ms.transform.position.x;
			float y = ms.transform.position.y;
			float z = ms.transform.position.z;

			int monsterID = ms.ID;

			// TODO: Send message to server with ability information
			// send(x, y, z, monsterID, name, destroyTime);
		}

		void onCast(int x, int y, int z, string name, float destroyTime){
			cast (name, new Vector3 (x, y, z), Quaternion.identity, destroyTime);
		}

		void onCast(int monsterID, string name, float destroyTime){
			GameObject[] mst = GameObject.FindGameObjectsWithTag ("Monster");

			for (int i = 0; i < mst.Length; i++) {
				Monster ms = mst [i].GetComponent<Monster> ();
				if (ms.ID == monsterID) {
					cast (name, ms, destroyTime);
				}
			}

		}

	}
}
