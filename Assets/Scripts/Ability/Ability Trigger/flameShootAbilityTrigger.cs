using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class flameShootAbilityTrigger : MonoBehaviour {

		[HideInInspector]public float damage;
		private float range = 10;
		ThirdPersonCharacter tc;

		// Use this for initialization
		void Start () {
			tc = GetComponent<ThirdPersonCharacter> ();
		}
		
		// Update is called once per frame
		public void launch(bool buttonPressed){
			if (buttonPressed) {
				Vector3 pos = tc.transform.position;
				pos.y += 1.42f;

				GameObject effect = Instantiate (Resources.Load ("FireExplosionEffects/Prefabs/FlameThrowerEffect") as GameObject, pos, tc.transform.rotation);
				effect.transform.parent = tc.transform;

				Destroy (effect, 1f);


				GameObject[] mst = GameObject.FindGameObjectsWithTag ("Monster");
				for (int i = 0; i < mst.Length; i++) {
					Monster ms = mst [i].GetComponent<Monster> ();
					if (Vector3.Distance (ms.transform.position, tc.transform.position) < range) {
						float angle = Vector3.Angle (tc.transform.forward, ms.transform.position);
						if (angle < 30) {
							ms.applyDamage (damage);
							Debug.Log (ms.Current_health);
						}
					}
				}
			}
		}
	}
}
