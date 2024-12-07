using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 商店的UI
/// </summary>

public class StoreUI : MonoBehaviour
{
    /// <summary>
    /// 工具商品价格
    /// </summary>
    public Text price_tool;
    /// <summary>
    /// 第一个道具价格
    /// </summary>
    public Text price_prop1;
    /// <summary>
    /// 第二个道具价格
    /// </summary>
    public Text price_prop2;

    /// <summary>
    /// 创建Store的数据
    /// </summary>
    public StoreData storeData;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        //寻找商店第一个slot
        GameObject StoreSlot = SetGameObjectToParent.FindChildRecursive(transform, "StoreSlot").gameObject;
        price_tool = ComponentFinder.GetChildComponent<Text>(StoreSlot, "NeedCoin");
        //寻找商店第二个slot
        GameObject StoreSlot1 = SetGameObjectToParent.FindChildRecursive(transform, "StoreSlot (1)").gameObject;
        price_prop1 = ComponentFinder.GetChildComponent<Text>(StoreSlot1, "NeedCoin");
        //寻找商店第三个slot
        GameObject StoreSlot2 = SetGameObjectToParent.FindChildRecursive(transform, "StoreSlot (2)").gameObject;
        price_prop2 = ComponentFinder.GetChildComponent<Text>(StoreSlot2, "NeedCoin");
    }

    /// <summary>
    /// 设定商店的数据，然后给StoreUI更新事件进行注册
    /// </summary>
    /// <param name="data"></param>
    public void SetStoreData(StoreData data)
    {
        storeData = data;
        //更新UI
        UpdataUI();
        //注册UI更新事件
        storeData.OnPriceChange += UpdataUI;
    }

    /// <summary>
    /// 更新UI
    /// </summary>
    public void UpdataUI()
    {
        price_tool.text = storeData.price_slot0.ToString();
        price_prop1.text = storeData.price_slot1.ToString();
        price_prop2.text = storeData.price_slot2.ToString();
    }
}
