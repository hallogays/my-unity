using UnityEditor;
using System.IO;

public class AssetRenamerAndOptimizer
{
    [MenuItem("Assets/Asset Manager Pro/Batch Rename Assets")]
    public static void BatchRenameAssets()
    {
        string prefix = EditorUtility.InputDialog("Add Prefix", "Enter prefix", "");
        string suffix = EditorUtility.InputDialog("Add Suffix", "Enter suffix", "");
        int startNumber = int.Parse(EditorUtility.InputDialog("Start Number", "Enter start number", "1"));

        Object[] selectedAssets = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);
        foreach (Object asset in selectedAssets)
        {
            string assetPath = AssetDatabase.GetAssetPath(asset);
            string newName = $"{prefix}{Path.GetFileNameWithoutExtension(assetPath)}{suffix}{startNumber++}";
            string newPath = Path.Combine(Path.GetDirectoryName(assetPath), newName + Path.GetExtension(assetPath));
            AssetDatabase.RenameAsset(assetPath, newName);
        }
        AssetDatabase.Refresh();
    }

    [MenuItem("Assets/Asset Manager Pro/Optimize Assets")]
    public static void OptimizeAssets()
    {
        // 集成第三方库（如TinyPNG API）实现资源优化
        // 这里只是示例，需要根据具体库的文档进行实现
        Object[] selectedAssets = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);
        foreach (Object asset in selectedAssets)
        {
            string assetPath = AssetDatabase.GetAssetPath(asset);
            // 调用TinyPNG API进行图片压缩
            // 示例代码：
            // TinyPNG.Compress(assetPath);
        }
        AssetDatabase.Refresh();
    }
}