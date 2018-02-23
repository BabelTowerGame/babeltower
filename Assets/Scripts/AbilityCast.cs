using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbilityCast : ScriptableObject {

	public string Name = "New Ability";
	public float BaseCoolDown = 1f;
	public Sprite aSprite;

	public abstract void Initialize(GameObject obj);
	public abstract void TriggerAbility();
}
