using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(MenuMetaData))]
public class MetaDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MenuMetaData script = (MenuMetaData)target;
        if (GUILayout.Button("Add MetaData"))
        {
            script.ParseThrough();
        }
    }
}