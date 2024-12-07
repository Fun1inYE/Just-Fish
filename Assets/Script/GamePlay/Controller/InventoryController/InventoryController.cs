using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����UI�Ŀ�����
/// </summary>
public class InventoryController : MonoBehaviour, IController
{
    /// <summary>
    /// �������GameObject
    /// </summary>
    public GameObject inventoryPanel;
    /// <summary>
    /// �������RectTransform
    /// </summary>
    public RectTransform inventoryRectTransform;
    /// <summary>
    /// ��ȡ������װ������ҳ��
    /// </summary>
    public RectTransform detailPanel;
    /// <summary>
    /// ��ȡ���������
    /// </summary>
    public RectTransform salePanel;
    /// <summary>
    /// �жϿ���Ƿ��(Ĭ��Ϊfalse)
    /// </summary>
    public bool isOpen = false;
    /// <summary>
    /// ��ȡ��������ť
    /// </summary>
    public Button backpackBtn;
    /// <summary>
    /// ��ȡ�����߱�����ť
    /// </summary>
    public Button toolpackBtn;
    /// <summary>
    /// ��ȡ�����߱�����ť
    /// </summary>
    public Button proppcakBtn;
    /// <summary>
    /// ������Content
    /// </summary>
    public GameObject backpackContent;
    /// <summary>
    /// ���߰���Content
    /// </summary>
    public GameObject toolpackContent;
    /// <summary>
    /// ���߰���Content
    /// </summary>
    public GameObject proppcakContent;

    public List<IUIObserver> oberverlist;

    /// <summary>
    /// ������ICommand�ӿڣ��жϸÿ������Ƿ�������
    /// </summary>
    private bool _canRun = true;
    public bool canRun
    {
        get { return _canRun; }
        set { _canRun = value; }
    }

    /// <summary>
    /// �����б�
    /// </summary>
    public List<IinventoryCommand> commandList { get; private set; }

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    private void Awake()
    {
        inventoryPanel = SetGameObjectToParent.FindChildRecursive(transform, "InventoryPanel").gameObject;
        if(inventoryPanel == null)
        {
            Debug.LogError("inventoryPanel�ǿյģ��������!");
        }

        detailPanel = ComponentFinder.GetChildComponent<RectTransform>(inventoryPanel, "DetailPanel");
        salePanel = ComponentFinder.GetChildComponent<RectTransform>(inventoryPanel, "SalePanel");

        //��ť��ʼ��
        backpackBtn = ComponentFinder.GetChildComponent<Button>(gameObject, "BackPackButton");
        toolpackBtn = ComponentFinder.GetChildComponent<Button>(gameObject, "ToolPackButton");
        proppcakBtn = ComponentFinder.GetChildComponent<Button>(gameObject, "PropPackButton");

        //UI������ʼ��
        backpackContent = ComponentFinder.GetChildComponent<SlotContainer>(gameObject, "Content").gameObject;
        toolpackContent = ComponentFinder.GetChildComponent<SlotContainer>(gameObject, "ToolItemListPanel").gameObject;
        proppcakContent = ComponentFinder.GetChildComponent<SlotContainer>(gameObject, "PropItemListPanel").gameObject;

        inventoryRectTransform = inventoryPanel.GetComponent<RectTransform>();
        if(inventoryRectTransform == null)
        {
            Debug.LogError("inventoryRectTransform�ǿյģ��������!");
        }

        oberverlist = new List<IUIObserver>();
        commandList = new List<IinventoryCommand>();
    }

    private void Start()
    {
        //����ť��Ӽ����¼�
        backpackBtn.onClick.AddListener(OpenBackPack);
        toolpackBtn.onClick.AddListener(OpenToolPack);
        proppcakBtn.onClick.AddListener(OpenPropPack);
    }

    /// <summary>
    /// ���Ʊ������صķ������ұ�Ϊװ�����飨����ģʽ��
    /// </summary>
    public void OpenAndCloseInventoryWithDetailPanel()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            isOpen = !isOpen;
            //��װ�����
            detailPanel.gameObject.SetActive(true);
            //�ر��������
            salePanel.gameObject.SetActive(false);
            commandList.Add(new OpenAndCloseInventory(inventoryPanel, inventoryRectTransform, isOpen));
        }
        //ֻ�йرյķ���
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isOpen = false;
            //��װ�����
            detailPanel.gameObject.SetActive(true);
            //�ر��������
            salePanel.gameObject.SetActive(false);
            commandList.Add(new OpenAndCloseInventory(inventoryPanel, inventoryRectTransform, isOpen));
        }
    }

    /// <summary>
    /// �򿪱���Content�ķ���������ģʽ��
    /// </summary>
    public void OpenBackPack()
    {
        commandList.Add(new OpenInventoryContent(true, false, false, backpackContent, toolpackContent, proppcakContent));
    }
    /// <summary>
    /// �򿪱���Content�ķ���������ģʽ��
    /// </summary>
    public void OpenToolPack()
    {
        commandList.Add(new OpenInventoryContent(false, true, false, backpackContent, toolpackContent, proppcakContent));
    }
    /// <summary>
    /// �򿪹��߰�Content�ķ���
    /// </summary>
    public void OpenPropPack()
    {
        commandList.Add(new OpenInventoryContent(false, false, true, backpackContent, toolpackContent, proppcakContent));
    }

    /// <summary>
    /// ִ�������б�
    /// </summary>
    public void runCommandList()
    {
        //������һ֡�����е������ִ�������е�Execute����
        foreach (IinventoryCommand command in commandList)
        {
            command.Execute();
        }
        //ִ����֮������б�
        commandList.Clear();
    }
}
