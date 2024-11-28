using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 初始化显示UI的类
/// </summary>
public class InitDisplayUI
{
    /// <summary>
    /// 图片类型UI指定文件夹路径
    /// </summary>
    public string imageUIFolderPath;
    /// <summary>
    /// Text类型UI指定文件夹路径
    /// </summary>
    public string textUIFolderPath;

    /// <summary>
    /// 引用文字数据
    /// </summary>
    public DisplayTextDataConfig textConfig;

    /// <summary>
    /// InitUI的构造函数
    /// </summary>
    /// <param name="prefabsFolderPath"></param>
    public InitDisplayUI(string imageUIFolderPath, string textUIFolderPath)
    {
        this.imageUIFolderPath = imageUIFolderPath;
        this.textUIFolderPath = textUIFolderPath;

        //初始化Text数据
        textConfig = new DisplayTextDataConfig();
    }

    /// <summary>
    /// 根据文件夹路径寻找鱼的prefab然后初始化的方法
    /// </summary>
    public void InitTypes()
    {
        //读取文件夹
        GameObject[] imageUIPrefabs = Resources.LoadAll<GameObject>(imageUIFolderPath);
        GameObject[] textUIPrefabs = Resources.LoadAll<GameObject>(textUIFolderPath);

        // 获取 DisplayTextDataConfig 类的 Type 对象
        Type type = textConfig.GetType();
        PropertyInfo[] propertyInfos = type.GetProperties();

        // 如果没有找到 prefab 文件，打印错误信息
        if (imageUIPrefabs.Length == 0)
        {
            Debug.LogError($"在路径 'Resources/{imageUIFolderPath}' 中没有找到任何 Prefab");
            return;
        }

        //遍历imageUIPrefabs
        foreach (GameObject prefab in imageUIPrefabs)
        {
            //直接将信息传入DisplayUIManager并且能在DisplayUIManager直接初始化
            DisplayUIManager.Instance.AddorGetDisplayUI(prefab.name.Replace("(Clone)", "").Trim(), prefab);
        }

        //遍历textUIPrefabs
        foreach (GameObject prefab in textUIPrefabs)
        {
            //将其中的TextUI池化，然后放入专用的Canvas对象池进行存储
            PoolManager.Instance.CreatGameObjectPool(prefab, 20, "ScreenModCanvasPool");
        }

        foreach (PropertyInfo info in propertyInfos)
        {
            //将textConfig录入到textConfig
            DisplayUIManager.Instance.AddTextInTextDic(info.Name, (string)info.GetValue(textConfig));
        }
    }
}
