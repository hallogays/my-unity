using NUnit.Framework;
using UnityEditor;
using System.IO;

public class AssetVersionManagerTests
{
    [Test]
    public void TestRecordAndRollbackAssetVersion()
    {
        // 创建一个临时资源
        string testAssetPath = "Assets/TestAsset.txt";
        File.WriteAllText(testAssetPath, "Initial content");
        AssetDatabase.ImportAsset(testAssetPath);

        // 记录版本
        AssetVersionManager.RecordAssetVersion();

        // 修改资源内容
        File.WriteAllText(testAssetPath, "Modified content");
        AssetDatabase.ImportAsset(testAssetPath);

        // 回滚版本
        AssetVersionManager.RollbackAssetVersion();

        // 检查内容是否回滚
        string content = File.ReadAllText(testAssetPath);
        Assert.AreEqual("Initial content", content);

        // 清理临时资源
        AssetDatabase.DeleteAsset(testAssetPath);
        AssetDatabase.Refresh();
    }
}