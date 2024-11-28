using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 加载页面的类
/// </summary>
public class LoadingScene : MonoBehaviour
{
    /// <summary>
    /// 获取到加载页面
    /// </summary>
    public Transform loadingPanel;
    /// <summary>
    /// 获取到加载文本
    /// </summary>
    public Text loadingText;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        loadingPanel = GetComponent<Transform>();
        loadingText = loadingPanel.Find("LoadingText").GetComponent<Text>();
    }
}
