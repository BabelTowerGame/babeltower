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
    [SerializeField]private int type;
    [SerializeField]private string name;
    [SerializeField]private Location location;
    [SerializeField]private bool inBattle;
    [SerializeField]private bool inMovement;
	[SerializeField]private int level;
	private int[] Lootlist = new int[10];
	private ItemDB ObjDB = new ItemDB();
	private int[] Loottable;
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

    public int Type {
        get {
            return type;
        }

        set {
            type = value;
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
	public ItemDB DB{
		get{ 
			return ObjDB;
		}
		set{ 
			ObjDB = value;
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

}
