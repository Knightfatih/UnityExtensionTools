using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionsMethods
{
    //Vector 2 to Vector 3 
    public static Vector2 ToXZ(this Vector3 v3)
    {
        return new Vector2(v3.x, v3.z);
    }
}
