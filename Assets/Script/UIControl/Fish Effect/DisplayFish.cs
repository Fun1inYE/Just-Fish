using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

/// <summary>
/// 在钓鱼成功后调用的方法
/// </summary>
public class DisplayFish
{
    /// <summary>
    /// 需要跟踪的鱼漂
    /// </summary>
    public GameObject needFollowDrift;
    /// <summary>
    /// 本次操作的鱼的GameObject
    /// </summary>
    public GameObject fishGameObject;
    /// <summary>
    /// 鱼的长度
    /// </summary>
    public double fishLength;
    /// <summary>
    /// 鱼的重量
    /// </summary>
    public double fishWeight;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="needFollowDrift"></param>
    public DisplayFish(GameObject needFollowDrift)
    {
        this.needFollowDrift = needFollowDrift;
    }

    /// <summary>
    /// 取出鱼的GameObject并且设置GameObject
    /// </summary>
    public void GetAndProcessingFishGameObject()
    {
        RandomGetFishGameObjectAndSetting();
    }

    /// <summary>
    /// 将鱼的GameObject的放回池子
    /// </summary>
    public void ReplaceFishGameObject()
    {
        //如果fishGameObject不为空的话，这里是防止玩家在Fishing状态的时候主动收杆导致fishGameObject为空的报错
        if (fishGameObject != null)
        {
            //将GameObject放回池子
            PoolManager.Instance.ReturnGameObjectToPool(fishGameObject);
            //将GameObject移动到Pool中
            SetGameObjectToParent.SetParent("Pool", fishGameObject);
        }
    }

    /// <summary>
    /// 随机拿到FishManager中字典中的鱼的信息，然后处理GameObject，然后随机生成数据
    /// </summary>
    public void RandomGetFishGameObjectAndSetting()
    {
        //随机生成字典中的一个数字
        int randomNumber = Random.Range(0, ItemManager.Instance.GetDictionary<FishType>().Count - 1);
        //使用ElementAt随机访问键值对
        var element = ItemManager.Instance.GetDictionary<FishType>().ElementAt(randomNumber);
        //从池子中取出GameObject
        fishGameObject = PoolManager.Instance.GetGameObjectFromPool(element.Value.name);
        Debug.Log($"取出成功，取出了一个{fishGameObject.name}");

        //处理GameObject
        InitFishGameObject(fishGameObject);

        //随机生成鱼的长度
        fishLength = System.Math.Round(Random.Range(1f, 5f), 2);
        //随机生成鱼的重量
        fishWeight = System.Math.Round(Random.Range(1f, 10f), 2);

        //存入Inventory
        InventoryManager.Instance.backpackManager.AddItemInList(new FishItem(element.Key, fishLength, fishWeight));
        //然后更新UI
        InventoryManager.Instance.backpackSlotContainer.RefreshSlotUI();
    }

    /// <summary>
    /// 处理新生成的FishGameObject的各种属性
    /// </summary>
    public void InitFishGameObject(GameObject fishObj)
    {
        //重新设定gameObject的本地Scale
        fishObj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        //将鱼的GameObject放在drift位置上
        fishObj.transform.SetParent(needFollowDrift.transform, true);
        //重新调整GameObject的本地坐标为原点坐标
        fishObj.transform.localPosition = new Vector3(0, 0, 0);
        //激活GameObject
        fishObj.SetActive(true);
    }
}
