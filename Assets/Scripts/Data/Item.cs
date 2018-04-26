using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
[Serializable]
public class Item : ScriptableObject{
    public enum ItemType {
        Normal,
        Weapon,
        Shield,
        Armor,
        Shoes
    }
    [SerializeField] private int id;
    [SerializeField] private string name;
    [SerializeField] private int level;
    [SerializeField] private ItemType type;
	[SerializeField] private Sprite icon;

    public string Name {
        get { return this.name; }
        set { this.name = value; }
    }

    public int ID {
        get { return this.id; }
        set { this.id = value; }
    }
    public int QualityLevel {
        get { return this.level; }
        set { this.level = value; }
    }
	public Sprite Icon{
		get { return this.icon; }
		set { this.icon = value; }
	}
	public ItemType Type{
		get { return this.type; }
		set { this.type = value; }
	}
	public Item Init(int ID,string name, int level) { 
        this.ID = ID;
        this.name = name;
        this.level = level;
        this.Type = ItemType.Normal;
        return this;
    }

    public void OnEnable() {
        //hideFlags = HideFlags.HideAndDontSave;
    }

    public virtual void OnGUI() {
        Editor.CreateEditor(this).DrawDefaultInspector();
    }


}

[Serializable]
public class Weapon : Item {
    [SerializeField] private int damage;
    [SerializeField] private int ab_id;
    [SerializeField] private int cd;
    public int Damage {
        get { return this.damage; }
        set { this.damage = value; }
    }
    public int Ab_ID {
        get { return this.ab_id; }
        set { this.ab_id = value; }
    }
    public int Cd {
        get { return this.cd; }
        set { this.cd = value; }
    }
	public Weapon Init(int ID,string name, int level, int damage, int ab, int cd) {
        this.Init(ID, name, level);
        this.damage = damage;
        this.ab_id = ab;
        this.cd = cd;
        this.Type = ItemType.Weapon;
        return this;
    }
    public override void OnGUI() {
        Editor.CreateEditor(this).DrawDefaultInspector();
		this.Type = ItemType.Weapon;
    }
}

[Serializable]
public class Shield : Weapon {
    [SerializeField] private int defense;
    public Shield Init(int ID,string name, int level, int damage, int ab, int cd, int defense) {
        this.Init(ID ,name, level, damage, ab, cd);
        this.defense = defense;
        this.Type = ItemType.Shield;
        return this;
//		this.Type = ItemType.Shield;
    }
    public int Defense {
        get { return this.defense; }
        set { this.defense = value; }
    }
    public override void OnGUI() {
        Editor.CreateEditor(this).DrawDefaultInspector();
    }
}

[Serializable]
public class Armor : Item {
    public enum armor_type {
        head,
        chest,
        leg
    }
    [SerializeField] private armor_type a_type;
    [SerializeField] private int defense;
    public armor_type Armor_Type {
        get { return this.a_type; }
        set { this.a_type = value; }
    }
    public int Defense {
        get { return this.defense; }
        set { this.defense = value; }
    }
	public Armor Init(int ID,string name, int level, armor_type type, int defense){
        this.Init(ID, name, level);
        this.defense = defense;
        this.a_type = type;
        this.Type = ItemType.Armor;
      
        return this;
    }
    public override void OnGUI() {
        Editor.CreateEditor(this).DrawDefaultInspector();
		this.Type = ItemType.Armor;
    }
}

[Serializable]
public class Shoes : Item {
    [SerializeField] private int speed;
    public int Speed {
        get { return this.speed; }
        set { this.speed = value; }
    }
	public Shoes Init(int ID, string name, int level, int speed) {
        this.Init(ID, name, level);
        this.speed = speed;
        this.Type = ItemType.Shoes;
        return this;
    }
    public override void OnGUI() {
        Editor.CreateEditor(this).DrawDefaultInspector();
		this.Type = ItemType.Shoes;
    }
}

[Serializable]
public abstract class Ability : ScriptableObject {
    
//    [SerializeField] private int A_damage;
//    [SerializeField] private int A_range;
//    [SerializeField] private int A_type;
//    [SerializeField] private int A_cd;
//    public int Ability_damage {
//        get { return this.A_damage; }
//        set { this.A_damage = value; }
//    }
//    public int Ability_range {
//        get { return this.A_range; }
//        set { this.A_range = value; }
//    }
//    public int Ability_type {
//        get { return this.A_type; }
//        set { this.A_type = value; }
//    }
//    public int Ability_cd {
//        get { return this.A_cd; }
//        set { this.A_cd = value; }
//    }

	[SerializeField] private int id;
	public Sprite aSprite;
	public float baseCoolDown;

    public int ID {
        get {   return this.id;}
        set {   this.id = value;}
    }

	public abstract void Initialize (GameObject obj);

	public abstract bool TriggerAbility (bool buttonPressed);

}
