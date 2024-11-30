using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ReplaceObject : EditorWindow
{
    private GameObject newPrefab;

    [MenuItem("Tools/Replace GameObject")]
    public static void ShowWindow()
    {
        GetWindow<ReplaceObject>("Replace GameObject");
    }

    void OnGUI()
    {
        GUILayout.Label("Select New Prefab", EditorStyles.boldLabel);
        newPrefab = (GameObject)EditorGUILayout.ObjectField("New Prefab", newPrefab, typeof(GameObject), false);

        if (GUILayout.Button("Replace Selected"))
        {
            ReplaceSelected();
        }
    }

    private void ReplaceSelected()
    {
        if (newPrefab == null)
        {
            Debug.LogError("No prefab selected!");
            return;
        }

        foreach (GameObject obj in Selection.gameObjects)
        {
            GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(newPrefab);
            if (newObject != null)
            {
                newObject.transform.position = obj.transform.position;
                newObject.transform.rotation = obj.transform.rotation;
                newObject.transform.localScale = obj.transform.localScale;

                Undo.RegisterCreatedObjectUndo(newObject, "Replace GameObject");
                Undo.DestroyObjectImmediate(obj);
            }
        }
    }
}
