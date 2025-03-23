using UnityEditor;
using UnityEngine;

public class LightmapOptimizerWindow : EditorWindow
{
    [MenuItem("Tools/Lightmap Optimizer")]
    public static void ShowWindow()
    {
        GetWindow<LightmapOptimizerWindow>("Lightmap Optimizer");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("自动UV展开"))
        {
            GameObject[] selectedObjects = Selection.gameObjects;
            foreach (GameObject obj in selectedObjects)
            {
                MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
                if (meshFilter != null)
                {
                    AutoUVUnwrapping.UnwrapUVs(meshFilter.mesh);
                }
            }
        }

        if (GUILayout.Button("光照探针自动生成"))
        {
            GameObject[] selectedObjects = Selection.gameObjects;
            foreach (GameObject obj in selectedObjects)
            {
                AutoLightProbeGenerator.GenerateLightProbes(obj);
            }
        }

        if (GUILayout.Button("烘焙参数智能推荐"))
        {
            GameObject[] selectedObjects = Selection.gameObjects;
            foreach (GameObject obj in selectedObjects)
            {
                Lightmapping.BakeSettings settings = BakingParameterRecommender.RecommendSettings(obj);
                Lightmapping.bakeSettings = settings;
            }
        }

        if (GUILayout.Button("烘焙结果分析"))
        {
            GameObject[] selectedObjects = Selection.gameObjects;
            foreach (GameObject obj in selectedObjects)
            {
                BakingResultAnalyzer analyzer = obj.AddComponent<BakingResultAnalyzer>();
                analyzer.AnalyzeBakingResult();
            }
        }
    }
}    