using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AllItems : MonoBehaviour
{
    public Dictionary<string, GameObject> allItems = new();
    void Start()
    {
        var prefabs = AssetDatabase.LoadAllAssetsAtPath("Assets/Prefabs/Items");
        foreach (var prefab in prefabs)
        {
            allItems.Add(prefab.name, prefab as GameObject);
        }
    }
}