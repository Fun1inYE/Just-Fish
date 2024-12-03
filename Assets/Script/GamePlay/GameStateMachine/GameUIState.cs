using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏状态接口
/// </summary>
public interface GameUIState
{
    /// <summary>
    /// 进入状态要执行的方法
    /// </summary>
    /// <param name="gsm"></param>
    public void OnEnter(GameUIStateMachine gsm);
    /// <summary>
    /// 进入状态后要持续更新的方法
    /// </summary>
    /// <param name="gsm"></param>
    public void OnUpdate(GameUIStateMachine gsm);
    /// <summary>
    /// 退出状态后要执行的方法
    /// </summary>
    /// <param name="gsm"></param>
    public void OnExit(GameUIStateMachine gsm);
    /// <summary>
    /// LateUpdate更新
    /// </summary>
    /// <param name="gsm"></param>
    public void OnLateUpdate(GameUIStateMachine gsm);
    
}


/// <summary>
/// 正在操作人物移动钓鱼等动作的状态
/// </summary>
public class isGaming : GameUIState
{
    public void OnEnter(GameUIStateMachine gsm)
    {

    }

    public void OnUpdate(GameUIStateMachine gsm)
    {
        //当检测到背包启动的时候，GameMachine就会转到OpeningInventory状态
        if (gsm.totalController.inventoryController.isOpen == true)
        {
            gsm.SetGameState(new OpeningInventory());
        }
        //当检测到启动到选项面板启动时
        if(gsm.totalController.storeController.optionPanelisOpening == true)
        {
            gsm.SetGameState(new OpeningStoreOption());
        }
        //如果玩家在游戏阶段按下esc就进入到暂停游戏页面
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gsm.SetGameState(new OpeningPausePanel());
        }
    }

    public void OnLateUpdate(GameUIStateMachine gsm)
    {
        //当玩家走进商店范围
        if(gsm.totalController.storeController.store.wasEnterStore == true)
        {
            //显示UI提示
            DisplayUIManager.Instance.DisplayUI("ButtonUI_E");
            //偏移UI
            DisplayUIManager.Instance.UIOffSet("ButtonUI_E");
        }
        if(gsm.totalController.storeController.store.wasEnterStore == false)
        {
            //关闭UI提示
            DisplayUIManager.Instance.HideUI("ButtonUI_E");
        }
    }

    public void OnExit(GameUIStateMachine gsm)
    {
        //关闭UI提示
        DisplayUIManager.Instance.HideUI("ButtonUI_E");
    }
}

/// <summary>
/// 玩家在打开背包的状态
/// </summary>
public class OpeningInventory : GameUIState
{
    public void OnEnter(GameUIStateMachine gsm)
    {
        //打开背包的时候不能进行钓鱼操作，不能进行商店操作
        gsm.totalController.ControlManager<FishandCastController>("GameState", false, gsm.totalController.fishandCastController);
        gsm.totalController.ControlManager<StoreController>("GameState", false, gsm.totalController.storeController);



    }
    public void OnUpdate(GameUIStateMachine gsm)
    {
        //当检测到背包关闭的时候，GameMachine就会转到isGaming状态
        if (gsm.totalController.inventoryController.isOpen == false)
        {
            //TODO: 关闭背包的时候同时也关闭悬浮板

            //转状态
            gsm.SetGameState(new isGaming());
        }

        //重新设定背包UI的位置
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
        //关闭背包
        gsm.totalController.ControlManager<FishandCastController>("GameState", true, gsm.totalController.fishandCastController);
        gsm.totalController.ControlManager<StoreController>("GameState", true, gsm.totalController.storeController);
    }
}

/// <summary>
/// 启动选项面板的状态
/// </summary>
public class OpeningStoreOption : GameUIState
{
    public void OnEnter(GameUIStateMachine gsm)
    {
        //打开选项面板的时候不能进行钓鱼操作
        gsm.totalController.ControlManager<InventoryController>("GameState", false, gsm.totalController.inventoryController);
        gsm.totalController.ControlManager<FishandCastController>("GameState", false, gsm.totalController.fishandCastController);
    }

    public void OnUpdate(GameUIStateMachine gsm)
    {
        //当玩家离开商店范围
        if(gsm.totalController.storeController.store.wasEnterStore == false)
        {
            //关闭商店选项界面
            gsm.totalController.storeController.IndividualTrigger_CloseOptionPanel();
        }
        //从这里到下面有三个分支：分别是打开了商店界面，一种是打开卖出界面，最后一种是什么都没开
        //当检测到关闭了选项面板的时候或者离开了商店范围
        if (gsm.totalController.storeController.optionPanelisOpening == false)
        {
            //打开了商店界面
            if (gsm.totalController.storeController.storePanelisOpening == true)
            {
                //转到商店界面了
                gsm.SetGameState(new OpeningStore());
            }
            //打开了背包界面
            else if(gsm.totalController.storeController.salePanelisOpening == true)
            {
                //转到卖出界面了
                gsm.SetGameState(new OpeningSalePanel());
            }
            //没有打开任何UI
            else
            {
                //回到游戏状态
                gsm.SetGameState(new isGaming());
            }
        }
    }

