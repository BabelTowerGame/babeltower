using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;


public class ArmorManager : MonoBehaviour {
	public BagManager bagmanager;
	public Character player;
	public GameObject armorObject;
	public UIEquipSlot[] armorslots = new UIEquipSlot[4];
	[SerializeField] private GameObject ArmorContent;
	public UIEquipSlot[] weaponslots = new UIEquipSlot[2];
	[SerializeField] private GameObject WeaponContent;

	// Use this for initialization
	void Start () {
		player = bagmanager.player;
		int i = 0;
		foreach (Transform child in ArmorContent.transform) {  
//			Debug.Log ("Child name is" + child.name);
			armorslots [i++] = child.gameObject.GetComponent<UIEquipSlot> ();  
		}
		i = 0;
		foreach (Transform child in WeaponContent.transform) {  
//			Debug.Log ("Child name is" + child.name);
			weaponslots [i++] = child.gameObject.GetComponent<UIEquipSlot> ();  
		}
	}
	
	// Update is called once per frame
	void Update () {
		//check if we need open or close the gui of bag
		if (Input.GetButtonDown("Character") && armorObject.activeSelf) {
			closeGui ();
		} else if (Input.GetButtonDown("Character") && !armorObject.activeSelf) {
			openGui ();
		}
	}

	public void updateGui(){
		//check player.equips, then show the icons
		if (player.equips.head != null) {
			armorslots [0].newAssign (player.equips.head, null);
		}
		if (player.equips.chest != null) {
			armorslots [1].newAssign (player.equips.chest, null);
		}
		if (player.equips.legs != null) {
			armorslots [2].newAssign (player.equips.legs, null);
		}
		if (player.equips.shoes != null) {
//			Debug.Log ("now shoes = " + player.equips.shoes);
			armorslots [3].newAssign (player.equips.shoes, null);
//			Debug.Log ("Now equip: " + player.equips.shoes.Name);
		}
		if (player.equips.weapon != null) {
			weaponslots [0].newAssign (player.equips.weapon, null);
		}
		if (player.equips.shield != null) {
			weaponslots [1].newAssign (player.equips.shield, null);
		}
	}

	//function that contorls gui to show/hide
	void openGui(){
		//updateGui ();
		armorObject.SetActive (true);
	}

	void closeGui(){
		armorObject.SetActive (false);
	}

    void getDefense() {
        float defense = 0.0f;
        if (player.equips.head != null) {
            defense += player.equips.head.Defense;
        }
        if (player.equips.chest != null) {
            defense += player.equips.chest.Defense;
        }
        if (player.equips.legs != null) {
            defense += player.equips.legs.Defense;
        }
    }
}
