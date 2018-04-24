using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class ItemDB : ScriptableObject {

    #region singleton
    private static ItemDB m_Instance;
    public static ItemDB Instance {
        get {
            if (m_Instance == null)
                m_Instance = Resources.Load("Databases/ItemDB") as ItemDB;

            return m_Instance;
        }
    }
    #endregion


    [SerializeField]
    public List<Item> items;



    public Item get(int index) {
        return (this.items[index]);
    }

    public Item getByID(int ID) {
        for (int i = 0; i < this.items.Capacity; i++) {
            if (this.items[i].ID == ID)
                return this.items[i];
        }

        return null;
    }

    public Item getByName(string name) {
        for (int i = 0; i < this.items.Capacity; i++) {
            if (this.items[i].Name.Equals(name))
                return this.items[i];
        }

        return null;
    }

    public void OnEnable() {
        if (items == null)
            items = new List<Item>();

        //hideFlags = HideFlags.HideAndDontSave;
    }

    //private void showItem(Item it, Item.ItemType type) {
    //    switch (type) {
    //        case Item.ItemType.Armor:
    //            ((Armor)it).OnGUI();
    //            break;
    //        case Item.ItemType.Shield:
    //            ((Shield)it).OnGUI();
    //            break;
    //        case Item.ItemType.Normal:
    //            it.OnGUI();
    //            break;
    //        case Item.ItemType.Shoes:
    //            ((Shoes)it).OnGUI();
    //            break;
    //        case Item.ItemType.Weapon:
    //            ((Weapon)it).OnGUI();
    //            break;
    //        default:
    //            Debug.Log("WTF");
    //            break;
    //    }
    //}

    public void OnGUI() {
        foreach (var instance in items) {
            GUILayout.Label(instance.ID.ToString());
            instance.OnGUI();
            if(GUILayout.Button("Remove")) {
                items.Remove(instance);
            }
        }
        if (GUILayout.Button("Add Item"))
            items.Add(CreateInstance<Item>().Init(items.Count + 1,"New Item",1));
        if (GUILayout.Button("Add Weapon"))
            items.Add(CreateInstance<Weapon>().Init(items.Count + 1, "New Weapon", 1, 0, 0, 0));
        if (GUILayout.Button("Add Shield"))
            items.Add(CreateInstance<Shield>().Init(items.Count + 1, "New Shield", 1, 0, 0, 0, 1));
        if (GUILayout.Button("Add Armor"))
            items.Add(CreateInstance<Armor>().Init(items.Count + 1, "New Armor", 1, Armor.armor_type.chest, 0));
        if (GUILayout.Button("Add Shoes"))
            items.Add(CreateInstance<Shoes>().Init(items.Count + 1, "New Shoes", 1, 1));
    }
}