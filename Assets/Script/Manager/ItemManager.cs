using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// ��Ʒ��Ϣ������
/// </summary>
public class ItemManager : MonoBehaviour
{
    /// <summary>
    /// FishManager�ĵ���
    /// </summary>
    public static ItemManager Instance;
    /// <summary>
    /// ����һ���ֵ������
    /// </summary>
    public Dictionary<System.Type, IDictionary> itemDictionaries;

    /// <summary>
    /// ����һ�����ֶ�ӦGameObject���ֵ䣬�����ٴδ���Type��
    /// </summary>
    public Dictionary<string, GameObject> itemNameDic;
    
    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        //���ֵ��������ʼ��
        itemDictionaries = new Dictionary<System.Type, IDictionary>();
        //�������ֵ��ʼ��
        itemDictionaries[typeof(FishType)] = new Dictionary<FishType, GameObject>();
        itemDictionaries[typeof(ToolType)] = new Dictionary<ToolType, GameObject>();
        itemDictionaries[typeof(PropType)] = new Dictionary<PropType, GameObject>();
        itemDictionaries[typeof(BaitType)] = new Dictionary<BaitType, GameObject>();

        //�ֵ��ʼ��
        itemNameDic = new Dictionary<string, GameObject>();

        //������ʼ��
        if (Instance != null )
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    /// <summary>
    /// ���Ӧ�ֵ�����ӻ�õ���Ϣ
    /// </summary>
    /// <typeparam name="TKey">Ҫ��ѯ���ֵ�ļ�</typeparam>
    /// <param name="key">Ҫ�����ѯ�ֵ�ı���</param>
    /// <param name="gameObj">������Ӧ��GameObjecet</param>
    /// <returns>���ض�Ӧ��GameObject</returns>
    public GameObject AddOrGetType<TKey>(TKey key, GameObject gameObj)
    {
        //�Ȼ�ȡ���ֵ�
        var dic = GetDictionary<TKey>();
        //���������������࣬�����ֵ�������������
        if (!dic.ContainsKey(key))
        {
            //�����ֵ�
            dic.Add(key, gameObj);
            //�����ɵ�gameObject�ػ�
            PoolManager.Instance.CreatGameObjectPool(gameObj, 10, "Pool");
        }

        //�����Ʒ���ֹ����������������Ʒ������,�����
        if(!itemNameDic.ContainsKey(gameObj.name))
        {
            itemNameDic.Add(gameObj.name, gameObj);
        }

        //���ض�Ӧ����Ϣ
        return dic[key];
    }

    /// <summary>
    /// ͨ�����ֻ�ȡGameObject�ķ���
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetGameObjectFromName(string name)
    {
        //�����Ʒ���ֹ����������������Ʒ������,�����
        if (itemNameDic.ContainsKey(name))
        {
            return itemNameDic[name];
        }
        else
        {
            Debug.LogWarning($"itemNameDic��û������Ϊ{name}����Ʒ");
            return null;
        }
    }

    /// <summary>
    /// ɾ���ֵ��е�����
    /// </summary>
    /// <param name="fishType">�������Ϣ</param>
    public void DeleteType<TKey>(TKey key)
    {
        //�Ȼ�ȡ���ֵ�
        var dic = GetDictionary<TKey>();

        if(dic.ContainsKey(key))
        {
            PoolManager.Instance.DeleteGameObjectPool(dic[key]);
            dic.Remove(key);
        }
    }

    /// <summary>
    /// ��dictionaries�л�ȡ����Ӧ�ֵ�
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    /// <returns></returns>
    public Dictionary<TKey, GameObject> GetDictionary<TKey>()
    {
        //��ȷ��Tkey������
        var type = typeof(TKey);
        //Ȼ�����ֵ���Ѱ�Ҷ�Ӧ��С�ֵ�
        if(!itemDictionaries.ContainsKey(type))
        {
            Debug.LogError($"����������Ϊ{type.Name}���ֵ�");
            return null;
        }
        //���ض�Ӧ���ֵ�
        return itemDictionaries[type] as Dictionary<TKey, GameObject>;
    }

    /// <summary>
    /// �鿴Ŀǰ�ж�����Ʒ����ʼ����
    /// </summary>
    public void CheckDictionary()
    {
        foreach(string key in itemNameDic.Keys)
        {
            Debug.Log(itemNameDic[key]);
        }
    }
}
