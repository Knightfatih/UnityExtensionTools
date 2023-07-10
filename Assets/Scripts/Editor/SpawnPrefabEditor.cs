using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SpawningPrefabs
{
    [CustomEditor(typeof(SpawningPrefabContainer))]
    public class SpawnPrefabEditor : Editor
    {
        private GameObject container;
        public string prefab;

        void OnSceneGUI()
        {
            MouseClick();
        }

        private void MouseClick()
        {
            Event guiEvent = Event.current;

            if (guiEvent.type == EventType.MouseDown && guiEvent.button == 1)
            {
                Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hitInfo;

                switch (Class1.prefabsTest)
                {
                    case Class1.PrefabsTest.Cube:
                        prefab = "Cube";
                        break;
                    case Class1.PrefabsTest.Sphere:
                        prefab = "Sphere";
                        break;
                    case Class1.PrefabsTest.Capsule:
                        prefab = "Capsule";
                        break;
                }
                    
                

                if (Physics.Raycast(worldRay, out hitInfo))
                {
                    GameObject prefabInstance = PrefabSpawner(prefab);
                    prefabInstance.transform.position = hitInfo.point;
                    container = hitInfo.transform.gameObject;
                    prefabInstance.transform.parent = container.transform;
                    EditorUtility.SetDirty(prefabInstance);

                }
            }
            Event.current.Use();
        }

        private GameObject PrefabSpawner(string prefabName)
        {
            GameObject prefabGameobject = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + prefabName + ".prefab", typeof(GameObject)) as GameObject; 
            GameObject prefabSpawner = Instantiate(prefabGameobject);
            prefabSpawner.name = prefabName;
            Undo.RegisterCreatedObjectUndo(prefabSpawner, "ShapeCreater Remove");
            return prefabSpawner;
        }

    }

}
