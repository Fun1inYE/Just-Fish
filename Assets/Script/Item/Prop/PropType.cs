using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 道具类
/// </summary>
[Serializable]
public class PropType
{
    /// <summary>
    /// 道具的名字
    /// </summary>
    public string PropName { get; private set; }

    /// <summary>
    /// 道具对应的GameObject
    /// </summary>
    private GameObject PropGameObject { get; set; }

    /// <summary>
    /// 道具的贴图
    /// </summary>
    private Sprite PropImage { get; set; }

    /// <summary>
    /// 道具描述
    /// </summary>
    public string PropDescription { get; set; }

    /// <summary>
    /// 道具的信息 PropSpritePath 的构造函数
    /// </summary>
    /// <param name="PropObj">道具的GameObject</param>
    public PropType(GameObject propObj)
    {
        //直接截取路径最后的文字作为鱼的名字,去掉后缀名
        PropName = propObj.name;
        //将生成的GameObject进行储存
        PropGameObject = propObj;
        //将图片获取并且进行储存
        PropImage = propObj.GetComponent<SpriteRenderer>().sprite;
        //将道具描述储存起来进行保存
        PropDescription = propObj.GetComponent<ItemDescription>().Desciption;
    }

    /// <summary>
    /// 获取PropGameObject
    /// </summary>
    /// <returns></returns>
    public GameObject GetPropGameObject()
    {
        return PropGameObject;
    }

    /// <summary>
    /// 获取PropImage
    /// </summary>
    /// <returns></returns>
    public Sprite GetPropImage()
    {
        return PropImage;
    }
}
