using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Unity.VisualScripting;

/// <summary>
/// 通过Resource.LoadAll初始化鱼类信息进入FishingManager的方法
/// </summary>
public class InitType<T>
{
    /// <summary>
    /// item的信息
    /// </summary>
    public T type { get; private set; }
    /// <summary>
    /// 指定文件夹路径
    /// </summary>
    public string prefabsFolderPath;
    /// <summary>
    /// 工厂类的接口
    /// </summary>
    public IItemFactory<T> factory;

    /// <summary>
    /// 构造函数
    /// </summary>
    public InitType(string prefabsFolderPath, IItemFactory<T> factory) 
    {
        this.prefabsFolderPath = prefabsFolderPath;
        this.factory = factory;
    }

    /// <summary>
    /// 根据文件夹路径寻找鱼的prefab然后初始化的方法
    /// </summary>
    public void InitTypes()
    {
        //读取文件夹
        GameObject[] Prefabs = Resources.LoadAll<GameObject>(prefabsFolderPath);

        // 如果没有找到 prefab 文件，打印错误信息
        if (Prefabs.Length == 0)
        {
            Debug.LogError($"在路径 'Resources/{prefabsFolderPath}' 中没有找到任何 Prefab");
            return;
        }

        //遍历fishPrefabs
        foreach (GameObject prefab in Prefabs)
        {
            type = factory.CreateItem(prefab);

            ItemManager.Instance.AddOrGetType<T>(type, prefab);
        }
    }
}
