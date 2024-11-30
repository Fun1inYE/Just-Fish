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
    /// 每个格子的GameObject
    /// </summary>
    public GameObject slotObj;
    /// <summary>
    /// 每个Icon的Image组件
    /// </summary>
    public Image icon;
    /// <summary>
    /// 每个格子存取的数目的Text组件
    /// </summary>
    public Text itemCountText;
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
    /// 脚本初始化
    /// </summary>
    public void InitEverySlot()
    {
        //找到slot下的组件
        if (slotObj != null)
        {
            icon = SetGameObjectToParent.FindChildBreadthFirst(slotObj.transform, "Icon").GetComponent<Image>();

            if(icon != null)
            {
                //脚本启动后先将gameObject和sprite全部关闭
                icon.gameObject.SetActive(false);
                //获取到数量显示Text
                itemCountText = SetGameObjectToParent.FindChildBreadthFirst(icon.gameObject.transform, "ItemCount").GetComponent<Text>();
                //先关闭数量显示Text
                itemCountText.gameObject.SetActive(false);
            }

            //只有buy格子不需要拖拽脚本
            if(slotType != SlotType.Buy)
            {
                //获取到拖拽脚本
                dragItemScript = ComponentFinder.GetOrAddComponent<DragItem>(icon.gameObject);
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
        UpdateSlot(item);
    }

    /// <summary>
    /// 通过传进来的ItemData基类判断属于哪种物品类型，然后进行更新Icon(主要是因为item是ItemData类型的，无法直接转换成对应物品类型)
    /// </summary>
    /// <param name="items"></param>
    public void UpdateSlot(ItemData item)
    {
        //如果item类是FishItem类
        if (item is FishItem fishItem)
        {
            UpdateIconAndAmountUI<FishItem>(fishItem);
        }
        //如果item类是ToolItem类
        else if (item is ToolItem toolItem)
        {
            UpdateIconAndAmountUI<ToolItem>(toolItem);
        }
        //如果item类是FishItem类
        else if (item is PropItem propItem)
        {
            UpdateIconAndAmountUI<PropItem>(propItem);
        }
        else if (item is BaitItem baitItem)
        {
            UpdateIconAndAmountUI<BaitItem>(baitItem);
        }
        //如果这个item的数据为空的话，直接将这个格子下的icon关闭
        else
        {
            icon.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 根据穿过来的数据更新Icon和数量UI
    /// </summary>
    /// <typeparam name="T">继承了ItemData的Item类</typeparam>
    /// <param name="item">传进来的ItemData</param>
    public void UpdateIconAndAmountUI<T>(ItemData item) where T : ItemData
    {
        if (item is T typeItem)
        {
            //获取图片
            icon.sprite = typeItem.type.GetImage();
            icon.gameObject.SetActive(true);
            //如果传进来的item中物品数量大于1
            if (item.amount > 1)
            {
                itemCountText.text = item.amount.ToString();
                itemCountText.gameObject.SetActive(true);
            }
            //如果物品数量等于1就关闭数字显示
            else if (item.amount == 1)
            {
                itemCountText.text = item.amount.ToString();
                itemCountText.gameObject.SetActive(false);
            }
            //如果物品等于0的话
            else
            {
                itemCountText.text = item.amount.ToString();
                //关闭文字UI
                itemCountText.gameObject.SetActive(false);
                //关闭图片显示
                icon.gameObject.SetActive(false);
            }
        }
    }
}
