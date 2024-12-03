using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

/// <summary>
/// 控制悬浮窗的方法
/// </summary>
public class DisplayFloatingPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    /// <summary>
    /// 引用floatingPanel脚本
    /// </summary>
    public FloatingPanel floatingPanel;
    /// <summary>
    /// 引用悬浮窗的RectTransform
    /// </summary>
    public RectTransform floatingPanelRectTransform;
    /// <summary>
    /// 渲染的主摄像机
    /// </summary>
    public Camera cameraCanvas;
    /// <summary>
    /// 引用挂载本Icon的slot
    /// </summary>
    public Slot slot;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    private void Awake()
    {
        //获取floatingPanel
        floatingPanel = SetGameObjectToParent.FindChildBreadthFirst(SetGameObjectToParent.FindFromFirstLayer("DisplayFloatingPanelCanvas").transform, "FloatingPanel").GetComponent<FloatingPanel>();
        if (floatingPanel == null)
        {
            Debug.LogError("floatingPanel是空的，请检查代码！");
        }
        //获取floatingPanelRectTransform
        floatingPanelRectTransform = floatingPanel.gameObject.GetComponent<RectTransform>();
        if (floatingPanelRectTransform == null)
        {
            Debug.LogError("floatingPanelRectTransform是空的，请检查代码！");
        }
        //获取slot脚本
        slot = GetComponentInParent<Slot>();
        if (slot == null)
        {
            Debug.LogError("floatingPanelScript是空的，请检查代码！");
        }
        //获取到渲染的Camera组件
        cameraCanvas = SetGameObjectToParent.FindFromFirstLayer("Main Camera").GetComponent<Camera>();
        if (cameraCanvas == null)
        {
            Debug.LogError("没有找到cameraCanvas，请检查代码！");
        }

        //初始化floatingPanel
        floatingPanel.InitFloatingPanelObj();
    }

    private void Update()
    {
        //调用鼠标跟随的方法
        MouseFollowWithOffSetOnUpdate();
    }

    /// <summary>
    /// 悬浮板跟随鼠标移动的方法（有偏移量）
    /// </summary>
    public void MouseFollowWithOffSetOnUpdate()
    {
        //悬浮板宽度的偏移量
        float widthOffset = floatingPanelRectTransform.rect.width / 2 + 10f;
        //悬浮板高度的偏移量
        float heightOffset = floatingPanelRectTransform.rect.height / 2 + 10f;
        //获取到鼠标位置
        Vector2 screenPoint = Input.mousePosition;

        //声明一个本地坐标，用来接收鼠标转换后的坐标
        Vector2 localPoint;
        //把屏幕坐标转换为本地坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            floatingPanelRectTransform.parent as RectTransform,
            screenPoint,
            cameraCanvas,
            out localPoint
        );

        //将输出的本地坐标加上偏移量
        floatingPanelRectTransform.localPosition = new Vector2(localPoint.x + widthOffset, localPoint.y + heightOffset);
    }

    /// <summary>
    /// 鼠标移动到UI内
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        //先识别鼠标指在什么物品上了
        var item = slot.inventory_Database.list[slot.Index];
        //打开悬浮板
        floatingPanel.gameObject.SetActive(true);
        //更新悬浮板内的信息
        UpdatePanelData(item);
        
    }

    /// <summary>
    /// 鼠标移动到UI外
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //关闭悬浮板
        floatingPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// 鼠标按下的方法
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //关闭悬浮板
        floatingPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// 鼠标松开的方法
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        //关闭悬浮板
        floatingPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// 通过slot的种类更新悬浮板的数据
    /// </summary>
    /// <param name="slotType">slot的种类</param>
    public void UpdatePanelData(ItemData item)
    {
        switch(item)
        {
            case FishItem fishItem:
                floatingPanel.GetNameUI().text = fishItem.type.name;
                floatingPanel.GetDeputyUI().text = fishItem.fishedTime.ToString();
                floatingPanel.GetDescriptionUI().text = fishItem.type.description;
                break;
            case ToolItem toolItem:
                floatingPanel.GetNameUI().text = toolItem.type.name;
                floatingPanel.GetDeputyUI().text = toolItem.toolQuality.ToString();
                floatingPanel.GetDescriptionUI().text = toolItem.type.description;
                break;
            case PropItem propItem:
                floatingPanel.GetNameUI().text = propItem.type.name;
                floatingPanel.GetDeputyUI().text = propItem.propQuality.ToString();
                floatingPanel.GetDescriptionUI().text = propItem.type.description;
                break;
            case BaitItem baitItem:
                floatingPanel.GetNameUI().text = baitItem.type.name;
                floatingPanel.GetDeputyUI().text = "数量：" + baitItem.amount.ToString();
                floatingPanel.GetDescriptionUI().text = baitItem.type.description;
                break;
            //一般不会执行到这里
            default:
                //关闭所有floating
                floatingPanel.gameObject.SetActive(false);
                break;
        }
    }

    /// <summary>
    /// 获取到工具的品质
    /// </summary>
    public string GetToolQualityInfo(ToolItem toolItem)
    {
        //先获取到工具品质
        ToolQuality toolQuality = toolItem.toolQuality;
        //通过武器品质返回对应文字
        switch (toolQuality)
        {
            case ToolQuality.Normal:
                return "普通";
            case ToolQuality.Advanced:
                return "高级";
            case ToolQuality.Epic:
                return "史诗";
            case ToolQuality.Legendary:
                return "传奇";
            default:
                return "返回品质错误";
        }
    }

    public string GetPropQualityInfo(PropItem propItem)
    {
        //先获取到道具品质
        PropQuality propQuality = propItem.propQuality;
        //通过武器品质返回对应文字
        switch (propQuality)
        {
            case PropQuality.Normal:
                return "普通";
            case PropQuality.Advanced:
                return "高级";
            case PropQuality.Epic:
                return "史诗";
            case PropQuality.Legendary:
                return "传奇";
            default:
                return "返回品质错误";
        }
    }
}
