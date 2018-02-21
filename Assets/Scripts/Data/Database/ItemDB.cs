using UnityEngine;

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

    public Item[] items;

    public Item get(int index) {
        return (this.items[index]);
    }

    public Item getByID(int ID) {
        for (int i = 0; i < this.items.Length; i++) {
            if (this.items[i].ID == ID)
                return this.items[i];
        }

        return null;
    }

    public Item getByName(string name) {
        for (int i = 0; i < this.items.Length; i++) {
            if (this.items[i].Name.Equals(name))
                return this.items[i];
        }

        return null;
    }
}