using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 悬浮面板具体参数的类
/// </summary>
public class FishItemFloating : MonoBehaviour
{
    /// <summary>
    /// 显示名字
    /// </summary>
    public Text Name { get; set; }
    /// <summary>
    /// 被钓起来的时间
    /// </summary>
    public Text FishedTime { get; set; }
    /// <summary>
    /// 显示长度
    /// </summary>
    public Text LengthValue { get; set; }
    /// <summary>
    /// 显示重量
    /// </summary>
    public Text WeightValue { get; set; }

    /// <summary>
    /// 脚本初始化
    /// </summary>
    private void Awake()
    {
        Name = transform.Find("Name").GetComponent<Text>();
        if (Name == null)
            Debug.LogError("Name是空的，请检查代码！");

        FishedTime = SetGameObjectToParent.FindChildBreadthFirst(Name.gameObject.transform, "fishedTime").GetComponent<Text>();
        if (FishedTime == null)
            Debug.LogError("FishedTime是空的，请检查代码！");

        LengthValue = SetGameObjectToParent.FindChildBreadthFirst(transform.Find("Length").gameObject.transform, "LengthValue").GetComponent<Text>();
        if (LengthValue == null)
            Debug.LogError("Length是空的，请检查代码！");

        WeightValue = SetGameObjectToParent.FindChildBreadthFirst(transform.Find("Weight").gameObject.transform, "WeightValue").GetComponent<Text>();
        if (WeightValue == null)
            Debug.LogError("Weight是空的，请检查代码！");
    }
}
