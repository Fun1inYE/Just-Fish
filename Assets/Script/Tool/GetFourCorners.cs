using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ȡ�ĸ��ǵ�����
/// </summary>
public static class GetFourCorners
{
    /// <summary>
    /// ���ش����RectTransform���ĸ��ǵ���������
    /// </summary>
    /// <param name="rectTransform"></param>
    /// <returns></returns>
    public static Vector3[] GetFourCornersCoordinate(RectTransform rectTransform)
    {
        //����Vector3������
        Vector3[] corners = new Vector3[4];

        rectTransform.GetWorldCorners(corners);
        return corners;
    }
}
