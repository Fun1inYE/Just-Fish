using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 格子的类别
/// </summary>
public enum SlotType { FishItem, ToolItem, PropItem, Equipment, Sale, Buy }

/// <summary>
/// 控制每一个slot的类
/// </summary>
public class Slot : MonoBehaviour
{
    /// <summary>
    /// 每个格子的Icon的GameObject
    /// </summary>
    public GameObject iconObj;
    /// <summary>
    /// 每个Icon的Image组件
    /// </summary>
    public Image icon;
    /// <summary>
    /// 每一个格子的序号，在外部赋值
    /// </summary>
    public int Index;
    /// <summary>
    /// 格子对应的数据库（通过slotType对应）
    /// </summary>
    public InventoryData inventory_Database;
    /// <summary>
    /// 每一种格子的类别
    /// </summary>
    [SerializeField]
    public SlotType slotType;
    /// <summary>
    /// 里slot的类别
    /// </summary>
    public SlotType interiorSlotType;
    /// <summary>
    /// 获取到拖拽脚本
    /// </summary>
    public DragItem dragItemScript;

    /// <summary>
    /// 脚本启动检测
    /// </summary>
    private void Awake()
    {
        //找到slot下的Icon，并且找到其Image组件
        iconObj = gameObject.transform.Find("Icon").gameObject;
        if (iconObj != null)
        {
            if (iconObj.GetComponent<Image>())
            {
                //获取到icon的Image组件
                icon = iconObj.GetComponent<Image>();
            }
            else
            {
                Debug.LogError("没有找到iconObj下的Image组件，请检查代码！");
            }
            //脚本启动后先将gameObject和sprite全部关闭
            iconObj.SetActive(false);

            //只有buy格子不需要拖拽脚本
            if(slotType != SlotType.Buy)
            {
                //获取到拖拽脚本
                dragItemScript = iconObj.GetComponent<DragItem>();
                if (dragItemScript == null)
                {
                    Debug.LogError("dragItemScript是空的，请检查代码或者Hierarchy窗口");
                }
            }
        }
        else
        {
            Debug.LogError("没有找到iconObj，请检查代码！");
        }
    }

    /// <summary>
    /// 更新格子UI的方法
    /// </summary>
    public void UpdateUI()
    {
        //先通过Slot的slotType寻找对应匹配的库存数据
        switch(slotType)
        {
            case SlotType.FishItem:
                inventory_Database = InventoryManager.Instance.backpackManager;
                break;
            case SlotType.ToolItem:
                inventory_Database = InventoryManager.Instance.toolpackManager;
                break;
            case SlotType.PropItem:
                inventory_Database = InventoryManager.Instance.proppackManager;
                break;
            case SlotType.Equipment:
                inventory_Database = InventoryManager.Instance.equipmentManager;
                break;
            case SlotType.Sale:
                inventory_Database = InventoryManager.Instance.saleManager;
                break;
            case SlotType.Buy:
                inventory_Database = InventoryManager.Instance.buyManager;
                break;
        }
        var item = inventory_Database.list[Index];

        //进行更新
        UpdateIcon(item);
    }

    /// <summary>
    /// 通过传进来的ItemData基类判断属于哪种物品类型，然后进行更新Icon(主要是因为item是ItemData类型的，无法直接转换成对应物品类型)
    /// </summary>
    /// <param name="items"></param>
    public void UpdateIcon(ItemData item)
    {
        //如果item类是FishItem类
        if (item is FishItem fishItem && item.itemIdentifier.Type == "FishItem")
        {
            //更新图片
            icon.sprite = fishItem.Type.GetFishImage();
            //启动Icon的GameObject
            iconObj.gameObject.SetActive(true);
        }
        //如果item类是ToolItem类
        else if (item is ToolItem toolItem && item.itemIdentifier.Type == "ToolItem")
        {
            //更新图片
            icon.sprite = toolItem.Type.GetToolImage();
            //启动Icon的GameObject
            iconObj.gameObject.SetActive(true);
        }
        //如果item类是FishItem类
        else if(item is PropItem propItem && item.itemIdentifier.Type == "PropItem")
        {
            //更新图片
            icon.sprite = propItem.Type.GetPropImage();
            //启动Icon的GameObject
            iconObj.gameObject.SetActive(true);
        }
        //如果这个item的数据为空的话，直接将这个格子下的icon关闭
        else
        {
            iconObj.gameObject.SetActive(false);
        }
    }
}
