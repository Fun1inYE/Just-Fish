using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// ����״̬�Ľӿ���
/// </summary>
public interface FishingStatus
{
    //ÿ��״̬�ĳ���������
    public void OnUpdate(FishingStateMachine fishing);

    public void OnLateUpdate(FishingStateMachine fishing);

    //��ǰ���˳��ķ���
    public void OnExit(FishingStateMachine fishing);
    //��һ�������Ҫִ�еķ���
    public void OnEnter(FishingStateMachine fishing);

}

/// <summary>
/// û�ڵ����״̬
/// </summary>
public class NoFishing : FishingStatus
{
    public void OnEnter(FishingStateMachine fishing)
    {
        //���õ���ָʾ��
        fishing.totalController.fishIndicatorController.enabled = false;
    }
    public void OnUpdate(FishingStateMachine fishing)
    {
        //ֻ������boolֵ��Ϊtrue��ʱ�򣬾ʹ����Ѿ�׼���õ����ˣ�����ת��ReadyToFish
        if(fishing.totalController.fishandCastController.isInitFishRod && fishing.totalController.fishandCastController.isInitDrift)
        {
            //��ʼ����ʾ�����
            fishing.InitializeDisPlayFish();
            //ת״̬
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
        //���õ���ָʾ��
        fishing.totalController.fishIndicatorController.enabled = false;
        //��Ϊ�������֮������������Nofishing������ֱ����NoFishing����������Ư����
        fishing.totalController.fishandCastController.driftScript.canAdsorb = true;
    }
    public void OnUpdate(FishingStateMachine fishing)
    {
        //---------�������ж�����---------//

        //������Ұ���ͻ�����������һ�����µĻ����ͻص�NoFishing
        if(!fishing.totalController.fishandCastController.isInitFishRod || !fishing.totalController.fishandCastController.isInitDrift)
        {
            //ת״̬
            fishing.SetFishingStatus(new NoFishing());
            return;
        }
        //������û�а���ͻ�����������ȥ�Ļ�
        else
        {
            //��ʼ����
            if (fishing.totalController.fishandCastController.driftScript.canAdsorb)
            {
                fishing.totalController.fishandCastController.MoveToWire();
            }

            //����Ư�����ͷ����һ��������ܽ�hasCastRod��Ϊtrue�������Ѿ��׸ͣ������ջ��ٴε���������ջ�
            if (Vector2.Distance(fishing.totalController.fishandCastController.fishRodScript.wireTransform.position, fishing.totalController.fishandCastController.driftScript.driftTransform.position) > 0.1f && fishing.totalController.fishandCastController.hasCastRod == false)
            {
                Debug.Log("�����ո���");
                //�����ո���
                fishing.totalController.fishandCastController.hasCastRod = true;
                //�������л������ˣ�ȥFishController.CastandReelInRod()��ȥ�ң���Ϊ�����������ָ��׸������ո������ͻᵥ���ɳ�ȥ��bug��
                //���Ի����ˣ�ͬ���ģ�ȥFishController.CastandReelInRod()���ң�����ͬ�ϣ�

                //��������ҽ���װ���л���
                InventoryManager.Instance.equipmentManager.ReverseCanExchageState(false);
            }

            //�ж���Ư������
            if (Vector2.Distance(fishing.totalController.fishandCastController.fishRodScript.wireTransform.position, fishing.totalController.fishandCastController.driftScript.driftTransform.position) < 0.05f && fishing.totalController.fishandCastController.hasCastRod == true)
            {
                Debug.Log("�����׸���");
                //����Ư����ײ���ٴδ�
                fishing.totalController.fishandCastController.driftScript.driftCollider.enabled = true;
                //������������
                fishing.totalController.fishandCastController.driftScript.driftRb.gravityScale = 1;
                //�ر���Ư
                fishing.totalController.fishandCastController.driftScript.driftTransform.gameObject.SetActive(false);
                //�رջ���
                fishing.totalController.fishandCastController.drawLine.canDrawing = false;
                //����Ư��Transform�ƶ�����ҵ�DriftPoint�£������������꣬�Զ����ع��ڸ������ı�������
                SetGameObjectToParent.SetParentFromFirstLayerParent("Player", "DriftPoint", fishing.totalController.fishandCastController.driftScript.gameObject);
                //�ر�����
                fishing.totalController.fishandCastController.driftScript.canAdsorb = false;
                //�����gameObject���ض����
                fishing.displayFish.ReplaceFishGameObject();
                //������ҽ���װ���л���
                InventoryManager.Instance.equipmentManager.ReverseCanExchageState(true);
                //�����׸���
                fishing.totalController.fishandCastController.hasCastRod = false;
                //�����л�������
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
            //�ж���Ư�Ƿ�Ӵ���ˮ
            if (fishing.totalController.fishandCastController.driftScript.isTouchWater == true)
            {
                fishing.SetFishingStatus(new Fishing());
                return;
            }
        }

        //�����ͺ������ľ���
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
/// ���ڵ����״̬
/// </summary>
public class Fishing : FishingStatus
{
    /// <summary>
    /// �ж�����Ƿ������ո�(Ĭ��false)
    /// </summary>
    public bool initiativeReel = false;

    public void OnEnter(FishingStateMachine fishing)
    {
        //���õ���ָʾ��
        fishing.totalController.fishIndicatorController.enabled = false;

        //��������������Ҫ�ȴ���ʱ��
        float countdownTime = Random.Range(2f, 5f);
        Debug.Log($"***������ɵĵȴ����ʱ��Ϊ{countdownTime}");

        //��OnTimerFinished����ע�ᵽ��ʱ��ί����
        fishing.timer.OnTimerFinished += fishing.OnFishingTimeUp;

        Debug.Log("***��ʼ���㣬�ȴ����Ϲ�...");
        //������ʱ��
        fishing.StartTimer(countdownTime);
    }

    public void OnUpdate(FishingStateMachine fishing)
    {
        //---------�������ж�����---------//

        //����Ư�뿪ˮ֮������ո˵����
        if (fishing.totalController.fishandCastController.driftScript.isTouchWater == false)
        {
            Debug.Log("***���ոˣ�ֹͣ��ʱ��");
            //�ж���������ո�
            initiativeReel = true;
            //ֹͣ��ʱ��
            fishing.StopFishingTimer();

            //������״̬ת��ReadyToFish
            fishing.SetFishingStatus(new ReadyToFish());
            return;
        }

        //�����ͺ������ľ���
        fishing.CheckDistanceRodAndDrift();
    }

    public void OnLateUpdate(FishingStateMachine fishing)
    {

    }
    public void OnExit(FishingStateMachine fishing)
    {
        //��Ϊ����������ոˣ���ƯҪ�����ƶ������Բ����õ��ο�ס��Ư
        if(initiativeReel == true)
        { 
            fishing.totalController.fishandCastController.driftScript.driftCollider.enabled = false;
        }

        //����Ӧ����ȡ��ע�ᵽ��ʱ����
        fishing.timer.OnTimerFinished -= fishing.OnFishingTimeUp;
    }

}

/// <summary>
/// ��ҧ����״̬
/// </summary>
public class BitingHook : FishingStatus
{
    /// <summary>
    /// ��¼UI��ԭʼScale
    /// </summary>
    Vector3 originalScale = new Vector3(0f, 0f, 0f);

    public void OnEnter(FishingStateMachine fishing)
    {
        //���õ���ָʾ��
        fishing.totalController.fishIndicatorController.enabled = false;
        //��Ҳ����ƶ�
        fishing.totalController.ControlManager<MovePlayerController>("FishState", false, fishing.totalController.movePlayerController);
        //ֹͣ���������
        fishing.totalController.ControlManager<FishandCastController>("FishState", false, fishing.totalController.fishandCastController);
        //TODO: ��ʾ����ҧ���Ķ���

        //���������ҷ�Ӧ��ʱ��
        float reactionTime = Random.Range(0.5f, 1.5f);
        Debug.Log($"***������ɵķ�Ӧ���ʱ��Ϊ{reactionTime}");
        //��OnLostFishTimeUp����ע�ᵽ��ʱ��ί����
        fishing.timer.OnTimerFinished += fishing.OnLostFishTimeUp;
        //������ʱ��
        fishing.StartTimer(reactionTime);


        //----------------------------------------UI��ʾ--------------------------------------------------------//
        DisplayWithDOTween.FadeAndMoveText(-0.2f, 0.2f, reactionTime - 0.2f, "bitingHookText", fishing.totalController.movePlayerController.TextUIPoint.position, 30);
    }
 
    public void OnUpdate(FishingStateMachine fishing)
    {
        //�������������ڹ涨ʱ�����ո˵����㣬����GameUIStateҪ����isGaming״̬
        if (Input.GetMouseButtonDown(0) && fishing.totalMachine.gameUIStateMachine.CheckStatus() == "isGaming")
        {
            Debug.Log("***�������ˣ���ʼ���㲫����");
            //ֹͣ��ʱ��
            fishing.StopFishingTimer();

            //TODO:�������UI������Ч��

            //��״̬ת��CatchingFish
            fishing.SetFishingStatus(new CatchingFish());
            return;
        }
    }

    public void OnLateUpdate(FishingStateMachine fishing)
    {

    }
    public void OnExit(FishingStateMachine fishing)
    {
        //��������߶�
        fishing.totalController.ControlManager<MovePlayerController>("FishState", true, fishing.totalController.movePlayerController);
        //�������������
        fishing.totalController.ControlManager<FishandCastController>("FishState", true, fishing.totalController.fishandCastController);

        //����Ӧ����ȡ��ע�ᵽ��ʱ����
        fishing.timer.OnTimerFinished -= fishing.OnLostFishTimeUp;
    }
}

/// <summary>
/// ���Ϲ���ʼץ���״̬����ʼ���㲫����
/// </summary>
public class CatchingFish : FishingStatus
{
    /// <summary>
    /// �ж��Ƿ����ɹ���Ĭ��false��
    /// </summary>
    public bool isSuccess = false;

    /// TODO: ץ��ʣ��ʱ��û��ʵװ����Ҫд(�漰��UI����)
    
    /// <summary>
    /// ץ��ʣ�µ�ʱ��
    /// </summary>
    public float holdTime = 10f;


    public void OnEnter(FishingStateMachine fishing)
    {
        //����ʹ�õ���ָʾ��
        fishing.totalController.fishIndicatorController.enabled = true;

        //�������ץ��״̬�����ÿ�����
        fishing.totalController.ControlManager<InventoryController>("FishState", false, fishing.totalController.inventoryController);
        //���ò����̵�
        fishing.totalController.ControlManager<StoreController>("FishState", false, fishing.totalController.storeController);
        //��������ƶ���������ֻ�ٿصĵ���ָʾ��
        fishing.totalController.ControlManager<MovePlayerController>("FishState", false, fishing.totalController.movePlayerController);
        //���õ������������ֹ��ץ���ʱ����ǰ�չ���
        fishing.totalController.ControlManager<FishandCastController>("FishState", false, fishing.totalController.fishandCastController);

        //���ƶ�����UI����������������
        fishing.totalController.fishIndicatorController.fishIndicatorRectTransform.anchoredPosition = UIOffset.CalculationOffset(
            fishing.totalController.movePlayerController.fishIndicatorPoint.position,
            fishing.totalController.fishIndicatorController.fishIndicatorRectTransform,
            OffsetLocation.Up
            );

        //�򿪵���ָʾ��UI
        fishing.totalController.fishIndicatorController.FishingIndicatorSetActive(true);
    }

    public void OnUpdate(FishingStateMachine fishing)
    {
        //---------�������ж�����---------//

        //�ж����Ƿ����
        if (fishing.totalController.fishIndicatorController.breakStripSlotScript.isBreakdown == true)
        {
            Debug.Log("***�߱��ˣ�");

            //����ʧ��
            isSuccess = false;

            //�ر���Ư��SpriteRenderer
            fishing.totalController.fishandCastController.driftScript.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //�ո�
            fishing.totalController.fishandCastController.IndividualTrigger_ReelInRodCommand();
            //״̬ת��Fishing
            fishing.SetFishingStatus(new Fishing());
            return;

        }
        //�ж��Ƿ������
        if(fishing.totalController.fishIndicatorController.processStripSlotScript.isFinish == true)
        {
            Debug.Log("***����ɹ���");

            //����ɹ�
            isSuccess = true;
            //�����ջ�
            fishing.totalController.fishandCastController.IndividualTrigger_ReelInRodCommand();
            
            //����Ư��Collider�رգ���ֹ���ջص�ʱ��Ῠס��ĳЩ�ط��ز���
            fishing.totalController.fishandCastController.driftScript.driftCollider.enabled = false;

            //״̬ת��Fishing
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
        //�жϵ����Ƿ�ɹ�
        if(isSuccess)
        {
            //�������������ͼ����
            fishing.displayFish.GetAndProcessingFishGameObject();
        }
        //����ʧ����(ʧ��ԭ���߱���)
        else if (fishing.totalController.fishIndicatorController.breakStripSlotScript.isBreakdown == true && !isSuccess)
        {
            //----------------------------------------UI��ʾ------------------------------------------------//
            //UI������ʾ
            DisplayWithDOTween.FadeAndMoveText(0.2f, 0.2f, 0.5f, "wireBreakText", fishing.totalController.movePlayerController.TextUIPoint.position, 25);

            //��������Sprite��Ⱦ�ر�
            fishing.totalController.fishandCastController.driftScript.GetComponent<SpriteRenderer>().enabled = false;
        }

        //��������ƶ�����
        fishing.totalController.ControlManager<MovePlayerController>("FishState", true, fishing.totalController.movePlayerController);
        //����fishandCastController
        fishing.totalController.ControlManager<FishandCastController>("FishState", true, fishing.totalController.fishandCastController);
        //����inventoryController
        fishing.totalController.ControlManager<InventoryController>("FishState", true, fishing.totalController.inventoryController);
        //���Բ����̵�
        fishing.totalController.ControlManager<StoreController>("FishState", true, fishing.totalController.storeController);
        //�رյ���ָʾ��UI
        fishing.totalController.fishIndicatorController.FishingIndicatorSetActive(false);
    }
}





