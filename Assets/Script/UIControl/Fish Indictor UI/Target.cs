using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Target : MonoBehaviour
{
    /// <summary>
    /// 引用FishIndicatorController
    /// </summary>
    public FishIndicatorController fishIndicatorController;
    /// <summary>
    /// 目标的RectTransform
    /// </summary>
    public RectTransform target;
    /// <summary>
    /// 目标的移动速度（默认25f）
    /// </summary>
    public float targetSpeed = 25f;
    /// <summary>
    /// 移动方向: 1向右，-1向左
    /// </summary>
    public float moveDirction;

    /// <summary>
    /// 获取到计时器
    /// </summary>
    public Timer timer;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    private void Awake()
    {
        target = GetComponent<RectTransform>();
        fishIndicatorController = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishIndicatorController>();
        timer = gameObject.AddComponent<Timer>();
    }

    private void OnEnable()
    {
        //检查target是否存在
        if (target == null)
        {
            Debug.LogError("target是空的，请检查代码是否有问题！");
        }

        //检查timer是否存在
        if (timer != null)
        {
            //将对应的方法注册到委托之中
            timer.OnTimerFinished += ChangeMoveDirectionThenRestarTimer;
            //启动随时间改变方向的方法
            StartTimerThenChangeDirction();
        }
        else
        {
            Debug.LogError("Timer是空的，请检查代码");
        }

    }
    private void OnDisable()
    {
        //对委托方法取消注册
        timer.OnTimerFinished -= ChangeMoveDirectionThenRestarTimer;
        //关闭计时器
        timer.StopTimer();
    }

    /// <summary>
    /// 使目标移动的脚本
    /// </summary>
    public void Move()
    {
        //先获取到target的轴心点坐标
        Vector2 newPosition = target.anchoredPosition;
        //将target进行移动
        newPosition.x += moveDirction * targetSpeed * Time.deltaTime;
        //更新target的轴心点坐标
        target.anchoredPosition = newPosition;
    }

    /// <summary>
    /// 随机生成更改方向的时间并且重新启动计时器
    /// </summary>
    public void StartTimerThenChangeDirction()
    {
        //随机生成一个更改方向的时间
        float changeDirctionTime = Random.Range(1f, 3f);
        //开启计时器
        timer.StartTimer(changeDirctionTime);
    }

    /// <summary>
    /// 改变方向并且重启计时器
    /// </summary>
    public void ChangeMoveDirectionThenRestarTimer()
    {
        //直接调至反方向
        //TODO:看效果，可能还会换逻辑
        if (moveDirction > 0) moveDirction = -1;
        else moveDirction = 1;

        //再次随机生成一个更改方向的时间,再次启动计时器
        StartTimerThenChangeDirction();
    }

    /// <summary>
    /// 目标碰壁检测
    /// </summary>
    public void CheckToNeedChangeDirction()
    {
        if (GetCorners()[0].x - 0.001f <= fishIndicatorController.fishIndicatorMoveRangeScript.GetCorners()[0].x)
        {
            moveDirction = 1f;
        }
        if (GetCorners()[3].x + 0.001f > fishIndicatorController.fishIndicatorMoveRangeScript.GetCorners()[3].x)
        {
            moveDirction = -1f;
        }
    }

    /// <summary>
    /// 返回四个角的坐标
    /// </summary>
    /// <returns></returns>
    public Vector3[] GetCorners()
    {
        return GetFourCorners.GetFourCornersCoordinate(target);
    }
}
