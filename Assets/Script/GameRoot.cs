using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ��Ϸ�����ࣨ��Ϣ��ʼ����
/// </summary>
public class GameRoot : MonoBehaviour
{
    //��ʼ��������Ϣ
    InitType<FishType> initFishType;
    //��ʼ��������Ϣ
    InitType<ToolType> initToolType;
    //��ʼ��������Ϣ
    InitType<PropType> initProptype;
    //��ʼ��UI��Ϣ
    InitDisplayUI initDisplayUI;

    //��ʼ��Panel
    InitPanel initPanel;

    //������������
    TotalController totalController;
    

    private void Awake()
    {
        //��ʼ������
        initFishType = new InitType<FishType>("Prefab/Fish", new FishTypeFactory());
        //��ʼ���������Ϣ
        initFishType.InitTypes();

        initToolType = new InitType<ToolType>("Prefab/FishRod", new ToolTypeFactory());
        initToolType.InitTypes();
        initProptype = new InitType<PropType>("Prefab/Drift", new PropTypeFactory());
        initProptype.InitTypes();
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
        //��ѯ�����ֵ��е�Ԫ��
        //ItemManager.Instance.CheckDicElement();
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
    }
}
