using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemWizard : EditorWindow {
    private ItemDB db;
    [MenuItem("Window/ItemWizard")]
    public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(ItemWizard));
    }
    public void OnEnable() {
        hideFlags = HideFlags.HideAndDontSave;
        db = ItemDB.Instance;
    }
    public void OnGUI() {
        GUILayout.Label("Item Database Wizard", EditorStyles.boldLabel);
        db.OnGUI();
    }
}
