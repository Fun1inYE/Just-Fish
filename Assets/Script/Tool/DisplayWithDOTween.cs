using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// ����UIͨ��DOTween���ر任����
/// </summary>
public static class DisplayWithDOTween
{
    /// <summary>
    /// ����Text���ŵķ���
    /// </summary>
    /// <param name="AddScale">Ҫ���ӵ�Scale</param>
    /// <param name="durationTime">�任ʱ��</param>
    /// <param name="appendTime">UI����ʱ��</param>
    /// <param name="UIName">UI������</param>
    /// <param name="initialPosition">��ʼλ��</param>
    /// <param name="fontSize">�����С</param>
    public static void ScaleText(Vector2 AddScale, float durationTime, float appendTime, string UIName, Vector2 initialPosition, int fontSize = 20)
    {
        //����UI��ͣ��ʱ��ȱ任ʱ�䳤����ֹ����UIû�任�����ʧ��bug
        if(appendTime >= durationTime)
        {
            //����һ��DOTween����
            Sequence sequence = DOTween.Sequence();

            //��ʾUI����ȡUI������
            GameObject obj = DisplayUIManager.Instance.DisplayTextUI(UIName);
            Text UIText = obj.GetComponent<Text>();
            UIText.fontSize = fontSize;
            RectTransform UIRectTransform = obj.GetComponent<RectTransform>();
            //��UI����ʼλ��
            obj.transform.position = initialPosition;
            //��¼һ��ԭUI��scale
            Vector2 orginalScale = UIRectTransform.transform.localScale;
            //��ʼ��¼�����任��ʽ
            sequence.Append(UIRectTransform.transform.DOScale(orginalScale + AddScale, durationTime).SetRelative());
            //�����ʾUI���ӳ�ʱ��
            sequence.AppendInterval(appendTime - durationTime);
            //ִ����϶���
            sequence.Play();
            //ʹUI���ԭ����Scale
            UIRectTransform.transform.localScale = orginalScale;
            //ִ���궯���ͽ�UI�ر�
            sequence.OnComplete(() => DisplayUIManager.Instance.HideTextUI(obj));
        }
        else
        {
            Debug.LogWarning("ScaleText�任ʱ���������ʱ����,��ʱ������UI��ʾ��������룡");
        }
    }

    /// <summary>
    /// ����Textǳ��ǳ���ķ���
    /// </summary>
    /// <param name="transformDistance">�����ƶ��ľ���</param>
    /// <param name="durationTime">�任ʱ��</param>
    /// <param name="appendTime">UI����ʱ��</param>
    /// <param name="UIName">UI������</param>
    /// <param name="initialPosition">UI���ֵĳ�ʼλ��</param>
    /// <param name="fontSize">�����ֺţ�Ĭ��20��</param>
    public static void FadeAndMoveText(float transformDistance, float durationTime, float appendTime, string UIName, Vector2 initialPosition, int fontSize = 20)
    {
        //����һ��DOTween����϶���
        Sequence sequence = DOTween.Sequence();
        //��ʾUI����ȡUI������
        GameObject obj = DisplayUIManager.Instance.DisplayTextUI(UIName);
        Text UIText = obj.GetComponent<Text>();
        UIText.fontSize = fontSize;
        RectTransform UIRectTransform = obj.GetComponent<RectTransform>();
        //��UI����ʼλ��
        obj.transform.position = initialPosition;
        //�Ƚ�UI��Ϊ͸��
        UIText.color = new Color(UIText.color.r, UIText.color.g, UIText.color.b, 0);
        //͸����Ϊ��͸��
        sequence.Append(UIText.DOFade(1, durationTime));
        //��UI�����ƶ�transformDistance����λ
        sequence.Join(UIRectTransform.DOMove(new Vector2(0f, transformDistance), durationTime).SetRelative());
        //�����ʾUI���ӳ�ʱ��
        sequence.AppendInterval(appendTime);
        //��UI�����ƶ�transformDistance����λ
        sequence.Append(UIRectTransform.DOMove(new Vector2(0f, transformDistance), durationTime).SetRelative());
        //��͸����Ϊ͸��
        sequence.Join(UIText.DOFade(0, durationTime));
        //ִ����϶���
        sequence.Play();
        //ִ���궯���ͽ�UI�ر�
        sequence.OnComplete(() => DisplayUIManager.Instance.HideTextUI(obj));
    }

    /// <summary>
    /// ����Panel�ƶ���ĳһ��λ�õķ���
    /// </summary>
    public static void TransitionPanelToPoint(GameObject movedPanel, Vector2 targetPosition)
    {

    }
}
