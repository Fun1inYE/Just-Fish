using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// 钓鱼状态的接口类
/// </summary>
public interface FishingStatus
{
    //每个状态的持续更新类
    public void OnUpdate(FishingStateMachine fishing);

    public void OnLateUpdate(FishingStateMachine fishing);

    //当前类退出的方法
    public void OnExit(FishingStateMachine fishing);
    //下一个类进入要执行的方法
    public void OnEnter(FishingStateMachine fishing);

}

/// <summary>
/// 没在钓鱼的状态
/// </summary>
public class NoFishing : FishingStatus
{
    public void OnEnter(FishingStateMachine fishing)
    {
        //禁用钓鱼指示器
        fishing.totalController.fishIndicatorController.enabled = false;
    }
    public void OnUpdate(FishingStateMachine fishing)
    {
        //只有两个bool值都为true的时候，就代表已经准备好钓鱼了，可以转到ReadyToFish
        if(fishing.totalController.fishandCastController.isInitFishRod && fishing.totalController.fishandCastController.isInitDrift)
        {
            //初始化显示鱼的类
            fishing.InitializeDisPlayFish();
            //转状态
            fishing.SetFishingStatus(new ReadyToFish());
            return;
        }
    }

    public void OnLateUpdate(FishingStateMachine fishing)
    {

    }

    public void OnExit(FishingStateMachine fishing)
    {

    }

    
}

public class ReadyToFish : FishingStatus
{
    public void OnEnter(FishingStateMachine fishing)
    {
        //禁用钓鱼指示器
        fishing.totalController.fishIndicatorController.enabled = false;
        //因为达成条件之后立马都是跳到Nofishing，所以直接在NoFishing这里启动鱼漂吸附
        fishing.totalController.fishandCastController.driftScript.canAdsorb = true;
    }
    public void OnUpdate(FishingStateMachine fishing)
    {
        //---------动画和判断条件---------//

        //但凡玩家把鱼竿或者鱼鳔其中一个拿下的话，就回到NoFishing
        if(!fishing.totalController.fishandCastController.isInitFishRod || !fishing.totalController.fishandCastController.isInitDrift)
        {
            //转状态
            fishing.SetFishingStatus(new NoFishing());
            return;
        }
        //如果玩家没有把鱼竿或者鱼鳔拿下去的话
        else
        {
            //开始吸附
            if (fishing.totalController.fishandCastController.driftScript.canAdsorb)
            {
                fishing.totalController.fishandCastController.MoveToWire();
            }

            //当鱼漂离鱼竿头部有一定距离才能将hasCastRod置为true，代表已经抛竿，可以收回再次点击鼠标进行收回
            if (Vector2.Distance(fishing.totalController.fishandCastController.fishRodScript.wireTransform.position, fishing.totalController.fishandCastController.driftScript.driftTransform.position) > 0.1f && fishing.totalController.fishandCastController.hasCastRod == false)
            {
                Debug.Log("可以收杆了");
                //可以收杆了
                fishing.totalController.fishandCastController.hasCastRod = true;
                //不可以切换空手了（去FishController.CastandReelInRod()中去找，因为放在这里会出现刚抛杆子再收竿鱼鳔就会单独飞出去的bug）
                //可以画线了（同样的，去FishController.CastandReelInRod()中找，理由同上）

                //不允许玩家进行装备切换了
                InventoryManager.Instance.equipmentManager.ReverseCanExchageState(false);
            }

            //判断鱼漂收起来
            if (Vector2.Distance(fishing.totalController.fishandCastController.fishRodScript.wireTransform.position, fishing.totalController.fishandCastController.driftScript.driftTransform.position) < 0.05f && fishing.totalController.fishandCastController.hasCastRod == true)
            {
                Debug.Log("可以抛杆了");
                //把鱼漂的碰撞体再次打开
                fishing.totalController.fishandCastController.driftScript.driftCollider.enabled = true;
                //启动鱼鳔重力
                fishing.totalController.fishandCastController.driftScript.driftRb.gravityScale = 1;
                //关闭鱼漂
                fishing.totalController.fishandCastController.driftScript.driftTransform.gameObject.SetActive(false);
                //关闭画线
                fishing.totalController.fishandCastController.drawLine.canDrawing = false;
                //将鱼漂的Transform移动到玩家的DriftPoint下，保持世界坐标，自动调控关于父类对象的本地坐标
                SetGameObjectToParent.SetParentFromFirstLayerParent("Player", "DriftPoint", fishing.totalController.fishandCastController.driftScript.gameObject);
                //关闭吸附
                fishing.totalController.fishandCastController.driftScript.canAdsorb = false;
                //将鱼的gameObject调回对象池
                fishing.displayFish.ReplaceFishGameObject();
                //允许玩家进行装备切换了
                InventoryManager.Instance.equipmentManager.ReverseCanExchageState(true);
                //可以抛杆了
                fishing.totalController.fishandCastController.hasCastRod = false;
                //可以切换空手了
                fishing.totalController.fishandCastController.canPutAwayRod = true;

                if (fishing.totalController.fishandCastController.driftScript.GetComponent<SpriteRenderer>().enabled == false)
                {
                    fishing.totalController.fishandCastController.driftScript.GetComponent<SpriteRenderer>().enabled = true;
                    InventoryManager.Instance.equipmentManager.DeleteItemInListFromIndex(1);
                    InventoryManager.Instance.equipmentSlotContainer.RefreshSlotUI();
                    fishing.SetFishingStatus(new NoFishing());
                    return;
                }
            }
            //判断鱼漂是否接触到水
            if (fishing.totalController.fishandCastController.driftScript.isTouchWater == true)
            {
                fishing.SetFishingStatus(new Fishing());
                return;
            }
        }

        //检测鱼竿和鱼鳔的距离
        fishing.CheckDistanceRodAndDrift();
    }

