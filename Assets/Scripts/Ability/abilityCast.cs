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

	}
}
