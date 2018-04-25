using UnityEngine;
using UnityEditor;

public class AbilityDBEditor {
    private static string GetSavePath() {
        return EditorUtility.SaveFilePanelInProject("New item database", "New item database", "asset", "Create a new item database.");
    }

    [MenuItem("Assets/Create/Databases/AbilityDB")]
    public static void CreateDatabase() {
        string assetPath = GetSavePath();
        AbilityDB asset = ScriptableObject.CreateInstance("AbilityDB") as AbilityDB;  //scriptable object
        AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(assetPath));
        AssetDatabase.Refresh();
    }
}

