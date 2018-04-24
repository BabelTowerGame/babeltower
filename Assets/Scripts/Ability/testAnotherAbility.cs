using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[CreateAssetMenu (menuName = "Ability/testAnotherAbility")]
	public class testAnotherAbility : Ability {

		private testAnotherAbilityTrigger tt;
		public override void Initialize(GameObject obj) {
			tt = obj.GetComponent<testAnotherAbilityTrigger> ();
		}

		public override void TriggerAbility(bool buttonPressed) {
			tt.launch (buttonPressed);
		}
	}
}