using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 工具类
/// </summary>
[Serializable]
public class ToolType
{
    /// <summary>
    /// 工具的名字
    /// </summary>
    public string ToolName { get; private set; }

    /// <summary>
    /// 工具对应的GameObject
    /// </summary>
    private GameObject ToolGameObject { get; set; }

    /// <summary>
    /// 工具的贴图
    /// </summary>
    private Sprite ToolImage { get; set; }

    /// <summary>
    /// 工具描述
    /// </summary>
    public string ToolDescription { get; set; }

    /// <summary>
    /// 道具的信息 ToolSpritePath 的构造函数
    /// </summary>
    /// <param name="ToolObj">工具的GameObject</param>
    public ToolType(GameObject toolObj)
    {
        //直接截取路径最后的文字作为鱼的名字,去掉后缀名
        ToolName = toolObj.name;
        //将生成的GameObject进行储存
        ToolGameObject = toolObj;
        //将图片获取并进行储存
        ToolImage = toolObj.GetComponent<SpriteRenderer>().sprite;
        //获取物品描述并且储存
        ToolDescription = toolObj.GetComponent<ItemDescription>().Desciption;
    }

    /// <summary>
    /// 获取ToolGameObject
    /// </summary>
    /// <returns></returns>
    public GameObject GetToolGameObject()
    {
        return ToolGameObject;
    }

    /// <summary>
    /// 获取ToolImage
    /// </summary>
    /// <returns></returns>
    public Sprite GetToolImage()
    {
        return ToolImage;
    }

}
