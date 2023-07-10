using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShapeEditorWindow : EditorWindow
{
    GameObject shapeCreater;
    GameObject meshObject;

    void OnEnable()
    {
        if (!GameObject.Find("Shape Creater"))
        {
            shapeCreater = new GameObject();
            shapeCreater.name = "Shape Creater";
            CreateShape createShape = shapeCreater.AddComponent<CreateShape>();
            Undo.RegisterCreatedObjectUndo(shapeCreater, "ShapeCreater Remove");

            meshObject = new GameObject();
            meshObject.name = "Mesh Object";
            var newMesh = meshObject.AddComponent<MeshFilter>();
            createShape.meshFilter = newMesh;
            Material material = Resources.Load<Material>("Materials/Blue");
            MeshRenderer mesh = meshObject.AddComponent<MeshRenderer>();
            mesh.material = material;
            meshObject.AddComponent<MeshCollider>();
            Selection.activeGameObject = shapeCreater;

            Undo.RegisterCreatedObjectUndo(meshObject, "MeshObject Remove");
        }
    }

    [MenuItem("Fatih Extensions / Shape Creater %g")]
    static void Init()
    {
        ShapeEditorWindow window = (ShapeEditorWindow)EditorWindow.GetWindow(typeof(ShapeEditorWindow));
        window.Close();
    }
}
