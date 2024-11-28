using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 分别代表八个方向的偏移，从左下角开始，顺时针旋转
/// </summary>
public enum OffsetLocation { DownLeft, Left, UpLeft, Up, UpRight, Right, DownRight, Down}

/// <summary>
/// 计算UI的偏移量的类
/// </summary>
public static class UIOffset
{
    /// <summary>
    /// 根据传进来的UI和八方向枚举做偏移计算
    /// </summary>
    /// <param name="UIRect">传进来的UI</param>
    /// <param name="offsetLocationType">传进来要做偏移的枚举</param>
    /// <returns></returns>
    public static Vector2 CalculationOffset(Vector2 propPosition, RectTransform UIRect, OffsetLocation offsetLocationType)
    {
        //先将传进来的UI支撑点传进来转换为屏幕坐标
        Vector2 screenPos = Camera.main.WorldToScreenPoint(propPosition);

        //因为RectTransform的锚点是作用于局部坐标的，所以还要转换一下坐标,local坐标就是最后的UI支撑点的坐标
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            UIRect.parent as RectTransform,
            screenPos,
            Camera.main,
            out localPos
        );

        //新的锚点坐标
        Vector2 newAnchoredPosition;

        //计算偏移量(注意是移动锚点，所以是相对坐标，需要反着计算)
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
                Debug.Log("请检查是否传进来正确的OffsetLocation值！");
                return new Vector2(0f, 0f);
        }

    }
}
