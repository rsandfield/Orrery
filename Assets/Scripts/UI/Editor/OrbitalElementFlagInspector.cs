using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OrbitalElementFlag))]
public class OrbitalElementFlagInspector : Editor
{
    OrbitalElementFlag flag;

    public override void OnInspectorGUI()
    {
        flag = target as OrbitalElementFlag;

        DrawPropertiesExcluding(serializedObject, new string[] {
            "_flagColor",
            "_textColor"
        });

        EditorGUI.BeginChangeCheck();
        Color flagColor = EditorGUILayout.ColorField("Flag Color", flag.flagColor);
        if(EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(flag, "Change Flag Color");
            flag.flagColor = flagColor;
            EditorUtility.SetDirty(flag);
        }

        EditorGUI.BeginChangeCheck();
        Color textColor = EditorGUILayout.ColorField("Text Color", flag.textColor);
        if(EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(flag, "Change Flag Color");
            flag.textColor = textColor;
            EditorUtility.SetDirty(flag);
        }
    }

}