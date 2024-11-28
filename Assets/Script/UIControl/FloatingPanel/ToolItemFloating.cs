using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���ߵ�������
/// </summary>
public class ToolItemFloating : MonoBehaviour
{
    /// <summary>
    /// ��������
    /// </summary>
    public Text Name;
    // <summary>
    /// ����Ʒ��
    /// </summary>
    public Text ToolQuality;
    /// <summary>
    /// ���ߵ�����
    /// </summary>
    public Text Description;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void InitializeToolItemFloating()
    {
        Name = transform.Find("Name").GetComponent<Text>();
        ToolQuality = ComponentFinder.GetChildComponent<Text>(gameObject, "Quality");
        Description = ComponentFinder.GetChildComponent<Text>(gameObject, "Description");
    }
}
