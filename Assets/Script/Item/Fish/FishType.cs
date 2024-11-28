using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using LitJson;

/// <summary>
/// 鱼的信息
/// </summary>
public class FishType
{
    /// <summary>
    /// 鱼的名字
    /// </summary>
    public string FishName;
    /// <summary>
    /// 鱼的对应的GameObject
    /// </summary>
    private GameObject FishGameObject { get; set; }

    /// <summary>
    /// 鱼的贴图
    /// </summary>
    private Sprite FishImage { get; set; }
   

    /// <summary>
    /// 被钓起来的时间
    /// </summary>
    public string fishedTime;

    /// <summary>
    /// 鱼的信息 FishSpritePath 的构造函数
    /// </summary>
    /// <param name="fishObj">鱼的GameObject</param>
    public FishType(GameObject fishObj)
    {
        //直接截取路径最后的文字作为鱼的名字,去掉后缀名
        FishName = fishObj.name;
        //将生成的GameObject进行储存
        FishGameObject = fishObj;
        //将图片获取到并进行储存
        FishImage = fishObj.GetComponent<SpriteRenderer>().sprite;

        // 获取当前系统时间
        DateTime currentTime = DateTime.Now;
        // 将系统时间格式化为字符串
        fishedTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    /// 返回鱼的贴图
    /// </summary>
    /// <returns></returns>
    public Sprite GetFishImage()
    {
        return FishImage;
    }
}
