using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class abilityCoolDown : MonoBehaviour {

		public string buttonName = "X";
		public Text cooldownDisplay;
		public Image darkMask;

		[SerializeField] private Ability ability;
		[SerializeField] private GameObject weaponHolder;
		private float coolDownDuration;
		private float nextReadyTime;
		private float coolDownTimeLeft;
		private Image buttonImage;

	//	private Ability ability;
	//	private GameObject weaponHolder; 


		// Use this for initialization
		void Start () {
			Initialize (ability, weaponHolder);
		}

		public void Initialize(Ability selectedAbility, GameObject obj){
			ability = selectedAbility;
			coolDownDuration = ability.baseCoolDown;
			buttonImage = GetComponent<Image> ();
			buttonImage.sprite = ability.aSprite;
			darkMask.sprite = ability.aSprite;
			ability.Initialize (obj);
			AbilityReady ();
		}

		public bool isInitialize(){
			return ability != null ? true : false;
		}

		// Update is called once per frame
		void Update () {
			bool coolDownComplete = (Time.time > nextReadyTime);

			ThirdPersonCharacter tc = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<ThirdPersonCharacter> ();

			if (coolDownComplete && !tc.inTransition ()) 
			{
				AbilityReady ();

				bool buttonPressed = Input.GetKeyDown (buttonName);
				
				ButtonTriggered (buttonPressed);
			} else 
			{
				CoolDown();
				ButtonTriggered (false);
			}
		}

		private void AbilityReady(){
			cooldownDisplay.enabled = false;
			darkMask.enabled = false;
		}

		private void CoolDown()
		{
			coolDownTimeLeft -= Time.deltaTime;
			float roundedCd = Mathf.Round (coolDownTimeLeft);
			cooldownDisplay.text = roundedCd.ToString ();
			darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
		}

		private void ButtonTriggered(bool buttonPressed)
		{
			buttonPressed = ability.TriggerAbility (buttonPressed);
			if (buttonPressed) {
				nextReadyTime = coolDownDuration + Time.time;
				coolDownTimeLeft = coolDownDuration;
				darkMask.enabled = true;
				cooldownDisplay.enabled = true;
			}
		}
	}
}
