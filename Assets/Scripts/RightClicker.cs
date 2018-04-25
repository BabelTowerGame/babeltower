using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;



public class RightClicker : MonoBehaviour, IPointerClickHandler{

	public BagManager bagManager;
	public Character player;
	public ArmorManager armorManager;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		player = bagManager.player;
	}

	public void OnPointerClick (PointerEventData eventData) {
		if (eventData.button == PointerEventData.InputButton.Right) {
			Debug.Log ("Right Mouse Button Clicked on: " + name);
			string id = Regex.Replace(this.name, @"[^\d.\d]", "");
			int slot = int.Parse (id) - 1;
			Item equipItem = player.inventory.list [slot];

			if (equipItem.Type.ToString () == "Shoes"){
				if (player.equips.shoes == null) {
					player.equips.shoes = (Shoes)equipItem;
					//TODO: After equipped, delete the item from inventory
					bagManager.deleteByID(slot);
				} else {
					//replace with the current item
					Item tempItem = player.equips.shoes;
					player.equips.shoes = (Shoes)equipItem;
					bagManager.replaceItem (slot, tempItem);
				}
			} else if (equipItem.Type.ToString () == "Armor"){
				//TODO: we have 3 types of armors
				Armor temp2 = (Armor)player.inventory.list [slot];
				if(temp2.Armor_Type == Armor.armor_type.chest){
					if (player.equips.chest == null) {
						player.equips.chest = temp2;
						//TODO: After equipped, delete the item from inventory
						bagManager.deleteByID(slot);
					} else {
						//replace with the current item
						Item tempItem = player.equips.chest;
						player.equips.chest = temp2;
						bagManager.replaceItem (slot, tempItem);
					}
				} else if(temp2.Armor_Type == Armor.armor_type.head){
					if (player.equips.head == null) {
						player.equips.head = temp2;
						//TODO: After equipped, delete the item from inventory
						bagManager.deleteByID(slot);
					} else {
						//replace with the current item
						Item tempItem = player.equips.head;
						player.equips.head = temp2;
						bagManager.replaceItem (slot, tempItem);
					}
				} else if(temp2.Armor_Type == Armor.armor_type.leg){
					if (player.equips.legs == null) {
						player.equips.legs = temp2;
						//TODO: After equipped, delete the item from inventory
						bagManager.deleteByID(slot);
					} else {
						//replace with the current item
						Item tempItem = player.equips.legs;
						player.equips.legs = temp2;
						bagManager.replaceItem (slot, tempItem);
					}
				}
			}
			armorManager.updateGui ();
		}
	}
}
