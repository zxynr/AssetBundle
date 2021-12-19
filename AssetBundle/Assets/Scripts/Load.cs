using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 读取AssetBundle文件
/// </summary>
public class Load : MonoBehaviour
{
    /// <summary>/// 读取路径/// </summary>
    private string pathUrl;

    private void Start()
    {
        pathUrl =
            #if UNITY_ANDROID
            "jar:file://" + Application.dataPath + "!/assets/";
            #elif UNITY_IPHONE
            Application.dataPath + "/Raw/";
            #elif UNITY_STANDALONE_WIN || UNITY_EDITOR
                        "file://" + Application.dataPath + "/StreamingAssets/";
            #endif
    }

    private IEnumerator LoadAsset(string assetBundle)
    {
        string mUrl = pathUrl + assetBundle;
        WWW www = new WWW(mUrl);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
            Debug.Log(www.error);
        else
        {
            UnityEngine.Object obj = www.assetBundle.LoadAsset("Cylinder");
            yield return Instantiate(obj);
            //assetbundle是否要卸载
            www.assetBundle.Unload(false);
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("LoadAsset"))
        {
            StartCoroutine(LoadAsset("cy"));
        }
        if (GUILayout.Button("Load"))
        {
            StartCoroutine(LoadAssetBundle("StreamingAssets", "cube","Cube"));
        }
    }


    private IEnumerator LoadAssetBundle(string path,string assetbundle,string res)
    {
        //获取assetbundle(path)
        string mUrl = pathUrl + path;
        WWW www = new WWW(mUrl);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
            Debug.Log(www.error);
        else
        {
            //获取ab对应的main文件
            AssetBundle mab = www.assetBundle;
            AssetBundleManifest manifest = (AssetBundleManifest)mab.LoadAsset("AssetBundleManifest");
            mab.Unload(false);
            //查找我们需要的ab文件的依赖文件
            string[] dps = manifest.GetAllDependencies(assetbundle);
            AssetBundle[] abs = new AssetBundle[dps.Length];
            for (int i = 0; i < dps.Length; i++)
            {
                string dUrl = pathUrl + dps[i];
                WWW dwww = new WWW(dUrl);
                yield return dwww;
                abs[i] = dwww.assetBundle;
            }
            //加载我们需要的ab文件（第二个参数）
            WWW cube = new WWW(pathUrl + res);
            yield return cube;
            if (!string.IsNullOrEmpty(cube.error))
                Debug.Log(cube.error);
            else
            {
                AssetBundle ab = cube.assetBundle;
                UnityEngine.Object obj = ab.LoadAsset(res);
                //实例化资源res
                Instantiate(obj);
                ab.Unload(false);
            }
            foreach (var item in abs)
            {
                item.Unload(false);
            }
        }
    }
}
