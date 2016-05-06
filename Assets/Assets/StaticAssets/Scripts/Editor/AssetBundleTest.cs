using UnityEngine;
using System.Collections;
using UnityEditor;

public class AssetBundleTest : MonoBehaviour 
{
    [MenuItem("Assets/Build AssetBundle")]
    static void ExportResource () 
    {
            string path = "Assets/myAssetBundle.unity3d";
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
        
    }
    
}
