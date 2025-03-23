using UnityEngine;

public class AutoLightProbeGenerator
{
    public static void GenerateLightProbes(GameObject root)
    {
        LightProbeGroup lightProbeGroup = root.AddComponent<LightProbeGroup>();
        Bounds bounds = CalculateSceneBounds(root);

        // 这里简单示例，可根据场景布局调整探针位置
        int probeCountX = 5;
        int probeCountY = 5;
        int probeCountZ = 5;

        Vector3[] positions = new Vector3[probeCountX * probeCountY * probeCountZ];
        int index = 0;
        for (int x = 0; x < probeCountX; x++)
        {
            for (int y = 0; y < probeCountY; y++)
            {
                for (int z = 0; z < probeCountZ; z++)
                {
                    positions[index] = new Vector3(
                        bounds.min.x + (float)x / (probeCountX - 1) * bounds.size.x,
                        bounds.min.y + (float)y / (probeCountY - 1) * bounds.size.y,
                        bounds.min.z + (float)z / (probeCountZ - 1) * bounds.size.z
                    );
                    index++;
                }
            }
        }

        lightProbeGroup.probePositions = positions;
    }

    private static Bounds CalculateSceneBounds(GameObject root)
    {
        Renderer[] renderers = root.GetComponentsInChildren<Renderer>();
        Bounds bounds = new Bounds();
        if (renderers.Length > 0)
        {
            bounds = renderers[0].bounds;
            for (int i = 1; i < renderers.Length; i++)
            {
                bounds.Encapsulate(renderers[i].bounds);
            }
        }
        return bounds;
    }
}    