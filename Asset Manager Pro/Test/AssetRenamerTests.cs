using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class AssetRenamerTests
{
    [Test]
    public void TestBatchRenameAssets()
    {
        // 创建一个临时资源
        GameObject testObject = new GameObject("TestObject");
        string assetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/TestObject.prefab");
        PrefabUtility.SaveAsPrefabAsset(testObject, assetPath);

        // 调用重命名方法
        string prefix = "Renamed_";
        string suffix = "_v1";
        int startNumber = 1;
        AssetRenamerAndOptimizer.BatchRenameAssets();

        // 检查资源是否被正确重命名
        string newName = $"{prefix}TestObject{suffix}{startNumber}";
        string newPath = Path.Combine(Path.GetDirectoryName(assetPath), newName + ".prefab");
        Assert.IsTrue(File.Exists(newPath));

        // 清理临时资源
        AssetDatabase.DeleteAsset(newPath);
        AssetDatabase.Refresh();
    }
}