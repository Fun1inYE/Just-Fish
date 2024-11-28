using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 初始化Panel的方法
/// </summary>
public class InitPanel
{
    /// <summary>
    /// 路径文件夹
    /// </summary>
    public string pathFolder;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="pathFolder"></param>
    public InitPanel(string pathFolder)
    {
        this.pathFolder = pathFolder;
    }


    /// <summary>
    /// 根据文件夹路径寻找鱼的prefab然后初始化的方法
    /// </summary>
    public void InitTypes(int poolSize)
    {
        //读取文件夹
        GameObject[] panelPrefabs = Resources.LoadAll<GameObject>(pathFolder);

        // 如果没有找到 prefab 文件，打印错误信息
        if (panelPrefabs.Length == 0)
        {
            Debug.LogError($"在路径 'Resources/{panelPrefabs}' 中没有找到任何 Prefab");
            return;
        }

        

        for(int i = 0; i < panelPrefabs.Length; i++)
        {
            //直接将信息传入DisplayUIManager并且能在DisplayUIManager直接初始化
            UIManager.Instance.AddInfoAndPoolUI(panelPrefabs[i], poolSize);
        }
    }
}
