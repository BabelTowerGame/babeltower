using UnityEngine;

using System;

public class ConstIntDB : ScriptableObject {

    #region singleton
    private static ConstIntDB m_Instance;
    public static ConstIntDB Instance {
        get {
            if (m_Instance == null)
                m_Instance = Resources.Load("Databases/ConstIntDB") as ConstIntDB;

            return m_Instance;
        }
    }
    #endregion

    public Pair<string,int>[] items;

    public Pair<string, int> get(int index) {
        return (this.items[index]);
    }

    public Pair<string, int> getByKey(string key) {
        for (int i = 0; i < this.items.Length; i++) {
            if (this.items[i].First.Equals(key))
                return this.items[i];
        }

        return null;
    }
}