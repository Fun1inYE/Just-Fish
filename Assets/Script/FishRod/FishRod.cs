using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 鱼竿的类
/// </summary>
public class FishRod : MonoBehaviour
{
    /// <summary>
    /// 获取到鱼竿的Transform
    /// </summary>
    public Transform fishRodTransform;
    /// <summary>
    /// 线的Transform
    /// </summary>
    public Transform wireTransform;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    private void Awake()
    {
        fishRodTransform = GetComponent<Transform>();
        wireTransform = transform.GetChild(0).GetComponent<Transform>();
    }
}
