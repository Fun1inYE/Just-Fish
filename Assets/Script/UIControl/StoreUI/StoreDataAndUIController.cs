using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreDataAndUIController : MonoBehaviour
{
    /// <summary>
    /// �����̵�UI
    /// </summary>
    public StoreUI storeUI;
    /// <summary>
    /// �����̵�����
    /// </summary>
    public StoreData storeData;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        storeData = new StoreData();

        storeUI = GetComponent<StoreUI>();
    }

    /// <summary>
    /// ��StoreUI����StoreData
    /// </summary>
    public void Start()
    {
        storeUI.SetStoreData(storeData);
    }

    /// <summary>
    /// ������Ʒһ�ļ۸�
    /// </summary>
    /// <param name="price"></param>
    public void ChangePrice_Slot0(int price)
    {
        storeData.NodifyPrice_Slot0(price);
    }

    /// <summary>
    /// ������Ʒ���ļ۸�
    /// </summary>
    /// <param name="price"></param>
    public void ChangePrice_Slot1(int price)
    {
        storeData.NodifyPrice_Slot1(price);
    }

    /// <summary>
    /// ������Ʒ���ļ۸�
    /// </summary>
    /// <param name="price"></param>
    public void ChangePrice_Slot2(int price)
    {
        storeData.NodifyPrice_Slot2(price);
    }
}
