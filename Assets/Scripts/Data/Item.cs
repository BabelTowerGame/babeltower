using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Item {
	private string name;
	private int level;
	public Item(string name,int level){
		this.name = name;
		this.level = level;
	}
}
public class Weapon:Item{
	private int damage;
	private Ability ab;
	private int cd;
	public Weapon(string name,int level,int damage, Ability ab,int cd):base(name,level){
		this.damage = damage;
		this.ab = ab;
		this.cd = cd;
	}
}

public class Armor:Item{
	public enum armor_type
	{
		head,
		chest,
		leg

	}
	private armor_type type;
	private int defense;
	public Armor(string name,int level,armor_type type,int defense):base(name,level){
		this.defense = defense;
		this.type = type;
	}
}
public class Shoes:Item{
	private int speed;
	public Shoes(string name,int level,int speed):base(name,level){
		this.speed = speed;
	}
}
	

public class Ability{
	private int A_damage;
	private int A_range;
	private int A_type;
	public Ability(int damage,int range, int type){
		this.A_damage = damage;
		this.A_range = range;
		this.A_type = type;
	}
}
