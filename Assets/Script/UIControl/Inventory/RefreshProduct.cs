using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 定时刷新商品的方法（因为需要一直刷新，所以要单开一个类挂载到StoreCanvas）
/// </summary>
public class RefreshProduct : MonoBehaviour
{
    /// <summary>
    /// 引用计时器
    /// </summary>
    public Timer timer;
    /// <summary>
    /// 刷新一次商品需要的时间（默认10s）
    /// </summary>
    public float refreshProductTime = 10f;
    
    /// <summary>
    /// 引用购买格子的容器
    /// </summary>
    public BuySlotContainer buySlotContainer;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        //然后给该gameObject上添加计时器
        timer = gameObject.AddComponent<Timer>();
        //获取BuySlotContainer脚本
        buySlotContainer = SetGameObjectToParent.FindChildRecursive(transform, "StoreContent").GetComponent<BuySlotContainer>();
    }

    public void Start()
    {
        //将刷新商品的方法注册到计时器当中
        timer.OnTimerFinished += buySlotContainer.RefreshProduct;
    }
}
