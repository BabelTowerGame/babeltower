using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Cheat : EditorWindow {
    [MenuItem("Window/Cheat")]
    public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(Cheat));
    }

    void OnGUI() {

        GUILayout.Label("Character", EditorStyles.boldLabel);
        GUILayout.Label("Level", EditorStyles.boldLabel);
        GUILayout.Label("Mob", EditorStyles.boldLabel);

        // Some cheat use to skip levels and debug


    }

}
