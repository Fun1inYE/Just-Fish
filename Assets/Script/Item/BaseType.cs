using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϣ��ĸ���
/// </summary>
[Serializable]
public class BaseType
{
    /// <summary>
    /// ��Ϣ������
    /// </summary>
    public string name { get; private set; }
    /// <summary>
    /// ��Ϣ��Ӧ��GameObject
    /// </summary>
    public GameObject obj;
    /// <summary>
    /// �����Ʒ�ǲ���������Ʒ��Ĭ��false��
    /// </summary>
    public bool isSpecial = false;
    /// <summary>
    /// GameObject��Ӧ��sprite
    /// </summary>
    public Sprite sprite { get; private set; }
    /// <summary>
    /// ��Ʒ������
    /// </summary>
    public string description { get; private set; }

    /// <summary>
    /// ���캯��
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
    /// �ṩ���޲κ���
    /// </summary>
    public BaseType()
    {
        name = "defaultName";
    }

    /// <summary>
    /// ��ȡ����Ӧ��sprite
    /// </summary>
    /// <returns></returns>
    public Sprite GetImage()
    {
        return sprite;
    }
}

/// <summary>
/// �����Ϣ
/// </summary>
[Serializable]
public class FishType : BaseType
{
    /// <summary>
    /// ����Ϣ�Ĺ��캯��
    /// </summary>
    /// <param name="fishObj">���GameObject</param>
    public FishType(GameObject fishObj) : base(fishObj) { }
}

/// <summary>
/// ������
/// </summary>
[Serializable]
public class ToolType : BaseType
{
    /// <summary>
    /// ���ߵ���Ϣ ToolSpritePath �Ĺ��캯��
    /// </summary>
    /// <param name="ToolObj">���ߵ�GameObject</param>
    public ToolType(GameObject toolObj) : base(toolObj) { }
}


/// <summary>
/// ������
/// </summary>
[Serializable]
public class PropType : BaseType
{
    /// <summary>
    /// ���ߵ���Ϣ PropSpritePath �Ĺ��캯��
    /// </summary>
    /// <param name="PropObj">���ߵ�GameObject</param>
    public PropType(GameObject propObj) : base (propObj) { }
}

/// <summary>
/// �����Ϣ
/// </summary>
[Serializable]
public class BaitType : BaseType
{
    /// <summary>
    /// ���캯��
    /// </summary>
    /// <param name="obj"></param>
    public BaitType(GameObject obj) : base(obj) { }
}