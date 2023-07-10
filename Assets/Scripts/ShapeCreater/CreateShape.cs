using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fatih.Editor;

public class CreateShape : MonoBehaviour
{
    public MeshFilter meshFilter;

    [HideInInspector]
    public List<Shape> shapes = new List<Shape>();

    [HideInInspector]
    public bool showShapesList;

    public float handleRadius = 0.5f;

    public void UpdateMeshDisplay()
    {
        CompositeShape compositeShape = new CompositeShape(shapes);
        meshFilter.mesh = compositeShape.GetMesh();
    }
}

/*
[System.Serializable]
public class Shape
{
    //List of list tactic
    public List<Vector3> points = new List<Vector3>();
}
*/