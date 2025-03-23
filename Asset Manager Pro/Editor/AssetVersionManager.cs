using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class AssetVersionManager
{
    private static Dictionary<string, List<string>> assetHistory = new Dictionary<string, List<string>>();

    [MenuItem("Assets/Asset Manager Pro/Record Asset Version")]
    public static void RecordAssetVersion()
    {
        Object[] selectedAssets = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);
        foreach (Object asset in selectedAssets)
        {
            string assetPath = AssetDatabase.GetAssetPath(asset);
            string assetContent = File.ReadAllText(assetPath);
            if (!assetHistory.ContainsKey(assetPath))
            {
                assetHistory[assetPath] = new List<string>();
            }
            assetHistory[assetPath].Add(assetContent);
        }
    }

    [MenuItem("Assets/Asset Manager Pro/Rollback Asset Version")]
    public static void RollbackAssetVersion()
    {
        Object[] selectedAssets = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);
        foreach (Object asset in selectedAssets)
        {
            string assetPath = AssetDatabase.GetAssetPath(asset);
            if (assetHistory.ContainsKey(assetPath) && assetHistory[assetPath].Count > 0)
            {
                string previousVersion = assetHistory[assetPath][assetHistory[assetPath].Count - 1];
                File.WriteAllText(assetPath, previousVersion);
                assetHistory[assetPath].RemoveAt(assetHistory[assetPath].Count - 1);
                AssetDatabase.ImportAsset(assetPath);
            }
        }
        AssetDatabase.Refresh();
    }
}