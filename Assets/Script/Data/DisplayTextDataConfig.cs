using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 显示文字UI的数据
/// </summary>
public class DisplayTextDataConfig
{
    /// <summary>
    /// 咬钩文字
    /// </summary>
    public string bitingHookText { get; private set; }
    /// <summary>
    /// 线断文字
    /// </summary>
    public string wireBreakText { get; private set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public DisplayTextDataConfig()
    {
        bitingHookText = "上钩了";
        wireBreakText = "线崩了";
    }
}
