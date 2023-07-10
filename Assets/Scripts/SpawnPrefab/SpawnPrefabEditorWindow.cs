using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SpawningPrefabs
{
   static public class Class1 {
        public enum PrefabsTest{  Cube, Sphere, Capsule }
        static public PrefabsTest prefabsTest;
    }

    public class SpawnPrefabEditorWindow : EditorWindow
    {
        
        [MenuItem("Fatih Extensions / Spawn Prefabs %h")]
        static void ShowWindow()
        {
            SpawnPrefabEditorWindow window = (SpawnPrefabEditorWindow)EditorWindow.GetWindow(typeof(SpawnPrefabEditorWindow));
            window.Show();
            window.minSize = new Vector2(400, 175);
            window.maxSize = new Vector2(400, 175);
        }

        GameObject previewObj;
        

        public void OnEnable()
        {
            if (!GameObject.Find("PreviewObj"))
            {
                previewObj = new GameObject();
                Undo.RegisterCreatedObjectUndo(previewObj, "previewObj Remove");
                previewObj.name = "Prefab Spawner";
                previewObj.AddComponent<SpawningPrefabContainer>();
                Selection.activeGameObject = previewObj;

            }
        }

        public void OnDestroy()
        {
            DestroyImmediate(previewObj);
        }

        public void OnGUI()
        {
            string helpMessage = "Right click to add prefabs to the scene. \nLeft click the button to change prefab.\nMake sure that selection active game object is Prefab Spawner ";

            EditorGUILayout.HelpBox(helpMessage, MessageType.Info);

            using (var horizontalScope = new GUILayout.HorizontalScope())
            {
                using (var verticalScope = new GUILayout.VerticalScope())
                {
                    GUILayout.Space(10);
                    if (GUILayout.Button("Cube", GUILayout.Height(30)))
                    {
                        Class1.prefabsTest = Class1.PrefabsTest.Cube;
                    }
                    GUILayout.Space(10);
                    if (GUILayout.Button("Sphere", GUILayout.Height(30)))
                    {
                        Class1.prefabsTest = Class1.PrefabsTest.Sphere;
                    }
                    GUILayout.Space(10);
                    if (GUILayout.Button("Capsule", GUILayout.Height(30)))
                    {
                        Class1.prefabsTest = Class1.PrefabsTest.Capsule;
                    }
                    GUILayout.Space(10);
                }
            }
        }
    }

}
