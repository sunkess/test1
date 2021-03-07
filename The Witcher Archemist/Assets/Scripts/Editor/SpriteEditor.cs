using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpriteEditor : EditorWindow
{
    public HouseData storedata = new HouseData();

    [MenuItem("Tools/CreateLevel", false, 0)]
    private static void init()
    {
        var window = GetWindow(typeof(LevelEditor), true, "CreateLevel");

        var width = 300;
        var height = 500;

        var x = (Screen.currentResolution.width - width) / 2;
        var y = (Screen.currentResolution.height - height) / 2;

        window.position = new Rect(x, y, width, height);
    }

    private void OnGUI()
    {
        GUILayout.Toolbar(0, new string[] { "CreateLevel" });
        Draw();
    }

    public void Draw()
    {
        GUILayout.Space(15);

        GUILayout.BeginHorizontal();

        EditorGUILayout.LabelField(new GUIContent("LV", "레벨을 넣으세유"), GUILayout.Width(20));

        

    }
}
