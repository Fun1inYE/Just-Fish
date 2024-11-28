using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Unity.VisualScripting;

/// <summary>
/// ͨ��Resource.LoadAll��ʼ��������Ϣ����FishingManager�ķ���
/// </summary>
public class InitType<T>
{
    /// <summary>
    /// item����Ϣ
    /// </summary>
    public T type { get; private set; }
    /// <summary>
    /// ָ���ļ���·��
    /// </summary>
    public string prefabsFolderPath;
    /// <summary>
    /// ������Ľӿ�
    /// </summary>
    public IItemFactory<T> factory;

    /// <summary>
    /// ���캯��
    /// </summary>
    public InitType(string prefabsFolderPath, IItemFactory<T> factory) 
    {
        this.prefabsFolderPath = prefabsFolderPath;
        this.factory = factory;
    }

    /// <summary>
    /// �����ļ���·��Ѱ�����prefabȻ���ʼ���ķ���
    /// </summary>
    public void InitTypes()
    {
        //��ȡ�ļ���
        GameObject[] Prefabs = Resources.LoadAll<GameObject>(prefabsFolderPath);

        // ���û���ҵ� prefab �ļ�����ӡ������Ϣ
        if (Prefabs.Length == 0)
        {
            Debug.LogError($"��·�� 'Resources/{prefabsFolderPath}' ��û���ҵ��κ� Prefab");
            return;
        }

        //����fishPrefabs
        foreach (GameObject prefab in Prefabs)
        {
            type = factory.CreateItem(prefab);

            ItemManager.Instance.AddOrGetType<T>(type, prefab);
        }
    }
}
