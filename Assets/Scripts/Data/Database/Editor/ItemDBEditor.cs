﻿using UnityEngine;
using UnityEditor;
public class ItemDBEditor {
    private static string GetSavePath() {
        return EditorUtility.SaveFilePanelInProject("New item database", "New item database", "asset", "Create a new item database.");
    }

    [MenuItem("Assets/Create/Databases/ItemDB")]
    public static void CreateDatabase() {
        string assetPath = GetSavePath();
        DataHolder data = (DataHolder)ScriptableObject.CreateInstance<DataHolder>();
        AssetDatabase.CreateAsset(data, ItemDB.dataAssetPath);
        ItemDB asset = ScriptableObject.CreateInstance("ItemDB") as ItemDB;  //scriptable object
        AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(assetPath));
        AssetDatabase.Refresh();
    }
}