using UnityEngine;
using UnityEditor;

public class UIItemDatabaseEditor {
    private static string GetSavePath() {
        return EditorUtility.SaveFilePanelInProject("New item database", "New item database", "asset", "Create a new item database.");
    }

    [MenuItem("Assets/Create/Databases/ItemDB")]
    public static void CreateDatabase() {
        string assetPath = GetSavePath();
        ItemDB asset = ScriptableObject.CreateInstance("ItemDB") as ItemDB;  //scriptable object
        AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(assetPath));
        AssetDatabase.Refresh();
    }
}

