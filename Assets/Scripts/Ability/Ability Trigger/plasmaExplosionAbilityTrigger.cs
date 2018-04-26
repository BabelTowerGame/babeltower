using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class plasmaExplosionAbilityTrigger : MonoBehaviour {

		ThirdPersonCharacter tc;
		[HideInInspector]public float damage;
		[HideInInspector]public float range;

		private void Start() {
			tc = GetComponent<ThirdPersonCharacter> ();
		}

		public bool launch(bool buttonPressed){
			if (buttonPressed) {
				// Find Monster within range
				GameObject[] mst = GameObject.FindGameObjectsWithTag ("Monster");

				bool monsterInRange = false;
				for (int i = 0; i < mst.Length; i++) {
					if (Vector3.Distance (mst [i].transform.position, tc.transform.position) < range && mst.Length > 0) {
						Monster ms = mst [i].GetComponent<Monster> ();

						if (ms.Current_health > 0) {

							ms.applyDamage (damage);

							GameObject effect = Instantiate (Resources.Load ("FireExplosionEffects/Prefabs/PlasmaExplosionEffect") as GameObject, ms.transform.position, ms.transform.rotation);

							effect.transform.parent = ms.transform;

							Destroy (effect, 1.5f);

							monsterInRange = true;
						}
					}
				}

				if (!monsterInRange) {
					Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;
					if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
					{
						if (Vector3.Distance (hit.point, tc.transform.position) < range) {
							GameObject effect = Instantiate (Resources.Load ("FireExplosionEffects/Prefabs/PlasmaExplosionEffect") as GameObject, hit.point, tc.transform.rotation);
							Destroy (effect, 1.5f);
						} else {
							buttonPressed = false;
						}
					}
				}

			}

			tc.useSkill (buttonPressed, "anotherSkill");
			return buttonPressed;
		}
	}
}