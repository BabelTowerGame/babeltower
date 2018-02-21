using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item {
    private int id;
    private string name;
    private int level;

    public string Name {
        get { return this.name; }
        set { this.name = value; }
    }

    public int ID {
        get { return this.id; }
        set { this.id = value; }
    }
	public int QualityLevel{
		get{ return this.level; }
		set{ this.level = value; }
	}
    public Item(string name, int level) {
        this.name = name;
        this.level = level;
    }
}

[Serializable]
public class Weapon : Item {
    private int damage;
    private Ability ab;
    private int cd;
	public int Damage{
		get{ return this.damage; }
		set{ this.damage = value; }
	}
	public Ability Ab{
		get{ return this.ab; }
		set{ this.ab = value; }
	}
	public int Cd{
		get{ return this.cd; }
		set{ this.cd = value; }
	}
    public Weapon(string name, int level, int damage, Ability ab, int cd) : base(name, level) {
        this.damage = damage;
        this.ab = ab;
        this.cd = cd;
    }
}

[Serializable]
public class Shield : Weapon {
    private int defense;
    public Shield(string name, int level, int damage, Ability ab, int cd, int defense) : base(name, level, damage, ab, cd) {
        this.defense = defense;
    }
	public int Defense{
		get{ return this.defense; }
		set{ this.defense = value; }
	}
}

[Serializable]
public class Armor : Item {
    public enum armor_type {
        head,
        chest,
        leg
    }
    private armor_type type;
    private int defense;
	public armor_type Armor_Type{
		get{ return this.type; }
		set{ this.type = value; }
	}
	public int Defense{
		get{ return this.defense; }
		set{ this.defense = value; }
	}
    public Armor(string name, int level, armor_type type, int defense) : base(name, level) {
        this.defense = defense;
        this.type = type;
    }
}

[Serializable]
public class Shoes : Item {
    private int speed;
	public int Speed{
		get{ return this.speed; }
		set{ this.speed = ValueType; }
	}
    public Shoes(string name, int level, int speed) : base(name, level) {
        this.speed = speed;
    }
}

[Serializable]
public class Ability {
    private int A_damage;
    private int A_range;
    private int A_type;
    private int A_cd;
	public int Ability_damage{
		get{return this.A_damage; }
		set{ this.A_damage = value; }
	}
	public int Ability_range{
		get{ return this.A_range; }
		set{ this.A_range = value; }
	}
	public int Ability_type{
		get{ return this.A_type; }
		set{ this.A_type = value; }
	}
	public int Ability_cd{
		get{ return this.A_cd; }
		set{ this.A_cd = value; }
	}
    public Ability(int damage, int range, int type, int cd) {
        this.A_damage = damage;
        this.A_range = range;
        this.A_type = type;
        this.A_cd = cd;
    }
}
