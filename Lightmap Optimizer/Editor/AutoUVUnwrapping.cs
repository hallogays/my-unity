using UnityEngine;

public class AutoUVUnwrapping
{
    public static void UnwrapUVs(Mesh mesh)
    {
        Unwrapping.GenerateSecondaryUVSet(mesh);
    }
}    