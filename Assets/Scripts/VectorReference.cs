using System;
using UnityEngine;

[Serializable]
public class VectorReference
{
    public VectorVariable Variable;

    public VectorReference()
    { }
    
    public Vector3 Value
    {
        get { return Variable.Value; }
    }

    public static implicit operator Vector3(VectorReference reference)
    {
        return reference.Value;
    }
}
