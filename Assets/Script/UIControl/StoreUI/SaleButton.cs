using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaleButton : MonoBehaviour
{
    /// <summary>
    /// ������ť
    /// </summary>
    public Button saleButton;

    /// <summary>
    /// �����ܿ�����
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        saleButton = GetComponent<Button>();
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
    }

    /// <summary>
    /// �󶨰�ť�����¼�
    /// </summary>
    public void OnEnable()
    {
        saleButton.onClick.AddListener(SaleItem);
    }

    public void OnDisable()
    {
        //�Ƴ�button�ļ����¼�
        saleButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// ������Ʒ�ķ���
    /// </summary>
    public void SaleItem()
    {
        //�����ǰ����Ǯ�Ҵ���0�Ļ�
        if(totalController.playerDataAndUIController.playerData.sallingCoin > 0)
        {
            //����������Ч
            AudioManager.Instance.PlayAudio("SaleItem");
            //��Ǯ�Ҽ������������
            totalController.playerDataAndUIController.ChangeCoin(totalController.playerDataAndUIController.playerData.sallingCoin);
        }
        
        //�Ƚ�sallingCoin����
        totalController.playerDataAndUIController.ChangeSallingCoinToZero();

        //���saleManager�е���Ʒ����
        InventoryManager.Instance.saleManager.DeletAllItemInList();
        //ˢ��saleSlotUI
        InventoryManager.Instance.saleSlotContainer.RefreshSlotUI();
    }

}
