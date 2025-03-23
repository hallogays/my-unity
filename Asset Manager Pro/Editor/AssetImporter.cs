using UnityEditor;
using System.IO;

public class AssetImporter
{
    [MenuItem("Assets/Asset Manager Pro/Batch Import Assets")]
    public static void BatchImportAssets()
    {
        string folderPath = EditorUtility.OpenFolderPanel("Select Folder", "", "");
        if (!string.IsNullOrEmpty(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                string extension = Path.GetExtension(file).ToLower();
                string targetFolder = "";
                if (extension == ".png" || extension == ".jpg" || extension == ".jpeg")
                {
                    targetFolder = "Assets/Textures";
                }
                else if (extension == ".mp3" || extension == ".wav")
                {
                    targetFolder = "Assets/Audio";
                }
                else if (extension == ".fbx" || extension == ".obj")
                {
                    targetFolder = "Assets/Models";
                }

                if (!string.IsNullOrEmpty(targetFolder))
                {
                    if (!Directory.Exists(targetFolder))
                    {
                        Directory.CreateDirectory(targetFolder);
                    }
                    string targetPath = Path.Combine(targetFolder, Path.GetFileName(file));
                    File.Copy(file, targetPath, true);
                    AssetDatabase.ImportAsset(targetPath);
                }
            }
            AssetDatabase.Refresh();
        }
    }
}