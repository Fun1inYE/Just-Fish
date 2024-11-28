using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ʱ�������ࣨͨ�ü�ʱ����
/// </summary>
public class Timer : MonoBehaviour
{
    /// <summary>
    /// ��ʱ��Э��
    /// </summary>
    private Coroutine timerCoroutine;

    /// <summary>
    /// ��ʱʱ��
    /// </summary>
    private float remainingTime;

    /// <summary>
    /// ��ʱ���Ƿ����ڼ�ʱ
    /// </summary>
    private bool isRunning = false;

    /// <summary>
    /// ί�к��¼���֪ͨ��ʱ������Ȼ��ִ��ί���е�����
    /// </summary>
    public delegate void TimerFinishedHandler();
    public event TimerFinishedHandler OnTimerFinished;

    /// <summary>
    /// ������ʱ��
    /// </summary>
    /// <param name="time"></param>
    public void StartTimer(float time)
    {
        //�����ʱ��������ת�Ļ�����ֹͣ��ʱ��
        if(isRunning)
        {
            StopTimer();
        }

        remainingTime = time;
        isRunning = true;
        timerCoroutine = StartCoroutine(CountdownCoroutine());
    }

    /// <summary>
    /// ֹͣ��ʱ���ķ���
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
    /// Э�̼�ʱ��
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
