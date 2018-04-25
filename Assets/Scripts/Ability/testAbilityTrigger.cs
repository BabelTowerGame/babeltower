using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class testAbilityTrigger : MonoBehaviour {

		ThirdPersonCharacter tc;
		[HideInInspector]public float damage;
		[HideInInspector]public float range;

		private void Start() {
			tc = GetComponent<ThirdPersonCharacter> ();
		}

		public void launch(bool buttonPressed){
			tc.useSkill (buttonPressed, "Skill");
			if (buttonPressed) {
				Debug.Log (range);
				// Find Monster within range
				GameObject[] mst = GameObject.FindGameObjectsWithTag ("Monster");
				for (int i = 0; i < mst.Length; i++) {
					if (Vector3.Distance (mst [i].transform.position, tc.transform.position) < range) {
						Monster ms = mst [i].GetComponent<Monster> ();
						ms.applyDamage (damage);
						Debug.Log (ms.Current_health);
					}
				}
			}
		}
	}
}
