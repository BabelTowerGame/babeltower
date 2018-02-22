using UnityEngine;

public class MonsterDB : ScriptableObject {

    #region singleton
    private static MonsterDB m_Instance;
    public static MonsterDB Instance {
        get {
            if (m_Instance == null)
                m_Instance = Resources.Load("Databases/MonsterDB") as MonsterDB;

            return m_Instance;
        }
    }
    #endregion

    public Monster[] items;

    public Monster get(int index) {
        return (this.items[index]);
    }

    public Monster getByID(int ID) {
        for (int i = 0; i < this.items.Length; i++) {
            if (this.items[i].ID == ID)
                return this.items[i];
        }

        return null;
    }

    public Monster getByName(string name) {
        for (int i = 0; i < this.items.Length; i++) {
            if (this.items[i].Name.Equals(name))
                return this.items[i];
        }

        return null;
    }
}