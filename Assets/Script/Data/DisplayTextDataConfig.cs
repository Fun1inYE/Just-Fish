using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ʾ����UI������
/// </summary>
public class DisplayTextDataConfig
{
    /// <summary>
    /// ҧ������
    /// </summary>
    public string bitingHookText { get; private set; }
    /// <summary>
    /// �߶�����
    /// </summary>
    public string wireBreakText { get; private set; }

    /// <summary>
    /// ���캯��
    /// </summary>
    public DisplayTextDataConfig()
    {
        bitingHookText = "�Ϲ���";
        wireBreakText = "�߱���";
    }
}
