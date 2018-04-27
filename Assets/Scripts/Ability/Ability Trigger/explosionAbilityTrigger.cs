using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class explosionAbilityTrigger : MonoBehaviour {

		ThirdPersonCharacter tc;
		abilityCast ac;
		[HideInInspector]public float damage;
		[HideInInspector]public float range;
		[HideInInspector]public float radius;

		private void Start() {
			tc = GetComponent<ThirdPersonCharacter> ();
			ac = GetComponent<abilityCast> ();
		}

		public bool launch(bool buttonPressed){

			if (buttonPressed) {
				GameObject[] mst = GameObject.FindGameObjectsWithTag ("Monster");
				Ray castPoint = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (castPoint, out hit, Mathf.Infinity)) {
					if (Vector3.Distance (hit.point, tc.transform.position) < range) {
						for (int i = 0; i < mst.Length; i++) {
							Monster ms = mst [i].GetComponent<Monster> ();
							AutoAttack aa = mst [i].GetComponent<AutoAttack> ();

							ac.cast ("explosion", hit.point, Quaternion.identity, 1.5f);

							if (Vector3.Distance (hit.point, ms.transform.position) < radius && ms.Current_health > 0) {
								aa.applyDamage (damage, tc.transform);
								Debug.Log (ms.Current_health);
							}
						}
					} else {
						buttonPressed = false;
					}
				}
			}
			tc.useSkill (buttonPressed, "Skill");
			return buttonPressed;
		}
	}
}
