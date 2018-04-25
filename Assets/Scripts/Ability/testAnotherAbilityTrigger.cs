using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class testAnotherAbilityTrigger : MonoBehaviour {

		ThirdPersonCharacter tc;

		private void Start() {
			tc = GetComponent<ThirdPersonCharacter> ();
		}

		public void launch(bool buttonPressed){
			tc.useSkill (buttonPressed, "anotherSkill");
		}
	}
}