using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 存档管理器
/// </summary>
public class SaveManager : MonoBehaviour
{
    /// <summary>
    /// SaveManager单例
    /// </summary>
    public static SaveManager Instance;

    /// <summary>
    /// 引用总控制器，用于存储玩家数据
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        //单例初始化
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        //不要删除gameObject
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 存档方法
    /// </summary>
    /// <param name="data"></param>
    public void Save(string archiveName, string saveName, object data)
    {
        SaveAndLoad.SaveByJson(archiveName, saveName, data);
    }

    /// <summary>
    /// 读取方法
    /// </summary>
    public T Load<T>(string archiveName, string saveName)
    {
        var data = SaveAndLoad.LoadByJson<T>(archiveName, saveName);
        return data;
    }

    /// <summary>
    /// 删除存档方法
    /// </summary>
    /// <param name="archiveName"></param>
    public void Delete(string archiveName)
    {
        SaveAndLoad.DeleteByJson(archiveName);
    }

    /// <summary>
    /// 检查文件是否存在
    /// </summary>
    /// <param name="archiveName"></param>
    /// <param name="saveFileName"></param>
    /// <returns></returns>
    public bool CheckDataExist(string archiveName, string saveFileName)
    {
        bool exist = SaveAndLoad.CheckDataExist(archiveName, saveFileName);
        return exist;
    }

    /// <summary>
    /// 列出存档名字的方法
    /// </summary>
    public List<string> ListArchiveName()
    {
        //存档文件夹
        string archivePath = Path.Combine(Application.persistentDataPath, "SaveFiles");

        //如果文件夹不存在的话
        if(!Directory.Exists(archivePath))
        {
            Debug.LogWarning($"存档文件夹不存在");
            Debug.LogWarning($"{archivePath}不存在");
            return new List<string>();
        }

        // 获取所有存档文件夹（子文件夹）
        string[] directories = Directory.GetDirectories(archivePath);

        // 存储存档文件夹的名称
        List<string> saveFolderNames = new List<string>();

        // 获取每个文件夹的名称（文件夹名称即存档名称）
        foreach (var dir in directories)
        {
            // 获取文件夹名称（去掉路径）
            string folderName = Path.GetFileName(dir);
            saveFolderNames.Add(folderName);
        }

        // 返回存档文件夹名称列表
        return saveFolderNames;
    }

    /// <summary>
    /// 存储游戏内所有该存储的数据
    /// </summary>
    public void SaveAllData()
    {
        //获取到totaltotalController
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
        //判断totaltotalController是否已被初始化
        if (totalController != null)
        {
            //玩家数据存储
            totalController.playerDataAndUIController.SaveData();
            //背包数据存储
            InventoryManager.Instance.SaveData();
            //游戏基础数据存储
            GameManager.Instance.SaveData();
        }
    }

    /// <summary>
    /// 加载游戏内所有的数据
    /// </summary>
    public void LoadAllData()
    {
        //获取到totaltotalController
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
        //判断totaltotalController是否已被初始化
        if (totalController != null)
        {
            //玩家数据读取
            totalController.playerDataAndUIController.LoadData();
            //背包数据读取
            InventoryManager.Instance.LoadData();
            //游戏基础数据读取
            GameManager.Instance.LoadData();
        }
    }
}
