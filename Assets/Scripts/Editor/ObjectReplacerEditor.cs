using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Replacer
{
    public class ObjectReplacerEditor : EditorWindow
    {
        int currentSelectionCount = 0;
        GameObject wantedObject;

        [MenuItem("Fatih Extensions / Object Replacer %k")]
        static void Init()
        {
            ObjectReplacerEditor window = (ObjectReplacerEditor)EditorWindow.GetWindow(typeof(ObjectReplacerEditor));
            window.Show();
            window.minSize = new Vector2(400, 105);
            window.maxSize = new Vector2(400, 105);
        }

        private void OnGUI()
        {
            GetSelection();

            using (var horizontalScope = new GUILayout.HorizontalScope ())
            {
                using (var verticalScope  = new GUILayout.VerticalScope())
                {
                    GUILayout.Space(10);
                    GUILayout.Label("Number of selected objects: " + currentSelectionCount.ToString(), EditorStyles.boldLabel);
                    GUILayout.Space(10);

                    wantedObject = (GameObject)EditorGUILayout.ObjectField("Replace Object: ", wantedObject, typeof(GameObject), true);
                    GUILayout.Space(10);

                    if (GUILayout.Button("Replace Selected Objects", GUILayout.Height(30)))
                    {
                        ReplacedObjects();
                    }
                    GUILayout.Space(10);
                }

            }
            Repaint();
        }

        void GetSelection()
        {
            currentSelectionCount = 0;
            currentSelectionCount = Selection.gameObjects.Length;
        }

        void ReplacedObjects()
        {
            if(currentSelectionCount == 0)
            {
                CustomDialog("At least one object needs to be selected to replace with!");
                return;
            }

            if (!wantedObject)
            {
                CustomDialog("The Replace Object is empty, please assign object!");
                return;
            }

            GameObject[] selectedObjects = Selection.gameObjects;
            for (int i = 0; i < selectedObjects.Length; i++)
            {
                Transform selectedTransform = selectedObjects[i].transform;
                GameObject newObject = Instantiate(wantedObject, selectedTransform.position, selectedTransform.rotation);
                newObject.transform.localScale = new Vector3(1,1,1);
                Undo.RegisterCreatedObjectUndo(newObject, "ShapeCreater Remove");
                
                
                DestroyImmediate(selectedObjects[i]);
            }

        }

        void CustomDialog(string message)
        {
            EditorUtility.DisplayDialog("Replace Objects Warning", message, "OK");
        }

    }

}
