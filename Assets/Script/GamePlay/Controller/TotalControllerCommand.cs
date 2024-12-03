using System.Windows.Input;
using UnityEngine;

/// <summary>
/// 控制器接口
/// </summary>
public interface IController
{
    /// <summary>
    /// 判断这个继承自这个接口的控制类是否可以运行
    /// </summary>
    public bool canRun { get; set; }
}


/// <summary>
/// 被挂起的状态机的总控制器指令类
/// </summary>
public class ConvertController
{
    /// <summary>
    /// 被挂起的状态机的名字
    /// </summary>
    public string StateMachineName { get; }
    /// <summary>
    /// 被挂起的要操作的控制器
    /// </summary>
    private readonly IController controller;
    /// <summary>
    /// 被挂起的指令
    /// </summary>
    private readonly bool enable;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="stateMachineName"></param>
    /// <param name="controller"></param>
    /// <param name="enable"></param>
    public ConvertController(string stateMachineName, IController controller, bool enable)
    {
        StateMachineName = stateMachineName;
        this.controller = controller;
        this.enable = enable;
    }

    /// <summary>
    /// 执行指令
    /// </summary>
    public void Execute()
    {
        controller.canRun = enable;
        Debug.Log($"[ConvertController] {StateMachineName} 设置 {controller.GetType().Name}.enabled = {enable}");
    }
}