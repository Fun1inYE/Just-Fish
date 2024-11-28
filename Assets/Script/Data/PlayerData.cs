using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关于玩家的数据类
/// </summary>
public class PlayerData
{
    /// <summary>
    /// 玩家的自己起的名字(默认名字为defaultName)
    /// </summary>
    public string name = "defaultName";
    /// <summary>
    /// 玩家所持有的硬币(默认为0)
    /// </summary>
    public int coin = 0;
    /// <summary>
    /// 玩家的经验（默认为0）
    /// </summary>
    public int experience = 0;
    /// <summary>
    /// 玩家预卖出的金币
    /// </summary>
    public int sallingCoin = 0;

    /// <summary>
    /// 委托事件，提醒系统该更新数据或者UI了
    /// </summary>
    public event Action OnPlayerDataChange;

    #region TestCode
    public void CheckOnPlayerDataChangeList()
    {
        // 获取事件的委托对象
        var invocationList = OnPlayerDataChange?.GetInvocationList();

        // 输出方法的数量
        Debug.Log($"注册的方法数量: {invocationList.Length}");
    }
    #endregion


    /// <summary>
    /// 提醒观察者要更新玩家的名字了
    /// </summary>
    /// <param name="name"></param>
    public void NodifyPlayerName(string name)
    {
        this.name = name;
        //提醒观察者要更新玩家名字了
        OnPlayerDataChange?.Invoke();
    }

    /// <summary>
    /// 提醒观察者更新Coin了
    /// </summary>
    /// <param name="coin"></param>
    public void NodifyCoin(int coin)
    {
        this.coin += coin;
        //提醒观察者该更新数据了
        OnPlayerDataChange?.Invoke();         
    }

    /// <summary>
    /// 提醒观察者更新experience
    /// </summary>
    /// <param name="experience"></param>
    public void NodifyExperience(int experience)
    {
        this.experience += experience;
        OnPlayerDataChange?.Invoke();
    }

    /// <summary>
    /// 提醒观察者应该更新sallingCoin
    /// </summary>
    /// <param name="sallingCoin"></param>
    public void NodifySalling(int sallingCoin)
    {
        this.sallingCoin += sallingCoin;
        OnPlayerDataChange?.Invoke();
    }

    /// <summary>
    /// 提醒观察者应该更新sallingCoin
    /// </summary>
    /// <param name="sallingCoin"></param>
    public void NodifySallingToZero()
    {
        this.sallingCoin = 0;
        OnPlayerDataChange?.Invoke();
    }
}
