using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

/// <summary>
/// �̵���صĲ�������
/// </summary>
public interface StoreCommand
{
    /// <summary>
    /// ִ������ķ���
    /// </summary>
    public void Execute();
}

/// <summary>
/// ִ�п����̵�ѡ�����ķ���
/// </summary>
public class OpenOrCloseOptionCommand : StoreCommand
{
    /// <summary>
    /// �̵����͵�ѡ�����
    /// </summary>
    public RectTransform optionPanel;
    /// <summary>
    /// �ж�ѡ������Ƿ��
    /// </summary>
    public bool isOpen;

    public OpenOrCloseOptionCommand(RectTransform optionPanel, bool isOpen)
    {
        this.optionPanel = optionPanel;
        this.isOpen = isOpen;
    }

    public void Execute()
    {
        optionPanel.gameObject.SetActive(isOpen);
    }
}


/// <summary>
/// �����̵������
/// </summary>
public class OpenOrCloseStoreCommand : StoreCommand
{
    /// <summary>
    /// �̵��GameObject
    /// </summary>
    public RectTransform storePanel;
    /// <summary>
    /// �̵��Ƿ�򿪣�Ĭ��false��
    /// </summary>
    public bool isOpen = false;

    /// <summary>
    /// ���캯��
    /// </summary>
    /// <param name="storePanel"></param>
    /// <param name="isOpen"></param>
    public OpenOrCloseStoreCommand(RectTransform storePanel, bool isOpen)
    {
        this.storePanel = storePanel;
        this.isOpen = isOpen;
    }

    /// <summary>
    /// ִ������
    /// </summary>
    public void Execute()
    {
        storePanel.gameObject.SetActive(isOpen);

        //���߹۲챳�����̵�Ľű��������������߹ر���
        TotalController.Instance.UIChangedNotify();
    }
}

/// <summary>
/// �����������������
/// </summary>
public class OpenOrCloseSaleCommand : StoreCommand
{
    /// <summary>
    /// ������
    /// </summary>
    public GameObject inventoryPanel;
    /// <summary>
    /// �ж����������Ƿ��
    /// </summary>
    public bool isOpen;

    /// <summary>
    /// ���캯��
    /// </summary>
    public OpenOrCloseSaleCommand(GameObject inventoryPanel, bool isOpen)
    {
        this.inventoryPanel = inventoryPanel;
        this.isOpen = isOpen;
    }

    /// <summary>
    /// ִ������
    /// </summary>
    public void Execute()
    {
        inventoryPanel.SetActive(isOpen);
        //ˢ����������UI
        InventoryManager.Instance.saleSlotContainer.RefreshSlotUI();

        //���߹۲챳�����̵�Ľű��������������߹ر���
        TotalController.Instance.UIChangedNotify();
    }
}