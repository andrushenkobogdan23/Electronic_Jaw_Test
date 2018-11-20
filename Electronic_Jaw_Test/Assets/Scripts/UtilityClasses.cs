using System.IO;
using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UtillityMethods
{
    private static Dictionary<string, GameObject> dictAssetBundleRefs = new Dictionary<string, GameObject>();
    
    public static GameObject GetRandomAB()
    {
        var jsonPath = Application.dataPath + "/Resources/JsonFile/JsonData.json";

        var json = File.ReadAllText(jsonPath);
        var gd = JsonUtility.FromJson<GeometryObjectData>(json);

        var idnex = Random.Range(0, gd.abNames.Count);
        GameObject prefab;

        if (!dictAssetBundleRefs.ContainsKey(gd.abNames[idnex]))
        {
            var myLoadedAssetBundle = AssetBundle.LoadFromFile(gd.abNames[idnex]);

            if (myLoadedAssetBundle == null)
            {
                Debug.LogFormat("Failed to load AssetBundle with name {0}", gd.abNames[idnex]);
                return null;
            }

            prefab = myLoadedAssetBundle.LoadAsset<GameObject>(myLoadedAssetBundle.GetAllAssetNames()[0]);
            dictAssetBundleRefs.Add(gd.abNames[idnex], prefab);

        }
        else
        {
            prefab = dictAssetBundleRefs[gd.abNames[idnex]];
        }

        return prefab;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = "Assets/AssetBundles";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows); 
    }
#endif
}

public struct GeometryObjectData
{
    public List<string> abNames;
}

#if UNITY_EDITOR
public class JsonGenerator : MonoBehaviour
{        
    static GeometryObjectData data;

    static string jsonName = "/Resources/JsonFile/JsonData.json";

    static string jsonObjList;


    [MenuItem("Assets/Create Json")]
    static void GenerateJson()
    {
        data.abNames = new List<string>();

        data.abNames.Add("Assets/AssetBundles/sphere");
        data.abNames.Add("Assets/AssetBundles/cube");
        data.abNames.Add("Assets/AssetBundles/capsule");

        var sjsonObjList = JsonUtility.ToJson(data);

        File.WriteAllText(Application.dataPath + jsonName, sjsonObjList);
        Debug.LogFormat("Sucsefully recorded {0} asset bundles records to {1}", data.abNames.Count, jsonName);
    }
}
#endif
