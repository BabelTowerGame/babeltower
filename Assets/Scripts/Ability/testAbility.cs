using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[CreateAssetMenu (menuName = "Ability/testAbility")]
	public class testAbility : Ability {

		private testAbilityTrigger tt;
		public override void Initialize(GameObject obj) {
			tt = obj.GetComponent<testAbilityTrigger> ();
		}

		public override void TriggerAbility(bool buttonPressed) {
			tt.launch (buttonPressed);
		}
	}
}
