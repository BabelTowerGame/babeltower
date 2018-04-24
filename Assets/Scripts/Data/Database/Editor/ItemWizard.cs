using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemWizard : EditorWindow {
    private ItemDB db;
    Vector2 sv;
    [MenuItem("Window/ItemWizard")]
    public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(ItemWizard));
    }
    public void OnEnable() {
        hideFlags = HideFlags.HideAndDontSave;
        db = ItemDB.Instance;
    }
    public void OnGUI() {
        sv = GUILayout.BeginScrollView(sv);
        GUILayout.Label("Item Database Wizard", EditorStyles.boldLabel);
        if(GUILayout.Button("Save Data")) {
            EditorUtility.SetDirty(db);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        db.OnGUI();
        GUILayout.EndScrollView();
    }
}
