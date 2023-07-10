using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RaycastReflection
{
    public class ReflectionWindow : EditorWindow
    {
        GameObject myGameObject;
        GameObject tempObject;
        int reflectionCount = 10;
        int reflectionDistance = 10;
        int reflectionThickness = 1;
        Color reflectionHandlesColor = Color.red;

        void OnEnable()
        {
            if (!GameObject.Find("tempObject"))
            {
                tempObject = new GameObject();
                Undo.RegisterCreatedObjectUndo(tempObject, "tempObject Remove");

                Ray ray = SceneView.lastActiveSceneView.camera.ScreenPointToRay(new Vector2(SceneView.lastActiveSceneView.position.width / 2, SceneView.lastActiveSceneView.position.height / 2));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    tempObject.transform.position = hit.point;
                }
                else
                {
                    //tempObject.transform.position = new Vector3(SceneView.lastActiveSceneView.position.width / 2, SceneView.lastActiveSceneView.position.height / 2) + Vector3.forward * 10f;
                }
                tempObject.name = "tempObject";
                AddingCompenent(tempObject);
                Selection.activeGameObject = tempObject;
            }
        }

        [MenuItem("Fatih Extensions / Raycast Reflection %j")]
        static void Init()
        {
            ReflectionWindow window = (ReflectionWindow)EditorWindow.GetWindow(typeof(ReflectionWindow));
            window.Show();
            window.minSize = new Vector2(470, 170); 
            window.maxSize = new Vector2(470, 170);
        }

        void OnGUI()
        {
            using (var horizantalScope = new GUILayout.HorizontalScope())
            {
                using (var verticalScope = new GUILayout.VerticalScope())
                {
                    GUILayout.Space(10);
                    reflectionHandlesColor = EditorGUILayout.ColorField("Handles Color ", reflectionHandlesColor);
                    GUILayout.Space(10);

                    using (var horizantalScope1 = new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label("Reflection Count: ");
                        GUILayout.FlexibleSpace();
                        using (var verticalScope1 = new GUILayout.VerticalScope())
                        {
                            reflectionCount = EditorGUILayout.IntSlider(reflectionCount, 1, 100);
                            GUILayout.Space(10);
                        }
                    }
                    using (var horizantalScope2 = new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label("Line Lenght: ");
                        GUILayout.FlexibleSpace();
                        using (var verticalScope1 = new GUILayout.VerticalScope())
                        {
                            reflectionDistance = EditorGUILayout.IntSlider(reflectionDistance, 1, 1000);
                            GUILayout.Space(10);
                        }
                    }
                    using (var horizantalScope2 = new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label("Line Thickness: ");
                        GUILayout.FlexibleSpace();
                        using (var verticalScope1 = new GUILayout.VerticalScope())
                        {                            
                            reflectionThickness = EditorGUILayout.IntSlider(reflectionThickness, 1, 10);
                            GUILayout.Space(10);
                        }
                    }

                    if (GUILayout.Button("Create a Reflection GameObject", GUILayout.ExpandWidth(true), GUILayout.Height(30)))
                    {
                        myGameObject = new GameObject();
                        Undo.RegisterCreatedObjectUndo(myGameObject, "myGameObject Remove");
                        myGameObject.name = "Reflection Object";
                        AddingCompenent(myGameObject);



                        if (tempObject != null)
                        {
                            Selection.activeGameObject = tempObject;
                        }

                        myGameObject.transform.position = tempObject.transform.position;
                        myGameObject.transform.rotation = tempObject.transform.rotation;
                    }
                    if (Selection.activeGameObject != null)
                    {
                        myGameObject = Selection.activeGameObject;
                        if (myGameObject.GetComponent<RaycastReflection>())
                        {
                            myGameObject.GetComponent<RaycastReflection>().handleColor = reflectionHandlesColor;
                            myGameObject.GetComponent<RaycastReflection>().maxReflectionCount = reflectionCount;
                            myGameObject.GetComponent<RaycastReflection>().maxStepDistance = reflectionDistance;
                            myGameObject.GetComponent<RaycastReflection>().thickness = reflectionThickness;
                        }
                    }
                    GUILayout.Space(10);
                }
            }
        }
        private void AddingCompenent(GameObject gameObjectName)
        {
            gameObjectName.AddComponent<RaycastReflection>().handleColor = reflectionHandlesColor;
            RaycastReflection raycastReflection = gameObjectName.GetComponent<RaycastReflection>();
            raycastReflection.maxReflectionCount = reflectionCount;
            raycastReflection.maxStepDistance = reflectionDistance;
            raycastReflection.thickness = reflectionThickness;
        }

        void OnDestroy()
        {
            DestroyImmediate(tempObject);
        }
    }
}

