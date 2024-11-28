using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 计时器工具类（通用计时器）
/// </summary>
public class Timer : MonoBehaviour
{
    /// <summary>
    /// 计时器协程
    /// </summary>
    private Coroutine timerCoroutine;

    /// <summary>
    /// 计时时间
    /// </summary>
    private float remainingTime;

    /// <summary>
    /// 计时器是否正在计时
    /// </summary>
    private bool isRunning = false;

    /// <summary>
    /// 委托和事件，通知计时结束，然后执行委托中的内容
    /// </summary>
    public delegate void TimerFinishedHandler();
    public event TimerFinishedHandler OnTimerFinished;

    /// <summary>
    /// 启动计时器
    /// </summary>
    /// <param name="time"></param>
    public void StartTimer(float time)
    {
        //如果计时器还在运转的话，就停止计时器
        if(isRunning)
        {
            StopTimer();
        }

        remainingTime = time;
        isRunning = true;
        timerCoroutine = StartCoroutine(CountdownCoroutine());
    }

    /// <summary>
    /// 停止计时器的方法
    /// </summary>
    public void StopTimer()
    {
        if(timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            isRunning = false;
        }
    }

    /// <summary>
    /// 协程计时器
    /// </summary>
    /// <returns></returns>
    private IEnumerator CountdownCoroutine()
    {
        while(remainingTime > 0)
        {
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        isRunning = false;
        OnTimerFinished?.Invoke();
    }
}
