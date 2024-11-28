using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 钓鱼状态的管理
/// </summary>
public class FishingStateMachine : MonoBehaviour
{
    /// <summary>
    /// 引用计时器
    /// </summary>
    public Timer timer { get; private set; }
    /// <summary>
    /// 引用总控制器
    /// </summary>
    public TotalController totalController { get; private set; }
    /// <summary>
    /// 引用总状态机
    /// </summary>
    public TotalMachine totalMachine { get; private set; }
    /// <summary>
    /// 引用显示鱼的类(将在钓鱼状态中初始化)
    /// </summary>
    public DisplayFish displayFish;
    /// <summary>
    /// 钓鱼的状态
    /// </summary>
    private FishingStatus fishingStatus;

    /// <summary>
    /// 因为MonoBehaviour会管理方法的生命周期，所以不能直接用构造函数
    /// </summary>
    private void Awake()
    {
        //计时器的初始化
        timer = gameObject.AddComponent<Timer>();
        
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
        if (totalController == null)
        {
            Debug.LogError("totalController是空的，请检查Hierarchy窗口！");
        }
        totalMachine = SetGameObjectToParent.FindFromFirstLayer("StateMachine").GetComponent<TotalMachine>();
        if (totalMachine == null)
        {
            Debug.LogError("totalMachine是空的，请检查Hierarchy窗口！");
        }

        //因为刚启动的时候脚本默认是enable是false
        enabled = false;
    }

    /// <summary>
    /// 启动状态机的方法
    /// </summary>
    public void StartMachine()
    {
        //钓鱼状态机的初始化
        this.fishingStatus = new NoFishing();
        //让状态机进入状态
        SetFishingStatus(fishingStatus);

        //启动状态机中的Updata等方法
        enabled = true;
    }

    /// <summary>
    /// 停止状态机
    /// </summary>
    public void StopMachine()
    {
        //先将状态转回到不钓鱼的状态
        SetFishingStatus(new NoFishing());
        //暂停状态机的运行
        enabled = false;
    }

    /// <summary>
    /// 在进入钓鱼状态的时候要调用这个方法以初始化显示鱼的方法
    /// </summary>
    public void InitializeDisPlayFish()
    {
        displayFish = new DisplayFish(totalController.fishandCastController.driftScript.gameObject);
    }

    /// <summary>
    /// 检测
    /// </summary>
    public void CheckDistanceRodAndDrift()
    {
        //如果鱼竿和鱼漂距离大于6.5个单位，就要自动收竿
        if (Vector2.Distance(totalController.fishandCastController.driftScript.gameObject.transform.position, totalController.fishandCastController.fishRodScript.gameObject.transform.position) >= totalController.fishandCastController.distance)
        {
            //收竿
            totalController.fishandCastController.IndividualTrigger_ReelInRodCommand();
            //转状态
            SetFishingStatus(new ReadyToFish());
        }
    }

    /// <summary>
    /// 设置FishingStatus的状态
    /// </summary>
    /// <param name="fishingStatus">钓鱼的状态</param>
    public void SetFishingStatus(FishingStatus fishingStatus)
    {
        //退出当前状态执行的方法
        this.fishingStatus?.OnExit(this);
        //转换状态
        this.fishingStatus = fishingStatus;
        //进入下一个状态执行的代码
        this.fishingStatus?.OnEnter(this);
    }

    /// <summary>
    /// 鱼上钩了，进入抓鱼状态
    /// </summary>
    public void OnFishingTimeUp()
    {
        Debug.Log("***鱼上钩了！");
        SetFishingStatus(new BitingHook());
    }

    /// <summary>
    /// 鱼挣脱鱼钩，回到钓鱼状态
    /// </summary>
    public void OnLostFishTimeUp()
    {
        Debug.Log("***鱼逃跑了！");
        SetFishingStatus(new Fishing());
    }

    /// <summary>
    /// 启动计时器
    /// </summary>
    /// <param name="time">所需要的时间</param>
    public void StartTimer(float countdownTime)
    {
        //启动计时器
        timer.StartTimer(countdownTime);
    }

    /// <summary>
    /// 关闭计时器
    /// </summary>
    public void StopFishingTimer()
    {
        timer.StopTimer();
    }

    /// <summary>
    /// 提供接口，供外部查询当前状态
    /// </summary>
    /// <returns>返回当前钓鱼的状态</returns>
    public string DownToCheckStatus()
    {
        return fishingStatus.GetType().Name;
    }

    /// <summary>
    /// 每帧执行
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