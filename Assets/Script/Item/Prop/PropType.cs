using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������
/// </summary>
[Serializable]
public class PropType
{
    /// <summary>
    /// ���ߵ�����
    /// </summary>
    public string PropName { get; private set; }

    /// <summary>
    /// ���߶�Ӧ��GameObject
    /// </summary>
    private GameObject PropGameObject { get; set; }

    /// <summary>
    /// ���ߵ���ͼ
    /// </summary>
    private Sprite PropImage { get; set; }

    /// <summary>
    /// ��������
    /// </summary>
    public string PropDescription { get; set; }

    /// <summary>
    /// ���ߵ���Ϣ PropSpritePath �Ĺ��캯��
    /// </summary>
    /// <param name="PropObj">���ߵ�GameObject</param>
    public PropType(GameObject propObj)
    {
        //ֱ�ӽ�ȡ·������������Ϊ�������,ȥ����׺��
        PropName = propObj.name;
        //�����ɵ�GameObject���д���
        PropGameObject = propObj;
        //��ͼƬ��ȡ���ҽ��д���
        PropImage = propObj.GetComponent<SpriteRenderer>().sprite;
        //���������������������б���
        PropDescription = propObj.GetComponent<ItemDescription>().Desciption;
    }

    /// <summary>
    /// ��ȡPropGameObject
    /// </summary>
    /// <returns></returns>
    public GameObject GetPropGameObject()
    {
        return PropGameObject;
    }

    /// <summary>
    /// ��ȡPropImage
    /// </summary>
    /// <returns></returns>
    public Sprite GetPropImage()
    {
        return PropImage;
    }
}
