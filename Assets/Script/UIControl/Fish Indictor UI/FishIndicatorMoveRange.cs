using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ָʾ�����ƶ���Χ
/// </summary>
public class FishIndicatorMoveRange : MonoBehaviour
{
    /// <summary>
    /// ����FishIndicatorController
    /// </summary>
    public FishIndicatorController fishIndicatorController;
    /// <summary>
    /// ��ȡ��FishIndicatorMoveRange��RectTransform
    /// </summary>
    public RectTransform fishIndicatorMoveRange;

    /// <summary>
    /// ��ȡ��FishIndicatorMoveRange���ĸ���
    /// </summary>
    public Vector3[] fishIndicatorMoveRangeFourCorners;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        fishIndicatorMoveRange = GetComponent<RectTransform>();
        if(fishIndicatorMoveRange == null)
        {
            Debug.LogError("fishIndicatorMoveRange�ǿյģ�������룡");
        }
        fishIndicatorMoveRangeFourCorners = new Vector3[4];

        fishIndicatorController = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishIndicatorController>();

    }

    /// <summary>
    /// ��ʼ���ĸ���
    /// </summary>
    public void Start()
    {
        //��ȡ���ĸ��ǵ�����
        fishIndicatorMoveRangeFourCorners = GetFourCorners.GetFourCornersCoordinate(fishIndicatorMoveRange);
    }

    /// <summary>
    /// �����ĸ��ǵ�����
    /// </summary>
    /// <returns></returns>
    public Vector3[] GetCorners()
    {
        return GetFourCorners.GetFourCornersCoordinate(fishIndicatorMoveRange);
    }
}