    public void OnLateUpdate(FishingStateMachine fishing)
    {

    }

    public void OnExit(FishingStateMachine fishing)
    {

    }
}

/// <summary>
/// 正在钓鱼的状态
/// </summary>
public class Fishing : FishingStatus
{
    /// <summary>
    /// 判断玩家是否主动收杆(默认false)
    /// </summary>
    public bool initiativeReel = false;

    public void OnEnter(FishingStateMachine fishing)
    {
        //禁用钓鱼指示器
        fishing.totalController.fishIndicatorController.enabled = false;

        //随机生成玩家所需要等待的时间
        float countdownTime = Random.Range(2f, 5f);
        Debug.Log($"***这次生成的等待随机时间为{countdownTime}");

        //将OnTimerFinished方法注册到计时器委托中
        fishing.timer.OnTimerFinished += fishing.OnFishingTimeUp;

        Debug.Log("***开始钓鱼，等待鱼上钩...");
        //启动计时器
        fishing.StartTimer(countdownTime);
    }

    public void OnUpdate(FishingStateMachine fishing)
    {
        //---------动画和判断条件---------//

        //当鱼漂离开水之后或者收杆的情况
        if (fishing.totalController.fishandCastController.driftScript.isTouchWater == false)
        {
            Debug.Log("***已收杆，停止计时！");
            //判断玩家主动收杆
            initiativeReel = true;
            //停止计时器
            fishing.StopFishingTimer();

            //将钓鱼状态转到ReadyToFish
            fishing.SetFishingStatus(new ReadyToFish());
            return;
        }

        //检测鱼竿和鱼鳔的距离
        fishing.CheckDistanceRodAndDrift();
    }

    public void OnLateUpdate(FishingStateMachine fishing)
    {

    }
    public void OnExit(FishingStateMachine fishing)
    {
        //因为是玩家主动收杆，鱼漂要往回移动，所以不能让地形卡住鱼漂
        if(initiativeReel == true)
        { 
            fishing.totalController.fishandCastController.driftScript.driftCollider.enabled = false;
        }

        //将对应方法取消注册到计时器中
        fishing.timer.OnTimerFinished -= fishing.OnFishingTimeUp;
    }

}

