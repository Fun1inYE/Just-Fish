using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸ״̬�ӿ�
/// </summary>
public interface GameUIState
{
    /// <summary>
    /// ����״̬Ҫִ�еķ���
    /// </summary>
    /// <param name="gsm"></param>
    public void OnEnter(GameUIStateMachine gsm);
    /// <summary>
    /// ����״̬��Ҫ�������µķ���
    /// </summary>
    /// <param name="gsm"></param>
    public void OnUpdate(GameUIStateMachine gsm);
    /// <summary>
    /// �˳�״̬��Ҫִ�еķ���
    /// </summary>
    /// <param name="gsm"></param>
    public void OnExit(GameUIStateMachine gsm);
    /// <summary>
    /// LateUpdate����
    /// </summary>
    /// <param name="gsm"></param>
    public void OnLateUpdate(GameUIStateMachine gsm);
    
}


/// <summary>
/// ���ڲ��������ƶ�����ȶ�����״̬
/// </summary>
public class isGaming : GameUIState
{
    public void OnEnter(GameUIStateMachine gsm)
    {

    }

    public void OnUpdate(GameUIStateMachine gsm)
    {
        //����⵽����������ʱ��GameMachine�ͻ�ת��OpeningInventory״̬
        if (gsm.totalController.inventoryController.isOpen == true)
        {
            gsm.SetGameState(new OpeningInventory());
        }
        //����⵽������ѡ���������ʱ
        if(gsm.totalController.storeController.optionPanelisOpening == true)
        {
            gsm.SetGameState(new OpeningStoreOption());
        }
        //����������Ϸ�׶ΰ���esc�ͽ��뵽��ͣ��Ϸҳ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gsm.SetGameState(new OpeningPausePanel());
        }
    }

    public void OnLateUpdate(GameUIStateMachine gsm)
    {
        //������߽��̵귶Χ
        if(gsm.totalController.storeController.store.wasEnterStore == true)
        {
            //��ʾUI��ʾ
            DisplayUIManager.Instance.DisplayUI("ButtonUI_E");
            //ƫ��UI
            DisplayUIManager.Instance.UIOffSet("ButtonUI_E");
        }
        if(gsm.totalController.storeController.store.wasEnterStore == false)
        {
            //�ر�UI��ʾ
            DisplayUIManager.Instance.HideUI("ButtonUI_E");
        }
    }

    public void OnExit(GameUIStateMachine gsm)
    {
        //�ر�UI��ʾ
        DisplayUIManager.Instance.HideUI("ButtonUI_E");
    }
}

/// <summary>
/// ����ڴ򿪱�����״̬
/// </summary>
public class OpeningInventory : GameUIState
{
    public void OnEnter(GameUIStateMachine gsm)
    {
        //�򿪱�����ʱ���ܽ��е�����������ܽ����̵����
        gsm.totalController.ControlManager<FishandCastController>("GameState", false, gsm.totalController.fishandCastController);
        gsm.totalController.ControlManager<StoreController>("GameState", false, gsm.totalController.storeController);



    }
    public void OnUpdate(GameUIStateMachine gsm)
    {
        //����⵽�����رյ�ʱ��GameMachine�ͻ�ת��isGaming״̬
        if (gsm.totalController.inventoryController.isOpen == false)
        {
            //TODO: �رձ�����ʱ��ͬʱҲ�ر�������

            //ת״̬
            gsm.SetGameState(new isGaming());
        }

        //�����趨����UI��λ��
        gsm.totalController.inventoryController.inventoryRectTransform.anchoredPosition = UIOffset.CalculationOffset(
            gsm.totalController.movePlayerController.propUIPoint.position,
            gsm.totalController.inventoryController.inventoryRectTransform,
            OffsetLocation.Down
        );
    }

    public void OnLateUpdate(GameUIStateMachine gsm)
    {
        
    }

    public void OnExit(GameUIStateMachine gsm)
    {
        //�رձ���
        gsm.totalController.ControlManager<FishandCastController>("GameState", true, gsm.totalController.fishandCastController);
        gsm.totalController.ControlManager<StoreController>("GameState", true, gsm.totalController.storeController);
    }
}

