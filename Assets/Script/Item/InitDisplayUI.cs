using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// ��ʼ����ʾUI����
/// </summary>
public class InitDisplayUI
{
    /// <summary>
    /// ͼƬ����UIָ���ļ���·��
    /// </summary>
    public string imageUIFolderPath;
    /// <summary>
    /// Text����UIָ���ļ���·��
    /// </summary>
    public string textUIFolderPath;

    /// <summary>
    /// ������������
    /// </summary>
    public DisplayTextDataConfig textConfig;

    /// <summary>
    /// InitUI�Ĺ��캯��
    /// </summary>
    /// <param name="prefabsFolderPath"></param>
    public InitDisplayUI(string imageUIFolderPath, string textUIFolderPath)
    {
        this.imageUIFolderPath = imageUIFolderPath;
        this.textUIFolderPath = textUIFolderPath;

        //��ʼ��Text����
        textConfig = new DisplayTextDataConfig();
    }

    /// <summary>
    /// �����ļ���·��Ѱ�����prefabȻ���ʼ���ķ���
    /// </summary>
    public void InitTypes()
    {
        //��ȡ�ļ���
        GameObject[] imageUIPrefabs = Resources.LoadAll<GameObject>(imageUIFolderPath);
        GameObject[] textUIPrefabs = Resources.LoadAll<GameObject>(textUIFolderPath);

        // ��ȡ DisplayTextDataConfig ��� Type ����
        Type type = textConfig.GetType();
        PropertyInfo[] propertyInfos = type.GetProperties();

        // ���û���ҵ� prefab �ļ�����ӡ������Ϣ
        if (imageUIPrefabs.Length == 0)
        {
            Debug.LogError($"��·�� 'Resources/{imageUIFolderPath}' ��û���ҵ��κ� Prefab");
            return;
        }

        //����imageUIPrefabs
        foreach (GameObject prefab in imageUIPrefabs)
        {
            //ֱ�ӽ���Ϣ����DisplayUIManager��������DisplayUIManagerֱ�ӳ�ʼ��
            DisplayUIManager.Instance.AddorGetDisplayUI(prefab.name.Replace("(Clone)", "").Trim(), prefab);
        }

        //����textUIPrefabs
        foreach (GameObject prefab in textUIPrefabs)
        {
            //�����е�TextUI�ػ���Ȼ�����ר�õ�Canvas����ؽ��д洢
            PoolManager.Instance.CreatGameObjectPool(prefab, 20, "ScreenModCanvasPool");
        }

        foreach (PropertyInfo info in propertyInfos)
        {
            //��textConfig¼�뵽textConfig
            DisplayUIManager.Instance.AddTextInTextDic(info.Name, (string)info.GetValue(textConfig));
        }
    }
}