    public void OnLateUpdate(GameUIStateMachine gsm)
    {
        //重新设定
        //
        //选项面板UI的位置
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
/// 启动商店页面的状态
/// </summary>
public class OpeningStore : GameUIState
{
    public void OnEnter(GameUIStateMachine gsm)
    {
        //打开商店的时候不能进行钓鱼操作，不能进行背包操作
        gsm.totalController.ControlManager<InventoryController>("GameState", false, gsm.totalController.inventoryController);
        gsm.totalController.ControlManager<FishandCastController>("GameState", false, gsm.totalController.fishandCastController);
    }

    public void OnUpdate(GameUIStateMachine gsm)
    {
        //当玩家离开商店范围
        if (gsm.totalController.storeController.store.wasEnterStore == false)
        {
            //关闭商店选项界面
            gsm.totalController.storeController.IndividualTrigger_CloseBuyPanel();
        }
        //当检测到关闭商店界面，跳转到isGaming状态
        if (gsm.totalController.storeController.storePanelisOpening == false)
        {
            //TODO: 关闭商店界面的时候同时也关闭悬浮板

            //跳转状态
            gsm.SetGameState(new isGaming());
        }
    }

    public void OnLateUpdate(GameUIStateMachine gsm)
    {
        //重新设定商店UI的位置
        gsm.totalController.storeController.storePanel.anchoredPosition = UIOffset.CalculationOffset(
            gsm.totalController.movePlayerController.propUIPoint.position,
            gsm.totalController.storeController.storePanel,
            OffsetLocation.Down
        );
    }


    public void OnExit(GameUIStateMachine gsm)
    {
        //关闭商店
        gsm.totalController.ControlManager<InventoryController>("GameState", true, gsm.totalController.inventoryController);
        gsm.totalController.ControlManager<FishandCastController>("GameState", true, gsm.totalController.fishandCastController);
    }
}

/// <summary>
/// 启动卖出页面的状态
/// </summary>
public class OpeningSalePanel : GameUIState
{
    public void OnEnter(GameUIStateMachine gsm)
    {
        //打开卖出界面的时候不能进行钓鱼操作，打开背包操作
        gsm.totalController.ControlManager<InventoryController>("GameState", false, gsm.totalController.inventoryController);
        gsm.totalController.ControlManager<FishandCastController>("GameState", false, gsm.totalController.fishandCastController);
        gsm.totalController.ControlManager<StoreController>("GameState", false, gsm.totalController.storeController);
    }

    public void OnUpdate(GameUIStateMachine gsm)
    {
        //当玩家离开商店范围
        if (gsm.totalController.storeController.store.wasEnterStore == false)
        {
            gsm.totalController.storeController.IndividualTrigger_CloseInventoryWithSalePanel();
        }

        //当检测到卖出界面被关闭之后
        if (gsm.totalController.storeController.salePanelisOpening == false)
        {
            //TODO: 关闭的时候同时也关闭悬浮板

            //转状态
            gsm.SetGameState(new isGaming());
        }
    }

    public void OnLateUpdate(GameUIStateMachine gsm)
    {
        //重新设定商店UI的位置
        gsm.totalController.storeController.inventoryPanelRectTransform.anchoredPosition = UIOffset.CalculationOffset(
            gsm.totalController.movePlayerController.propUIPoint.position,
            gsm.totalController.storeController.inventoryPanelRectTransform,
            OffsetLocation.Down
        );
    }

    public void OnExit(GameUIStateMachine gsm)
    {
        //恢复所有操作
        gsm.totalController.ControlManager<InventoryController>("GameState", true, gsm.totalController.inventoryController);
        gsm.totalController.ControlManager<FishandCastController>("GameState", true, gsm.totalController.fishandCastController);
        gsm.totalController.ControlManager<StoreController>("GameState", true, gsm.totalController.storeController);
    }
}

/// <summary>
/// 打开暂停页面的方法
/// </summary>
public class OpeningPausePanel : GameUIState
{
    public void OnEnter(GameUIStateMachine gsm)
    {
        //暂停玩家的一切行动
        gsm.totalController.ControlManager<MovePlayerController>("GameState", false, gsm.totalController.movePlayerController);
        gsm.totalController.ControlManager<InventoryController>("GameState", false, gsm.totalController.inventoryController);
        gsm.totalController.ControlManager<FishandCastController>("GameState", false, gsm.totalController.fishandCastController);
        gsm.totalController.ControlManager<StoreController>("GameState", false, gsm.totalController.storeController);
        //gsm.totalController.enabled = false;
        //将暂停页面压入PanelManager中
        UIManager.Instance.panelManager.Push(new PausePanel());
    }

    public void OnUpdate(GameUIStateMachine gsm)
    {
        //判断如果PanelManager中的窗口如果为0的话，代表玩家已经在游戏中退出了暂停页面
        if (UIManager.Instance.panelManager.stackPanel.Count == 0)
        {
            //返回正在游戏状态
            gsm.SetGameState(new isGaming());
        }


    }

    public void OnLateUpdate(GameUIStateMachine gsm)
    {

    }

    public void OnExit(GameUIStateMachine gsm)
    {
        //恢复玩家的一切行动
        gsm.totalController.ControlManager<MovePlayerController>("GameState", true, gsm.totalController.movePlayerController);
        gsm.totalController.ControlManager<InventoryController>("GameState", true, gsm.totalController.inventoryController);
        gsm.totalController.ControlManager<FishandCastController>("GameState", true, gsm.totalController.fishandCastController);
        gsm.totalController.ControlManager<StoreController>("GameState", true, gsm.totalController.storeController);

    }
}


