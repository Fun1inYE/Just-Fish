using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 信息类的父类
/// </summary>
[Serializable]
public class BaseType
{
    /// <summary>
    /// 信息的名字
    /// </summary>
    public string name { get; private set; }
    /// <summary>
    /// 信息对应的GameObject
    /// </summary>
    public GameObject obj;
    /// <summary>
    /// 这个物品是不是特殊物品（默认false）
    /// </summary>
    public bool isSpecial = false;
    /// <summary>
    /// GameObject对应的sprite
    /// </summary>
    public Sprite sprite { get; private set; }
    /// <summary>
    /// 物品的描述
    /// </summary>
    public string description { get; private set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="obj"></param>
    public BaseType(GameObject obj)
    {
        this.obj = obj;
        name = obj.name;
        sprite = obj.GetComponent<SpriteRenderer>().sprite;
        description = obj.GetComponent<ItemDescription>().desciption;
    }

    /// <summary>
    /// 提供的无参函数
    /// </summary>
    public BaseType()
    {
        name = "defaultName";
    }

    /// <summary>
    /// 获取到对应的sprite
    /// </summary>
    /// <returns></returns>
    public Sprite GetImage()
    {
        return sprite;
    }
}

/// <summary>
/// 鱼的信息
/// </summary>
[Serializable]
public class FishType : BaseType
{
    /// <summary>
    /// 鱼信息的构造函数
    /// </summary>
    /// <param name="fishObj">鱼的GameObject</param>
    public FishType(GameObject fishObj) : base(fishObj) { }
}

/// <summary>
/// 工具类
/// </summary>
[Serializable]
public class ToolType : BaseType
{
    /// <summary>
    /// 道具的信息 ToolSpritePath 的构造函数
    /// </summary>
    /// <param name="ToolObj">工具的GameObject</param>
    public ToolType(GameObject toolObj) : base(toolObj) { }
}


/// <summary>
/// 道具类
/// </summary>
[Serializable]
public class PropType : BaseType
{
    /// <summary>
    /// 道具的信息 PropSpritePath 的构造函数
    /// </summary>
    /// <param name="PropObj">道具的GameObject</param>
    public PropType(GameObject propObj) : base (propObj) { }
}

/// <summary>
/// 鱼饵信息
/// </summary>
[Serializable]
public class BaitType : BaseType
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="obj"></param>
    public BaitType(GameObject obj) : base(obj) { }
}