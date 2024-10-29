using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
[CustomEditor(typeof(SkillTreeItem))]
public class SkillTreeItemEditor : Editor // 스킬트리 아이템을 위한 커스텀 인스펙터
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
        
        //이전 프레임의 입력값을 저장
        last = monoScript;
        //모노 스크립트를 인스펙터로 입력받음
        monoScript = EditorGUILayout.ObjectField("Skill", monoScript, typeof (MonoScript)) as MonoScript;
        //이전과 다른 스크립트면
        if (last != monoScript) {
            if (!item.SetSkill(monoScript))//skillBase type이 아니면 false 반환
            {//스킬을 변경하고 결과값에 따라 null로 변경 
                monoScript = null;
            }
        }
        
        if (monoScript != null) {//스크립트 입력이 있어야만 표시
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_conditions"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_skillLevel")); 
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_maxLevel"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_skillType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_statType"));
        }
        
        
        serializedObject.ApplyModifiedProperties();
    }
}
