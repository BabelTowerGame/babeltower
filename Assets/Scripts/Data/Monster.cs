using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monster: MonoBehaviour  {
    [SerializeField]private int id;
    [SerializeField]private float health;
    [SerializeField]private float current_health;
    [SerializeField]private int damage;
    [SerializeField]private int defense;
    [SerializeField]private string name;
    [SerializeField]private Location location;
    [SerializeField]private bool inBattle;
    [SerializeField]private bool inMovement;
	[SerializeField]private int level;
	private DemonSkin DS;
	private DemonType DT;
	private WeaponType WT;
	private int[] Lootlist = new int[10];
	private int[] Loottable;
	public enum DemonType {
		Demon1,
		Demon2,
		Demon3,
		Demon4
	}
	public enum DemonSkin
	{
		Demons1,
		Demons2,
		Demons3,
		Demons4,
		Demons5,
		Demons6,
		Demons7,
		Demons8,
		Demons9,
		Demons10,
		Demons11,
		Demons12,
		Demons13,
		Demons14,
		Demons15,
		Demons16,
		Demons17,
		Demons18,
		Demons19,
		Demons20,
		Demons21

	}
	public enum WeaponType{
		Sword,
		Scythe,
		Trident,
		Pike,
		Hammer,
		Axe

	}
    public int ID {
        get {
            return id;
        }

        set {
            id = value;
        }
    }

    public float Health {
        get {
            return health;
        }

        set {
            health = value;
        }
    }

    public float Current_health {
        get {
            return current_health;
        }

        set {
            current_health = value;
        }
    }

    public int Damage {
        get {
            return damage;
        }

        set {
            damage = value;
        }
    }

    public int Defense {
        get {
            return defense;
        }

        set {
            defense = value;
        }
    }
		

    public string Name {
        get {
            return name;
        }

        set {
            name = value;
        }
    }

    public Location Location {
        get {
            return location;
        }

        set {
            location = value;
        }
    }

    public bool InBattle {
        get {
            return inBattle;
        }

        set {
            inBattle = value;
        }
    }

    public bool InMovement {
        get {
            return inMovement;
        }

        set {
            inMovement = value;
        }
    }
	public int[] LootList{
		get{ 
			return Lootlist;
		
		}
		set{ 
			Lootlist = value;
		}
	}
	public int Level{
		get{ 
			return level;
		}
		set{ 
			level = value;
		}
	}
	public int[] LootTable{
		get{ 
			return Loottable;
		}
		set{ 
			LootTable = value;
		}
	}
	public DemonSkin Skin{
		get{ 
			return DS;
		}
		set{ 
			DS = value;
		}
	}
	public DemonType Type{
		get{ 
			return DT;
		}
		set{ 
			DT = value;
		}
	}
	public WeaponType Weapon{
		get{ 
			return WT;
		}
		set{ 
			WT = value;
		}
	}


}
