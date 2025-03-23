using UnityEngine;

public class BakingParameterRecommender
{
    public static Lightmapping.BakeSettings RecommendSettings(GameObject root)
    {
        Bounds bounds = CalculateSceneBounds(root);
        float sceneSize = bounds.size.magnitude;

        Lightmapping.BakeSettings settings = Lightmapping.GetDefaultBakeSettings();
        if (sceneSize < 10f)
        {
            settings.lightmapResolution = 100;
            settings.shadowResolution = 1024;
        }
        else if (sceneSize < 50f)
        {
            settings.lightmapResolution = 50;
            settings.shadowResolution = 2048;
        }
        else
        {
            settings.lightmapResolution = 20;
            settings.shadowResolution = 4096;
        }
        return settings;
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