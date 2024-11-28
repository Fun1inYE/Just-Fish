using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ָʾ���ű�
/// </summary>
public class Indicator : MonoBehaviour
{
    /// <summary>
    /// ����FishIndicatorController
    /// </summary>
    public FishIndicatorController fishIndicatorController;
    /// <summary>
    /// ��ȡ��Indicator��RectTransform
    /// </summary>
    public RectTransform indicator;

    /// <summary>
    /// ��ȡ��Indicator���ĸ���
    /// </summary>
    public Vector3[] indicatorFourCorners;

    /// <summary>
    /// Indicator���ƶ��ٶ�
    /// </summary>
    public float indicatorMoveSpeed = 50f;

    /// <summary>
    /// �ж�Indicator�������Ǹ�������
    /// </summary>
    public bool canMoveToLeft = true;
    public bool canMoveToRight = true;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        fishIndicatorController = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishIndicatorController>();
        indicator = GetComponent<RectTransform>();
        if (indicator == null)
        {
            Debug.LogError("fishIndicatorMoveRange�ǿյģ�������룡");
        }
        indicatorFourCorners = new Vector3[4];
    }

    /// <summary>
    /// ��ʼ���ĸ���
    /// </summary>
    public void Start()
    {
        //��ȡ���ĸ��ǵ�����
        indicatorFourCorners = GetFourCorners.GetFourCornersCoordinate(indicator);
    }
    
    /// <summary>
    /// Indicator�ƶ��ķ���
    /// </summary>
    public void Move()
    {
        float horizontal = 0f;
        if (Input.GetKey(KeyCode.A) && canMoveToLeft)
        {
            horizontal = -1f;
        }
        if (Input.GetKey(KeyCode.D) && canMoveToRight)
        {
            horizontal = 1f;
        }
        // ��ȡ indicatorRange �����ĵ������
        Vector2 newPosition = indicator.anchoredPosition;

        // �ж� indicatorScript �Ƿ��� fishIndicatorMoveRangeScript ���ƶ�
        bool isWithinLeftBound = fishIndicatorController.fishIndicatorMoveRangeScript.GetCorners()[0].x <= GetCorners()[0].x;
        bool isWithinRightBound = fishIndicatorController.fishIndicatorMoveRangeScript.GetCorners()[3].x >= GetCorners()[3].x;

        // ������߽磬���������ƶ��ļ�����Ӧ
        if (!isWithinLeftBound)
        {
            canMoveToLeft = false;
        }
        else
        {
            canMoveToLeft = true;
        }

        // �����ұ߽磬���������ƶ��ļ�����Ӧ
        if (!isWithinRightBound)
        {
            canMoveToRight = false;
        }
        else
        {
            canMoveToRight = true;
        }

        //����Indicator���µ�λ��
        newPosition.x += horizontal * indicatorMoveSpeed * Time.deltaTime;

        // ���� indicatorScript �� anchoredPosition
        indicator.anchoredPosition = newPosition;
    }

    /// <summary>
    /// �����ĸ��ǵ�����
    /// </summary>
    /// <returns></returns>
    public Vector3[] GetCorners()
    {
        return GetFourCorners.GetFourCornersCoordinate(indicator);
    }
}
