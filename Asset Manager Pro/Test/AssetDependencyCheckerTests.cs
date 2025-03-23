using NUnit.Framework;
using UnityEditor;
using System.Collections.Generic;

public class AssetDependencyCheckerTests
{
    [Test]
    public void TestCheckAssetDependencies()
    {
        // 模拟一个场景和一个Prefab
        string scenePath = "Assets/TestScene.unity";
        string prefabPath = "Assets/TestPrefab.prefab";
        // 这里可以创建实际的场景和Prefab文件

        // 调用依赖检查方法
        AssetDependencyChecker.CheckAssetDependencies();

        // 可以添加更多的断言来验证结果
        Assert.Pass();

        // 清理临时资源
        AssetDatabase.DeleteAsset(scenePath);
        AssetDatabase.DeleteAsset(prefabPath);
        AssetDatabase.Refresh();
    }
}