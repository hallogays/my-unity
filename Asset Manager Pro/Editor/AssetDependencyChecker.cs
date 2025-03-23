using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class AssetDependencyChecker
{
    [MenuItem("Assets/Asset Manager Pro/Check Asset Dependencies")]
    public static void CheckAssetDependencies()
    {
        string[] allAssets = AssetDatabase.GetAllAssetPaths();
        List<string> usedAssets = new List<string>();

        // 扫描场景
        string[] scenePaths = EditorBuildSettings.scenes.Select(s => s.path).ToArray();
        foreach (string scenePath in scenePaths)
        {
            string[] dependencies = AssetDatabase.GetDependencies(scenePath);
            usedAssets.AddRange(dependencies);
        }

        // 扫描Prefab
        string[] prefabPaths = AssetDatabase.FindAssets("t:Prefab");
        foreach (string prefabGuid in prefabPaths)
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(prefabGuid);
            string[] dependencies = AssetDatabase.GetDependencies(prefabPath);
            usedAssets.AddRange(dependencies);
        }

        // 标记未使用的资源
        foreach (string assetPath in allAssets)
        {
            if (!usedAssets.Contains(assetPath))
            {
                Debug.LogWarning($"Unused asset: {assetPath}");
            }
        }
    }
}