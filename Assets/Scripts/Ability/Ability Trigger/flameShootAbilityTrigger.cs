using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class flameShootAbilityTrigger : MonoBehaviour {

		[HideInInspector]public float damage;
		private float range = 10;
		ThirdPersonCharacter tc;
		abilityCast ac;

		// Use this for initialization
		void Start () {
			tc = GetComponent<ThirdPersonCharacter> ();
			ac = GetComponent<abilityCast> ();
		}
		
		// Update is called once per frame
		public bool launch(bool buttonPressed){
			if (buttonPressed) {
				Vector3 pos = tc.transform.position;
				pos.y += 1.42f;

				ac.cast ("flameShoot", pos, tc.transform.rotation, 1);

				GameObject[] mst = GameObject.FindGameObjectsWithTag ("Monster");
				for (int i = 0; i < mst.Length; i++) {
					Monster ms = mst [i].GetComponent<Monster> ();
					AutoAttack aa = mst [i].GetComponent<AutoAttack> ();
					if (Vector3.Distance (ms.transform.position, tc.transform.position) < range) {
						float angle = Vector3.Angle (tc.transform.forward, ms.transform.position);
						if (angle < 30) {
							aa.applyDamage (damage, tc.transform);
						}
					}
				}
			}
			return buttonPressed;
		}
	}
}
