using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

/// <summary>
/// 控制悬浮窗的方法
/// </summary>
public class DisplayFloatingPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    /// <summary>
    /// 引用到悬浮窗的GameObject
    /// </summary>
    public GameObject floatingPanel;
    /// <summary>
    /// 引用悬浮面板控制器
    /// </summary>
    public FloatingController floatingController;
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
        floatingPanel = SetGameObjectToParent.FindChildBreadthFirst(SetGameObjectToParent.FindFromFirstLayer("DisplayFloatingPanelCanvas").transform, "FloatingPanel").gameObject;
        if (floatingPanel == null)
        {
            Debug.LogError("floatingPanel是空的，请检查代码！");
        }
        //获取floatingPanelRectTransform
        floatingPanelRectTransform = floatingPanel.GetComponent<RectTransform>();
        if(floatingPanelRectTransform == null)
        {
            Debug.LogError("floatingPanelRectTransform是空的，请检查代码！");
        }
        //获取floatingController
        floatingController = floatingPanel.GetComponent<FloatingController>();
        if (floatingController == null)
        {
            Debug.LogError("floatingController是空的，请检查代码！");
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
        //先识别鼠标指在什么Slot上了
        SlotType slotType = slot.interiorSlotType;
        //打开悬浮板
        floatingPanel.SetActive(true);
        //更新悬浮板内的信息
        UpdatePanelData(slotType);
    }

    /// <summary>
    /// 鼠标移动到UI外
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //关闭所有悬浮板
        floatingController.CloseAllFloating();
        //关闭悬浮板
        floatingPanel.SetActive(false);
    }

    /// <summary>
    /// 鼠标按下的方法
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //关闭所有悬浮板
        floatingController.CloseAllFloating();
        //关闭悬浮板
        floatingPanel.SetActive(false);
    }

    /// <summary>
    /// 鼠标松开的方法
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        //关闭所有悬浮板
        floatingController.CloseAllFloating();
        //关闭悬浮板
        floatingPanel.SetActive(false);
    }

    /// <summary>
    /// 通过slot的种类更新悬浮板的数据
    /// </summary>
    /// <param name="slotType">slot的种类</param>
    public void UpdatePanelData(SlotType slotType)
    {
        switch(slotType)
        {
            case SlotType.FishItem:
                if (slot.inventory_Database.list[slot.Index] is FishItem fishItem)
                {
                    floatingController.OpenFloating(true, false, false);
                    //更新fishItemFloatingScript数据
                    floatingController.fishItemFloatingScript.Name.text = fishItem.type.name;
                    floatingController.fishItemFloatingScript.FishedTime.text = fishItem.fishedTime;
                    floatingController.fishItemFloatingScript.LengthValue.text = fishItem.Length.ToString();
                    floatingController.fishItemFloatingScript.WeightValue.text = fishItem.Weight.ToString();
                }
                break;
            case SlotType.ToolItem:
                if (slot.inventory_Database.list[slot.Index] is ToolItem toolItem)
                {
                    floatingController.OpenFloating(false, true, false);
                    //更新toolItemFloatingScript的数据
                    floatingController.toolItemFloatingScript.Name.text = toolItem.type.name;
                    floatingController.toolItemFloatingScript.ToolQuality.text = GetToolQualityInfo(toolItem);
                    floatingController.toolItemFloatingScript.Description.text = toolItem.type.description;
                }
                break;
            case SlotType.PropItem:
                if (slot.inventory_Database.list[slot.Index] is PropItem propItem)
                {
                    floatingController.OpenFloating(false, false, true);
                    //更新propItemFloatingScript的数据
                    floatingController.propItemFloatingScript.Name.text = propItem.type.name;
                    floatingController.propItemFloatingScript.PropQuality.text = GetPropQualityInfo(propItem);
                    floatingController.propItemFloatingScript.Description.text = propItem.type.description;
                }
                break;
            //一般不会执行到这里
            default:
                //关闭所有floating
                floatingController.CloseAllFloating();
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
