using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class explosionAbilityTrigger : MonoBehaviour {

		ThirdPersonCharacter tc;
		[HideInInspector]public float damage;
		[HideInInspector]public float range;
		[HideInInspector]public float radius;

		private void Start() {
			tc = GetComponent<ThirdPersonCharacter> ();
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

							GameObject effect = Instantiate (Resources.Load ("FireExplosionEffects/Prefabs/BigExplosionEffect") as GameObject, hit.point, tc.transform.rotation);

							Destroy (effect, 2f);

							if (Vector3.Distance (hit.point, ms.transform.position) < radius && ms.Current_health > 0) {
								ms.applyDamage (damage);
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
