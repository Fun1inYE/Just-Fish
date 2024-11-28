using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 工具的悬浮板
/// </summary>
public class ToolItemFloating : MonoBehaviour
{
    /// <summary>
    /// 工具名字
    /// </summary>
    public Text Name;
    // <summary>
    /// 工具品质
    /// </summary>
    public Text ToolQuality;
    /// <summary>
    /// 工具的描述
    /// </summary>
    public Text Description;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void InitializeToolItemFloating()
    {
        Name = transform.Find("Name").GetComponent<Text>();
        ToolQuality = ComponentFinder.GetChildComponent<Text>(gameObject, "Quality");
        Description = ComponentFinder.GetChildComponent<Text>(gameObject, "Description");
    }
}
