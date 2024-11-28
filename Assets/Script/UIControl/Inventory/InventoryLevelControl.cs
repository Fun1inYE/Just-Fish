//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

///// <summary>
///// 控制背包等级和容量的类(设置成静态类)
///// </summary>
//public static class InventoryLevelControl
//{
//    /// <summary>
//    /// 背包提升等级
//    /// </summary>
//    public static void LevelUp()
//    {
//        if(InventoryManager.Instance.bacPackManager.inventoryLevel == InventoryLevel.Big)
//        {
//            Debug.Log("***背包已经是最高等级了，不能再升级了！");
//        }
//        else
//        {
//            //先获取到背包等级枚举数
//            int levelNum = (int)InventoryManager.Instance.backPackManager.inventoryLevel;
//            //然后给背包升级
//            InventoryManager.Instance.backPackManager.inventoryLevel = (InventoryLevel)(levelNum + 1);
//            //提升20容量
//            for (int i = 0; i < 20; i++)
//            {
//                InventoryManager.Instance.backPackManager.fishItemList.Add(null);
//            }

//            Debug.Log("***背包升级成功！");
//        }
//    }

//    /// <summary>
//    /// 背包减低等级
//    /// </summary>
//    public static void LevelDown()
//    {
//        if(InventoryManager.Instance.backPackManager.inventoryLevel == InventoryLevel.Small)
//        {
//            Debug.Log("***背包等级已经是最低的了，无法再降级了");
//        }
//        else
//        {
//            //先获取到背包等级枚举数
//            int levelNum = (int)InventoryManager.Instance.backPackManager.inventoryLevel;
//            //然后给背包降级
//            InventoryManager.Instance.backPackManager.inventoryLevel = (InventoryLevel)(levelNum - 1);
//            //从背包数据最后以以为开始减少20容量
//            for (int i = InventoryManager.Instance.backPackManager.fishItemList.Count - 1; i >= ((int)InventoryManager.Instance.backPackManager.inventoryLevel + 1) * 20; i--)
//            {
//                InventoryManager.Instance.backPackManager.fishItemList.RemoveAt(i);
//            }
//            Debug.Log("***背包降级成功！");
//        }
//    }
//}
