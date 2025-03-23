using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class EventEditorWindow : EditorWindow
{
    private AnimationClip currentClip;
    private SerializedObject serializedClip;
    private SerializedProperty eventProperty;

    [MenuItem("Tools/Timeline Enhancer/Event Editor")]
    static void ShowEventEditor()
    {
        GetWindow<EventEditorWindow>("Event Editor");
    }

    void OnGUI()
    {
        currentClip = Selection.activeObject as AnimationClip;
        if (currentClip != null)
        {
            serializedClip = new SerializedObject(currentClip);
            eventProperty = serializedClip.FindProperty("m_Events");
        }

        if (eventProperty != null)
        {
            serializedClip.Update();

            EditorGUI.BeginDisabledGroup(eventProperty.arraySize == 0);
            for (int i = 0; i < eventProperty.arraySize; i++)
            {
                SerializedProperty eventElement = eventProperty.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(eventElement, new GUIContent($"Event {i + 1}"), true);
            }
            EditorGUI.EndDisabledGroup();

            if (GUILayout.Button("Add Event"))
            {
                eventProperty.InsertArrayElementAtIndex(eventProperty.arraySize);
                SerializedProperty newEvent = eventProperty.GetArrayElementAtIndex(eventProperty.arraySize - 1);
                newEvent.FindPropertyRelative("time").floatValue = 1.0f;
                newEvent.FindPropertyRelative("functionName").stringValue = "TestFunction";
                serializedClip.ApplyModifiedProperties();
                Repaint();
            }
        }
    }
}