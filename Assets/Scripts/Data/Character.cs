using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

[Serializable]
public class Character {
    #region general
    private string name;
    private float maxHealth = 100;
    private uint level = 1;
	public Equipped equips;
    public string Name {
        get {
            return name;
        }

        set {
            name = value;
        }
    }

    public float MaxHealth {
        get {
            return maxHealth;
        }

        set {
            maxHealth = value;
        }
    }

    public uint Level {
        get {
            return level;
        }

        set {
            level = value;
        }
    }
    #endregion

    #region status
    private bool inBattle;
    private bool inMovement;
    public bool IsDead {
        get { return currentHealth <= 0; }
    }
    public Inventory inventory;
    private float currentHealth = 100;
    public Location location;
	public Appearance appearance;
    public float CurrentHealth {
        get {
            return currentHealth;
        }
        set {
            currentHealth = value;
        }
    }
    public float Defense {
        get { return 0; }
    }
    public float Damage {
        get { return 0; }
    }

    #endregion


    public Character() {
        this.name = "";
        this.appearance = new Appearance();
    }
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
		public Shield shield;
    }

    public void setHairColor(Color color) {
        this.appearance.hairColor = color;
    }

    public void setGender(Appearance.Gender gender) {
        this.appearance.gender = gender;
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

    public Appearance() { }

    public Appearance(Gender gender, Color hairColor) {
        this.gender = gender;
        this.hairColor = hairColor;
    }
    //public Color skinColor;

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