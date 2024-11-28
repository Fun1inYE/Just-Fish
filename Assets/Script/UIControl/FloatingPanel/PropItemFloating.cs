using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���ߵ�������
/// </summary>
public class PropItemFloating : MonoBehaviour
{
    /// <summary>
    /// ��������
    /// </summary>
    public Text Name;
    /// <summary>
    /// ���ߵ�����
    /// </summary>
    public Text Description;
    /// <summary>
    /// ���ߵ�Ʒ��
    /// </summary>
    public Text PropQuality;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void InitializePropItemFloating()
    {
        Name = transform.Find("Name").GetComponent<Text>();
        Description = ComponentFinder.GetChildComponent<Text>(gameObject, "Description");
        PropQuality = ComponentFinder.GetChildComponent<Text>(gameObject, "Quality");
    }
}
