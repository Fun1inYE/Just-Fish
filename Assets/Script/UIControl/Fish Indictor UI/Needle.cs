using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Needle : MonoBehaviour
{
    /// <summary>
    /// ����FishIndicatorController
    /// </summary>
    public FishIndicatorController fishIndicatorController;
    /// <summary>
    /// ָʾ��
    /// </summary>
    public RectTransform needle;
    /// <summary>
    /// ָʾ���ĳ�ʼ���(Ĭ��1f)
    /// </summary>
    public float needleWidth = 1f;
    /// <summary>
    /// ָʾ�������ٶ�(Ĭ��30f)
    /// </summary>
    public float needleThicknessSpeed = 15f;
    /// <summary>
    /// ָʾ����խ���ٶ�(Ĭ��15f)
    /// </summary>
    public float needleThinkDownSpeed = 15f;
    
    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        needle = GetComponent<RectTransform>();
        fishIndicatorController = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishIndicatorController>();
    }

    /// <summary>
    /// �����ű�ִ��
    /// </summary>
    public void OnEnable()
    {
        if (needle != null)
        {
            //��ָʾ����ʼ��
            needle.sizeDelta = new Vector2(needleWidth, needle.sizeDelta.y);
        }
    }

    /// <summary>
    /// Indicator��IndicatorRange�б��ķ���
    /// </summary>
    public void CtrlThickness()
    {
        float currentWidth = needle.sizeDelta.x;

        if (Input.GetKey(KeyCode.Space))
        {
            //�����¿��
            currentWidth += needleThicknessSpeed * Time.deltaTime;

            //���������
            currentWidth = Mathf.Min(currentWidth, fishIndicatorController.indicatorScript.indicator.sizeDelta.x);
        }
        else
        {
            //�ɿ��ո��֮��ָʾ�������ص���ʼ���

            //�����¿��
            currentWidth -= needleThinkDownSpeed * Time.deltaTime;
            //���������
            currentWidth = Mathf.Max(currentWidth, needleWidth);

        }
        //����ָʾ���Ŀ��
        needle.sizeDelta = new Vector2(currentWidth, needle.sizeDelta.y);


    }

    /// <summary>
    /// �����ĸ��ǵ�����
    /// </summary>
    /// <returns></returns>
    public Vector3[] GetCorners()
    {
        return GetFourCorners.GetFourCornersCoordinate(needle);
    }
}
