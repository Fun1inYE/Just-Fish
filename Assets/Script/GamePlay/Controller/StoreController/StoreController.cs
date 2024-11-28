using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �̵����صĿ�����
/// </summary>
public class StoreController : MonoBehaviour, IController
{
    //(�������ڱ�����һ����)
    /// <summary>
    /// �������GameObject
    /// </summary>
    public RectTransform inventoryPanelRectTransform;
    /// <summary>
    /// ��ȡ������װ������ҳ��
    /// </summary>
    public RectTransform detailPanel;
    /// <summary>
    /// ��ȡ���������
    /// </summary>
    public RectTransform salePanel;

    /// <summary>
    /// �̵��GameObject
    /// </summary>
    public RectTransform storePanel;
    /// <summary>
    /// �ж��̵��Ƿ����ڿ�����Ĭ��false��
    /// </summary>
    public bool storePanelisOpening = false;
    /// <summary>
    /// �ж���������Ƿ����ڿ�����Ĭ��false��
    /// </summary>
    public bool salePanelisOpening = false;
    /// <summary>
    /// �ж��̵�ѡ������Ƿ�����Ĭ��false��
    /// </summary>
    public bool optionPanelisOpening = false;

    /// <summary>
    /// �̵����͵�ѡ�����
    /// </summary>
    public RectTransform optionPanel;
    /// <summary>
    /// ���̵갴ť
    /// </summary>
    public Button buyButton;
    /// <summary>
    /// ������Ʒ��ť
    /// </summary>
    public Button saleButton;

    /// <summary>
    /// ��ȡ���̵���ؽű�
    /// </summary>
    public Store store;

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
    /// �̵������б�
    /// </summary>
    public List<StoreCommand> commandList;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        inventoryPanelRectTransform = SetGameObjectToParent.FindChildBreadthFirst(SetGameObjectToParent.FindFromFirstLayer("InventoryCanvas").transform, "InventoryPanel").GetComponent<RectTransform>();
        detailPanel = ComponentFinder.GetChildComponent<RectTransform>(inventoryPanelRectTransform.gameObject, "DetailPanel");
        salePanel = ComponentFinder.GetChildComponent<RectTransform>(inventoryPanelRectTransform.gameObject, "SalePanel");

        storePanel = transform.Find("StorePanel").GetComponent<RectTransform>();
        optionPanel = transform.Find("OptionsPanel").GetComponent<RectTransform>();

        buyButton = optionPanel.transform.GetChild(0).GetComponent<Button>();
        saleButton = optionPanel.transform.GetChild(1).GetComponent<Button>();

        store = ComponentFinder.GetChildComponent<Store>(SetGameObjectToParent.FindFromFirstLayer("Prop"), "Store");


        commandList = new List<StoreCommand>();
    }

    /// <summary>
    /// �󶨰�ť�����¼�
    /// </summary>
    public void Start()
    {
        buyButton.onClick.AddListener(OpenBuyPanel);
        saleButton.onClick.AddListener(OpenInventoryWithSalePanel);
    }

    /// <summary>
    /// ���̵�ѡ����������
    /// </summary>
    public void OpenOrCloseOptionPanel()
    {
        //����̵������������涼û�п������Ϳ��Կ���ѡ�����
        if (Input.GetKeyDown(KeyCode.E) && salePanelisOpening == false && storePanelisOpening == false && salePanelisOpening == false && store.wasEnterStore)
        {
            //ת��optionPanelisOpening��״̬
            optionPanelisOpening = !optionPanelisOpening;
            commandList.Add(new OpenOrCloseOptionCommand(optionPanel, optionPanelisOpening));
        }
    }

    /// <summary>
    /// ���̵���������
    /// </summary>
    public void OpenBuyPanel()
    {
        //�ж��̵�����Ƿ��ǿ����ģ����ǿ����ľͿ��Կ���
        if(storePanelisOpening == false)
        {
            storePanelisOpening = true;
            //�ٿ����̵����֮ǰ��Ҫ�Ȱ�ѡ�����ر�
            IndividualTrigger_CloseOptionPanel();
            commandList.Add(new OpenOrCloseStoreCommand(storePanel, true));
        }
    }

    /// <summary>
    /// �ر��̵���������
    /// </summary>
    public void CloseBuyPanel()
    {
        //�ж��̵�����Ƿ��ǿ����ģ�����ǿ����ľͿ��Թر� (��esc�ر�)
        if (Input.GetKeyDown(KeyCode.Escape) && storePanelisOpening == true)
        {
            storePanelisOpening = false;
            commandList.Add(new OpenOrCloseStoreCommand(storePanel, false));
        }
    }

    /// <summary>
    /// ����������ķ���
    /// </summary>
    public void OpenInventoryWithSalePanel()
    {
        //����������û�б��򿪵�ʱ��
        if(salePanelisOpening == false)
        {
            //�ڿ���������֮ǰ��Ҫ�Ȱ�ѡ�����ر�
            IndividualTrigger_CloseOptionPanel();
            detailPanel.gameObject.SetActive(false);
            salePanel.gameObject.SetActive(true);
            salePanelisOpening = true;
            commandList.Add(new OpenOrCloseSaleCommand(inventoryPanelRectTransform.gameObject, true));
        }
    }

    /// <summary>
    /// �ر�����ҳ��ķ���
    /// </summary>
    public void CloseInventoryWithSalePanel()
    {
        //������esc��������������򿪵�ʱ��
        if (Input.GetKeyDown(KeyCode.Escape) && salePanelisOpening == true)
        {
            detailPanel.gameObject.SetActive(true);
            salePanel.gameObject.SetActive(false);
            salePanelisOpening = false;
            commandList.Add(new OpenOrCloseSaleCommand(inventoryPanelRectTransform.gameObject, false));
        }
        
    }

    /// <summary>
    /// ���������ر�ѡ���״̬
    /// </summary>
    public void IndividualTrigger_CloseOptionPanel()
    {
        //ת��optionPanelisOpening��״̬
        optionPanelisOpening = false;
        commandList.Add(new OpenOrCloseOptionCommand(optionPanel, false));
    }

    /// <summary>
    /// ���������رչ������ķ���
    /// </summary>
    public void IndividualTrigger_CloseBuyPanel()
    {
        storePanelisOpening = false;
        commandList.Add(new OpenOrCloseStoreCommand(storePanel, false));
    }

    /// <summary>
    /// ���������ر���������
    /// </summary>
    public void IndividualTrigger_CloseInventoryWithSalePanel()
    {
        detailPanel.gameObject.SetActive(true);
        salePanel.gameObject.SetActive(false);
        salePanelisOpening = false;
        commandList.Add(new OpenOrCloseSaleCommand(inventoryPanelRectTransform.gameObject, false));
    }

    /// <summary>
    /// ִ�������б�
    /// </summary>
    public void runCommandList()
    {
        //������һ֡�����е������ִ�������е�Execute����
        foreach (StoreCommand command in commandList)
        {
            command.Execute();
        }
        //ִ����֮������б�
        commandList.Clear();
    }
}
