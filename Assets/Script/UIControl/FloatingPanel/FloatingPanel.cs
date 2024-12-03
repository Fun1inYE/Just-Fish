using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 漂浮信息板类
/// </summary>
public class FloatingPanel : MonoBehaviour
{
    /// <summary>
    /// 获取到漂浮板子的GameObject
    /// </summary>
    public GameObject floatingPanelObj;
    /// <summary>
    /// 漂浮板的NameUI
    /// </summary>
    private Text nameUI;
    /// <summary>
    /// 附加信息UI
    /// </summary>
    private Text deputyUI;
    /// <summary>
    /// 描述UI
    /// </summary>
    private Text descriptionUI;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void InitFloatingPanelObj()
    {
        if (floatingPanelObj != null)
        {
            nameUI = SetGameObjectToParent.FindChildRecursive(floatingPanelObj.transform, "Name").GetComponent<Text>();
            deputyUI = SetGameObjectToParent.FindChildRecursive(floatingPanelObj.transform, "Deputy").GetComponent<Text>();
            descriptionUI = SetGameObjectToParent.FindChildRecursive(floatingPanelObj.transform, "Description").GetComponent<Text>();
        }
        else
        {
            Debug.LogError("floatingPanelObj是空的，请假查Hierarchy窗口");
        }
    }

    /// <summary>
    /// 获取nameUI
    /// </summary>
    /// <returns></returns>
    public Text GetNameUI()
    {
        return nameUI;
    }

    /// <summary>
    /// 获取deputyUI
    /// </summary>
    /// <returns></returns>
    public Text GetDeputyUI()
    {
        return deputyUI;
    }

    /// <summary>
    /// 获取descriptionUI
    /// </summary>
    /// <returns></returns>
    public Text GetDescriptionUI()
    {
        return descriptionUI;
    }
}
