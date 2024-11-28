using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ����״̬�Ĺ���
/// </summary>
public class FishingStateMachine : MonoBehaviour
{
    /// <summary>
    /// ���ü�ʱ��
    /// </summary>
    public Timer timer { get; private set; }
    /// <summary>
    /// �����ܿ�����
    /// </summary>
    public TotalController totalController { get; private set; }
    /// <summary>
    /// ������״̬��
    /// </summary>
    public TotalMachine totalMachine { get; private set; }
    /// <summary>
    /// ������ʾ�����(���ڵ���״̬�г�ʼ��)
    /// </summary>
    public DisplayFish displayFish;
    /// <summary>
    /// �����״̬
    /// </summary>
    private FishingStatus fishingStatus;

    /// <summary>
    /// ��ΪMonoBehaviour����������������ڣ����Բ���ֱ���ù��캯��
    /// </summary>
    private void Awake()
    {
        //��ʱ���ĳ�ʼ��
        timer = gameObject.AddComponent<Timer>();
        
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
        if (totalController == null)
        {
            Debug.LogError("totalController�ǿյģ�����Hierarchy���ڣ�");
        }
        totalMachine = SetGameObjectToParent.FindFromFirstLayer("StateMachine").GetComponent<TotalMachine>();
        if (totalMachine == null)
        {
            Debug.LogError("totalMachine�ǿյģ�����Hierarchy���ڣ�");
        }

        //��Ϊ��������ʱ��ű�Ĭ����enable��false
        enabled = false;
    }

    /// <summary>
    /// ����״̬���ķ���
    /// </summary>
    public void StartMachine()
    {
        //����״̬���ĳ�ʼ��
        this.fishingStatus = new NoFishing();
        //��״̬������״̬
        SetFishingStatus(fishingStatus);

        //����״̬���е�Updata�ȷ���
        enabled = true;
    }

    /// <summary>
    /// ֹͣ״̬��
    /// </summary>
    public void StopMachine()
    {
        //�Ƚ�״̬ת�ص��������״̬
        SetFishingStatus(new NoFishing());
        //��ͣ״̬��������
        enabled = false;
    }

    /// <summary>
    /// �ڽ������״̬��ʱ��Ҫ������������Գ�ʼ����ʾ��ķ���
    /// </summary>
    public void InitializeDisPlayFish()
    {
        displayFish = new DisplayFish(totalController.fishandCastController.driftScript.gameObject);
    }

    /// <summary>
    /// ���
    /// </summary>
    public void CheckDistanceRodAndDrift()
    {
        //�����ͺ���Ư�������6.5����λ����Ҫ�Զ��ո�
        if (Vector2.Distance(totalController.fishandCastController.driftScript.gameObject.transform.position, totalController.fishandCastController.fishRodScript.gameObject.transform.position) >= totalController.fishandCastController.distance)
        {
            //�ո�
            totalController.fishandCastController.IndividualTrigger_ReelInRodCommand();
            //ת״̬
            SetFishingStatus(new ReadyToFish());
        }
    }

    /// <summary>
    /// ����FishingStatus��״̬
    /// </summary>
    /// <param name="fishingStatus">�����״̬</param>
    public void SetFishingStatus(FishingStatus fishingStatus)
    {
        //�˳���ǰ״ִ̬�еķ���
        this.fishingStatus?.OnExit(this);
        //ת��״̬
        this.fishingStatus = fishingStatus;
        //������һ��״ִ̬�еĴ���
        this.fishingStatus?.OnEnter(this);
    }

    /// <summary>
    /// ���Ϲ��ˣ�����ץ��״̬
    /// </summary>
    public void OnFishingTimeUp()
    {
        Debug.Log("***���Ϲ��ˣ�");
        SetFishingStatus(new BitingHook());
    }

    /// <summary>
    /// �������㹳���ص�����״̬
    /// </summary>
    public void OnLostFishTimeUp()
    {
        Debug.Log("***�������ˣ�");
        SetFishingStatus(new Fishing());
    }

    /// <summary>
    /// ������ʱ��
    /// </summary>
    /// <param name="time">����Ҫ��ʱ��</param>
    public void StartTimer(float countdownTime)
    {
        //������ʱ��
        timer.StartTimer(countdownTime);
    }

    /// <summary>
    /// �رռ�ʱ��
    /// </summary>
    public void StopFishingTimer()
    {
        timer.StopTimer();
    }

    /// <summary>
    /// �ṩ�ӿڣ����ⲿ��ѯ��ǰ״̬
    /// </summary>
    /// <returns>���ص�ǰ�����״̬</returns>
    public string DownToCheckStatus()
    {
        return fishingStatus.GetType().Name;
    }

    /// <summary>
    /// ÿִ֡��
    /// </summary>
    private void Update()
    {
        fishingStatus.OnUpdate(this);

        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(DownToCheckStatus());
        }
    }

    private void LateUpdate()
    {
        fishingStatus.OnLateUpdate(this);
    }
}