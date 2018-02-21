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
    public Weapon(string name, int level, int damage, Ability ab, int cd) : base(name, level) {
        this.damage = damage;
        this.ab = ab;
        this.cd = cd;
    }
}

[Serializable]
public class Shield : Weapon {
    private int defense;
    public Shield(string name, int level, int damage, Ability ab, int cd, int defense) : base(name, level, damge, ab, cd) {
        this.defense = defense;
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
    public Armor(string name, int level, armor_type type, int defense) : base(name, level) {
        this.defense = defense;
        this.type = type;
    }
}

[Serializable]
public class Shoes : Item {
    private int speed;
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
    public Ability(int damage, int range, int type, int cd) {
        this.A_damage = damage;
        this.A_range = range;
        this.A_type = type;
        this.A_cd = cd;
    }
}
