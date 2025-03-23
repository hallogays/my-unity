using NUnit.Framework;
using UnityEditor;
using System.IO;

public class AssetImporterTests
{
    [Test]
    public void TestBatchImportAssets()
    {
        // 模拟一个临时文件夹
        string tempFolder = "TempTestFolder";
        Directory.CreateDirectory(tempFolder);

        // 创建一个临时图片文件
        string testImagePath = Path.Combine(tempFolder, "test_image.png");
        File.WriteAllBytes(testImagePath, new byte[] { 0x01, 0x02, 0x03 });

        // 调用批量导入方法
        AssetImporter.BatchImportAssets();

        // 检查资源是否被正确导入到目标文件夹
        string targetPath = "Assets/Textures/test_image.png";
        Assert.IsTrue(File.Exists(targetPath));

        // 清理临时文件和文件夹
        Directory.Delete(tempFolder, true);
        File.Delete(targetPath);
        AssetDatabase.Refresh();
    }
}