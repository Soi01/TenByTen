using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor( typeof( DataTableMaker ) )]
public class ScriptableDataBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DataTableMaker dataTableMaker = ( DataTableMaker )target;

        if( GUILayout.Button( "테이블 업데이트" ) )
        {
            dataTableMaker.LoadData();
        }
    }
}
#endif