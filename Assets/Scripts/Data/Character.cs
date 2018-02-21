using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

[Serializable]
public class Character {
    #region general
    public string name;
    public float maxHealth;
    public uint level;
    #endregion

    #region status
    public bool inBattle;
    public bool inMovement;
    public bool isDead {
        get { return currentHealth <= 0; }
    }

    public Inventory inventory;
    public float currentHealth;
    public Location location;
	public Appearance appearance;
    public float Defense {
        get { return 0; }
    }
    public float Damage {
        get { return 0; }
    }
    #endregion

	public Character(string name, Appearance app) {
		this.name = name;
		this.appearance = app;
	}

    public struct Equipped {
        public Weapon weapon;
        public Armor head;
        public Armor chest;
        public Armor legs;
        public Shoes shoes;
    }
}

[Serializable]
public class Appearance {
    public enum Gender {
        Male,
        Female
    }

    public Gender gender;
    public Color hairColor;
    public Color skinColor;

}


[Serializable]
public class Inventory {
    public uint occupied = 0;
    public uint capacity = 0;
    public List<Item> list;

    
    public Inventory() {
        this.list = new List<Item>();
    }
}