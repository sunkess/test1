using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityScript.Steps;

public class LevelEditor : EditorWindow
{
    public static LevelEditor instance;
    public LVHouseData storedata = new LVHouseData();

    public int currentChooseTile = 0;
    public int tileWidth = 32;
    public int prevWidth;
    public int prevHeight;
    public string textureKey = "";
    public List<TileEdit> tileEdit = new List<TileEdit>();

    public Vector2 scrollPos;


    [MenuItem("Tools/CreateLevel", false, 0)]
    private static void init()
    {

        LevelEditor window =
          (LevelEditor)EditorWindow.GetWindow(typeof(LevelEditor),
                                                       utility: true,
                                                       title: "CreateLevel");

        var width = 500;
        var height = 800;

        var x = (Screen.currentResolution.width - width) / 2;
        var y = (Screen.currentResolution.height - height) / 2;

        window.position = new Rect(x, y, width, height);

        TileList.ClearTileList();

        if(TileList.isEmptyImageList())
        {
            TileList.SetImageList();
            Debug.Log("이미지 불러오기 세팅");
        }
        window.Show();
    }

    

    

    private void OnGUI()
    {
        instance = this;

        GUILayout.Toolbar(0, new string[] { "CreatLevel" });

        Draw();
    }

    public void Draw()
    {
        
        GUILayout.Space(15);

        GUILayout.BeginHorizontal();

        prevWidth = storedata.x;
        prevHeight = storedata.y;

        EditorGUILayout.LabelField(new GUIContent("LV", "레벨을 넣으세유"), GUILayout.Width(20));

        if (string.IsNullOrEmpty(storedata.lv))
        {
            storedata.lv = "0";
        }

        storedata.lv =  EditorGUILayout.TextField(storedata.lv, GUILayout.Width(40));

        GUILayout.Space(15);

        EditorGUILayout.LabelField(new GUIContent("NextEXP", "경험치를 넣어주세요"), GUILayout.Width(50));

        storedata.nextExp = EditorGUILayout.IntField(storedata.nextExp, GUILayout.Width(50));

        GUILayout.Space(5);

        EditorGUILayout.LabelField(new GUIContent("X", "건물의 가로길이"), GUILayout.Width(15));


        storedata.x = EditorGUILayout.IntField(storedata.x, GUILayout.Width(30));

        GUILayout.Space(5);

        EditorGUILayout.LabelField(new GUIContent("Y", "건물의 세로길이"), GUILayout.Width(15));


        storedata.y = EditorGUILayout.IntField(storedata.y, GUILayout.Width(30));

        GUILayout.Space(15);

        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("저장", GUILayout.Height(30)))
        {
            Save();
        }

        if (GUILayout.Button("이미지 선택", GUILayout.Height(30)))
        {
            ChooseImageEditor window =
          (ChooseImageEditor)EditorWindow.GetWindow(typeof(ChooseImageEditor),
                                                       utility: true,
                                                       title: "ChooseImage");

            var width = 300;
            var height = 500;

            var x = (Screen.currentResolution.width - width) / 2;
            var y = (Screen.currentResolution.height - height) / 2;

            window.position = new Rect(x, y, width, height);

            window.Show();
        }

        if (GUILayout.Button("열기", GUILayout.Height(30)))
        {
            Load();
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        //if (GUILayout.Button("가구배치타일", GUILayout.Height(15)))
        //{
        //    currentChooseTile = 0;
        //}

        //if (GUILayout.Button("가구배치타일X", GUILayout.Height(15)))
        //{
        //    currentChooseTile = 1;
        //}

        //if (GUILayout.Button("출입문", GUILayout.Height(15)))
        //{
        //    currentChooseTile = 2;

        //}

        //if (GUILayout.Button("연금술문", GUILayout.Height(15)))
        //{
        //    currentChooseTile = 3;

        //}

        GUILayout.EndHorizontal();

        

        if (prevWidth != storedata.x || prevHeight != storedata.y)
        {
            storedata.tiles = new List<TileEdit>(storedata.x * storedata.y);

            for (int i = 0; i < storedata.x * storedata.y; i++)
            {
                storedata.tiles.Add(new TileEdit());
            }
        }

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos,
                                                      true,
                                                      true);

        CreateTile();

        EditorGUILayout.EndScrollView();

    }
    
    
    private void CreateTile()
    {
        
        //현재 인덱스
        int idx = 0;
        
        for (int y = 0; y < storedata.y; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < storedata.x; x++)
            {
                GUI.backgroundColor = TileColor.colorArray[storedata.tiles[idx].tileNum];

                
                if (GUILayout.Button(storedata.tiles[idx].texture, GUILayout.Width(48), GUILayout.Height(48)))
                {
                    if(textureKey != "")
                    {
                        storedata.tiles[idx].texture = TileList.textureToName[textureKey];

                    }
                }
                
                idx++;
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void Save()
    {
        string path = Application.dataPath + "/Resources/Levels/" + storedata.lv + ".json";

        string json = JsonUtility.ToJson(storedata, true);
        

        if (storedata.lv != "0")
        {
            if (File.Exists(path))
            {
                bool window = EditorUtility.DisplayDialog("파일 존재", "이미 파일이 존재합니다 덮어 씁니까?", "확인", "취소");
                if(window)
                {
                    File.WriteAllText(path, json);
                    EditorUtility.DisplayDialog("저장성공", "저장 되었습니다.", "확인");
                    Reset();
                }
            }
            else
            {
                File.WriteAllText(path, json);
                EditorUtility.DisplayDialog("저장성공", "저장 되었습니다.", "확인");
                Reset();
            }
        }
        else
        {
            EditorUtility.DisplayDialog("저장실패", "저장 실패", "확인");
        }
    }

    

    private void Load()
    {
        var path = EditorUtility.OpenFilePanel("열기", Application.dataPath + "/Resources/Levels", "json");

        if (!string.IsNullOrEmpty(path))
        {
            string json = File.ReadAllText(path);

            storedata = JsonUtility.FromJson<LVHouseData>(json);
            Draw();
        }
    }

    private void Reset()
    {
        storedata.lv = string.Empty;
        storedata.nextExp = 0;
        storedata.x = 0;
        storedata.y = 0;
    }

    
}