/// <summary>
/// 鱼咬钩的状态
/// </summary>
public class BitingHook : FishingStatus
{
    /// <summary>
    /// 记录UI的原始Scale
    /// </summary>
    Vector3 originalScale = new Vector3(0f, 0f, 0f);

    public void OnEnter(FishingStateMachine fishing)
    {
        //禁用钓鱼指示器
        fishing.totalController.fishIndicatorController.enabled = false;
        //玩家不能移动
        fishing.totalController.ControlManager<MovePlayerController>("FishState", false, fishing.totalController.movePlayerController);
        //停止钓鱼控制器
        fishing.totalController.ControlManager<FishandCastController>("FishState", false, fishing.totalController.fishandCastController);
        //TODO: 提示有鱼咬钩的动作

        //随机生成玩家反应的时间
        float reactionTime = Random.Range(0.5f, 1.5f);
        Debug.Log($"***这次生成的反应随机时间为{reactionTime}");
        //将OnLostFishTimeUp方法注册到计时器委托中
        fishing.timer.OnTimerFinished += fishing.OnLostFishTimeUp;
        //启动计时器
        fishing.StartTimer(reactionTime);


        //----------------------------------------UI显示--------------------------------------------------------//
        DisplayWithDOTween.FadeAndMoveText(-0.2f, 0.2f, reactionTime - 0.2f, "bitingHookText", fishing.totalController.movePlayerController.TextUIPoint.position, 30);
    }
 
    public void OnUpdate(FishingStateMachine fishing)
    {
        //按下鼠标左键，在规定时间内收杆钓到鱼，并且GameUIState要处于isGaming状态
        if (Input.GetMouseButtonDown(0) && fishing.totalMachine.gameUIStateMachine.CheckStatus() == "isGaming")
        {
            Debug.Log("***钓到鱼了，开始与鱼搏斗！");
            //停止计时器
            fishing.StopFishingTimer();

            //TODO:钓到鱼的UI，动画效果

            //将状态转到CatchingFish
            fishing.SetFishingStatus(new CatchingFish());
            return;
        }
    }

    public void OnLateUpdate(FishingStateMachine fishing)
    {

    }
    public void OnExit(FishingStateMachine fishing)
    {
        //允许玩家走动
        fishing.totalController.ControlManager<MovePlayerController>("FishState", true, fishing.totalController.movePlayerController);
        //启动钓鱼控制器
        fishing.totalController.ControlManager<FishandCastController>("FishState", true, fishing.totalController.fishandCastController);

        //将对应方法取消注册到计时器中
        fishing.timer.OnTimerFinished -= fishing.OnLostFishTimeUp;
    }
}

/// <summary>
/// 鱼上钩开始抓鱼的状态（开始和鱼搏斗）
/// </summary>
public class CatchingFish : FishingStatus
{
    /// <summary>
    /// 判断是否钓鱼成功（默认false）
    /// </summary>
    public bool isSuccess = false;

    /// TODO: 抓鱼剩下时间没有实装，还要写(涉及到UI部分)
    
    /// <summary>
    /// 抓鱼剩下的时间
    /// </summary>
    public float holdTime = 10f;


