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
    public int level;
    #endregion

    #region status
    public bool inBattle;
    public bool inMovement;
    public bool isDead {
        get { return currentHealth <= 0; }
    }

    //public Inventory inventory;
    public float currentHealth;
    //public Location location
    public float Defense {
        get { return 0; }
    }
    public float Damage {
        get { return 0; }
    }
    #endregion

    public struct Equipped {
        //public Weapon weapon;
        //public Armor head;
        //public Armor chest;
        //public Armor legs;
        //public Shoe shoe;
    }
   




}