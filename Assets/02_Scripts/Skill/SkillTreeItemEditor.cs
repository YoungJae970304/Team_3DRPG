using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
[CustomEditor(typeof(SkillTreeItem))]
public class SkillTreeItemEditor : Editor
{
    MonoScript monoScript;
    SkillTreeItem item;
    MonoScript last;
    private void OnEnable()
    {
        item = (SkillTreeItem)target;
        monoScript = item._skillScript;
    }
    public override void OnInspectorGUI()
    {
        
        //var monoScript = serializedObject.FindProperty("Mono");
        last = monoScript;

        monoScript = EditorGUILayout.ObjectField("Skill", monoScript, typeof (MonoScript)) as MonoScript;

        if (last != monoScript) {
            if (!item.SetSkill(monoScript))
            {
                Logger.Log("23213");
                monoScript = null;
            }
        }
        
        if (monoScript != null) {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_skillLevel"));
        }
        
        
        serializedObject.ApplyModifiedProperties();
    }
}
