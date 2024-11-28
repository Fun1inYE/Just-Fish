using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 道具的悬浮板
/// </summary>
public class PropItemFloating : MonoBehaviour
{
    /// <summary>
    /// 道具名字
    /// </summary>
    public Text Name;
    /// <summary>
    /// 道具的描述
    /// </summary>
    public Text Description;
    /// <summary>
    /// 道具的品质
    /// </summary>
    public Text PropQuality;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void InitializePropItemFloating()
    {
        Name = transform.Find("Name").GetComponent<Text>();
        Description = ComponentFinder.GetChildComponent<Text>(gameObject, "Description");
        PropQuality = ComponentFinder.GetChildComponent<Text>(gameObject, "Quality");
    }
}