/// <summary>
/// ����ѡ������״̬
/// </summary>
public class OpeningStoreOption : GameUIState
{
    public void OnEnter(GameUIStateMachine gsm)
    {
        //��ѡ������ʱ���ܽ��е������
        gsm.totalController.ControlManager<InventoryController>("GameState", false, gsm.totalController.inventoryController);
        gsm.totalController.ControlManager<FishandCastController>("GameState", false, gsm.totalController.fishandCastController);
    }

    public void OnUpdate(GameUIStateMachine gsm)
    {
        //������뿪�̵귶Χ
        if(gsm.totalController.storeController.store.wasEnterStore == false)
        {
            //�ر��̵�ѡ�����
            gsm.totalController.storeController.IndividualTrigger_CloseOptionPanel();
        }
        //�����ﵽ������������֧���ֱ��Ǵ����̵���棬һ���Ǵ��������棬���һ����ʲô��û��
        //����⵽�ر���ѡ������ʱ������뿪���̵귶Χ
        if (gsm.totalController.storeController.optionPanelisOpening == false)
        {
            //�����̵����
            if (gsm.totalController.storeController.storePanelisOpening == true)
            {
                //ת���̵������
                gsm.SetGameState(new OpeningStore());
            }
            //���˱�������
            else if(gsm.totalController.storeController.salePanelisOpening == true)
            {
                //ת������������
                gsm.SetGameState(new OpeningSalePanel());
            }
            //û�д��κ�UI
            else
            {
                //�ص���Ϸ״̬
                gsm.SetGameState(new isGaming());
            }
        }
    }

    public void OnLateUpdate(GameUIStateMachine gsm)
    {
        //�����趨
        //
        //ѡ�����UI��λ��
        gsm.totalController.storeController.optionPanel.anchoredPosition = UIOffset.CalculationOffset(
            gsm.totalController.movePlayerController.propUIPoint.position,
            gsm.totalController.storeController.optionPanel,
            OffsetLocation.Down
        );
    }

    public void OnExit(GameUIStateMachine gsm)
    {
        gsm.totalController.ControlManager<InventoryController>("GameState", true, gsm.totalController.inventoryController);
        gsm.totalController.ControlManager<FishandCastController>("GameState", true, gsm.totalController.fishandCastController);
    }
}

/// <summary>
/// �����̵�ҳ���״̬
/// </summary>
public class OpeningStore : GameUIState
{
    public void OnEnter(GameUIStateMachine gsm)
    {
        //���̵��ʱ���ܽ��е�����������ܽ��б�������
        gsm.totalController.ControlManager<InventoryController>("GameState", false, gsm.totalController.inventoryController);
        gsm.totalController.ControlManager<FishandCastController>("GameState", false, gsm.totalController.fishandCastController);
    }

    public void OnUpdate(GameUIStateMachine gsm)
    {
        //������뿪�̵귶Χ
        if (gsm.totalController.storeController.store.wasEnterStore == false)
        {
            //�ر��̵�ѡ�����
            gsm.totalController.storeController.IndividualTrigger_CloseBuyPanel();
        }
        //����⵽�ر��̵���棬��ת��isGaming״̬
        if (gsm.totalController.storeController.storePanelisOpening == false)
        {
            //TODO: �ر��̵�����ʱ��ͬʱҲ�ر�������

            //��ת״̬
            gsm.SetGameState(new isGaming());
        }
    }

    public void OnLateUpdate(GameUIStateMachine gsm)
    {
        //�����趨�̵�UI��λ��
        gsm.totalController.storeController.storePanel.anchoredPosition = UIOffset.CalculationOffset(
            gsm.totalController.movePlayerController.propUIPoint.position,
            gsm.totalController.storeController.storePanel,
            OffsetLocation.Down
        );
    }


