using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;  // 引入 LitJson

/// <summary>
/// 存储和读取类
/// </summary>
public static class SaveAndLoad
{
    /// <summary>
    /// 存档方法
    /// </summary>
    /// <param name="archiveName">玩家存档名字</param>
    /// <param name="saveFileName">储存数据文件名字</param>
    /// <param name="data">要存储的数据名字</param>
    public static void SaveByJson(string archiveName, string saveFileName, object data)
    {
        // 创建玩家存档的路径
        string playerFolderPath = Path.Combine(Application.persistentDataPath, "SaveFiles", archiveName);

        // 如果不存在该文件夹，就创建一个这个文件夹
        if (!Directory.Exists(playerFolderPath))
        {
            Directory.CreateDirectory(playerFolderPath);
        }

        // 使用 LitJson 序列化数据
        var json = JsonMapper.ToJson(data);

        // 将 json 文件存储到对应路径：玩家存档路径 + 存储名字
        var path = Path.Combine(playerFolderPath, saveFileName);

        try
        {
            File.WriteAllText(path, json);
            Debug.Log("文件保存成功！路径为：" + path);
        }
        catch (System.Exception)
        {
            Debug.Log("文件保存失败");
        }
    }

    /// <summary>
    /// 读档方法
    /// </summary>
    /// <typeparam name="T">要读取的文件类型</typeparam>
    /// <param name="archiveName">玩家存档名字</param>
    /// <param name="saveFileName">储存数据文件名字</param>
    /// <returns></returns>
    public static T LoadByJson<T>(string archiveName, string saveFileName)
    {
        // 创建玩家存档的路径
        string playerFolderPath = Path.Combine(Application.persistentDataPath, "SaveFiles", archiveName);
        // 如果不存在这个文件夹
        if (!Directory.Exists(playerFolderPath))
        {
            Debug.LogWarning($"不存在{playerFolderPath}文件夹！请检查代码或者文件管理器！");
            return default;
        }

        string path = Path.Combine(playerFolderPath, saveFileName).Replace("\\", "/");  // 修正路径合成方式

        // 输出最终路径，以便调试
        Debug.Log("最终路径：" + path);

        if(File.Exists(path))
        {
            var json = File.ReadAllText(path);
            // 使用 LitJson 反序列化
            var data = JsonMapper.ToObject<T>(json);
            Debug.Log("读取文件成功");
            return data;
        }
        else
        {
            Debug.Log("没有在 " + path + " 下找到 " + saveFileName);
            return default;
        }
    }

    /// <summary>
    /// 删除方法
    /// </summary>
    /// <param name="archiveName">存档名字</param>
    public static void DeleteByJson(string archiveName)
    {
        // 创建玩家存档的路径
        string playerFolderPath = Path.Combine(Application.persistentDataPath, "SaveFiles", archiveName);

        // 判断路径是否存在
        if (Directory.Exists(playerFolderPath))
        {
            Directory.Delete(playerFolderPath, true);  // 删除整个目录及其中的文件
            Debug.Log($"存档{archiveName}删除成功");
        }
        else
        {
            Debug.LogWarning($"没有找到{archiveName}存档");
            Debug.LogWarning($"没有找到{playerFolderPath}路径");
        }
    }

    /// <summary>
    /// 检测文件是否存在的方法
    /// </summary>
    /// <param name="saveFileName"></param>
    /// <returns></returns>
    public static bool CheckDataExist(string archiveName, string saveFileName)
    {
        // 创建玩家存档的路径
        string playerFolderPath = Path.Combine(Application.persistentDataPath, "SaveFiles", archiveName);
        // 存档文件路径
        var path = Path.Combine(playerFolderPath, saveFileName);  // 修正路径合成方式
        return File.Exists(path);
    }
}