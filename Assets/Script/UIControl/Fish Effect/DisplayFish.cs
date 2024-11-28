using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

/// <summary>
/// �ڵ���ɹ�����õķ���
/// </summary>
public class DisplayFish
{
    /// <summary>
    /// ��Ҫ���ٵ���Ư
    /// </summary>
    public GameObject needFollowDrift;
    /// <summary>
    /// ���β��������GameObject
    /// </summary>
    public GameObject fishGameObject;
    /// <summary>
    /// ��ĳ���
    /// </summary>
    public double fishLength;
    /// <summary>
    /// �������
    /// </summary>
    public double fishWeight;

    /// <summary>
    /// ���캯��
    /// </summary>
    /// <param name="needFollowDrift"></param>
    public DisplayFish(GameObject needFollowDrift)
    {
        this.needFollowDrift = needFollowDrift;
    }

    /// <summary>
    /// ȡ�����GameObject��������GameObject
    /// </summary>
    public void GetAndProcessingFishGameObject()
    {
        RandomGetFishGameObjectAndSetting();
    }

    /// <summary>
    /// �����GameObject�ķŻس���
    /// </summary>
    public void ReplaceFishGameObject()
    {
        //���fishGameObject��Ϊ�յĻ��������Ƿ�ֹ�����Fishing״̬��ʱ�������ո˵���fishGameObjectΪ�յı���
        if (fishGameObject != null)
        {
            //��GameObject�Żس���
            PoolManager.Instance.ReturnGameObjectToPool(fishGameObject);
            //��GameObject�ƶ���Pool��
            SetGameObjectToParent.SetParent("Pool", fishGameObject);
        }
    }

    /// <summary>
    /// ����õ�FishManager���ֵ��е������Ϣ��Ȼ����GameObject��Ȼ�������������
    /// </summary>
    public void RandomGetFishGameObjectAndSetting()
    {
        //��������ֵ��е�һ������
        int randomNumber = Random.Range(0, ItemManager.Instance.GetDictionary<FishType>().Count - 1);
        //ʹ��ElementAt������ʼ�ֵ��
        var element = ItemManager.Instance.GetDictionary<FishType>().ElementAt(randomNumber);
        //�ӳ�����ȡ��GameObject
        fishGameObject = PoolManager.Instance.GetGameObjectFromPool(element.Value.name);
        Debug.Log($"ȡ���ɹ���ȡ����һ��{fishGameObject.name}");

        //����GameObject
        InitFishGameObject(fishGameObject);

        //���������ĳ���
        fishLength = System.Math.Round(Random.Range(1f, 5f), 2);
        //��������������
        fishWeight = System.Math.Round(Random.Range(1f, 10f), 2);

        //����Inventory
        InventoryManager.Instance.backpackManager.AddItemInList(new FishItem(element.Key, fishLength, fishWeight));
        //Ȼ�����UI
        InventoryManager.Instance.backpackSlotContainer.RefreshSlotUI();
    }

    /// <summary>
    /// ���������ɵ�FishGameObject�ĸ�������
    /// </summary>
    public void InitFishGameObject(GameObject fishObj)
    {
        //�����趨gameObject�ı���Scale
        fishObj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        //�����GameObject����driftλ����
        fishObj.transform.SetParent(needFollowDrift.transform, true);
        //���µ���GameObject�ı�������Ϊԭ������
        fishObj.transform.localPosition = new Vector3(0, 0, 0);
        //����GameObject
        fishObj.SetActive(true);
    }
}
