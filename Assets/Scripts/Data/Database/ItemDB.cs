using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
[Serializable]
public class ItemDB : ScriptableObject {

    public static string dataAssetPath = "Assets/Resources/Databases/ItemDBData.asset";

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
    [SerializeField]
    public DataHolder data;

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
        if (items == null) {
            items = new List<Item>();
        }
        if (data == null)
            data = (DataHolder)AssetDatabase.LoadAssetAtPath(dataAssetPath, typeof(DataHolder));

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
        if (data == null)
            data = (DataHolder)AssetDatabase.LoadAssetAtPath(dataAssetPath, typeof(DataHolder));
        foreach (var instance in items) {
            GUILayout.Label(instance.ID.ToString());
            instance.OnGUI();
            if(GUILayout.Button("Remove")) {
                items.Remove(instance);
                DestroyImmediate(instance,true);

            }
        }
        if (GUILayout.Button("Add Item")) {
            Item i = CreateInstance<Item>().Init(items.Count + 1, "New Item", 1);
            AssetDatabase.AddObjectToAsset(i, data);
            items.Add(i);
        }
        if (GUILayout.Button("Add Weapon")) {
            Weapon i = CreateInstance<Weapon>().Init(items.Count + 1, "New Weapon", 1, 0, 0, 0);
            Debug.Log(i.ToString());
            Debug.Log(data.ToString());
            AssetDatabase.AddObjectToAsset(i, data);
            items.Add(i);
        }
        if (GUILayout.Button("Add Shield")) {
            Shield i = CreateInstance<Shield>().Init(items.Count + 1, "New Shield", 1, 0, 0, 0, 1);
            AssetDatabase.AddObjectToAsset(i, data);
            items.Add(i);
        }
        if (GUILayout.Button("Add Armor")) {
            Armor i = CreateInstance<Armor>().Init(items.Count + 1, "New Armor", 1, Armor.armor_type.chest, 0);
            AssetDatabase.AddObjectToAsset(i, data);
            items.Add(i);
        }
        if (GUILayout.Button("Add Shoes")) {
            Shoes i = CreateInstance<Shoes>().Init(items.Count + 1, "New Shoes", 1, 1);
            AssetDatabase.AddObjectToAsset(i, data);
            items.Add(i);
        }
    }
}