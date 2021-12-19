using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// AssetBundles工具类 菜单
/// </summary>
public class CreateAssetBundles
{
    [MenuItem("Build/Build AssetBundle")]
    //点击菜单时调用方法
    static void BuildAssetBundle()
    {
#if UNITY_ANDROID
        BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath,
            BuildAssetBundleOptions.UncompressedAssetBundle,
            BuildTarget.Android);
#elif UNITY_IPHONE
        BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath,
            BuildAssetBundleOptions.UncompressedAssetBundle,
            BuildTarget.iOS);
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath,
            BuildAssetBundleOptions.UncompressedAssetBundle,
            BuildTarget.StandaloneWindows);
#endif
    }
}
