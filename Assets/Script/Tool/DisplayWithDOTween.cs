using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// 帮助UI通过DOTween来回变换的类
/// </summary>
public static class DisplayWithDOTween
{
    /// <summary>
    /// 帮助Text缩放的方法
    /// </summary>
    /// <param name="AddScale">要增加的Scale</param>
    /// <param name="durationTime">变换时间</param>
    /// <param name="appendTime">UI滞留时间</param>
    /// <param name="UIName">UI的名字</param>
    /// <param name="initialPosition">初始位置</param>
    /// <param name="fontSize">字体大小</param>
    public static void ScaleText(Vector2 AddScale, float durationTime, float appendTime, string UIName, Vector2 initialPosition, int fontSize = 20)
    {
        //必须UI的停留时间比变换时间长，防止出现UI没变换完就消失的bug
        if(appendTime >= durationTime)
        {
            //创建一个DOTween动画
            Sequence sequence = DOTween.Sequence();

            //显示UI并获取UI相关组件
            GameObject obj = DisplayUIManager.Instance.DisplayTextUI(UIName);
            Text UIText = obj.GetComponent<Text>();
            UIText.fontSize = fontSize;
            RectTransform UIRectTransform = obj.GetComponent<RectTransform>();
            //让UI到初始位置
            obj.transform.position = initialPosition;
            //记录一下原UI的scale
            Vector2 orginalScale = UIRectTransform.transform.localScale;
            //开始记录动画变换方式
            sequence.Append(UIRectTransform.transform.DOScale(orginalScale + AddScale, durationTime).SetRelative());
            //添加显示UI的延长时间
            sequence.AppendInterval(appendTime - durationTime);
            //执行组合动画
            sequence.Play();
            //使UI变回原来的Scale
            UIRectTransform.transform.localScale = orginalScale;
            //执行完动画就将UI关闭
            sequence.OnComplete(() => DisplayUIManager.Instance.HideTextUI(obj));
        }
        else
        {
            Debug.LogWarning("ScaleText变换时间大于滞留时间了,暂时不给与UI显示，请检查代码！");
        }
    }

    /// <summary>
    /// 帮助Text浅入浅出的方法
    /// </summary>
    /// <param name="transformDistance">向上移动的距离</param>
    /// <param name="durationTime">变换时间</param>
    /// <param name="appendTime">UI滞留时间</param>
    /// <param name="UIName">UI的名字</param>
    /// <param name="initialPosition">UI出现的初始位置</param>
    /// <param name="fontSize">文字字号（默认20）</param>
    public static void FadeAndMoveText(float transformDistance, float durationTime, float appendTime, string UIName, Vector2 initialPosition, int fontSize = 20)
    {
        //创建一个DOTween的组合动画
        Sequence sequence = DOTween.Sequence();
        //显示UI并获取UI相关组件
        GameObject obj = DisplayUIManager.Instance.DisplayTextUI(UIName);
        Text UIText = obj.GetComponent<Text>();
        UIText.fontSize = fontSize;
        RectTransform UIRectTransform = obj.GetComponent<RectTransform>();
        //让UI到初始位置
        obj.transform.position = initialPosition;
        //先将UI变为透明
        UIText.color = new Color(UIText.color.r, UIText.color.g, UIText.color.b, 0);
        //透明变为不透明
        sequence.Append(UIText.DOFade(1, durationTime));
        //将UI向上移动transformDistance个单位
        sequence.Join(UIRectTransform.DOMove(new Vector2(0f, transformDistance), durationTime).SetRelative());
        //添加显示UI的延长时间
        sequence.AppendInterval(appendTime);
        //将UI向上移动transformDistance个单位
        sequence.Append(UIRectTransform.DOMove(new Vector2(0f, transformDistance), durationTime).SetRelative());
        //不透明变为透明
        sequence.Join(UIText.DOFade(0, durationTime));
        //执行组合动画
        sequence.Play();
        //执行完动画就将UI关闭
        sequence.OnComplete(() => DisplayUIManager.Instance.HideTextUI(obj));
    }

    /// <summary>
    /// 帮助Panel移动到某一个位置的方法
    /// </summary>
    public static void TransitionPanelToPoint(GameObject movedPanel, Vector2 targetPosition)
    {

    }
}
