using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// 
/// </summary>
public class AsyncLoad : MonoBehaviour
{
    private void Start()
    {
        //同步加载
        //GameObject.Find("/Canvas/Panel/Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("DOAX");
        //异步加载 分不同帧加载
        //用协程
        //StartCoroutine(AsyncLoadRes());
        //异步加载AB包
        StartCoroutine(AsyncAB());


    }

    private IEnumerator AsyncAB()
    {
        //开启异步加载
        AssetBundleCreateRequest abr = AssetBundle.LoadFromFileAsync(Application.dataPath + "/StreamingAssets/" + "mat1");
        yield return abr;
        GameObject.Find("/Canvas/Panel/Image").GetComponent<Image>().sprite = abr.assetBundle.LoadAsset<Sprite>("DOAX");


    }

    private IEnumerator AsyncLoadRes()
    {
        //开启异步加载
        ResourceRequest rr = Resources.LoadAsync<Sprite>("DOAX");
        //协程会在加载资源成功后，继续执行（底层封装了线程加载资源）
        yield return rr;
        //将加载成功的资源显示出来
        GameObject.Find("/Canvas/Panel/Image").GetComponent<Image>().sprite = rr.asset as Sprite;
    }

}

