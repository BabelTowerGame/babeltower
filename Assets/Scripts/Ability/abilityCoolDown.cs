using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class abilityCoolDown : MonoBehaviour {

	public string buttonName = "X";
	public Text cooldownDisplay;
	public Image darkMask;


	private float coolDownDuration;
	private float nextReadyTime;
	private float coolDownTimeLeft;
	private Image buttonImage;

	private AbilityCast ability;
	private GameObject weaponHolder; 


	// Use this for initialization
	void Start () {
		//Initialize (ability, weaponHolder);
	}

	public void Initialize(AbilityCast selectedAbility, GameObject obj){
		ability = selectedAbility;
		coolDownDuration = ability.BaseCoolDown;
		buttonImage = GetComponent<Image> ();
		buttonImage.sprite = ability.aSprite;
		darkMask.sprite = ability.aSprite;
		ability.Initialize (obj);
		AbilityReady ();
	}

	// Update is called once per frame
	void Update () {
		bool coolDownComplete = (Time.time > nextReadyTime);
		if (coolDownComplete) 
		{
			AbilityReady ();
			if (Input.GetKeyDown(buttonName)) 
			{
				ButtonTriggered ();
			}
		} else 
		{
			CoolDown();
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

	private void ButtonTriggered()
	{
		nextReadyTime = coolDownDuration + Time.time;
		coolDownTimeLeft = coolDownDuration;
		darkMask.enabled = true;
		cooldownDisplay.enabled = true;

		ability.TriggerAbility ();
	}
}
