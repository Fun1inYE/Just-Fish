using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ���ڱ���UI�Ŀ�������ӿ�
/// </summary>
public interface IinventoryCommand
{
    /// <summary>
    /// ִ������ķ���
    /// </summary>
    public void Execute();
}

/// <summary>
/// ���ر����Ĳ���
/// </summary>
public class OpenAndCloseInventory : IinventoryCommand
{
    /// <summary>
    /// �������
    /// </summary>
    public GameObject inventoryPanel;
    /// <summary>
    /// ������RectTransform
    /// </summary>
    public RectTransform inventoryRectTransform;
    /// <summary>
    /// �жϱ����Ƿ񱻴�, ͨ����紫ֵ��Ĭ����false��
    /// </summary>
    public bool isOpen;
    /// <summary>
    /// ���캯��
    /// </summary>
    /// <param name="inventoryPanel">�������</param>
    public OpenAndCloseInventory(GameObject inventoryPanel, RectTransform inventoryRectTransform, bool isOpen)
    {
        this.inventoryPanel = inventoryPanel;
        this.inventoryRectTransform = inventoryRectTransform;
        this.isOpen = isOpen;
    }
    //ִ������
    public void Execute()
    {
        //�򿪱���panel��GameObject
        inventoryPanel.SetActive(isOpen);

        //���isOpenΪtrue�Ļ����͸���һ��backpackContainer
        if (isOpen == true)
        {
            InventoryManager.Instance.RefreshAllContainer();
        }

        //���߹۲챳�����̵�Ľű��������������߹ر���
        TotalController.Instance.UIChangedNotify();
    }
}

/// <summary>
/// �л�������������
/// </summary>
public class OpenInventoryContent : IinventoryCommand
{
    /// <summary>
    /// ������UI����
    /// </summary>
    public GameObject backpackContent;
    /// <summary>
    /// ���߰���UI����
    /// </summary>
    public GameObject toolpackContent;
    /// <summary>
    /// ���߰���UI����
    /// </summary>
    public GameObject proppackContent;
    /// <summary>
    /// ���������Ƿ���(Ĭ��Ϊfalse)
    /// </summary>
    bool isBackPackOpen = false;
    /// <summary>
    /// ���߰������Ƿ�����(Ĭ��Ϊfalse)
    /// </summary>
    bool isToolPackOpen = false;
    /// <summary>
    /// ���߰��Ƿ�����(Ĭ��Ϊfalse)
    /// </summary>
    bool isPropPackOpen = false;
    /// <summary>
    /// ���캯��
    /// </summary>
    public OpenInventoryContent(bool isBackPackOpen, bool isToolPackOpen, bool isPropPackOpen, GameObject backpackContent, GameObject toolpackContent, GameObject proppackContent)
    {
        this.isBackPackOpen = isBackPackOpen;
        this.isToolPackOpen = isToolPackOpen;
        this.isPropPackOpen = isPropPackOpen;
        this.backpackContent = backpackContent;
        this.toolpackContent = toolpackContent;
        this.proppackContent = proppackContent;
    }
    /// <summary>
    /// ִ������
    /// </summary>
    public void Execute()
    {
        //�򿪻��߹رմ���
        backpackContent.SetActive(isBackPackOpen);
        toolpackContent.SetActive(isToolPackOpen);
        proppackContent.SetActive(isPropPackOpen);
        //ת�괰��ˢ��һ��UI
        InventoryManager.Instance.RefreshAllContainer();
    }
}
