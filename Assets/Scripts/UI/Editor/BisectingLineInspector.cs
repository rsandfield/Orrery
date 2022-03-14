using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BisectingLine))]
public class BisectingLineInspector : Editor
{
    BisectingLine line;

    
    // public override void OnInspectorGUI()
    // {
    //     line = target as BisectingLine;
        
        // DrawPropertiesExcluding(serializedObject, new string[] {
        //     "_lineWidth"
        // });

        // EditorGUI.BeginChangeCheck();
        // float lineWidth = EditorGUILayout.FloatField("Line Width", line.lineWidth);
        // if(EditorGUI.EndChangeCheck())
        // {
        //     Undo.RecordObject(line, "Change Flag Color");
        //     line.lineWidth = lineWidth;
        //     EditorUtility.SetDirty(line);
        // }
    // }
}
