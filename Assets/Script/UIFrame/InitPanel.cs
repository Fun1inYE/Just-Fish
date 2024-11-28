using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// ��ʼ��Panel�ķ���
/// </summary>
public class InitPanel
{
    /// <summary>
    /// ·���ļ���
    /// </summary>
    public string pathFolder;

    /// <summary>
    /// ���캯��
    /// </summary>
    /// <param name="pathFolder"></param>
    public InitPanel(string pathFolder)
    {
        this.pathFolder = pathFolder;
    }


    /// <summary>
    /// �����ļ���·��Ѱ�����prefabȻ���ʼ���ķ���
    /// </summary>
    public void InitTypes(int poolSize)
    {
        //��ȡ�ļ���
        GameObject[] panelPrefabs = Resources.LoadAll<GameObject>(pathFolder);

        // ���û���ҵ� prefab �ļ�����ӡ������Ϣ
        if (panelPrefabs.Length == 0)
        {
            Debug.LogError($"��·�� 'Resources/{panelPrefabs}' ��û���ҵ��κ� Prefab");
            return;
        }

        

        for(int i = 0; i < panelPrefabs.Length; i++)
        {
            //ֱ�ӽ���Ϣ����DisplayUIManager��������DisplayUIManagerֱ�ӳ�ʼ��
            UIManager.Instance.AddInfoAndPoolUI(panelPrefabs[i], poolSize);
        }
    }
}
