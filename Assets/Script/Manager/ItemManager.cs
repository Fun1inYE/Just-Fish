using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 物品信息管理器
/// </summary>
public class ItemManager : MonoBehaviour
{
    /// <summary>
    /// FishManager的单例
    /// </summary>
    public static ItemManager Instance;
    /// <summary>
    /// 创建一个字典管理器
    /// </summary>
    public Dictionary<System.Type, IDictionary> itemDictionaries;

    /// <summary>
    /// 创建一个名字对应GameObject的字典，用于再次创建Type类
    /// </summary>
    public Dictionary<string, GameObject> itemNameDic;
    
    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        //给字典管理器初始化
        itemDictionaries = new Dictionary<System.Type, IDictionary>();
        //给各个字典初始化
        itemDictionaries[typeof(FishType)] = new Dictionary<FishType, GameObject>();
        itemDictionaries[typeof(ToolType)] = new Dictionary<ToolType, GameObject>();
        itemDictionaries[typeof(PropType)] = new Dictionary<PropType, GameObject>();
        itemDictionaries[typeof(BaitType)] = new Dictionary<BaitType, GameObject>();

        //字典初始化
        itemNameDic = new Dictionary<string, GameObject>();

        //单例初始化
        if (Instance != null )
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    /// <summary>
    /// 向对应字典中添加或得到信息
    /// </summary>
    /// <typeparam name="TKey">要查询的字典的键</typeparam>
    /// <param name="key">要导入查询字典的变量</param>
    /// <param name="gameObj">变量对应的GameObjecet</param>
    /// <returns>返回对应的GameObject</returns>
    public GameObject AddOrGetType<TKey>(TKey key, GameObject gameObj)
    {
        //先获取到字典
        var dic = GetDictionary<TKey>();
        //如果不包含这个鱼类，则向字典中添加这个鱼类
        if (!dic.ContainsKey(key))
        {
            //加入字典
            dic.Add(key, gameObj);
            //将生成的gameObject池化
            PoolManager.Instance.CreatGameObjectPool(gameObj, 10, "Pool");
        }

        //如果物品名字管理器不包含这个物品的名字,就添加
        if(!itemNameDic.ContainsKey(gameObj.name))
        {
            itemNameDic.Add(gameObj.name, gameObj);
        }

        //返回对应类信息
        return dic[key];
    }

    /// <summary>
    /// 通过名字获取GameObject的方法
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetGameObjectFromName(string name)
    {
        //如果物品名字管理器不包含这个物品的名字,就添加
        if (itemNameDic.ContainsKey(name))
        {
            return itemNameDic[name];
        }
        else
        {
            Debug.LogWarning($"itemNameDic中没有名字为{name}的物品");
            return null;
        }
    }

    /// <summary>
    /// 删除字典中的鱼类
    /// </summary>
    /// <param name="fishType">鱼类的信息</param>
    public void DeleteType<TKey>(TKey key)
    {
        //先获取到字典
        var dic = GetDictionary<TKey>();

        if(dic.ContainsKey(key))
        {
            PoolManager.Instance.DeleteGameObjectPool(dic[key]);
            dic.Remove(key);
        }
    }

    /// <summary>
    /// 从dictionaries中获取到对应字典
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    /// <returns></returns>
    public Dictionary<TKey, GameObject> GetDictionary<TKey>()
    {
        //先确定Tkey的类型
        var type = typeof(TKey);
        //然后在字典总寻找对应的小字典
        if(!itemDictionaries.ContainsKey(type))
        {
            Debug.LogError($"不存在类型为{type.Name}的字典");
            return null;
        }
        //返回对应的字典
        return itemDictionaries[type] as Dictionary<TKey, GameObject>;
    }

    /// <summary>
    /// 查看目前有多少物品被初始化了
    /// </summary>
    public void CheckDictionary()
    {
        foreach(string key in itemNameDic.Keys)
        {
            Debug.Log(itemNameDic[key]);
        }
    }
}
