using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RaycastReflection
{
    public class RaycastReflection : MonoBehaviour
    {
        public Color handleColor = Color.red;
        public int maxReflectionCount = 5;
        public int maxStepDistance = 10;
        public int thickness = 1;

        private void OnDrawGizmos()
        {
            Handles.color = handleColor;
            Handles.ArrowHandleCap(0, this.transform.position + this.transform.forward * 0.25f, this.transform.rotation, 0.5f, EventType.Repaint);
            Gizmos.color = handleColor;
            Gizmos.DrawWireSphere(this.transform.position, 0.25f);

            DrawRaycastReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);
        }

        private void DrawRaycastReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
        {
            if (reflectionsRemaining == 0)
            {
                return;
            }

            Vector3 startingPosition = position;

            Ray ray = new Ray(position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxStepDistance))
            {
                direction = Vector3.Reflect(direction, hit.normal);
                position = hit.point;
            }
            else
            {
                position += direction * maxStepDistance;
            }

            for (int i = 0; i < thickness; ++i)
            {
                Gizmos.color = handleColor;
                Handles.DrawLine(startingPosition, position, i);
            }

            DrawRaycastReflectionPattern(position, direction, reflectionsRemaining - 1);
        }
    }
}

