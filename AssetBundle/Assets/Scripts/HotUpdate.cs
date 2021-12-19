using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class HotUpdate : MonoBehaviour
{
    public Image Icon;
    private void Start()
    {
        AssetBundle ab = AssetBundle.LoadFromFile(Application.dataPath + "/StreamingAssets/mat1");
        Sprite sp = ab.LoadAsset<Sprite>("DOAX");
        Icon.sprite = sp;
        ab.Unload(false);
    }

    public void ChangeABRes()
    {
        AssetBundle ab = AssetBundle.LoadFromFile(Application.dataPath + "/StreamingAssets/mat2");
        Sprite sp = ab.LoadAsset<Sprite>("DOAX1");
        Icon.sprite = sp;
        ab.Unload(false);
    }
}

