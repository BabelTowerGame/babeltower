﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class DeleteButton : MonoBehaviour {
	//slot ids
	string newID;
	string oldID;
	//outer use
	public BagManager bagManager;
	public GameObject confirmWindow;
	public EasyEquipmentSystem.ArmorManager am;
	public string name;
	public string type;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void deleteItem(){
		if (type == "Slot") {
			oldID = Regex.Replace (name, @"[^\d.\d]", "");
			int old_slot = int.Parse (oldID) - 1;
			//Item temp = player.inventory.list [old_slot];
			bagManager.deleteByIndex (old_slot);
			//close the confirm window
			confirmWindow.SetActive (false);
			//canvasGroup.blocksRaycasts = true; 
		} else if (type == "Equip") {
			Debug.Log ("name = " + name);
			am.deleteEquip (name);
			confirmWindow.SetActive (false);
		}
	}
}
