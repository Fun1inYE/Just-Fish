using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 总状态机类
/// </summary>
public class TotalMachine : MonoBehaviour
{
    /// <summary>
    /// 引用钓鱼状态机
    /// </summary>
    public FishingStateMachine fishingStateMachine;
    /// <summary>
    /// 引用游戏内UI的状态机
    /// </summary>
    public GameUIStateMachine gameUIStateMachine;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        fishingStateMachine = GetComponent<FishingStateMachine>();
        gameUIStateMachine = GetComponent<GameUIStateMachine>();
    }

    /// <summary>
    /// 启动所有状态机
    /// </summary>
    public void StartAllMachine()
    {
        fishingStateMachine.StartMachine();
        gameUIStateMachine.StartMachine();
    }

    /// <summary>
    /// 停止所有状态机
    /// </summary>
    public void StopAllMachine()
    {
        fishingStateMachine.StopMachine();
        gameUIStateMachine.StopMachine();
    }
}
