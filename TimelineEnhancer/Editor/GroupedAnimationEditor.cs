using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
[CustomEditor(typeof(GroupedAnimation))]
class GroupedAnimationEditor : Editor {
    private SerializedProperty groupTrack;

    void OnEnable() {
        groupTrack = serializedObject.FindProperty("groupTrack");
    }

    void OnGUI() {
        EditorGUILayout.PropertyField(groupTrack);
        // 分组管理按钮
        if (GUILayout.Button("Create New Group")) {
            CreateGroup();
        }
    }

    private void CreateGroup() {
        // 实现分组创建逻辑
        GroupedAnimation group = new GroupedAnimation();
        group.groupTrack = "New Group";
        Undo.RecordObject(group, "Create Group");
        UnityEditor.AssetDatabase.AddAsset(group);
    }
}

[System.Serializable]
class GroupedAnimation {
    string groupTrack;
}