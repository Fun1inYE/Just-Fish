using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 观察者模式的被观察者的接口
/// </summary>
public interface IUIObserver
{
    /// <summary>
    /// 打开UI时要调用的方法
    /// </summary>
    public void OnOpenUI();
    /// <summary>
    /// 关闭Ui要调用的方法
    /// </summary>
    public void OnCloseUI();
}

/// <summary>
/// 背包相关的控制时的方法
/// </summary>
public interface IInventoyControllerObserver : IUIObserver
{
    
}

/// <summary>
/// 商店页面相关的控制的方法
/// </summary>
public interface IStoreControllerObserver : IUIObserver
{
    
}

/// <summary>
/// 游戏管理器的结束游戏观察者模式的接口
/// </summary>
public interface IEndGameObserver
{
    /// <summary>
    /// 结束游戏之后要广播的方法
    /// </summary>
    public void OnEndGame();
}