using UnityEngine;

using System;

public class AbilityDB : ScriptableObject {

    #region singleton
    private static AbilityDB m_Instance;
    public static AbilityDB Instance {
        get {
            if (m_Instance == null)
                m_Instance = Resources.Load("Databases/AbilityDB") as AbilityDB;

            return m_Instance;
        }
    }
    #endregion

    public Ability[] items;

    public Ability get(int index) {
        return (this.items[index]);
    }

}