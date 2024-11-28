using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// װ�����Ĺ��������̳���SlotContainer��
/// </summary>
public class EquipmentSlotContainer : SlotContainer
{
    /// <summary>
    /// �����ܿ�����
    /// </summary>
    public TotalController totalController;
    /// <summary>
    /// װ�������е����(Ĭ��Ϊnull)
    /// </summary>
    public GameObject fishRod;
    /// <summary>
    /// װ�������е�����(Ĭ��Ϊnull)
    /// </summary>
    public GameObject drift;
    /// <summary>
    /// ���һ�β�����ToolItem
    /// </summary>
    public ItemData lastToolItem;
    /// <summary>
    /// ���һ�β�����PropItem
    /// </summary>
    public ItemData lastPropItem;

    /// <summary>
    /// ע���ܿ�����
    /// </summary>
    public void RegisterTotalController()
    {
        //�ܿ�������ʼ��
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
        //ͬʱ������װ����ʼ��
        fishRod = null;
        drift = null;
    }

    /// <summary>
    /// ��������дˢ��װ�����ӵķ���                                               
    /// </summary>
    public override void RefreshSlotUI()
    {
        for (int i = 0; i < slotGameObjects.Length; i++)
        {
            slots[i].UpdateUI();
        }

        //�����Ҷ�װ�����ӵĲ���
        CheckEquipmentSlotAndEquip();
    }

    #region TestCode
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log(fishRod + "   " + drift);
        }
    }
    #endregion 

    /// <summary>
    /// �������װ�����ķ����������װ��������װ�������еĻ����͸��¶�Ӧλ�õ�GameObject
    /// </summary>
    public void CheckEquipmentSlotAndEquip()
    {
        //���װ����������ǿյĻ�
        if (slots[0].inventory_Database.list[slots[0].Index].itemIdentifier.Type == "ToolItem")
        {
            if (fishRod == null && slots[0].inventory_Database.list[slots[0].Index] is ToolItem toolItem)
            {
                //�������һ�β�����ToolItem
                lastToolItem = toolItem;
                //��ȡ���������
                string name = toolItem.Type.ToolName;
                //�Ӷ�������ó���Ӧ��GameObject
                fishRod = PoolManager.Instance.GetGameObjectFromPool(name);
                //������͵�Scale
                AdjustScaleFromPlayer(fishRod);
                //������õ����obj�Ķ�Ӧ��λ��
                SetGameObjectToParent.SetParentFromFirstLayerParent("Player", "FishRodPoint", fishRod);
                //���õ����������ʼ�����
                totalController.fishandCastController.EquipmentFishRod();
            }
            //ֱ�Ӹ����߸��ӽ�����Ʒ�����
            if (fishRod != null && slots[0].inventory_Database.list[slots[0].Index] is ToolItem toolItem1)
            {
                //�����ε�toolItem����ǰ�Ĳ�һ���Ļ����ͽ��н���
                if(lastToolItem != toolItem1)
                {
                    //�������һ�β�����ToolItem
                    lastToolItem = toolItem1;

                    //�Ƚ�ԭ��ͷŻض������
                    PoolManager.Instance.ReturnGameObjectToPool(fishRod);
                    SetGameObjectToParent.SetParent("Pool", fishRod);
                    //ж��һ�����
                    totalController.fishandCastController.UnEquipmentFishRod();                

                    //��ȡ�����������
                    string name = toolItem1.Type.ToolName;
                    //�Ӷ�������ó���Ӧ��GameObject
                    fishRod = PoolManager.Instance.GetGameObjectFromPool(name);
                    //������͵�Scale
                    AdjustScaleFromPlayer(fishRod);
                    //������õ����obj�Ķ�Ӧ��λ��
                    SetGameObjectToParent.SetParentFromFirstLayerParent("Player", "FishRodPoint", fishRod);
                    //���õ����������ʼ�����
                    totalController.fishandCastController.EquipmentFishRod();
                }
            }
        }
        //ֱ�ӽ���ʹ�װ�������������
        else if (slots[0].inventory_Database.list[slots[0].Index].itemIdentifier.Type == "defaultType" && fishRod != null)
        {
            PoolManager.Instance.ReturnGameObjectToPool(fishRod);
            SetGameObjectToParent.SetParent("Pool", fishRod);
            //������ÿ�
            fishRod = null;
            //ж�����
            totalController.fishandCastController.UnEquipmentFishRod();
        }

        //-----------------------------------------------------------------------------------------------------------------------//

        //��Ưװ���������PropItem����
        if (slots[1].inventory_Database.list[slots[1].Index].itemIdentifier.Type == "PropItem" && slots[1].inventory_Database.list[slots[1].Index] is PropItem propItem)
        {
            //���propItem��Ϊ����װ������û������
            if (drift == null)
            {
                //�������һ�β�����ToolItem
                lastPropItem = propItem;

                //��ȡ������������
                string name = propItem.Type.PropName;
                //�Ӷ�������ó���Ӧ��GameObject
                drift = PoolManager.Instance.GetGameObjectFromPool(name);
                //�������õ����obj�Ķ�Ӧ��λ��
                SetGameObjectToParent.SetParentFromFirstLayerParent("Player", "DriftPoint", drift);
                //���õ����������ʼ����Ư
                totalController.fishandCastController.EquipmentDrift();
            }
            //ֱ�Ӹ����߸��ӽ�����Ʒ�����
            if (drift != null)
            {
                //���propItem��lastpropItem�ǲ�һ���Ļ�
                if (lastPropItem != propItem)
                {
                    //�������һ�β�����ToolItem
                    lastPropItem = propItem;

                    //�Ƚ�ԭ�����Żض������
                    PoolManager.Instance.ReturnGameObjectToPool(drift);
                    SetGameObjectToParent.SetParent("Pool", drift);
                    //ж��һ������
                    totalController.fishandCastController.UnEquipmentDrift();

                    //��ȡ������������
                    string name = propItem.Type.PropName;
                    //�Ӷ�������ó���Ӧ��GameObject
                    drift = PoolManager.Instance.GetGameObjectFromPool(name);
                    //�������õ����obj�Ķ�Ӧ��λ��
                    SetGameObjectToParent.SetParentFromFirstLayerParent("Player", "DriftPoint", drift);
                    //���õ����������ʼ����Ư
                    totalController.fishandCastController.EquipmentDrift();
                }
            }

        }
        //ֱ�ӽ�������װ�������������
        else if (slots[1].inventory_Database.list[slots[1].Index].itemIdentifier.Type == "defaultType" && drift != null)
        {
            //ֱ�ӽ�drift�ͻض����
            PoolManager.Instance.ReturnGameObjectToPool(drift);
            SetGameObjectToParent.SetParent("Pool", drift);
            //����Ư�ÿ�
            drift = null;
            //ж����Ư
            totalController.fishandCastController.UnEquipmentDrift();
        }

        //TODO:��������λ

    }

    /// <summary>
    /// ��ȡ��ҵ�Scale����ֹ��װ����װ����ʱ���ַ��򲻶�
    /// </summary>
    /// <param name="obj">Ҫ������gameObject</param>
    public void AdjustScaleFromPlayer(GameObject obj)
    {
        //��ȡ�����
        GameObject player = SetGameObjectToParent.FindFromFirstLayer("Player").gameObject;
        //��ȡ����ҵı���Scale
        Vector3 playerScale = player.transform.localScale;
        //����Ҫ������gameObject���е���
        obj.transform.localScale = playerScale;
    }
}
