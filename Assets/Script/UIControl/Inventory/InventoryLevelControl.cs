//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

///// <summary>
///// ���Ʊ����ȼ�����������(���óɾ�̬��)
///// </summary>
//public static class InventoryLevelControl
//{
//    /// <summary>
//    /// ���������ȼ�
//    /// </summary>
//    public static void LevelUp()
//    {
//        if(InventoryManager.Instance.bacPackManager.inventoryLevel == InventoryLevel.Big)
//        {
//            Debug.Log("***�����Ѿ�����ߵȼ��ˣ������������ˣ�");
//        }
//        else
//        {
//            //�Ȼ�ȡ�������ȼ�ö����
//            int levelNum = (int)InventoryManager.Instance.backPackManager.inventoryLevel;
//            //Ȼ�����������
//            InventoryManager.Instance.backPackManager.inventoryLevel = (InventoryLevel)(levelNum + 1);
//            //����20����
//            for (int i = 0; i < 20; i++)
//            {
//                InventoryManager.Instance.backPackManager.fishItemList.Add(null);
//            }

//            Debug.Log("***���������ɹ���");
//        }
//    }

//    /// <summary>
//    /// �������͵ȼ�
//    /// </summary>
//    public static void LevelDown()
//    {
//        if(InventoryManager.Instance.backPackManager.inventoryLevel == InventoryLevel.Small)
//        {
//            Debug.Log("***�����ȼ��Ѿ�����͵��ˣ��޷��ٽ�����");
//        }
//        else
//        {
//            //�Ȼ�ȡ�������ȼ�ö����
//            int levelNum = (int)InventoryManager.Instance.backPackManager.inventoryLevel;
//            //Ȼ�����������
//            InventoryManager.Instance.backPackManager.inventoryLevel = (InventoryLevel)(levelNum - 1);
//            //�ӱ��������������Ϊ��ʼ����20����
//            for (int i = InventoryManager.Instance.backPackManager.fishItemList.Count - 1; i >= ((int)InventoryManager.Instance.backPackManager.inventoryLevel + 1) * 20; i--)
//            {
//                InventoryManager.Instance.backPackManager.fishItemList.RemoveAt(i);
//            }
//            Debug.Log("***���������ɹ���");
//        }
//    }
//}
