using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class IconCapturer : MonoBehaviour
{
    public static string IconsPath = $"Assets{Path.DirectorySeparatorChar}Resources{Path.DirectorySeparatorChar}GameIcons{Path.DirectorySeparatorChar}";
    private const string IconCaptureScenePath = "Assets/Scenes/IconCaptureScene.unity";
    private const string DefaultScenePath = "Assets/Scenes/GameScene.unity";
    private const string RenderTextureName = "IconCaptureTexture"; // A render texture in assets is needed


    public static Sprite GetSprite(string iconID)
    {
        var sprite = Resources.Load<Sprite>($"GameIcons{Path.DirectorySeparatorChar}{iconID}");
        return sprite;
    }
#if UNITY_EDITOR

    [MenuItem("Holy Actions/Render Icons")]
    public static void RenderIcons()
    {

        UnityEditor.SceneManagement.EditorSceneManager.OpenScene(IconCaptureScenePath);

        var items = FindAllItemsInAssets<Item>();
        foreach (var item in items)
        {
            var model = Instantiate(item.Model, item.CaptureOffset, Quaternion.Euler(item.CaptureRotation));
            model.transform.localScale = Vector3.one* item.CaptureScale;

            CaptureAndSave(item.ID);
            DestroyImmediate(model);
        }

        UnityEditor.SceneManagement.EditorSceneManager.OpenScene(DefaultScenePath);

        Debug.Log("Icons captured");

    }
#endif
    public static void CaptureAndSave(string fileName)
    {
        Camera captureCamera = FindAnyObjectByType<Camera>();
        RenderTexture renderTexture = (RenderTexture)Resources.Load(RenderTextureName);
        captureCamera.targetTexture = renderTexture;

        captureCamera.Render();

        RenderTexture.active = renderTexture;
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();
        string path = $"{IconsPath}{fileName}.png";
        File.WriteAllBytes(path, bytes);


        RenderTexture.active = null;
        captureCamera.targetTexture = null;

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();

        TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
        if (textureImporter != null)
        {
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.spritePixelsPerUnit = 100;
            textureImporter.isReadable= true;
            textureImporter.spriteImportMode = SpriteImportMode.Single;
            textureImporter.SaveAndReimport();
        }
#endif
    }

    public static List<T>FindAllItemsInAssets<T>() where T : UnityEngine.Object
        {
            List<T> foundItems = new List<T>();
#if UNITY_EDITOR
            string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();

            foreach (string assetPath in allAssetPaths)
            {
                UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object));

                if (asset is T)
                {
                    foundItems.Add(asset as T);
                }
            }
#endif
            return foundItems;
        }
}
