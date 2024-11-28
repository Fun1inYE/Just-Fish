using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ֱ����˸������ƫ�ƣ������½ǿ�ʼ��˳ʱ����ת
/// </summary>
public enum OffsetLocation { DownLeft, Left, UpLeft, Up, UpRight, Right, DownRight, Down}

/// <summary>
/// ����UI��ƫ��������
/// </summary>
public static class UIOffset
{
    /// <summary>
    /// ���ݴ�������UI�Ͱ˷���ö����ƫ�Ƽ���
    /// </summary>
    /// <param name="UIRect">��������UI</param>
    /// <param name="offsetLocationType">������Ҫ��ƫ�Ƶ�ö��</param>
    /// <returns></returns>
    public static Vector2 CalculationOffset(Vector2 propPosition, RectTransform UIRect, OffsetLocation offsetLocationType)
    {
        //�Ƚ���������UI֧�ŵ㴫����ת��Ϊ��Ļ����
        Vector2 screenPos = Camera.main.WorldToScreenPoint(propPosition);

        //��ΪRectTransform��ê���������ھֲ�����ģ����Ի�Ҫת��һ������,local�����������UI֧�ŵ������
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            UIRect.parent as RectTransform,
            screenPos,
            Camera.main,
            out localPos
        );

        //�µ�ê������
        Vector2 newAnchoredPosition;

        //����ƫ����(ע�����ƶ�ê�㣬������������꣬��Ҫ���ż���)
        switch (offsetLocationType)
        {
            case OffsetLocation.DownLeft:
                newAnchoredPosition = new Vector2(localPos.x + (UIRect.sizeDelta.x / 2), localPos.y + (UIRect.sizeDelta.y / 2));
                return newAnchoredPosition;

            case OffsetLocation.Left:
                newAnchoredPosition = new Vector2(localPos.x + (UIRect.sizeDelta.x / 2), localPos.y);
                return newAnchoredPosition;

            case OffsetLocation.UpLeft:
                newAnchoredPosition = new Vector2(localPos.x + (UIRect.sizeDelta.x / 2), localPos.y - (UIRect.sizeDelta.y / 2));
                return newAnchoredPosition;

            case OffsetLocation.Up:
                newAnchoredPosition = new Vector2(localPos.x, localPos.y - (UIRect.sizeDelta.y / 2));
                return newAnchoredPosition;

            case OffsetLocation.UpRight:
                newAnchoredPosition = new Vector2(localPos.x - (UIRect.sizeDelta.x / 2), localPos.y - (UIRect.sizeDelta.y / 2));
                return newAnchoredPosition;

            case OffsetLocation.Right:
                newAnchoredPosition = new Vector2(localPos.x - (UIRect.sizeDelta.x / 2), localPos.y);
                return newAnchoredPosition;

            case OffsetLocation.DownRight:
                newAnchoredPosition = new Vector2(localPos.x + (UIRect.sizeDelta.x / 2), localPos.y - (UIRect.sizeDelta.y / 2));
                return newAnchoredPosition;

            case OffsetLocation.Down:
                newAnchoredPosition = new Vector2(localPos.x, localPos.y + (UIRect.sizeDelta.y / 2));
                return newAnchoredPosition;

            default:
                Debug.Log("�����Ƿ񴫽�����ȷ��OffsetLocationֵ��");
                return new Vector2(0f, 0f);
        }

    }
}
