using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ��Ϸ�����ࣨ��Ϣ��ʼ����
/// </summary>
public class GameRoot : MonoBehaviour
{
    //Ҫ��ʼ��FishType���б�
    public List<FishType> fishTypeList;
    //Ҫ��ʼ��ToolType���б�
    public List<ToolType> toolTypeList;
    //Ҫ��ʼ��PropType���б�
    public List<PropType> propTypeList;
    //Ҫ��ʼ��BaseType���б�
    public List<BaitType> baitTypeList;

    //��ʼ��UI��Ϣ
    InitDisplayUI initDisplayUI;

    //��ʼ��Panel
    InitPanel initPanel;

    //������������
    TotalController totalController;
    

    private void Awake()
    {
        //��ʼ����Ʒ�б�
        InitList(fishTypeList, new FishTypeFactory());
        InitList(toolTypeList, new ToolTypeFactory());
        InitList(propTypeList, new PropTypeFactory());
        InitList(baitTypeList, new BaitTypeFactory());

        initDisplayUI = new InitDisplayUI("Prefab/UI/DisplayUI", "Prefab/UI/DisplayText");
        initDisplayUI.InitTypes();

        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();

        initPanel = new InitPanel("Prefab/UI/MainUI");
        initPanel.InitTypes(1);

        //�����浵����
        initPanel = new InitPanel("Prefab/UI/ArchiveInfoPanel");
        initPanel.InitTypes(20);
    }
    private void Start()
    {
        //��֡�����Ƶ�165֡
        Application.targetFrameRate = 165;
    }

    /// <summary>
    /// �Դ������ĳ�ʼ���б����г�ʼ��
    /// </summary>
    /// <typeparam name="T">�̳�BaseType����</typeparam>
    /// <param name="list">Ҫ��ʼ���б�</param>
    /// <param name="factory">��Ӧ��ʵ����������</param>
    public void InitList<T>(List<T> list, IItemFactory<T> factory) where T : BaseType
    {
        //�����������list��Ϊ�յĻ�
        if (list.Count == 0)
        {
            Debug.LogWarning($"{list.GetType().Name}���ǿյģ�������б��ǲ���������");
            return;
        }
        foreach (T type in list)
        {
            Debug.Log(type.obj);
            //�ӳ��󹤳����ȡ��Ӧ���ʵ��
            T buildType = factory.CreateItem(type.obj);
            //��ʵ������ItemManager�е��ֵ�
            ItemManager.Instance.AddOrGetType(buildType, buildType.obj);
        }
    }

    private void Update()
    {
        //------------------------------------------TestCode--------------------------------------------------//

        if (Input.GetKeyDown(KeyCode.V))
        {
            //��������ֵ��е�һ������
            int randomNumber = Random.Range(0, ItemManager.Instance.GetDictionary<FishType>().Count - 1);
            //ʹ��ElementAt������ʼ�ֵ��
            var element = ItemManager.Instance.GetDictionary<FishType>().ElementAt(randomNumber);

            //���������ĳ���
            double fishLength = System.Math.Round(Random.Range(1f, 5f), 2);
            //��������������
            double fishWeight = System.Math.Round(Random.Range(1f, 10f), 2);

            //����Inventory
            InventoryManager.Instance.backpackManager.AddItemInList(new FishItem(element.Key, fishLength, fishWeight));
            //Ȼ�����UI
            InventoryManager.Instance.backpackSlotContainer.RefreshSlotUI();
        }
        //TODO: (����)ɾ�������е���        
        if (Input.GetKeyDown(KeyCode.N))
        {
            InventoryManager.Instance.backpackManager.DeleteLastItemInList();
            //Ȼ�����UI
            InventoryManager.Instance.backpackSlotContainer.RefreshSlotUI();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            //��������ֵ��е�һ������
            int randomNumber = Random.Range(0, ItemManager.Instance.GetDictionary<ToolType>().Count - 1);
            //ʹ��ElementAt������ʼ�ֵ��
            var element = ItemManager.Instance.GetDictionary<ToolType>().ElementAt(randomNumber);
            //�������һ������Ʒ������
            int randomNumber2 = Random.Range(0, 4);

            //����Inventory
            InventoryManager.Instance.toolpackManager.AddItemInList(new ToolItem(element.Key, (ToolQuality)randomNumber2));
            //Ȼ�����UI
            InventoryManager.Instance.toolpackSlotContainer.RefreshSlotUI();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //��������ֵ��е�һ������
            int randomNumber = Random.Range(0, ItemManager.Instance.GetDictionary<PropType>().Count - 1);
            //ʹ��ElementAt������ʼ�ֵ��
            var element = ItemManager.Instance.GetDictionary<PropType>().ElementAt(randomNumber);
            //�������һ������Ʒ������
            int randomNumber2 = Random.Range(0, 4);

            //����Inventory
            InventoryManager.Instance.proppackManager.AddItemInList(new PropItem(element.Key, (PropQuality)randomNumber2));
            //Ȼ�����UI
            InventoryManager.Instance.proppackSlotContainer.RefreshSlotUI();
        }

        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            //��������ֵ��е�һ������
            int randomNumber = Random.Range(0, ItemManager.Instance.GetDictionary<BaitType>().Count - 1);
            //ʹ��ElementAt������ʼ�ֵ��
            var element = ItemManager.Instance.GetDictionary<BaitType>().ElementAt(randomNumber);

            //����Inventory
            InventoryManager.Instance.proppackManager.AddItemInList(new BaitItem(element.Key, 99, 1), 1);
            //Ȼ�����UI
            InventoryManager.Instance.proppackSlotContainer.RefreshSlotUI();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            InventoryManager.Instance.proppackManager.DeleteLastItemInList();
            //Ȼ�����UI
            InventoryManager.Instance.proppackSlotContainer.RefreshSlotUI();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ItemManager.Instance.CheckDictionary();
        }
    }
}
