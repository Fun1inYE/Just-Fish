using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��ʱˢ����Ʒ�ķ�������Ϊ��Ҫһֱˢ�£�����Ҫ����һ������ص�StoreCanvas��
/// </summary>
public class RefreshProduct : MonoBehaviour
{
    /// <summary>
    /// ���ü�ʱ��
    /// </summary>
    public Timer timer;
    /// <summary>
    /// ˢ��һ����Ʒ��Ҫ��ʱ�䣨Ĭ��10s��
    /// </summary>
    public float refreshProductTime = 10f;
    
    /// <summary>
    /// ���ù�����ӵ�����
    /// </summary>
    public BuySlotContainer buySlotContainer;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        //Ȼ�����gameObject����Ӽ�ʱ��
        timer = gameObject.AddComponent<Timer>();
        //��ȡBuySlotContainer�ű�
        buySlotContainer = SetGameObjectToParent.FindChildRecursive(transform, "StoreContent").GetComponent<BuySlotContainer>();
    }

    public void Start()
    {
        //��ˢ����Ʒ�ķ���ע�ᵽ��ʱ������
        timer.OnTimerFinished += buySlotContainer.RefreshProduct;
    }
}
