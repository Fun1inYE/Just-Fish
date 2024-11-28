using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 获取四个角的坐标
/// </summary>
public static class GetFourCorners
{
    /// <summary>
    /// 返回传入的RectTransform的四个角的世界坐标
    /// </summary>
    /// <param name="rectTransform"></param>
    /// <returns></returns>
    public static Vector3[] GetFourCornersCoordinate(RectTransform rectTransform)
    {
        //声明Vector3的数组
        Vector3[] corners = new Vector3[4];

        rectTransform.GetWorldCorners(corners);
        return corners;
    }
}
