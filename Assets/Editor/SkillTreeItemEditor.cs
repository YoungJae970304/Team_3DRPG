using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
[CustomEditor(typeof(SkillTreeItem))]
public class SkillTreeItemEditor : Editor // 스킬트리 아이템을 위한 커스텀 인스펙터
{
    SkillTreeItem item;
    int id=-1;
    private void OnEnable()
    {
        item = (SkillTreeItem)target;
    }
    
    public override void OnInspectorGUI()
    {
        //모노 스크립트를 인스펙터로 입력받음
       
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_skillId"));
        id = item._skillId;

        
        if (id >0) {//스크립트 입력이 있어야만 표시
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_conditions"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_skillLevel")); 
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_maxLevel"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_skillInfo"));
        }
        
        
        serializedObject.ApplyModifiedProperties();
    }
   
}
