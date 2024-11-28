using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏UI状态机
/// </summary>
public class GameUIStateMachine : MonoBehaviour
{
    /// <summary>
    /// 引用总控制器
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// 引用游戏状态
    /// </summary>
    public GameUIState gameState;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    private void Awake()
    {
        //脚本初始化时默认不能执行Update等方法
        enabled = false;

        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
    }

    /// <summary>
    /// 启动状态机的方法
    /// </summary>
    public void StartMachine()
    {
        //默认进入操作状态
        gameState = new isGaming();
        //将状态导入状态机
        SetGameState(gameState);
        enabled = true;
    }

    /// <summary>
    /// 停止状态机
    /// </summary>
    public void StopMachine()
    {
        //先将状态转回到不钓鱼的状态
        SetGameState(new isGaming());
        //暂停状态机的运行
        enabled = false;
    }

    /// <summary>
    /// 转换状态的方法
    /// </summary>
    /// <param name="gameState"></param>
    public void SetGameState(GameUIState gameState)
    {
        this.gameState?.OnExit(this);
        this.gameState = gameState;
        this.gameState?.OnEnter(this);
    }

    /// <summary>
    /// 提供接口，供外部查询当前状态
    /// </summary>
    /// <returns>返回当前钓鱼的状态</returns>
    public string CheckStatus()
    {
        return gameState.GetType().Name;
    }

    /// <summary>
    /// 持续更新
    /// </summary>
    public void Update()
    {
        this.gameState?.OnUpdate(this);
    }

    public void LateUpdate()
    {
        this.gameState?.OnLateUpdate(this);

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(CheckStatus());
        }
    }
}
