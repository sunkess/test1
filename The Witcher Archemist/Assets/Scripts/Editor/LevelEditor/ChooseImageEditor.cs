using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChooseImageEditor : EditorWindow
{
    public string firstFindString = "Find";
    public Vector2 scrollPos;

    string t = "This is a string inside a Scroll view!";

    public string findTextureName = "";
    private void OnGUI()
    {
        Draw();
    }

    public void Draw()
    {
        GUILayout.Toolbar(0, new string[] { "사용할 이미지를 선택하세요." });

        GUILayout.BeginHorizontal();

        findTextureName = EditorGUILayout.TextField(findTextureName);

        GUILayout.EndHorizontal();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos,
                                                          true,
                                                          true,
                                                          GUILayout.Width(Screen.width),
                                                          GUILayout.Height(Screen.height - 50));

        //scrollPos = GUI.BeginScrollView(new Rect(0, 0, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height));

        CreateButton(findTextureName);

        EditorGUILayout.EndScrollView();

    }

    private GUIStyle contentStyle = new GUIStyle();
    void CreateButton(string _findTextureName)
    {
        contentStyle.alignment = TextAnchor.MiddleLeft;
        
        if (_findTextureName == "")
        {
            foreach (var texture in TileList.textureToName)
            {
                if (GUILayout.Button(TileList.textureToName[texture.Key], GUILayout.Width(32)))
                {
                    LevelEditor.instance.textureKey = texture.Key;
                    this.Close();
                }

                GUILayout.Label(texture.Key);
            }
            return;
        }

        foreach (var texture in TileList.textureToName)
        {
            if (texture.Key == _findTextureName)
            {
                if (GUILayout.Button(TileList.textureToName[_findTextureName], GUILayout.Width(32)))
                {
                    LevelEditor.instance.textureKey = texture.Key;
                    this.Close();
                }

                GUILayout.Label(texture.Key);
            }
        }
    }
}
