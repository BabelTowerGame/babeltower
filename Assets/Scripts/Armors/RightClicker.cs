﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;



public class RightClicker : MonoBehaviour, IPointerClickHandler{

	public BagManager bagManager;
	public Character player;
	public EasyEquipmentSystem.ArmorManager armorManager;
	public PickupManager pickupManager;
	public ItemDB db;
	public GameObject confirmWindow;
    UnityStandardAssets.Characters.ThirdPerson.equipWeapon ew;


	// Use this for initialization
	void Start () {
		db = ItemDB.Instance;
        GameObject playerObject = GameObject.FindWithTag("Player");
        player = CharacterManager.character;
        ew = GameObject.FindGameObjectWithTag("Skill").
            GetComponent<UnityStandardAssets.Characters.ThirdPerson.equipWeapon> ();
    }

	// Update is called once per frame
	void Update () {
		player = bagManager.player;
	}

	public void OnPointerClick (PointerEventData eventData) {
		if (eventData.button == PointerEventData.InputButton.Right) {
//			Debug.Log ("Right Mouse Button Clicked on: " + name);
			string id = Regex.Replace(this.name, @"[^\d.\d]", "");
			if (this.tag == "Slot") {
				int slot = int.Parse (id) - 1;
				//bagmanager, equipped items
				
				if (slot >= player.inventory.list.Count) {
					return;
				}

				Item equipItem = player.inventory.list [slot];
//				Debug.Log("item type = " + equipItem.Type.ToString());
				if (equipItem.Type.ToString () == "Shoes") {
					if (player.equips.shoes == null) {
						player.equips.shoes = (Shoes)equipItem;
						//TODO: After equipped, delete the item from inventory
						bagManager.deleteByIndex (slot);
					} else {
						//replace with the current item
						Item tempItem = player.equips.shoes;
						player.equips.shoes = (Shoes)equipItem;
						bagManager.replaceItem (slot, tempItem);
					}
				} else if (equipItem.Type.ToString () == "Armor") {
					//TODO: we have 3 types of armors
					Armor temp2 = (Armor)player.inventory.list [slot];
					if (temp2.Armor_Type == Armor.armor_type.chest) {
						if (player.equips.chest == null) {
							player.equips.chest = temp2;
							//TODO: After equipped, delete the item from inventory
							bagManager.deleteByIndex (slot);
						} else {
							//replace with the current item
							Item tempItem = player.equips.chest;
							player.equips.chest = temp2;
							bagManager.replaceItem (slot, tempItem);
						}
					} else if (temp2.Armor_Type == Armor.armor_type.head) {
						if (player.equips.head == null) {
							player.equips.head = temp2;
							//TODO: After equipped, delete the item from inventory
							bagManager.deleteByIndex (slot);
						} else {
							//replace with the current item
							Item tempItem = player.equips.head;
							player.equips.head = temp2;
							bagManager.replaceItem (slot, tempItem);
						}
					} else if (temp2.Armor_Type == Armor.armor_type.leg) {
						if (player.equips.legs == null) {
							player.equips.legs = temp2;
							//TODO: After equipped, delete the item from inventory
							bagManager.deleteByIndex (slot);
						} else {
							//replace with the current item
							Item tempItem = player.equips.legs;
							player.equips.legs = temp2;
							bagManager.replaceItem (slot, tempItem);
						}
					}
				} else if (equipItem.Type.ToString () == "Weapon") {
					if (player.equips.weapon == null) {
						player.equips.weapon = (Weapon)equipItem;
						//TODO: After equipped, delete the item from inventory
						bagManager.deleteByIndex (slot);
					} else {
						//replace with the current item
						Item tempItem = player.equips.weapon;
						player.equips.weapon = (Weapon)equipItem;
						bagManager.replaceItem (slot, tempItem);
					}
                    ew.equip((Weapon)equipItem);
					
				} else if (equipItem.Type.ToString () == "Shield") {
					if (player.equips.shield == null) {
						player.equips.shield = (Shield)equipItem;
						//TODO: After equipped, delete the item from inventory
						bagManager.deleteByIndex (slot);
					} else {
						//replace with the current item
						Item tempItem = player.equips.shield;
						player.equips.shield = (Shield)equipItem;
						bagManager.replaceItem (slot, tempItem);
					}
                    ew.equip((Shield)equipItem);
				}
				armorManager.updateGui ();
			} else if (this.tag == "Pickup") {
				//pick up items
				int slot = int.Parse (id) - 1;
				if (slot >= pickupManager.pickupList.Count) {
					return;
				}
				Pickitem pick = pickupManager.pickupList [slot];
				Item it = db.getByID (pick.id);
				if (pick.monster.lootItem (pick.index) != -1) {
					//successfully delete from monster's lootlist
					if (bagManager.addItem (it) == 1) {
						//added
//						Debug.Log ("Pick up " + it.name);
						//update pickup's gui
						pickupManager.deleteItem (slot);
					} else {
						//bag is full, then back item to the monster's lootlist
						//pick.monster.backItem (pick.id);
					}
				}
			} else if (this.tag == "Equip") {
				string equipname = this.name;
				if (!confirmWindow.activeSelf && armorManager.ifExist(equipname)) {
					confirmWindow.SetActive (true);
					confirmWindow.transform.SetAsLastSibling ();
					GameObject button = GameObject.Find("Delete_Yes_Button");
					button.GetComponent<DeleteButton> ().type = "Equip";
					button.GetComponent<DeleteButton> ().name = equipname;
				}
			}
		}
	}
}
