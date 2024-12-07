using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreDataAndUIController : MonoBehaviour
{
    /// <summary>
    /// 引用商店UI
    /// </summary>
    public StoreUI storeUI;
    /// <summary>
    /// 引用商店数据
    /// </summary>
    public StoreData storeData;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        storeData = new StoreData();

        storeUI = GetComponent<StoreUI>();
    }

    /// <summary>
    /// 给StoreUI附上StoreData
    /// </summary>
    public void Start()
    {
        storeUI.SetStoreData(storeData);
    }

    /// <summary>
    /// 更改商品一的价格
    /// </summary>
    /// <param name="price"></param>
    public void ChangePrice_Slot0(int price)
    {
        storeData.NodifyPrice_Slot0(price);
    }

    /// <summary>
    /// 更改商品二的价格
    /// </summary>
    /// <param name="price"></param>
    public void ChangePrice_Slot1(int price)
    {
        storeData.NodifyPrice_Slot1(price);
    }

    /// <summary>
    /// 更改商品三的价格
    /// </summary>
    /// <param name="price"></param>
    public void ChangePrice_Slot2(int price)
    {
        storeData.NodifyPrice_Slot2(price);
    }
}
