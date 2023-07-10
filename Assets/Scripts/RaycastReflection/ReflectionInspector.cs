using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RaycastReflection
{
    [CustomEditor(typeof(RaycastReflection))]
    [CanEditMultipleObjects]
    public class ReflectionInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            RaycastReflection raycastReflection = (RaycastReflection)target;

            using (var horizantalScope = new GUILayout.HorizontalScope())
            {
                using (var verticalScope = new GUILayout.VerticalScope())
                {
                    raycastReflection.handleColor = EditorGUILayout.ColorField("Handles Color ", raycastReflection.handleColor);
                    using (var horizantalScope1 = new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label("Reflection Count: ");
                        GUILayout.FlexibleSpace();
                        raycastReflection.maxReflectionCount = EditorGUILayout.IntSlider(raycastReflection.maxReflectionCount, 1, 100, GUILayout.Width(250));
                    }
                    using (var horizantalScope1 = new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label("Line Lenght: ");
                        GUILayout.FlexibleSpace();
                        raycastReflection.maxStepDistance = EditorGUILayout.IntSlider(raycastReflection.maxStepDistance, 1, 1000,  GUILayout.Width(250));
                    }
                    using (var horizantalScope1 = new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label("Line Thickness: ");
                        GUILayout.FlexibleSpace();
                        raycastReflection.thickness = EditorGUILayout.IntSlider(raycastReflection.thickness, 1, 10, GUILayout.Width(250));
                    }
                }
                    
            }
        }
    }
}