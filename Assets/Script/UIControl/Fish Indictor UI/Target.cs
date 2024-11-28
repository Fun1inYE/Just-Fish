using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Target : MonoBehaviour
{
    /// <summary>
    /// ����FishIndicatorController
    /// </summary>
    public FishIndicatorController fishIndicatorController;
    /// <summary>
    /// Ŀ���RectTransform
    /// </summary>
    public RectTransform target;
    /// <summary>
    /// Ŀ����ƶ��ٶȣ�Ĭ��25f��
    /// </summary>
    public float targetSpeed = 25f;
    /// <summary>
    /// �ƶ�����: 1���ң�-1����
    /// </summary>
    public float moveDirction;

    /// <summary>
    /// ��ȡ����ʱ��
    /// </summary>
    public Timer timer;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    private void Awake()
    {
        target = GetComponent<RectTransform>();
        fishIndicatorController = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishIndicatorController>();
        timer = gameObject.AddComponent<Timer>();
    }

    private void OnEnable()
    {
        //���target�Ƿ����
        if (target == null)
        {
            Debug.LogError("target�ǿյģ���������Ƿ������⣡");
        }

        //���timer�Ƿ����
        if (timer != null)
        {
            //����Ӧ�ķ���ע�ᵽί��֮��
            timer.OnTimerFinished += ChangeMoveDirectionThenRestarTimer;
            //������ʱ��ı䷽��ķ���
            StartTimerThenChangeDirction();
        }
        else
        {
            Debug.LogError("Timer�ǿյģ��������");
        }

    }
    private void OnDisable()
    {
        //��ί�з���ȡ��ע��
        timer.OnTimerFinished -= ChangeMoveDirectionThenRestarTimer;
        //�رռ�ʱ��
        timer.StopTimer();
    }

    /// <summary>
    /// ʹĿ���ƶ��Ľű�
    /// </summary>
    public void Move()
    {
        //�Ȼ�ȡ��target�����ĵ�����
        Vector2 newPosition = target.anchoredPosition;
        //��target�����ƶ�
        newPosition.x += moveDirction * targetSpeed * Time.deltaTime;
        //����target�����ĵ�����
        target.anchoredPosition = newPosition;
    }

    /// <summary>
    /// ������ɸ��ķ����ʱ�䲢������������ʱ��
    /// </summary>
    public void StartTimerThenChangeDirction()
    {
        //�������һ�����ķ����ʱ��
        float changeDirctionTime = Random.Range(1f, 3f);
        //������ʱ��
        timer.StartTimer(changeDirctionTime);
    }

    /// <summary>
    /// �ı䷽����������ʱ��
    /// </summary>
    public void ChangeMoveDirectionThenRestarTimer()
    {
        //ֱ�ӵ���������
        //TODO:��Ч�������ܻ��ỻ�߼�
        if (moveDirction > 0) moveDirction = -1;
        else moveDirction = 1;

        //�ٴ��������һ�����ķ����ʱ��,�ٴ�������ʱ��
        StartTimerThenChangeDirction();
    }

    /// <summary>
    /// Ŀ�����ڼ��
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
    /// �����ĸ��ǵ�����
    /// </summary>
    /// <returns></returns>
    public Vector3[] GetCorners()
    {
        return GetFourCorners.GetFourCornersCoordinate(target);
    }
}
