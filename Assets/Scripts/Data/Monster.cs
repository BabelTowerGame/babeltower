using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NewBehaviourScript{
	public float health;
	public float current_health;
	public int damage;
	public int defense;
	public int type;
	public string monsterName;
	public Location location;
	public bool in_battle;
	public bool in_movement;

	public float getDamage(){
		return damage;
	}

	public float getDefense(){
		return defense;
	}

}
