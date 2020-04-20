using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CanEditMultipleObjects]
public class SpawnerEditr : Editor
{
    SerializedProperty spawnRate;
    SerializedProperty isSpawning;
    SerializedProperty spawnable;
    SerializedProperty spawnPoint;
    SerializedProperty spawnTarget;
    SerializedProperty velocity;
    bool setToSpawnerPostion;

    void OnEnable() {
        spawnRate = serializedObject.FindProperty("spawnRate");
        isSpawning = serializedObject.FindProperty("isSpawning");
        spawnable = serializedObject.FindProperty("spawnable");
        spawnPoint = serializedObject.FindProperty("spawnPoint");
        spawnTarget = serializedObject.FindProperty("spawnTarget");
        velocity = serializedObject.FindProperty("velocity");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(spawnRate);
        EditorGUILayout.PropertyField(isSpawning);
        EditorGUILayout.PropertyField(spawnable);
        EditorGUILayout.PropertyField(velocity);
        EditorGUILayout.PropertyField(spawnPoint);
        EditorGUILayout.PropertyField(spawnTarget);
        if(GUILayout.Button(setToSpawnerPostion? "Unbind spawn point from spawner":"Bind spawn point to spawner")) {
            setToSpawnerPostion = !setToSpawnerPostion;
            spawnPoint.vector3Value = (target as Spawner).transform.position;

        }
        serializedObject.ApplyModifiedProperties();
    }

    public void OnSceneGUI()
    {
        /*var t = (target as Spawner);

        EditorGUI.BeginChangeCheck();

        //Vector3 pos = setToSpawnerPostion? 
            t.transform.position : Handles.CircleCap(0,)
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Move point");
            t.spawnPoint = pos;
        }
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(pos, Vector3.forward, 0.5f);
        */

    }



}
