using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������
/// </summary>
[Serializable]
public class ToolType
{
    /// <summary>
    /// ���ߵ�����
    /// </summary>
    public string ToolName { get; private set; }

    /// <summary>
    /// ���߶�Ӧ��GameObject
    /// </summary>
    private GameObject ToolGameObject { get; set; }

    /// <summary>
    /// ���ߵ���ͼ
    /// </summary>
    private Sprite ToolImage { get; set; }

    /// <summary>
    /// ��������
    /// </summary>
    public string ToolDescription { get; set; }

    /// <summary>
    /// ���ߵ���Ϣ ToolSpritePath �Ĺ��캯��
    /// </summary>
    /// <param name="ToolObj">���ߵ�GameObject</param>
    public ToolType(GameObject toolObj)
    {
        //ֱ�ӽ�ȡ·������������Ϊ�������,ȥ����׺��
        ToolName = toolObj.name;
        //�����ɵ�GameObject���д���
        ToolGameObject = toolObj;
        //��ͼƬ��ȡ�����д���
        ToolImage = toolObj.GetComponent<SpriteRenderer>().sprite;
        //��ȡ��Ʒ�������Ҵ���
        ToolDescription = toolObj.GetComponent<ItemDescription>().Desciption;
    }

    /// <summary>
    /// ��ȡToolGameObject
    /// </summary>
    /// <returns></returns>
    public GameObject GetToolGameObject()
    {
        return ToolGameObject;
    }

    /// <summary>
    /// ��ȡToolImage
    /// </summary>
    /// <returns></returns>
    public Sprite GetToolImage()
    {
        return ToolImage;
    }

}
