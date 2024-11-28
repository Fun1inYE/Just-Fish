using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 悬浮面板的总控制器
/// </summary>
public class FloatingController : MonoBehaviour
{
    /// <summary>
    /// 引用鱼类悬浮面板的脚本
    /// </summary>
    public FishItemFloating fishItemFloatingScript;
    /// <summary>
    /// 引用工具悬浮面板的脚本
    /// </summary>
    public ToolItemFloating toolItemFloatingScript;
    /// <summary>
    /// 引用道具悬浮面板的脚本
    /// </summary>
    public PropItemFloating propItemFloatingScript;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void InitializeFloatingPanel()
    {
        fishItemFloatingScript = transform.Find("FishItemFloatingPanel").GetComponent<FishItemFloating>();
        toolItemFloatingScript = transform.Find("ToolItemFloatingPanel").GetComponent<ToolItemFloating>();
        //初始化toolItemFloatingScript
        toolItemFloatingScript.InitializeToolItemFloating();
        propItemFloatingScript = transform.Find("PropItemFloatingPanel").GetComponent<PropItemFloating>();
        propItemFloatingScript.InitializePropItemFloating();

    }

    /// <summary>
    /// 关闭所有悬浮面板
    /// </summary>
    public void CloseAllFloating()
    {
        fishItemFloatingScript.gameObject.SetActive(false);
        toolItemFloatingScript.gameObject.SetActive(false);
        propItemFloatingScript.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// 启动某一个悬浮板（如果多的话也可以用队列遍历）
    /// </summary>
    public void OpenFloating(bool isFishItemFloatingOpen, bool isToolItemFloatingOpen, bool isPropItemFloatingOpen)
    {
        fishItemFloatingScript.gameObject.SetActive(isFishItemFloatingOpen);
        toolItemFloatingScript.gameObject.SetActive(isToolItemFloatingOpen);
        propItemFloatingScript.gameObject.SetActive(isPropItemFloatingOpen);
    }
}