    public void OnEnter(FishingStateMachine fishing)
    {
        //可以使用钓鱼指示器
        fishing.totalController.fishIndicatorController.enabled = true;

        //如果进入抓鱼状态，不让开背包
        fishing.totalController.ControlManager<InventoryController>("FishState", false, fishing.totalController.inventoryController);
        //不让操作商店
        fishing.totalController.ControlManager<StoreController>("FishState", false, fishing.totalController.storeController);
        //禁用玩家移动控制器，只操控的钓鱼指示器
        fishing.totalController.ControlManager<MovePlayerController>("FishState", false, fishing.totalController.movePlayerController);
        //禁用钓鱼控制器，防止在抓鱼的时候提前收钩子
        fishing.totalController.ControlManager<FishandCastController>("FishState", false, fishing.totalController.fishandCastController);

        //先移动钓鱼UI，以免坐标计算出错
        fishing.totalController.fishIndicatorController.fishIndicatorRectTransform.anchoredPosition = UIOffset.CalculationOffset(
            fishing.totalController.movePlayerController.fishIndicatorPoint.position,
            fishing.totalController.fishIndicatorController.fishIndicatorRectTransform,
            OffsetLocation.Up
            );

        //打开钓鱼指示器UI
        fishing.totalController.fishIndicatorController.FishingIndicatorSetActive(true);
    }

    public void OnUpdate(FishingStateMachine fishing)
    {
        //---------动画和判断条件---------//

        //判断线是否崩线
        if (fishing.totalController.fishIndicatorController.breakStripSlotScript.isBreakdown == true)
        {
            Debug.Log("***线崩了！");

            //钓鱼失败
            isSuccess = false;

            //关闭鱼漂的SpriteRenderer
            fishing.totalController.fishandCastController.driftScript.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //收竿
            fishing.totalController.fishandCastController.IndividualTrigger_ReelInRodCommand();
            //状态转到Fishing
            fishing.SetFishingStatus(new Fishing());
            return;

        }
        //判断是否钓到鱼
        if(fishing.totalController.fishIndicatorController.processStripSlotScript.isFinish == true)
        {
            Debug.Log("***钓鱼成功！");

            //钓鱼成功
            isSuccess = true;
            //把鱼收回
            fishing.totalController.fishandCastController.IndividualTrigger_ReelInRodCommand();
            
            //将鱼漂的Collider关闭，防止再收回的时候会卡住在某些地方回不来
            fishing.totalController.fishandCastController.driftScript.driftCollider.enabled = false;

            //状态转到Fishing
            fishing.SetFishingStatus(new Fishing());
            return;
        }
    }

    public void OnLateUpdate(FishingStateMachine fishing)
    {
        fishing.totalController.fishIndicatorController.fishIndicatorRectTransform.anchoredPosition = UIOffset.CalculationOffset(
            fishing.totalController.movePlayerController.fishIndicatorPoint.position,
            fishing.totalController.fishIndicatorController.fishIndicatorRectTransform,
            OffsetLocation.Up
            );
    }
    public void OnExit(FishingStateMachine fishing)
    {
        //判断钓鱼是否成功
        if(isSuccess)
        {
            //调用生成鱼的贴图方法
            fishing.displayFish.GetAndProcessingFishGameObject();
        }
        //钓鱼失败了(失败原因：线崩了)
        else if (fishing.totalController.fishIndicatorController.breakStripSlotScript.isBreakdown == true && !isSuccess)
        {
            //----------------------------------------UI显示------------------------------------------------//
            //UI动画显示
            DisplayWithDOTween.FadeAndMoveText(0.2f, 0.2f, 0.5f, "wireBreakText", fishing.totalController.movePlayerController.TextUIPoint.position, 25);

            //将鱼鳔的Sprite渲染关闭
            fishing.totalController.fishandCastController.driftScript.GetComponent<SpriteRenderer>().enabled = false;
        }

        //重启玩家移动方法
        fishing.totalController.ControlManager<MovePlayerController>("FishState", true, fishing.totalController.movePlayerController);
        //启动fishandCastController
        fishing.totalController.ControlManager<FishandCastController>("FishState", true, fishing.totalController.fishandCastController);
        //启动inventoryController
        fishing.totalController.ControlManager<InventoryController>("FishState", true, fishing.totalController.inventoryController);
        //可以操作商店
        fishing.totalController.ControlManager<StoreController>("FishState", true, fishing.totalController.storeController);
        //关闭钓鱼指示器UI
        fishing.totalController.fishIndicatorController.FishingIndicatorSetActive(false);
    }
}





