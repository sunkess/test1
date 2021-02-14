using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Page
{
    public string Text;
}

[System.Serializable]
public class Chapter
{
    public string chapterName;
    public string abc;
    public List<Page> pageList;
}


[CreateAssetMenu(menuName = "Example", fileName = "new Book")]
public class Book : ScriptableObject
{
    public string bookName;
    public List<Chapter> chapterList;

    [CustomEditor(typeof(Book))]
    private class BookDrawer : Editor
    {
        private ReorderableList chapterReList;
        private SerializedProperty chapterListSP;
        private bool isExpand = false;

        private float oneLineHeight;

        private void OnEnable()
        {
            oneLineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            chapterListSP = serializedObject.FindProperty("chapterList");

            chapterReList = new ReorderableList(serializedObject, chapterListSP)
            {
                displayAdd = true,
                displayRemove = true,
                draggable = true,

                drawHeaderCallback = rect =>
                {
                    EditorGUI.LabelField(rect, "안녕?");
                },

                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    var chapterSP = chapterListSP.GetArrayElementAtIndex(index);

                    var nameSP = chapterSP.FindPropertyRelative("abc");

                    int i = 0;
                    var chapterLabel = new GUIContent();
                    chapterLabel.text = "제목";
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y + (i * oneLineHeight), rect.width, oneLineHeight), nameSP, chapterLabel);
                },

                elementHeightCallback = index =>
                {
                    return 1 * oneLineHeight;
                }
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            //var nameSP = serializedObject.FindProperty("bookName");
            //var bookLabel = new GUIContent();
            //bookLabel.text = "김기욱";
            //EditorGUILayout.PropertyField(nameSP, bookLabel);

            isExpand = EditorGUILayout.Foldout(isExpand, "챕터 리스트");

            if (isExpand)
            {
                chapterReList.DoLayoutList();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
