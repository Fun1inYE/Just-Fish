using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �̵��UI
/// </summary>

public class StoreUI : MonoBehaviour
{
    /// <summary>
    /// ������Ʒ�۸�
    /// </summary>
    public Text price_tool;
    /// <summary>
    /// ��һ�����߼۸�
    /// </summary>
    public Text price_prop1;
    /// <summary>
    /// �ڶ������߼۸�
    /// </summary>
    public Text price_prop2;

    /// <summary>
    /// ����Store������
    /// </summary>
    public StoreData storeData;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        //Ѱ���̵��һ��slot
        GameObject StoreSlot = SetGameObjectToParent.FindChildRecursive(transform, "StoreSlot").gameObject;
        price_tool = ComponentFinder.GetChildComponent<Text>(StoreSlot, "NeedCoin");
        //Ѱ���̵�ڶ���slot
        GameObject StoreSlot1 = SetGameObjectToParent.FindChildRecursive(transform, "StoreSlot (1)").gameObject;
        price_prop1 = ComponentFinder.GetChildComponent<Text>(StoreSlot1, "NeedCoin");
        //Ѱ���̵������slot
        GameObject StoreSlot2 = SetGameObjectToParent.FindChildRecursive(transform, "StoreSlot (2)").gameObject;
        price_prop2 = ComponentFinder.GetChildComponent<Text>(StoreSlot2, "NeedCoin");
    }

    /// <summary>
    /// �趨�̵�����ݣ�Ȼ���StoreUI�����¼�����ע��
    /// </summary>
    /// <param name="data"></param>
    public void SetStoreData(StoreData data)
    {
        storeData = data;
        //����UI
        UpdataUI();
        //ע��UI�����¼�
        storeData.OnPriceChange += UpdataUI;
    }

    /// <summary>
    /// ����UI
    /// </summary>
    public void UpdataUI()
    {
        price_tool.text = storeData.price_slot0.ToString();
        price_prop1.text = storeData.price_slot1.ToString();
        price_prop2.text = storeData.price_slot2.ToString();
    }
}