    public void OnExit(GameUIStateMachine gsm)
    {
        //�ر��̵�
        gsm.totalController.ControlManager<InventoryController>("GameState", true, gsm.totalController.inventoryController);
        gsm.totalController.ControlManager<FishandCastController>("GameState", true, gsm.totalController.fishandCastController);
    }
}

/// <summary>
/// ��������ҳ���״̬
/// </summary>
public class OpeningSalePanel : GameUIState
{
    public void OnEnter(GameUIStateMachine gsm)
    {
        //�����������ʱ���ܽ��е���������򿪱�������
        gsm.totalController.ControlManager<InventoryController>("GameState", false, gsm.totalController.inventoryController);
        gsm.totalController.ControlManager<FishandCastController>("GameState", false, gsm.totalController.fishandCastController);
        gsm.totalController.ControlManager<StoreController>("GameState", false, gsm.totalController.storeController);
    }

    public void OnUpdate(GameUIStateMachine gsm)
    {
        //������뿪�̵귶Χ
        if (gsm.totalController.storeController.store.wasEnterStore == false)
        {
            gsm.totalController.storeController.IndividualTrigger_CloseInventoryWithSalePanel();
        }

        //����⵽�������汻�ر�֮��
        if (gsm.totalController.storeController.salePanelisOpening == false)
        {
            //TODO: �رյ�ʱ��ͬʱҲ�ر�������

            //ת״̬
            gsm.SetGameState(new isGaming());
        }
    }

    public void OnLateUpdate(GameUIStateMachine gsm)
    {
        //�����趨�̵�UI��λ��
        gsm.totalController.storeController.inventoryPanelRectTransform.anchoredPosition = UIOffset.CalculationOffset(
            gsm.totalController.movePlayerController.propUIPoint.position,
            gsm.totalController.storeController.inventoryPanelRectTransform,
            OffsetLocation.Down
        );
    }

    public void OnExit(GameUIStateMachine gsm)
    {
        //�ָ����в���
        gsm.totalController.ControlManager<InventoryController>("GameState", true, gsm.totalController.inventoryController);
        gsm.totalController.ControlManager<FishandCastController>("GameState", true, gsm.totalController.fishandCastController);
        gsm.totalController.ControlManager<StoreController>("GameState", true, gsm.totalController.storeController);
    }
}

/// <summary>
/// ����ͣҳ��ķ���
/// </summary>
public class OpeningPausePanel : GameUIState
{
    public void OnEnter(GameUIStateMachine gsm)
    {
        //��ͣ��ҵ�һ���ж�
        gsm.totalController.ControlManager<MovePlayerController>("GameState", false, gsm.totalController.movePlayerController);
        gsm.totalController.ControlManager<InventoryController>("GameState", false, gsm.totalController.inventoryController);
        gsm.totalController.ControlManager<FishandCastController>("GameState", false, gsm.totalController.fishandCastController);
        gsm.totalController.ControlManager<StoreController>("GameState", false, gsm.totalController.storeController);
        //gsm.totalController.enabled = false;
        //����ͣҳ��ѹ��PanelManager��
        UIManager.Instance.panelManager.Push(new PausePanel());
    }

    public void OnUpdate(GameUIStateMachine gsm)
    {
        //�ж����PanelManager�еĴ������Ϊ0�Ļ�����������Ѿ�����Ϸ���˳�����ͣҳ��
        if (UIManager.Instance.panelManager.stackPanel.Count == 0)
        {
            //����������Ϸ״̬
            gsm.SetGameState(new isGaming());
        }


    }

    public void OnLateUpdate(GameUIStateMachine gsm)
    {

    }

    public void OnExit(GameUIStateMachine gsm)
    {
        //�ָ���ҵ�һ���ж�
        gsm.totalController.ControlManager<MovePlayerController>("GameState", true, gsm.totalController.movePlayerController);
        gsm.totalController.ControlManager<InventoryController>("GameState", true, gsm.totalController.inventoryController);
        gsm.totalController.ControlManager<FishandCastController>("GameState", true, gsm.totalController.fishandCastController);
        gsm.totalController.ControlManager<StoreController>("GameState", true, gsm.totalController.storeController);

    }
}


