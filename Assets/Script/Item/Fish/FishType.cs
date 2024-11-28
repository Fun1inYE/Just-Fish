using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using LitJson;

/// <summary>
/// �����Ϣ
/// </summary>
public class FishType
{
    /// <summary>
    /// �������
    /// </summary>
    public string FishName;
    /// <summary>
    /// ��Ķ�Ӧ��GameObject
    /// </summary>
    private GameObject FishGameObject { get; set; }

    /// <summary>
    /// �����ͼ
    /// </summary>
    private Sprite FishImage { get; set; }
   

    /// <summary>
    /// ����������ʱ��
    /// </summary>
    public string fishedTime;

    /// <summary>
    /// �����Ϣ FishSpritePath �Ĺ��캯��
    /// </summary>
    /// <param name="fishObj">���GameObject</param>
    public FishType(GameObject fishObj)
    {
        //ֱ�ӽ�ȡ·������������Ϊ�������,ȥ����׺��
        FishName = fishObj.name;
        //�����ɵ�GameObject���д���
        FishGameObject = fishObj;
        //��ͼƬ��ȡ�������д���
        FishImage = fishObj.GetComponent<SpriteRenderer>().sprite;

        // ��ȡ��ǰϵͳʱ��
        DateTime currentTime = DateTime.Now;
        // ��ϵͳʱ���ʽ��Ϊ�ַ���
        fishedTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    /// ���������ͼ
    /// </summary>
    /// <returns></returns>
    public Sprite GetFishImage()
    {
        return FishImage;
    }
}
